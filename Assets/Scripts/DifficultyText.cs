using UnityEngine;
using UnityEngine.UI;

public class DifficultyText : MonoBehaviour
{
    void Start()
    {
        string diff = FindFirstObjectByType<Difficulty>().difficultyLevel;
        GetComponent<Text>().text = diff;
    }
}
