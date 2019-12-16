using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("escape")){
            PauseGame();
            pausePanel.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
    }
}
