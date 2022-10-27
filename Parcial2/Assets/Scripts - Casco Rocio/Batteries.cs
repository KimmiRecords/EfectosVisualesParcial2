using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Batteries : MonoBehaviour
{
    public GameObject myImage;
    public Slots slot;
    public BatteryPickup batteryPickup;
    public FlashlightLife flashlightLife;
    public int batteryRecharge;

    
    public void NewColor() 
    {
        if (slot.id == 1)
        {
            myImage.GetComponent<Image>().color = new Color32(0, 255, 0, 255);

        }
    }

    public void UseBatteries()
    {
        if (slot.id == 1)
        {
            flashlightLife.timer += batteryRecharge;
            Debug.Log("active las baterias");
            slot.id = 0;
            AudioManager.instance.PlayPickup(2f);
        }
    }
    
}
