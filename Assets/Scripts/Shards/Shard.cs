using System;
using System.Collections;
using UnityEngine;

public class Shard : MonoBehaviour
{
    [SerializeField] GameObject liveWorldMask;
    [SerializeField] float size = 1f;

    GameObject _instantiated;

    private bool hasTriggered = false;

    private void Update()
    {
        if (_instantiated)
        {
            _instantiated.transform.localScale = Vector2.Lerp(_instantiated.transform.localScale, new Vector2(size, size), Time.deltaTime * 3);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            
            Debug.Log($"Shard {gameObject.name} collected!");
            
            _instantiated = Instantiate(liveWorldMask, transform.position, Quaternion.identity);
            _instantiated.transform.localScale = new Vector2(0, 0);

            // Notify the ShardManager directly
            if (ShardManager.Instance != null)
            {
                ShardManager.Instance.OnShardCollected();
            }
            else
            {
                Debug.LogError("ShardManager.Instance is null! Cannot notify shard collection.");
            }

            StartCoroutine(DisableAfterDelay());
        }
    }

    private void OnEnable()
    {
        // Reset the flag when the shard is re-enabled
        hasTriggered = false;
    }

    IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
