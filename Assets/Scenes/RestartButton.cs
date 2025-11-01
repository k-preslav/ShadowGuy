using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;               

public class RestartButton : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}