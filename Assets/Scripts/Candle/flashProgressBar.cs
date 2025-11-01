using UnityEngine;
using UnityEngine.UI;

public class flashProgressBar : MonoBehaviour
{
    [Header("References")]
    public Candle candle; 
    public Slider candleSlider; 

    void Start()
    {
        candleSlider.value = candle.candleFill;
        if (candleSlider == null)
            candleSlider = GetComponent<Slider>();
        if (candle == null)
            Debug.LogWarning("CandleBarUI: Candle reference not set!");
    }

    void Update()
    {
        if (candle != null && candleSlider != null)
        {
            candleSlider.value = candle.candleFill;
        }
    }
}

