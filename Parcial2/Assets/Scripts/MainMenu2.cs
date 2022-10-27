using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu2 : MonoBehaviour
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
            SceneManager.LoadScene("Nivel3_TP1_LevelDesign"); //voy al tp1
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
