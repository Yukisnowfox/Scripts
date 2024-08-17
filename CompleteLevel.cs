using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{

    public string menuSceneName = "MainMenu";

    public string nextLevel = "Level02";
    public int levelToUnlock = 2;

    public SceneTransition sceneTransition;

    public void Continue ()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneTransition.FadeTo(nextLevel);
    }
    
    public void Menu ()
    {
        sceneTransition.FadeTo(menuSceneName);
    }

}
