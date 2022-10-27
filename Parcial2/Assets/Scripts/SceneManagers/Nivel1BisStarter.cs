using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel1BisStarter : MonoBehaviour
{
    public DoorController[] puertasQueQuedaronAbiertas;
    public Light[] lucesQueQuedaronVerdes;
    public Color verde;
    public FinalUSB finalUsb;


    void Start()
    {
        OpenTheDoors();
        TurnGreen();
        //Invoke("AddNecessaryUsbs", 0.1f);

        finalUsb.OnFinalUSBPickup += AudioManager.instance.TurnOnFinalAlarm; //suscribo el metodo PrenderAlarmas al evento
    }

    public void TurnGreen()
    {
        for (int i = 0; i < lucesQueQuedaronVerdes.Length; i++)
        {
            lucesQueQuedaronVerdes[i].color = verde;
        }
    }

    public void OpenTheDoors()
    {
        for (int i = 0; i < puertasQueQuedaronAbiertas.Length; i++)
        {
            puertasQueQuedaronAbiertas[i].OpenDoor();
        }
    }

    //public void AddNecessaryUsbs()
    //{
    //    print("agregue usbs");
    //    PlayerStats.instance.UsbsCollected = 3;
    //}
}
