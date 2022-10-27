using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerAnimations
{
    public Animator anim;
    public BoomerAnimations(Animator a)
    {
        anim = a;
    }

    public void StartWalking()
    {
        Debug.Log("dispare startwalking");
        anim.SetBool("isRunning", false);
        anim.SetBool("isPain", false);
    }

    public void StartRunning()
    {
        anim.SetBool("isRunning", true);
    }

    public void StartPain()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isPain", true);
    }
}
