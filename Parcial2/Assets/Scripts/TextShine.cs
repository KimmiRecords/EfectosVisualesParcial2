using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShine : MonoBehaviour
{
    public float delayTime;
    public Text text;
    public Color startColor;
    public float frequency;
    public float amplitude;

    bool letsgo;
    void Start()
    {
        if (GetComponent<Text>() != null)
        {
            text = GetComponent<Text>();
        }
        startColor = text.color;
        letsgo = false;
        StartCoroutine("StartShine");
    }

    void Update()
    {
        //adjunta este codigo a cualquier texto UI
        //hace aparecer y desaparecer el texto
        if (letsgo)
        {
            text.color = startColor + new Color(0, 0, 0, Mathf.Sin(frequency * Time.time) * amplitude);
        }
    }

    IEnumerator StartShine()
    {
        yield return new WaitForSeconds(delayTime);
        letsgo = true;
    }
}
