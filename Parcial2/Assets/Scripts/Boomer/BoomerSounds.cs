using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerSounds
{
    Patrol boomer;

    

    public BoomerSounds(Patrol p)
    {
        boomer = p;
    }

    public void UpdateSoundsPosition()
    {
        boomer.walk.transform.position = boomer.transform.position;
        boomer.run.transform.position = boomer.transform.position;
        boomer.scream.transform.position = boomer.transform.position;
    }
}
