using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    private Scene currentScene;

    public void Restart()
    {
        currentScene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(currentScene.buildIndex);
        SceneManager.LoadScene(currentScene.name);
    }
}
