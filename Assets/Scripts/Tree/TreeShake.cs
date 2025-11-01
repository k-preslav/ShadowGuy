using UnityEngine;

public class TreeShake : MonoBehaviour
{
    private Animator animator;
    private bool shaking;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 treePos = transform.position;
            Vector2 playerPos = collision.transform.position;

            if (animator == null) return;

            // Check which side player is on
            int shakeDir = playerPos.x < treePos.x ? -1 : 1;
            animator.SetInteger("ShakeDir", shakeDir);
            shaking = true;
        }
    }

    private void Update()
    {
        if (!shaking) return;

        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

        if ((info.IsName("treeShakeLeft") || info.IsName("treeShakeRight")) && info.normalizedTime >= 1f)
        {
            animator.SetInteger("ShakeDir", 0);
            shaking = false;
        }
    }
}
