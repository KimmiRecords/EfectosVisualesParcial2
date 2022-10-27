using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Instructions : MonoBehaviour
{
    //public Text controls;
    public Text objectiveWhite;
    public Text objectiveRed;
    public Text credits;

    public Transform MainCamera;
    public Transform Camera2;

    public GameObject usbsgroup;
    public GameObject objectiveRedAccents;

    private Color _controlsInitialColor;
    private Color _objectiveRedInitialColor;
    private Color _creditsInitialColor;

    private bool _instructionsSeen = false;
    private float _timer;
    private float _cameraTimer;
    public float canvasTimerSpeed;
    public float cameraTimerSpeed;


    void Start()
    {
        _timer = 0;

        //_controlsInitialColor = controls.color; //guardo el color inicial
        _objectiveRedInitialColor = objectiveRed.color;
        _creditsInitialColor = credits.color;

        //controls.color = Color.clear; //arrancan invisibles
        objectiveRed.color = Color.clear;
        objectiveWhite.color = Color.clear;
        credits.color = Color.clear;
    }

    void Update()
    {
        _timer += (Time.deltaTime / canvasTimerSpeed);
        _cameraTimer += (Time.deltaTime / cameraTimerSpeed);


        //MOVIMIENTO CINEMATICO DE LA CAMARA
        //HAGO QUE VAYA DESDE LA POSICION Y ROTACION INICIAL HASTA LAS NUEVAS

        MainCamera.position = Vector3.Lerp(MainCamera.position, Camera2.position, _cameraTimer);
        MainCamera.rotation = Quaternion.Lerp(MainCamera.rotation, Camera2.rotation, _cameraTimer);

        ////lerpeo la posicion de la maincamera, desde su posicion inicial hasta la de la camera2
        //MainCamera.position = new Vector3(Mathf.Lerp(MainCamera.position.x, Camera2.position.x, _cameraTimer),
        //                                   Mathf.Lerp(MainCamera.position.y, Camera2.position.y, _cameraTimer),
        //                                   Mathf.Lerp(MainCamera.position.z, Camera2.position.z, _cameraTimer));

        ////misma pero para la rotacion, sin embargo...
        ////parece que funciona, pero hace que la camara se vuelva loca y gire como trompo.
        ////queda muy copado y lo voy a dejar
        //MainCamera.rotation = Quaternion.Euler(Mathf.Lerp(MainCamera.rotation.eulerAngles.z, Camera2.rotation.eulerAngles.z, _cameraTimer),
        //                                       Mathf.Lerp(MainCamera.rotation.eulerAngles.y, Camera2.rotation.eulerAngles.y, _cameraTimer),
        //                                       Mathf.Lerp(MainCamera.rotation.eulerAngles.x, Camera2.rotation.eulerAngles.x, _cameraTimer));


        //LOGICA DEL CANVAS
        if (!_instructionsSeen) //fadein de los creditos
        {
            credits.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, _timer));
        }

        if (Input.anyKey && _instructionsSeen == false && _timer >= 0.5) //paso a mostrar las instrucciones
        {
            _instructionsSeen = true;
            _timer = 0; //reseteo el timer para fadear las instrucciones tambien
        }

        if (_instructionsSeen) //fadein de las instrucciones
        {
            credits.color = Color.clear; //apaga los credits

            objectiveRed.color = new Color(_objectiveRedInitialColor.r, _objectiveRedInitialColor.g, _objectiveRedInitialColor.b, Mathf.Lerp(0, 1, _timer-0.5f));
            objectiveWhite.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, _timer-0.5f));
            //Invoke("ShowRedAccents", 4);

            Invoke("ShowUSBGroup", 6);
        }

        //CAMBIO DE ESCENA
        if (Input.GetKeyDown(KeyCode.E) && _instructionsSeen == true)
        {
            ChangeScene("Nivel1");
        }

        if (Input.GetKeyDown(KeyCode.R) && _instructionsSeen == true)
        {
            ChangeScene("Nivel2");
        }

        if (Input.GetKeyDown(KeyCode.T) && _instructionsSeen == true)
        {
            ChangeScene("Nivel1bis");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void ShowUSBGroup()
    {
        usbsgroup.SetActive(true);
    }

    void ShowRedAccents()
    {
        objectiveRedAccents.SetActive(true);
    }

    void ChangeScene(string sceneName)
    {
        AudioManager.instance.StopMainMenuMusic();
        Destroy(AudioManager.instance.gameObject);
        SceneManager.LoadScene(sceneName);
    }
}
