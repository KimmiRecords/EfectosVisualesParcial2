using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryPickup : Collectables
{
    public int batteryRecharge; //cuanto recarga cada pickup
    public Text count; //cuantos pickups de bateria conseguiste
    public FlashlightLife wasteBattery; //cantidad de vida de bateria

    public override void Interact()
    {
        base.Interact();
        PlayerStats.instance.batteriesObtained++;
        count.text = PlayerStats.instance.batteriesObtained.ToString("f0") + "/5";

        //wasteBattery.timer += batteryRecharge;

        

        CanvasManager.instance.TurnOnCanvas("CanvasBatteries");
    }

    
}