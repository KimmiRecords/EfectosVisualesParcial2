using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class YouDiedScene : MonoBehaviour
{
    public Text youDied;
    public Text pressAnyKey;
    private float timer;
    private float timer2;

    public float timeUntilFadeIn;
    public float oscillationFrequency;
    public float transparencyOffset;

    string ultimoLvl = "";

    void Start()
    {
        AudioManager.instance.StopAll();
        AudioManager.instance.PlayByName("Belveth");
        AudioManager.instance.PlayByName("YouDiedSFX");

        youDied.color = new Color(1, 0, 0, 0);
        pressAnyKey.color = new Color(1, 0.5f, 0.5f, 0);

        if (StatsManager.instance.ultimoNivelJugado != null)
        {
            ultimoLvl = StatsManager.instance.ultimoNivelJugado;
        }

        print(ultimoLvl);
    }

    void Update()
    {
        timer += 0.15f * Time.deltaTime;
        timer2 += Time.deltaTime;

        youDied.color = new Color(1, 0, 0, Mathf.Lerp(0,1,timer));

        if (timer2 > timeUntilFadeIn) //a los tantos segs aparece oscilando
        {
            pressAnyKey.color = new Color(1, 0.5f, 0.5f, Mathf.Sin(oscillationFrequency * Time.time)+transparencyOffset);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            AudioManager.instance.StopAll();
            AudioManager.instance.PlayMainMenuMusic();

            SceneManager.LoadScene("MainMenuScene"); //volves al main menu
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            AudioManager.instance.StopAll();
            Destroy(AudioManager.instance.gameObject);

            if (ultimoLvl == "")
            {
                ultimoLvl = "Nivel1";
            }

            SceneManager.LoadScene(ultimoLvl); //volves al nivel
        }

    }
}
