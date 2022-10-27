using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GranadasCollected : MonoBehaviour
{
    //este script actualiza la cantidad de granadas recolectadas en el canvas
    //por diego katabian

    public string FirstPartOfText;
    Text _granadasCollectedText;
    string _amount;

    void Start()
    {
        if (GetComponent<Text>() != null)
        {
            _granadasCollectedText = GetComponent<Text>();
        }
    }

    void Update()
    {
        _amount = PlayerStats.instance.Grenades.ToString();
        _granadasCollectedText.text = FirstPartOfText + _amount;
    }
}