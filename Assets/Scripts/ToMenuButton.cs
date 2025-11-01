using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMenuButton : MonoBehaviour
{
    public void ToMenu()
    {
        SceneManager.LoadScene("main menu");
    }
}
