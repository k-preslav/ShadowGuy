using UnityEngine;

public class SplashSfx : MonoBehaviour
{
    [SerializeField] AudioClip sfx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(sfx, transform.position);
        }
    }
}
