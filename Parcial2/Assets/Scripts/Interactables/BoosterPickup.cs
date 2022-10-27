using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterPickup : Collectables
{
    public override void Interact()
    {
        if (PlayerStats.instance.SpeedBoosts == 0)
        {
            CanvasManager.instance.TurnOnCanvas("CanvasJeringas");
        }

        PlayerStats.instance.SpeedBoosts++;
        base.Interact(); //el base es solo reproducir un sfx
    }
}
