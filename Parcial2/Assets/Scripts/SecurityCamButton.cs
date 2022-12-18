using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SecurityCamButton : Interactable
{
    //este script se lo agregas al casco vr para que active el shader de pp de securitycam.
    //ademas, hace aparecer a las estatuas
    public Material securityCam;
    public GameObject[] estatuas;
    public GameObject cascoVr;

    bool is1 = true;

    public override void Interact()
    {
        print("interact");
        if (is1)
        {
            securityCam.SetFloat("_TODO", 1f);
            securityCam.SetFloat("_Pixelation", 0.26f);

            for (int i = 0; i < estatuas.Length; i++)
            {
                estatuas[i].SetActive(true);
            }

            cascoVr.SetActive(false);

        }
        else
        {
            securityCam.SetFloat("_TODO", 0f);
            securityCam.SetFloat("_Pixelation", 0f);

            for (int i = 0; i < estatuas.Length; i++)
            {
                estatuas[i].SetActive(false);
            }
            cascoVr.SetActive(true);


        }
        is1 = !is1;
    }
}
