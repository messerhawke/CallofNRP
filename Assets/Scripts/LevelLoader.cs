using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    int currentSceneIndex;

    // Start is called before the first frame update

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex +1);
        
    }
    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(currentSceneIndex-1);
    }
    public void LoadWinScene()
    {
        //SceneManager.LoadScene(5);
    }
    public void LoadLostScene()
    {
        SceneManager.LoadScene(5);
    }
    public void ExitFromGame()
    {
        Application.Quit();
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

}
