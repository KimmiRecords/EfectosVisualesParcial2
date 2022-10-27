using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    //con este script prendo y apago canvases
    //los ordene en un diccionario para poder disparar el metodo en otros script y no necesitar refe al go, sino solo saber el nombre. 
    //por diego katabian

    public static CanvasManager instance;

    public GameObject[] canvasGameObjects; //array de los canvases

    public GameObject linternaActiveIcon;
    public GameObject jeringaActiveIcon;
    public GameObject granadaActiveIcon;

    int turnOnCanvasCount = 0;

    public Dictionary<string, GameObject> canvases = new Dictionary<string, GameObject>();

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        for (int i = 0; i < canvasGameObjects.Length; i++) //el array de gameobjects lo uso para armarme el diccionario
        {
            canvases.Add(canvasGameObjects[i].name, canvasGameObjects[i]);
        }
    }

    public void TurnOnCanvas(string canvasName)
    {
        if (turnOnCanvasCount == 0)
        {
            canvases["CanvasBorde"].SetActive(true);
        }

        if (canvases[canvasName] != null)
        {
            canvases[canvasName].SetActive(true);
        }
        turnOnCanvasCount++;
    }
}
