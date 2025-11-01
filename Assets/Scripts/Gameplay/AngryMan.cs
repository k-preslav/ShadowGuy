using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AngryMan : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform player;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float chaseRadius = 10f;
    public float stopDistance = 1.5f;

    private Rigidbody2D rb;
    private Vector2 smoothVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.gravityScale = 0; // prevent falling if this is top-down
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("⚠️ AngryMan has no player assigned!");
            return;
        }

        float distance = Vector2.Distance(rb.position, player.position);

        if (distance <= chaseRadius && distance > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 targetVelocity = direction * speed;

            rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, targetVelocity, ref smoothVelocity, 0.05f);
        }
        else
        {
            rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, Vector2.zero, ref smoothVelocity, 0.05f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}