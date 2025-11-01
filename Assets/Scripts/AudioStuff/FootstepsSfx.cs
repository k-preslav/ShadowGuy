using UnityEngine;

public class FootstepsSfx : MonoBehaviour
{
    [SerializeField] AudioClip[] footsteps;
    [SerializeField] private float stepInterval = 0.15f; // Time between footstep sounds
    [SerializeField] private float volumeScale = 1f;

    private bool isWalking = false;
    private float stepTimer = 0f;

    private void Update()
    {
        if (isWalking)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
    }

    public void StartWalking()
    {
        isWalking = true;
        stepTimer = 0f; // Play immediately when starting to walk
    }

    public void StopWalking()
    {
        isWalking = false;
        stepTimer = 0f;
    }

    public void PlayFootstep()
    {
        if (footsteps.Length == 0) return;
        int index = Random.Range(0, footsteps.Length);
        AudioSource.PlayClipAtPoint(footsteps[index], transform.position, volumeScale);
    }
}
