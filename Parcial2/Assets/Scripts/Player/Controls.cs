using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls
{
    //clase construida por playerMovement
    //incluye cheats para sumarse usbs, linterna y cardkey
    //por diego katabian

    public float h;
    public float v;
    public bool isJump;

    PlayerMovement _playerMovement;

    public Controls(PlayerMovement pm)
    {
        _playerMovement = pm;
        isJump = false;
    }
    public void CheckControls()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _playerMovement.Run();
            AudioManager.instance.isRunning = true;
            AudioManager.instance.ChangePitchPasos(true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _playerMovement.StopRunning();
            AudioManager.instance.isRunning = false;
            AudioManager.instance.ChangePitchPasos(false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            isJump = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJump = false;
        }

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    _playerMovement.StartSpeedBoost();
        //}

        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    _playerMovement.playerCamera.GetComponent<Inventory>().InventoryOpen();
        //}

        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    _playerMovement.lounchGranada.Launch();
        //}



        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    Flashlight.FlashlightFunction();
        //}





        //CHEATS

        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    PlayerStats.instance.UsbsCollected++;
        //    CanvasManager.instance.TurnOnCanvas("CanvasUSB");
        //    Debug.Log("CHEAT: te sumaste un usb. Ahora tenes " + PlayerStats.instance.UsbsCollected);
        //}

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    PlayerStats.instance.GetCardKey();
        //    Debug.Log("CHEAT: conseguiste la cardkey");
        //}

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    PlayerStats.instance.GetFlashlight();
        //    Debug.Log("CHEAT: conseguiste la linterna");
        //}

        //if (Input.GetKeyDown(KeyCode.Numlock))
        //{
        //    PlayerStats.instance.SpeedBoosts++;
        //    CanvasManager.instance.TurnOnCanvas("CanvasJeringas");

        //    Debug.Log("CHEAT: conseguiste 1 speedboost");
        //}

        //if (Input.GetKeyDown(KeyCode.Semicolon))
        //{
        //    PlayerStats.instance.Grenades++;
        //    CanvasManager.instance.TurnOnCanvas("CanvasGranadas");
        //    Debug.Log("CHEAT: conseguiste 1 granada");
        //}
    }
}
