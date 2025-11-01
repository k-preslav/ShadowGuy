using UnityEngine;

public class FlyingSqrManager : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform worldStart;

    float spawnInterval = 0.1f;

    void Update()
    {
        spawnInterval -= Time.deltaTime;

        if (spawnInterval <= 0f)
        {
            spawnInterval = 3f;

            for (int i = 0; i < Random.Range(5, 15); i++)
            {
                Vector2 spawnPos = new Vector2(
                    worldStart.position.x + Random.Range(-5f, 5f),
                    Random.Range(-15, 15f)
                );
                Instantiate(prefab, spawnPos, Quaternion.identity);
            }
        }
    }
}
