using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    public Transform candle;

    [Header("Movement Settings")]
    [SerializeField] public float speed = 5f;
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float collisionOffset = 0.05f;

    [Header("Candle Settings")]
    [SerializeField] private float candleRadius = 3f;
    [SerializeField] private float candleSmooth = 15f;

    private Vector2 movementInput;
    private BoxCollider2D boxCollider;
    private FootstepsSfx footstepsSfx;
    private bool wasWalking = false;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        if (!boxCollider)
        {
            Debug.LogWarning("Player needs a BoxCollider2D for collision detection.");
        }

        footstepsSfx = GetComponent<FootstepsSfx>();
    }

    private void Update()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        bool isWalking = movementInput.sqrMagnitude > 0.01f;
        animator.SetBool("IsWalking", isWalking);

        // Candle follows mouse
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        Vector2 dir = (mouseWorld - transform.position).normalized;
        Vector3 targetPos = transform.position + (Vector3)dir * candleRadius;
        candle.position = Vector3.Lerp(candle.position, targetPos, Time.deltaTime * candleSmooth);

        // Audio - only notify when walking state changes
        if (footstepsSfx != null)
        {
            if (isWalking && !wasWalking)
            {
                footstepsSfx.StartWalking();
            }
            else if (!isWalking && wasWalking)
            {
                footstepsSfx.StopWalking();
            }
        }

        wasWalking = isWalking;
    }

    private void LateUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            MoveAndCollide(movementInput);
        }
    }

    private void MoveAndCollide(Vector2 inputDir)
    {
        if (!boxCollider) return;

        Vector2 moveDelta = inputDir * speed * Time.deltaTime;

        // Move X first
        if (moveDelta.x != 0)
        {
            MoveSingleAxis(Vector2.right * Mathf.Sign(moveDelta.x), Mathf.Abs(moveDelta.x));
        }

        // Then Y
        if (moveDelta.y != 0)
        {
            MoveSingleAxis(Vector2.up * Mathf.Sign(moveDelta.y), Mathf.Abs(moveDelta.y));
        }
    }

    private void MoveSingleAxis(Vector2 dir, float distance)
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            dir,
            distance + collisionOffset,
            collisionMask
        );

        if (hit.collider == null)
        {
            transform.Translate(dir * distance);
        }
        else
        {
            float dist = hit.distance - collisionOffset;
            if (dist > 0)
                transform.Translate(dir * dist);
        }
    }
}