using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetariumButton : Interactable
{

    //este script se lo agregas a la consolita que activa el planetario

    public Animator planetariumAnimator;

    bool is1 = false;

    public override void Interact()
    {
        print("interact");
        if (is1)
        {
            planetariumAnimator.Play("LerpTo0");
        }
        else
        {
            planetariumAnimator.Play("LerpTo1");
        }
        is1 = !is1;
    }
}
