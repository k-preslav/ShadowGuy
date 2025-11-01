using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip music;
    private AudioSource source;

    private void Awake()
    {
        // Make it persistent across scenes
        DontDestroyOnLoad(gameObject);

        // Prevent duplicates
        var existing = FindObjectsByType<MusicPlayer>(sortMode: FindObjectsSortMode.None);
        if (existing.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        // Create or get the AudioSource
        source = GetComponent<AudioSource>();
        if (source == null)
            source = gameObject.AddComponent<AudioSource>();

        source.clip = music;
        source.loop = true;
        source.playOnAwake = false;
        source.volume = 0.25f; // optional
        source.spatialBlend = 0f; // make it 2D
        source.Play();
    }
}
