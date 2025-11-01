using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Candle : MonoBehaviour
{
    public float candleFill = 1f;       // 1 = full, 0 = empty
    [SerializeField] float drainSpeed = 0.05f;    // How fast it burns

    [SerializeField] Light2D candleLight;

    [HideInInspector] public bool doDrain = true;

    private Vector3 initialScale;
    private Vector3 initialPos;

    void Start()
    {

    }

    void Update()
    {
        if (!doDrain)
        {
            return;
        }

        candleFill = Mathf.Clamp01(candleFill - Time.deltaTime * drainSpeed);
        if (candleLight)
        {
            candleLight.intensity = candleFill * 1.5f;
        }
    }
}
