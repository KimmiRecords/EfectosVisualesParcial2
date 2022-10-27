using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidGamePanel : DoorLightButton
{
    //este es un doorControllerButton que pide n usbs para dejarte pasar
    //acordate de listar TODOS los paneles en playerstats
    //TP2 - Francisco Serra y Diego Katabian

    public int usbsRequired;

    public override void Interact()
    {
        if (access && PlayerStats.instance.UsbsCollected >= usbsRequired)
        {
            AudioManager.instance.PlayPickup(1.1f);

            if (quePuertaAbro._doorAnim.GetBool("isOpening"))
            {
                quePuertaAbro.CloseDoor();
                TurnRed();

            }
            else
            {
                quePuertaAbro.OpenDoor();
                TurnGreen();

            }

            //if (!_yaPrendiLasLuces)
            //{
            //    TurnGreen();
            //    _yaPrendiLasLuces = true;
            //}
            //else
            //{
            //    TurnRed();
            //    _yaPrendiLasLuces = false;
            //}
        }
        else
        {
            AudioManager.instance.PlayAccessDenied();
        }
    }
}
