using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;      
    public GameObject difficultyPanel;

    public GameObject firstDifficultyButton;
        public GameObject secondDifficultyButton;
    public GameObject thirdDifficultyButton;


    public void OpenDifficulty()
    {
        mainMenuPanel.SetActive(false);
        difficultyPanel.SetActive(true);

        if (firstDifficultyButton != null && EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(firstDifficultyButton);
        if (secondDifficultyButton != null && EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(secondDifficultyButton);
             if (thirdDifficultyButton != null && EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(thirdDifficultyButton);
    }

    public void CloseDifficulty()
    {
        difficultyPanel.SetActive(false);
        mainMenuPanel.SetActive(true);

        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);
    }
}