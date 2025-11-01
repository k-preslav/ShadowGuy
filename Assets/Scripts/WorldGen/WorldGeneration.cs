using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    [Header("World Objects")]
    [Header("Dead World")]
    [SerializeField] GameObject deadTreePrefab;
    [SerializeField] GameObject deadBushPrefab;
    [SerializeField] GameObject deadGrassPrefab;
    [Header("Alive World")]
    [SerializeField] GameObject treePrefab;
    [SerializeField] GameObject bushPrefab;
    [SerializeField] GameObject grassPrefab;

    [Space(10)]
    [Header("World Settings")]
    [SerializeField] Transform worldStart;
    [SerializeField] Transform worldEnd;
    [SerializeField] int treeCount = 100;
    [SerializeField] int grassCount = 100;
    [SerializeField] int bushCount = 0;

    [Space(10)]
    [Header("World Parents")]
    [SerializeField] Transform deadWorldParent;
    [SerializeField] Transform aliveWorldParent;

    void Start()
    {
        GenerateWorld();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateWorld();
        }
    }

    void GenerateWorld()
    {
        // Kill all children
        foreach (Transform child in deadWorldParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in aliveWorldParent)
        {
            if (child.gameObject.name.Contains("Ground"))
                continue;

            Destroy(child.gameObject);
        }

        for (int i = 0; i < treeCount; i++)
        {
            Vector2 position = GetRandomPosition();
            Instantiate(deadTreePrefab, position, Quaternion.identity, deadWorldParent);
            Instantiate(treePrefab, position, Quaternion.identity, aliveWorldParent);
        }
        for (int i = 0; i < grassCount; i++)
        {
            Vector2 position = GetRandomPosition();
            Instantiate(deadGrassPrefab, position, Quaternion.identity, deadWorldParent);
            Instantiate(grassPrefab, position, Quaternion.identity, aliveWorldParent);
        }
        for (int i = 0; i < bushCount; i++)
        {
            Vector2 position = GetRandomPosition();
            Instantiate(deadBushPrefab, position, Quaternion.identity, deadWorldParent);
            Instantiate(bushPrefab, position, Quaternion.identity, aliveWorldParent);
        }
    }

    Vector2 GetRandomPosition()
    {
        float x = Random.Range(worldStart.position.x, worldEnd.position.x);
        float y = Random.Range(worldStart.position.y, worldEnd.position.y);
        return new Vector2(x, y);
    }
}
