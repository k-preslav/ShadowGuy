using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GateOpen : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] AudioClip openSfx;
    [SerializeField] Collider2D gateCol;
    bool isTriggered = false;

    ShardManager shardManager;

    private void Start()
    {
        shardManager = FindFirstObjectByType<ShardManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (shardManager.collected < 21) return;

            if (isTriggered) return;

            isTriggered = true;
            
            AudioSource.PlayClipAtPoint(openSfx, transform.position);
            StartCoroutine(StartAnim());
        }
    }

    IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(0.67f);
        
        Collider2D[] colliders = gateCol.GetComponents<Collider2D>();
        if (colliders.Length >= 6)
        {
            colliders[5].enabled = false;
        }

        anim.SetTrigger("Open");
    }
}
