using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public float shardCooldown = 0f;

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
        // Remove duplicate shards from the array
        RemoveDuplicates();

        // Deactivate all shards BEFORE shuffling
        foreach (GameObject shard in shreds)
        {
            if (shard != null)
            {
                shard.SetActive(false);
            }
        }

        // Shuffle shards array, keeping "LAST" shard at the end
        ShuffleShards();

        // Activate the first one
        if (shreds.Length > 0 && shreds[0] != null)
        {
            //shreds[0].SetActive(true);
        }
    }

    private void RemoveDuplicates()
    {
        // Use a HashSet to track unique shards
        HashSet<GameObject> uniqueShards = new HashSet<GameObject>();
        List<GameObject> uniqueList = new List<GameObject>();

        foreach (GameObject shard in shreds)
        {
            if (shard != null && !uniqueShards.Contains(shard))
            {
                uniqueShards.Add(shard);
                uniqueList.Add(shard);
            }
        }

        // Update the array with unique shards only
        shreds = uniqueList.ToArray();
        
        Debug.Log($"ShardManager: Found {shreds.Length} unique shards after removing duplicates");
    }

    private void ShuffleShards()
    {
        // Find the shard with "LAST" in its name
        GameObject lastShard = null;
        List<GameObject> otherShards = new List<GameObject>();

        foreach (GameObject shard in shreds)
        {
            if (shard.name.ToUpper().Contains("LAST"))
            {
                lastShard = shard;
            }
            else
            {
                otherShards.Add(shard);
            }
        }

        // Shuffle the other shards using Fisher-Yates algorithm
        for (int i = otherShards.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = otherShards[i];
            otherShards[i] = otherShards[randomIndex];
            otherShards[randomIndex] = temp;
        }

        // Rebuild the shreds array with shuffled shards
        for (int i = 0; i < otherShards.Count; i++)
        {
            shreds[i] = otherShards[i];
        }

        // Place the "LAST" shard at the end if it exists
        if (lastShard != null)
        {
            shreds[shreds.Length - 1] = lastShard;
        }
        
        Debug.Log($"ShardManager: Shuffled {otherShards.Count} shards, LAST shard: {(lastShard != null ? lastShard.name : "None")}");
    }

    // This method is called directly by the Shard script
    public void OnShardCollected()
    {
        Debug.Log($"Shard collected! Total: {collected + 1}");
        collected++;
        index++;
        
        if (index < shreds.Length)
        {
            StartCoroutine(ShowShard(index));
        }
    }
    IEnumerator ShowShard(int shardIndex)
    {
        yield return new WaitForSeconds(shardCooldown);
        shreds[shardIndex].SetActive(true);
    }

    private void Update()
    {
        shadesScoreText.text = $"{collected} / 22";
    }
}
