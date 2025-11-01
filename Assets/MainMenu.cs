using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;      
    public GameObject difficultyPanel;

    private void Start()
    {
        difficultyPanel.SetActive(false);
    }

    public void OpenDifficulty()
    {
        mainMenuPanel.SetActive(false);
        difficultyPanel.SetActive(true);
    }

    public void CloseDifficulty()
    {
        difficultyPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OpenScene()
    {
        SceneManager.LoadScene("Opening");
    }
}