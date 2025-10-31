using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AngryMan : MonoBehaviour
{
    public Transform player;
    public float speed = 6f;
    public float chaseRadius = 100f;
    public float stopDistance = 3f;

    private Rigidbody2D rb;
    private Vector2 velocitySmooth;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

   void FixedUpdate()
    {
        if (player == null) return;

        float dist = Vector2.Distance(rb.position, player.position);

        if (dist <= chaseRadius && dist > stopDistance)
        {
            Vector2 dir = ((Vector2)player.position - rb.position).normalized;

            float t = Mathf.InverseLerp(stopDistance, chaseRadius, dist); // 0 at stopDistance, 1 at chaseRadius
            float targetSpeed = speed * t;

            Vector2 targetVel = dir * targetSpeed;

            rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, targetVel, ref velocitySmooth, 0.08f, Mathf.Infinity, Time.fixedDeltaTime);
        }
        else
        {
            rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, Vector2.zero, ref velocitySmooth, 0.05f, Mathf.Infinity, Time.fixedDeltaTime);
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