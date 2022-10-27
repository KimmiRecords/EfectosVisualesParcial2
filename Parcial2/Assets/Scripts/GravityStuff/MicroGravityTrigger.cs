using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroGravityTrigger : MonoBehaviour
{
    //este script se lo pones a un trigger para que dispare el IMicrogravity. 
    //por diego katabian


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IMicroGravity>() != null)
        {
            var afectado = other.GetComponent<IMicroGravity>();
            afectado.EnterMicroGravity();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IMicroGravity>() != null)
        {
            var afectado = other.GetComponent<IMicroGravity>();
            afectado.ExitMicroGravity();
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.GetComponent<IMicroGravity>() != null)
    //    {
    //        var afectado = other.GetComponent<IMicroGravity>();

    //        if (!afectado.IsInsideMicroGravity)
    //        {
    //            afectado.IsInsideMicroGravity = true;
    //        }
    //    }
    //}

}
