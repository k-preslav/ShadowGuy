using UnityEngine;

public class FlyingSquare : MonoBehaviour
{
    [Header("Wave Settings")]
    [Tooltip("Vertical amplitude in world units")]
    public float amplitude = 0.05f;
    [Tooltip("Vertical wave frequency (cycles per second)")]
    public float frequency = 13f;

    [Header("Horizontal Settings")]
    [Tooltip("Horizontal speed in units per second. Positive = right, negative = left")]
    public float horizontalSpeed = 0.01f;

    private Vector2 startPosition;
    private float timeCounter = 0f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // accumulate time
        timeCounter += Time.deltaTime;

        // horizontal: move forever in one direction at horizontalSpeed
        float x = startPosition.x + horizontalSpeed * timeCounter;

        // vertical: sine wave around startPosition.y
        float y = startPosition.y + amplitude * Mathf.Sin(timeCounter * frequency * Mathf.PI * 2f);

        transform.position = new Vector2(x, y);
    }
}