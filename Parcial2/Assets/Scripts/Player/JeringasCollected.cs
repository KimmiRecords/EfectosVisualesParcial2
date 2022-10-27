using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JeringasCollected : MonoBehaviour
{
    //este script actualiza la cantidad de usbs recolectados en el canvas
    //por diego katabian

    public string FirstPartOfText;
    private Text jeringasCollectedText;
    private string amount;

    void Start()
    {
        if (GetComponent<Text>() != null)
        {
            jeringasCollectedText = GetComponent<Text>();
        }
    }

    void Update()
    {
        amount = PlayerStats.instance.SpeedBoosts.ToString();
        jeringasCollectedText.text = FirstPartOfText + amount;
    }
}
