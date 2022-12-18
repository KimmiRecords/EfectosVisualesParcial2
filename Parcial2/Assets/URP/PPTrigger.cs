using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPTrigger : MonoBehaviour
{
    public Material underwater;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IMicroGravity>() != null)
        {
            underwater.SetFloat("_Intensity", 0.06f);
            underwater.SetFloat("_Intensity2", 1f);
            underwater.SetFloat("_ColorIntensity", 0.2f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<IMicroGravity>() != null)
        {
            underwater.SetFloat("_Intensity", 0f);
            underwater.SetFloat("_Intensity2", 0f);
            underwater.SetFloat("_ColorIntensity", 0f);
        }
    }
}
