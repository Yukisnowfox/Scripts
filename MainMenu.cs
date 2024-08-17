using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "MainLevel";

    public SceneTransition sceneTransition;

    public void Play ()
    {
        sceneTransition.FadeTo(levelToLoad);
    }
    
    public void Exit ()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}