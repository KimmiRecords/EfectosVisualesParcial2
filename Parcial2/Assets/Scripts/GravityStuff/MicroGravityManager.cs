using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroGravityManager : MonoBehaviour
{
    public MicroGravityTrigger[] allMicroGravs;
    public MicroGravityGenerator mgGenerator;


    void Start()
    {
        mgGenerator.TurnOnGenerator += TurnOnMicroGravity;
        mgGenerator.TurnOffGenerator += TurnOffMicroGravity;
    }

    public void TurnOnMicroGravity()
    {
        for (int i = 0; i < allMicroGravs.Length; i++)
        {
            allMicroGravs[i].gameObject.SetActive(true);
        }
    }


    public void TurnOffMicroGravity()
    {
        for (int i = 0; i < allMicroGravs.Length; i++)
        {
            allMicroGravs[i].gameObject.SetActive(false);
        }
    }
}
