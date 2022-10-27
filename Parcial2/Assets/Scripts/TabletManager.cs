using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletManager : MonoBehaviour
{
    //este script administra las tablets para que sus pantallas se prendan 
    //por diego katabian

    public GameObject[] tabletCanvas;
    public FinalUSB finalUsb;

    private void Start()
    {
        if (finalUsb != null)
        {
            finalUsb.OnFinalUSBPickup += TurnOnTablets; //nos suscribimossss
        }
    }

    public void TurnOnTablets()
    {
        for (int i = 0; i < tabletCanvas.Length; i++)
        {
            tabletCanvas[i].gameObject.SetActive(true);
        }
    }
}
