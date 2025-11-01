using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FromIntroToGame : MonoBehaviour
{
    [SerializeField] AudioClip sfx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Intro());   
    }

    IEnumerator Intro()
    {
        // Start preloading the scene in the background
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");
        asyncLoad.allowSceneActivation = false;

        yield return new WaitForSeconds(7.5f);
        AudioSource.PlayClipAtPoint(sfx, transform.position, 1);

        yield return new WaitForSeconds(1.5f);
        
        // Wait until the scene is fully loaded
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }
        
        // Activate the scene
        asyncLoad.allowSceneActivation = true;
    }
}
