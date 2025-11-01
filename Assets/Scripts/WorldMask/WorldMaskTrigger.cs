using UnityEngine;

public class WorldMaskTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            player.candle.GetComponent<Candle>().doDrain = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            player.candle.GetComponent<Candle>().doDrain = true;
        }
    }
}
