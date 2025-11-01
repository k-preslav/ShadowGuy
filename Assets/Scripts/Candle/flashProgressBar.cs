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

        if (candleSlider.fillAmount <= 0.35f)
        {
            float alpha = Mathf.PingPong(Time.time * 2f, 1f);
            candleSlider.color = new Color(1f, 0f, 0f, alpha);
        }
        else
        {
            candleSlider.color = Color.white;
        }
    }
}

