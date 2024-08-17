using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;

    public string menuSceneName = "MainMenu";

    public SceneTransition sceneTransition;

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle ()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf) //time.fixdeltaTime needed for if speed up!!
        {
            Time.timeScale = 0f;
        } else
        {
            Time.timeScale = 1f;
        }
    }

    public void Restart ()
    {
        Toggle();
        sceneTransition.FadeTo(SceneManager.GetActiveScene().name);
        
    }

    public void Menu ()
    {
        Toggle();
        sceneTransition.FadeTo(menuSceneName);
    }

}