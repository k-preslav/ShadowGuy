using UnityEngine;
using UnityEngine.UI;

public class ShardManager : MonoBehaviour
{
    public static ShardManager Instance { get; private set; }

    [SerializeField] private GameObject[] shreds;
    int index = 0;

    [SerializeField] Text shadesScoreText;

    public int activeShards => index + 1;
    public int collected = 0;

    private void Awake()
    {
        // Set up singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple ShardManagers detected!");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Deactivate all shards
        foreach (GameObject shard in shreds)
        {
            shard.SetActive(false);
        }

        // Activate the first one
        shreds[0].SetActive(true);
    }

    // This method is called directly by the Shard script
    public void OnShardCollected()
    {
        Debug.Log($"Shard collected! Total: {collected + 1}");
        collected++;
        index++;
        
        if (index < shreds.Length)
        {
            shreds[index].SetActive(true);
        }
    }

    private void Update()
    {
        shadesScoreText.text = $"{collected} / 21";
    }
}
