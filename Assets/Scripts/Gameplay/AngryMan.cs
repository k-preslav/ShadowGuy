using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class AngryMan : MonoBehaviour
{
    [SerializeField] private GameObject restartButton; // Reference to restart button
[SerializeField] private AudioSource gameOverSound;
    [Header("Target Settings")]
    public GameObject player;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float stopRadius = 1.5f;
    public float detectionRadius = 10f;
    
    [Header("Detection Settings")]
    [SerializeField] private float detectionMultiplier = 2.75f;

    [Header("Wander Settings")]
    [SerializeField] private float wanderSpeed = 2f;
    [SerializeField] private Vector2 wanderAreaSize = new Vector2(10f, 6f); // Width x Height
    [SerializeField] private float wanderChangeInterval = 3f;
    [SerializeField] private float wanderPauseChance = 0.3f;
    [SerializeField] private float retreatSpeed = 8f; // Speed when retreating from player

    Candle candle;

    [HideInInspector] public bool canChase = true;

    bool canMove = true;

    [SerializeField] Animator animator;
    [SerializeField] AudioClip deadSfx;

    private Vector2 wanderTarget;
    private float wanderTimer;
    private bool isPaused;
    private Vector2 startPosition;
    private bool wasChasing = false; // Track if we were just chasing
    private bool isRetreating = false; // Track if we're in retreat mode

    private void Start()
    {
        candle = player.GetComponent<Player>().candle.GetComponent<Candle>();
        startPosition = transform.position;
        SetNewWanderTarget();
    }

    private void LateUpdate()
    {
        if (player == null) return;

        if (!canMove)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(0, 0, Camera.main.transform.position.z), Time.deltaTime * 10f);
            return;
        }

        // Handle chasing behavior - less candle = more detection
        if (candle.candleFill > 0.9f) // High threshold to catch fresh refills
        {
            stopRadius = 1.5f;
            detectionRadius = detectionMultiplier / Mathf.Max(candle.candleFill, 0.01f);
            detectionRadius = Mathf.Min(detectionRadius, 8f);

            // If we were chasing and candle got refilled, enter retreat mode
            if (wasChasing && !isRetreating)
            {
                SetNewWanderTarget();
                isRetreating = true;
                wasChasing = false;
            }

            Wander();
        }
        else if (candle.candleFill > 0.012f) // Normal wander range
        {
            stopRadius = 1.5f;
            detectionRadius = detectionMultiplier / Mathf.Max(candle.candleFill, 0.01f);
            detectionRadius = Mathf.Min(detectionRadius, 8f);

            Wander();
        }
        else // Candle is out
        {
            stopRadius = 0.01f;
            detectionRadius = 100f;
            
            // Only mark as chasing if we can actually chase
            if (canChase)
            {
                wasChasing = true; // Mark that we're chasing
                isRetreating = false; // Not retreating anymore
                Chase();
            }
            else
            {
                // Can't chase, so just wander
                Wander();
            }
        }
    }

    void Chase()
    {
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = player.transform.position;
        float distance = Vector2.Distance(currentPosition, targetPosition);

        // Don't chase if player is outside detection radius
        if (distance > detectionRadius)
            return;

        if (distance <= stopRadius)
            return;

        // Move directly toward the player at constant speed
        Vector2 direction = (targetPosition - currentPosition).normalized;
        float step = speed * Time.deltaTime;

        // Don't overshoot the target
        if (step > distance - stopRadius)
            step = distance - stopRadius;

        transform.position = currentPosition + direction * step;
    }

    void Wander()
    {
        // Check if player is within detection radius while wandering
        // BUT: Don't detect if candle is fresh (>0.9), we're retreating, or can't chase
        Vector2 currentPosition = transform.position;
        Vector2 playerPosition = player.transform.position;
        float distanceToPlayer = Vector2.Distance(currentPosition, playerPosition);

        if (canChase && !isRetreating && candle.candleFill <= 0.9f && distanceToPlayer <= detectionRadius)
        {
            Chase();
            return;
        }

        wanderTimer -= Time.deltaTime;

        // Check if we should pick a new target
        if (wanderTimer <= 0f)
        {
            SetNewWanderTarget();
            wanderTimer = wanderChangeInterval;
        }

        // If paused, don't move
        if (isPaused)
            return;

        float distance = Vector2.Distance(currentPosition, wanderTarget);

        // If we're close enough to the target, consider it reached
        if (distance < 0.1f)
        {
            // If we were retreating and reached the target, stop retreating
            if (isRetreating)
                isRetreating = false;
                
            SetNewWanderTarget();
            return;
        }

        // Move toward wander target
        Vector2 direction = (wanderTarget - currentPosition).normalized;
        
        // Use retreat speed if retreating, otherwise normal wander speed
        float moveSpeed = isRetreating ? retreatSpeed : wanderSpeed;
        float step = moveSpeed * Time.deltaTime;

        if (step > distance)
            step = distance;

        transform.position = currentPosition + direction * step;
    }

    void SetNewWanderTarget()
    {
        // Don't pause when retreating
        if (!isRetreating)
        {
            // Random chance to pause
            if (Random.value < wanderPauseChance)
            {
                isPaused = true;
                wanderTarget = transform.position;
                return;
            }
        }

        isPaused = false;

        // Pick a random point within rectangular wander area from start position
        float randomX = Random.Range(-wanderAreaSize.x / 2f, wanderAreaSize.x / 2f);
        float randomY = Random.Range(-wanderAreaSize.y / 2f, wanderAreaSize.y / 2f);
        Vector2 randomOffset = new Vector2(randomX, randomY);
        wanderTarget = startPosition + randomOffset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("AngryMan collided with " + collision.name);

        if (collision.CompareTag("Player"))
        {
            canMove = false;

            var p = collision.GetComponent<Player>();
            p.speed = 0f;

            var camMove = Camera.main.GetComponent<CameraMovement>();
            camMove.isFollowing = false;

            AudioSource.PlayClipAtPoint(deadSfx, transform.position);

            animator.enabled = true;
            animator.SetTrigger("Catch");
            StartCoroutine(EndGame());
        }
    }
    IEnumerator EndGame()
{
    yield return new WaitForSecondsRealtime(0.22f); // ✅ unaffected by timeScale

    Debug.Log("GAME OVER!");

    if (restartButton == null)
    {
        Debug.LogWarning("AngryMan: restartButton reference is null. Assign it in the Inspector.");
        yield break;
    }

    // Make sure the canvas is visible
    var parentCanvas = restartButton.GetComponentInParent<Canvas>();
    if (parentCanvas != null && !parentCanvas.gameObject.activeInHierarchy)
    {
        Debug.Log("AngryMan: Activating parent Canvas for restart button.");
        parentCanvas.gameObject.SetActive(true);
    }

    // Check if any parent panels are inactive
    Transform parent = restartButton.transform;
    while (parent != null)
    {
        if (!parent.gameObject.activeSelf)
        {
            Debug.Log("AngryMan: Activating parent: " + parent.name);
            parent.gameObject.SetActive(true);
        }
        parent = parent.parent;
    }

    // Finally show the restart button
    restartButton.SetActive(true);
    Debug.Log("AngryMan: restart button active? " + restartButton.activeSelf);
}

      
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopRadius);

        // Draw wander area (rectangle)
        Gizmos.color = Color.cyan;
        Vector3 center = Application.isPlaying ? (Vector3)startPosition : transform.position;
        
        // Draw rectangle using lines
        Vector3 halfSize = new Vector3(wanderAreaSize.x / 2f, wanderAreaSize.y / 2f, 0f);
        Vector3 topLeft = center + new Vector3(-halfSize.x, halfSize.y, 0f);
        Vector3 topRight = center + new Vector3(halfSize.x, halfSize.y, 0f);
        Vector3 bottomLeft = center + new Vector3(-halfSize.x, -halfSize.y, 0f);
        Vector3 bottomRight = center + new Vector3(halfSize.x, -halfSize.y, 0f);
        
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);

        // Draw current wander target
        if (Application.isPlaying)
        {
            Gizmos.color = isRetreating ? Color.magenta : Color.green;
            Gizmos.DrawLine(transform.position, wanderTarget);
            Gizmos.DrawSphere(wanderTarget, 0.2f);
        }
    }
}
