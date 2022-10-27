using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LounchGranada : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject granada;
    public float range;

    Rigidbody _granadaRb;

    public void Launch()
    {

        if (PlayerStats.instance.Grenades > 0)
        {
            AudioManager.instance.PlayByNamePitch("JumpUpSFX", 2);
            GameObject granadaInstance = Instantiate(granada, spawnPoint.position, spawnPoint.rotation);

            if (granadaInstance.GetComponent<Rigidbody>() != null)
            {
                _granadaRb = granadaInstance.GetComponent<Rigidbody>();
            }

            _granadaRb.AddForce(spawnPoint.forward * range, ForceMode.Impulse);
            PlayerStats.instance.Grenades--;
        }
        else
        {
            print("no tenes granadas para tirar");
        }
    }
}
