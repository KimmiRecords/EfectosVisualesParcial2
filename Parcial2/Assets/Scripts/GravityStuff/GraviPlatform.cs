using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraviPlatform : GraviBox
{
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<ITransportable>() != null)
        {
            var elotro = other.GetComponent<ITransportable>();
            elotro.Transport(_rb.velocity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PasosSFX>() != null)
        {
            var elotro = other.GetComponent<PasosSFX>();
            elotro.EnterPlatform();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PasosSFX>() != null)
        {
            var elotro = other.GetComponent<PasosSFX>();
            elotro.ExitPlatform();
        }
    }
}
