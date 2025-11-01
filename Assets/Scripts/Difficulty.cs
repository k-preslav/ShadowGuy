using UnityEngine;
using UnityEngine.SceneManagement;

public class Difficulty : MonoBehaviour
{
    public string difficultyLevel;

    public float drainSpeed;
    public int limitOfKibrit;
    public float shardCooldown;

    bool isTriggered = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetEasy()
    {
        difficultyLevel = "Easy";
        isTriggered = false;

        drainSpeed = 0.05f;
        limitOfKibrit = 999;
        shardCooldown = 0f;
    }

    public void SetMedium()
    {
        difficultyLevel = "Medium";

        isTriggered = false;
        drainSpeed = 0.1f;
        limitOfKibrit = 9;
        shardCooldown = 0f;
    }

    public void SetHard()
    {
        difficultyLevel = "Hard";

        isTriggered = false;
        drainSpeed = 0.085f;
        limitOfKibrit = 6;
        shardCooldown = 2.67f;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (!isTriggered)
            {
                isTriggered = true;
                Trigger();
            }
        }
    }

    void Trigger()
    {
        FindFirstObjectByType<Candle>().drainSpeed = drainSpeed;
        FindFirstObjectByType<KibritManager>().totalKibritsMax = limitOfKibrit;
        FindFirstObjectByType<ShardManager>().shardCooldown = shardCooldown;

        Debug.Log("Difficulty Set!");
        Debug.Log("Candle Drain Speed: " + drainSpeed);
        Debug.Log("Kibrit Limit: " + limitOfKibrit);
        Debug.Log("Shard Cooldown: " + shardCooldown);
    }
}
