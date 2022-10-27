using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickupDespawner : BatteryPickup
{
    public GameObject chebolaCrux;

    public override void Interact()
    {
        //base.Interact();
        //if (chebolaCrux != null) //esto no funca porque el racyast battery se resuelve antes y hace desaparecer al go
        //{
        //    Destroy(chebolaCrux, 0.1f); //destruye al chebola colgado
        //}

        //aca va lo que hace el raycast batteries
    }

    public void OnDisable()
    {
        if (chebolaCrux != null)
        {
            Destroy(chebolaCrux, 0.1f); //destruye al chebola colgado
        }
    }
}
