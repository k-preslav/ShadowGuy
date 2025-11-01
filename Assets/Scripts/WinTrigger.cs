using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] GameObject ui;
    [SerializeField] Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ui.SetActive(true);
            anim.SetTrigger("Win");

            var player = collision.gameObject.GetComponent<Player>();
            player.speed = 0;
            player.tag = "Untagged";

            var witch = FindFirstObjectByType<Witch>();
            Destroy(witch.gameObject);
        }
    }
}
