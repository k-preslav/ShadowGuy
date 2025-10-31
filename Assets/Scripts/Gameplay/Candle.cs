using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Candle : MonoBehaviour
{
    [SerializeField] float candleFill = 1f;       // 1 = full, 0 = empty
    [SerializeField] Transform foreground;        // The fill sprite or mesh
    [SerializeField] float drainSpeed = 0.05f;    // How fast it burns

    [SerializeField] Light2D candleLight;

    private Vector3 initialScale;
    private Vector3 initialPos;

    void Start()
    {
        if (foreground)
        {
            initialScale = foreground.localScale;
            initialPos = foreground.localPosition;
        }
    }

    void Update()
    {
        candleFill = Mathf.Clamp01(candleFill - Time.deltaTime * drainSpeed);
        if (candleLight)
        {
            candleLight.intensity = candleFill * 1.5f;
        }

        if (foreground)
        {
            // Scale vertically
            foreground.localScale = new Vector3(initialScale.x, initialScale.y * candleFill, initialScale.z);

            // Move upward so the bottom stays in place
            float offset = (1 - candleFill) * (initialScale.y / 2f);
            foreground.localPosition = initialPos + new Vector3(0f, -offset, 0f);
        }
    }
}
