using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpin : MonoBehaviour
{
    private float spin;

    float originalx;
    float originaly;

    private void Start()
    {
        originalx = transform.rotation.eulerAngles.x;
        originaly = transform.rotation.eulerAngles.y;

    }
    void Update()
    {
        spin += 0.1f;
        transform.rotation = Quaternion.Euler(originalx, originaly, spin);
    }
}
