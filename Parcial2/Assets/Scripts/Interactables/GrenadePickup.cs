using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : Collectables
{
    public override void Interact()
    {
        if (PlayerStats.instance.Grenades == 0)
        {
            CanvasManager.instance.TurnOnCanvas("CanvasGranadas");
        }

        base.Interact();
        PlayerStats.instance.Grenades++;
    }
}
