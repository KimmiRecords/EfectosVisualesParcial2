using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalUSB : USBPickup
{
    //este script agrega funciones especiales al ultimo USB que agarras
    //ademas de ser un usbpickup normal
    //TP2 - Diego Katabian

    public delegate void MyDelegate();
    public event MyDelegate OnFinalUSBPickup;

    public override void Interact()
    {
        base.Interact();
        OnFinalUSBPickup(); //se disparan todos los metodos suscritos
    }
}
