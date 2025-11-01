using System;
using UnityEngine;

public class DrasniPalniKlechica : MonoBehaviour
{
    [SerializeField] AudioClip sfx;
    public Action OnTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(sfx, transform.position);
            
            var candle = collision.GetComponentInChildren<Candle>();
            candle.candleFill = 1f;

            OnTrigger?.Invoke();
            Destroy(gameObject);
        }
    }
}
