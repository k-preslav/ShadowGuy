using UnityEngine;
using UnityEngine.SceneManagement;

public class difBut : MonoBehaviour
{
    // Call this from the Button OnClick to load the game scene at build index 0
    public void LoadGameIndex0()
    {
        // Make sure the target scene is added to Build Settings -> "Scenes In Build" (index 0)
        SceneManager.LoadScene(0);
    }
}