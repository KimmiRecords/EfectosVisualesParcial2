using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransportable
{
    void Transport(Vector3 v);
    void EnterPlatform();
    void ExitPlatform();

}
