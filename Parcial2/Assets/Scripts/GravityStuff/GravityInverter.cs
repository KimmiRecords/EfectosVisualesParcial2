using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityInverter : MonoBehaviour
{
    void Start()
    {
        Physics.gravity = new Vector3(1, 9.81f, 1);
        print("a");
    }

}
