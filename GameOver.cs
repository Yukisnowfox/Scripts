using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string menuSceneName = "MainMenu";

    public SceneTransition sceneTransition;

    public void Retry ()
    {
        sceneTransition.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu ()
    {
        sceneTransition.FadeTo(menuSceneName);
    }
}