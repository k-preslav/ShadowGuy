using UnityEngine;

public class DrasniPalniKlechica : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var candle = collision.GetComponentInChildren<Candle>();
            candle.candleFill = 1f;

            Destroy(this.gameObject);
        }
    }
}
