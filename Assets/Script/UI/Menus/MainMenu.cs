using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Loads the gaming Scene
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("Generation_Tester");
    }
    
    /// <summary>
    /// Loads the settings Scene
    /// </summary>
    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    /// <summary>
    /// Closes the game
    /// </summary>
    public void ExitGame()
    {
        //TODO: Implement
    }
}
