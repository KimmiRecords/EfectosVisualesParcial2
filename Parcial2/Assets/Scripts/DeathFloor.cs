using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    //este script se lo pones a un trigger para que insta mate a todo lo que lo atraviese
    //bueno, en realidad a todo lo que suscriba con IDeathflooreable.
    //por diego katabian


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDeathflooreable>() != null)
        {
            var matable = other.GetComponent<IDeathflooreable>();
            matable.EnterDeathFloor();
        }

        //if (other.gameObject.layer == 3)
        //{
        //    PlayerStats.instance.InstaDeath();
        //}
    }

}
