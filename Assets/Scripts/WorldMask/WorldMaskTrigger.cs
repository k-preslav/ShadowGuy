using UnityEngine;

public class WorldMaskTrigger : MonoBehaviour
{
    private static int safeZoneCount = 0; // Static to share count across all triggers
    private static GameObject angryMan; // Cache the AngryMan reference statically
    private static Candle playerCandle; // Cache the player candle statically

    private void Start()
    {
        // Only find AngryMan once, shared across all instances
        if (angryMan == null)
        {
            angryMan = GameObject.FindGameObjectWithTag("AngryMan");
            if (angryMan == null)
            {
                Debug.LogWarning("WorldMaskTrigger: No GameObject with tag 'AngryMan' found!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            safeZoneCount++;

            var player = collision.GetComponent<Player>();
            if (player != null && player.candle != null)
            {
                // Cache the candle reference
                if (playerCandle == null)
                {
                    playerCandle = player.candle.GetComponent<Candle>();
                }

                if (playerCandle != null)
                {
                    playerCandle.doDrain = false;
                }
            }

            if (angryMan != null)
            {
                var angryManScript = angryMan.GetComponent<AngryMan>();
                if (angryManScript != null)
                {
                    angryManScript.canChase = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            safeZoneCount--;

            // Only re-enable drain and chase if player is not in ANY safe zone
            if (safeZoneCount <= 0)
            {
                safeZoneCount = 0; // Safety clamp

                if (playerCandle != null)
                {
                    playerCandle.doDrain = true;
                }

                if (angryMan != null)
                {
                    var angryManScript = angryMan.GetComponent<AngryMan>();
                    if (angryManScript != null)
                    {
                        angryManScript.canChase = true;
                    }
                }
            }
        }

    }
}
