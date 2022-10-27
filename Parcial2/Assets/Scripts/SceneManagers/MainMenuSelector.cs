using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSelector : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.StopAll();
        AudioManager.instance.PlayByName("MainMenuMusic");
    }

    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("InstructionsScene"); //instrucciones
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
