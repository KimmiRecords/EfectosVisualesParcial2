using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroGravityGenerator : Interactable
{
    public GameObject generatorFX;
    public Light[] luces;

    public Color rojo = new Color(225 / 255, 70 / 255, 70 / 255, 1);
    public Color verde = new Color(125 / 255, 255 / 255, 125 / 255, 1);

    bool encendido = false;

    public delegate void MyDelegate();
    public event MyDelegate TurnOnGenerator;

    public delegate void MyDelegate2();
    public event MyDelegate2 TurnOffGenerator;

    public PlayerMovement pm;


    public override void Interact()
    {
        encendido = !encendido;

        if (encendido)
        {
            TurnOnGenerator();
            GeneratorFXOn();
            TurnGreen();
        }
        else
        {
            TurnOffGenerator();
            GeneratorFXOff();
            TurnRed();
            pm.ExitMicroGravity();
        }
        base.Interact();
    }

    public void GeneratorFXOn()
    {
        generatorFX.SetActive(true);
    }

    public void GeneratorFXOff()
    {
        generatorFX.SetActive(false);
    }

    public void TurnRed()
    {
        for (int i = 0; i < luces.Length; i++)
        {
            luces[i].color = rojo;
        }
    }
    public void TurnGreen()
    {
        for (int i = 0; i < luces.Length; i++)
        {
            luces[i].color = verde;
        }
    }
}
