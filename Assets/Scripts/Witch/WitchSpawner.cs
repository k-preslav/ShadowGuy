using System.Collections;
using UnityEngine;

public class WitchSpawner : MonoBehaviour
{
    [SerializeField] GameObject witchPrefab;
    [SerializeField] AudioClip witchSfx;

    ShardManager shardManager;
    bool isRan = false;

    Player player;
    CameraMovement cameraMovement;

    private void Start()
    {
        shardManager = FindFirstObjectByType<ShardManager>();
        player = FindFirstObjectByType<Player>();
        cameraMovement = FindFirstObjectByType<CameraMovement>();
    }

    private void Update()
    {
        if (shardManager.collected < 21) return;

        if (isRan) return;

        Debug.Log("Spawning Witch...");

        player.speed = 0f;
        cameraMovement.StartCutsceneWitchEnd();

        isRan = true;
        StartCoroutine(SpawnWitch());
    }

    IEnumerator SpawnWitch()
    {
        yield return new WaitForSeconds(1.5f);
        AudioSource.PlayClipAtPoint(witchSfx, transform.position);

        yield return new WaitForSeconds(1.5f);
        Instantiate(witchPrefab, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(3f);
        player.speed = 5f;
    }
}
