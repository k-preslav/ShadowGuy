using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Witch : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 10f;
    public float smoothness = 0.5f;
    public Vector3 offset;
    public float catchDistance = 0.5f;
    private Vector3 velocity = Vector3.zero;

    float cooldownTimer = 5f;

    public Animator anim;
    public AudioClip sfx;

    private void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                Debug.LogError("Player not found! Please ensure the player has the 'Player' tag.");
            }
        }
    }
    void LateUpdate()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer > 0f) return;

        if (target == null) return;
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (distanceToPlayer <= catchDistance)
        {
            CatchPlayer();
            return;
        }
        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothness,
            followSpeed
        );
    }
    void CatchPlayer()
    {
        Debug.Log("Player Caught!");

        anim.enabled = true;
        anim.SetTrigger("Catch");
        var cam = Camera.main.GetComponent<CameraMovement>();
        cam.isFollowing = false;
        Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);
        AudioSource.PlayClipAtPoint(sfx, transform.position);
        StartCoroutine(Diead());
    }
    IEnumerator Diead()
    {
        yield return new WaitForSeconds(0.11f);
        Debug.Log("DEAD");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, catchDistance);
    }
}