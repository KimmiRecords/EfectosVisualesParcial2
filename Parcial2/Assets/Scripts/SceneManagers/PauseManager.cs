using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject allPauseTexts;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PlayerStats.instance.isPaused)
            {
                TurnOnPauseCanvas();
            }
            else
            {
                TurnOffPauseCanvas();
            }
        }

        if (PlayerStats.instance.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //Time.timeScale = 1;
                //SceneManager.LoadScene("MainMenuScene");
                Application.Quit();
            }
        }
    }

    void TurnOnPauseCanvas()
    {
        PlayerStats.instance.isPaused = true;
        allPauseTexts.SetActive(true);
        Time.timeScale = 0f;
    }

    void TurnOffPauseCanvas()
    {
        PlayerStats.instance.isPaused = false;
        allPauseTexts.SetActive(false);
        Time.timeScale = 1;
    }
}
