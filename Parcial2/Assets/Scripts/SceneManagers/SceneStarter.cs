using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneStarter : MonoBehaviour
{
    //este script carga los stats al inicio de la escena para preservar continuidad
    //por diego katabian

    public string conQueTemaArranco;

    public int necessaryUsbs;
    public bool necessaryCardkey;

    void Start()
    {

        if (StatsManager.instance.ultimoNivelJugado != SceneManager.GetActiveScene().name)
        {
            StatsManager.instance.LoadStats();
        }

        AudioManager.instance.StopAll();
        AudioManager.instance.PlayByName(conQueTemaArranco);

        if (necessaryCardkey)
        {
            PlayerStats.instance.GetCardKey();
        }

        PlayerStats.instance.UsbsCollected = necessaryUsbs;
        CanvasManager.instance.TurnOnCanvas("CanvasUSB");
        

        if (PlayerStats.instance.hasFlashlight == true)
        {
            CanvasManager.instance.TurnOnCanvas("CanvasVidaUtil");
        }

        if (PlayerStats.instance.SpeedBoosts > 0)
        {
            CanvasManager.instance.TurnOnCanvas("CanvasJeringas");
        }

        StatsManager.instance.SaveStats();
    }
}
