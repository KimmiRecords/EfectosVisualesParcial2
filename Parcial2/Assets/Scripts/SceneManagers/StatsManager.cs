using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StatsManager : MonoBehaviour
{
    //la idea de este script es que perdura entre escenas y guarda toda la info necesaria, por ej. usbs recolectados
    //por diego katabian

    public static StatsManager instance;

    int usbsRecolectados;
    bool tengoCardKey;
    int jeringasRecolectadas;

    [HideInInspector]
    public string ultimoNivelJugado;

    private void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);


        ultimoNivelJugado = SceneManager.GetActiveScene().name;
        print(ultimoNivelJugado);
    }

    public void SaveStats()
    {
        usbsRecolectados = PlayerStats.instance.UsbsCollected;
        tengoCardKey = PlayerStats.instance.hasCardKey;
        jeringasRecolectadas = PlayerStats.instance.SpeedBoosts;


        ultimoNivelJugado = SceneManager.GetActiveScene().name;
    }

    public void LoadStats()
    {
        PlayerStats.instance.UsbsCollected = usbsRecolectados;
        PlayerStats.instance.hasCardKey = tengoCardKey;
        PlayerStats.instance.SpeedBoosts = jeringasRecolectadas;

        if (PlayerStats.instance.hasCardKey)
        {
            PlayerStats.instance.GrantAccess(PlayerStats.instance.cardKeyAccesses);
        }
    }

    void ActivarModeloLinterna()
    {
        //PlayerStats.instance.CanvasVidaUtil.SetActive(true);
        //PlayerStats.instance.ModeloLinterna.SetActive(true);
    }

}
