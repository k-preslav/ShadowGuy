using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform candle;
    [SerializeField] float speed = 5f;
    [SerializeField] float candleRadius = 3f;
    [SerializeField] float candleSmooth = 15f;

    private void Update()
    {
        // --- Move player ---
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(movement * speed * Time.deltaTime);

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
