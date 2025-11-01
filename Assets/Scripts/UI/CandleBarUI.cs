using UnityEngine;
using UnityEngine.UI;

public class CandleBarUI : MonoBehaviour
{
    [Header("References")]
    public Candle candle; // Assign your Candle script in the Inspector
    public Slider candleSlider; // Assign your UI Slider in the Inspector

    void Start()
    {
        if (candleSlider == null)
            candleSlider = GetComponent<Slider>();
        if (candle == null)
            Debug.LogWarning("CandleBarUI: Candle reference not set!");
    }

    void Update()
    {
        if (candle != null && candleSlider != null)
        {
            // Assumes candle.candleFill is normalized (0 to 1). Adjust if needed.
            candleSlider.value = candle.candleFill;
        }
    }
}
