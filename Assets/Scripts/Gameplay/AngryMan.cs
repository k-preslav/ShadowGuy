using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AngryMan : MonoBehaviour
{
    public Transform player;
    public float speed = 4f;
    public float chaseRadius = 5f;
    public float stopDistance = 3f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; 
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float dist = Vector2.Distance(rb.position, player.position);

        if (dist <= chaseRadius && dist > stopDistance)
        {
            Vector2 dir = ((Vector2)player.position - rb.position).normalized;
            Vector2 newPos = rb.position + dir * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
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