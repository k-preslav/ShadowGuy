using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator animator;

    [Space(5)]

    public Transform candle;
    [SerializeField] float speed = 5f;
    [SerializeField] float candleRadius = 3f;
    [SerializeField] float candleSmooth = 15f;

    private void Update()
    {
        // --- Move player ---
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(movement * speed * Time.deltaTime);

        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        // --- Rotate candle around player based on mouse position ---
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        Vector2 direction = (mouseWorld - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x);

        // Desired candle position around the player
        Vector3 targetPos = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * candleRadius;

        // Smoothly move the candle instead of the player
        candle.position = Vector3.Lerp(candle.position, targetPos, Time.deltaTime * candleSmooth);
    }
}
