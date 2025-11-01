using UnityEngine;
using UnityEngine.UI;

public class flashProgressBar : MonoBehaviour
{
    [Header("References")]
    public Candle candle; 
    public Image candleSlider; 

    void Start()
    {
        candleSlider.fillAmount = candle.candleFill;
    }

    void Update()
    {
        if (candle != null && candleSlider != null)
        {
            candleSlider.fillAmount = candle.candleFill;
        }
    }
}

