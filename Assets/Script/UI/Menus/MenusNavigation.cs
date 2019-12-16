using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusNavigation : MonoBehaviour
{
    private Scene currentScene;

    /// <summary>
    /// Reloads the current scene
    /// </summary>
    public void ReloadCurrentScene()
    {
        currentScene = SceneManager.GetActiveScene();

        Time.timeScale = 1.0f;
        SceneManager.LoadScene(currentScene.name);
    }

    /// <summary>
    /// Loads the Main Menu Scene
    /// </summary>
    public void ToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main_Menu");
    }

    /// <summary>
    /// Loads the Town Scene
    /// </summary>
    public void ToTown()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Town");
    }


}
