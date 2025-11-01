using UnityEngine;

public class FlyingSquare : MonoBehaviour
{
    [Header("Wave Settings")]
    [Tooltip("Vertical amplitude in world units")]
    public float amplitude = 0.056f;
    [Tooltip("Vertical wave frequency (cycles per second)")]
    public float frequency = 1.11f;

    [Header("Horizontal Settings")]
    [Tooltip("Horizontal speed in units per second. Positive = right, negative = left")]
    public float horizontalSpeed = 0.5f;

    [Header("Randomization Settings")]
    [Tooltip("Random range for amplitude variation (±)")]
    public float amplitudeVariation = 0.02f;
    [Tooltip("Random range for frequency variation (±)")]
    public float frequencyVariation = 0.3f;
    [Tooltip("Random range for horizontal speed variation (±)")]
    public float speedVariation = 0.2f;

    private Vector2 startPosition;
    private float timeCounter = 0f;
    private SpriteRenderer spriteRenderer;
    TrailRenderer trailRenderer;

    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();

        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Randomize amplitude
        amplitude += Random.Range(-amplitudeVariation, amplitudeVariation);

        // Randomize frequenc
        frequency += Random.Range(-frequencyVariation, frequencyVariation);

        // Randomize horizontal speed
        horizontalSpeed += Random.Range(-speedVariation, speedVariation);

        // Randomize sprite color with vibrant colors using HSV
        if (spriteRenderer != null)
        {
            // Random hue (0-1 covers full rainbow)
            float hue = Random.Range(0f, 1f);
            // High saturation for vibrant colors
            float saturation = Random.Range(0.7f, 1f);
            // High value/brightness for vivid colors
            float value = Random.Range(0.8f, 1f);
            
            spriteRenderer.color = Color.HSVToRGB(hue, saturation, value);
        }

        trailRenderer.startColor = spriteRenderer.color;
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