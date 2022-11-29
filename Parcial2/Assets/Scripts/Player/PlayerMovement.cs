using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IRalentizable, IMicroGravity, ITransportable
{
    //el movimiento del player. con character controller y a mano
    //llama por composicion a playeranimations y controls
    //por diego katabian, francisco serra, valentino roman, mateo palma, rocio casco.

    [HideInInspector]
    public float playerSpeed;

    public float walkingSpeed;
    public float runningSpeed;
    public float jumpHeight;
    public float jumpHeightOnMicroGravityMultiplier;
    public float jumpHeightOnSlowMultiplier;
    public float gravityValue;          //gravedad extra para que quede linda la caida del salto
    public float gravityValueOnMicroGravity;
    public float speedModifierOnMicroGravity;
    public bool agency = true;
    public float boostSpeedMultiplier;
    public float boostDuration;
    public float boostJumpMultiplier;

    float _verticalVelocity;
    float _speedModifier;
    float _groundedTimer;
    bool _boostOn;
    Vector3 _move;

    float initialGravityValue;

    public CharacterController controller;
    public PlayerAnimations pAnims;
    public Controls controls;
    public Camera playerCamera;
    public float cameraFOVChangeDuration;

    Animator _anim;

    [HideInInspector]
    public LounchGranada lounchGranada;

    void Start()
    {
        if (GetComponent<CharacterController>() != null)
        {
            controller = GetComponent<CharacterController>();
        }

        if (GetComponent<Animator>() != null)
        {
            _anim = GetComponent<Animator>();
        }

        playerSpeed = walkingSpeed;
        _speedModifier = 1;
        initialGravityValue = gravityValue;
        controls = new Controls(this);
        pAnims = new PlayerAnimations(_anim); //construyo scripts x composicion

        PlayerStats.instance.OnDeath += TPToCheckpoint; //ya enterate

        lounchGranada = playerCamera.GetComponent<LounchGranada>();

    }

    void Update()
    {
        controls.CheckControls();

        bool groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            _groundedTimer = 0.2f; //mientras este en el suelo
            pAnims.StopJumping();
            pAnims.StopFalling();
            pAnims.PlayLanding();
        }

        if (_groundedTimer > 0)
        {
            _groundedTimer -= Time.deltaTime;
        }

        if (!groundedPlayer && _verticalVelocity <= -2f) //si esta cayendo pero no tocando el suelo empieza a caer
        {
            pAnims.StopJumping();
            pAnims.PlayFalling();
        }

        if (groundedPlayer && _verticalVelocity <= 0) //corta la caida cuando toco el suelo
        {
            _verticalVelocity = 0f;
            //AudioManager.instance.PlayJumpDown();
        }

        _verticalVelocity -= gravityValue * Time.deltaTime; //aplica gravedad extra
        _move = transform.right * controls.h + transform.forward * controls.v; //cargo mi vector movimiento

        if (_move.magnitude > 1)
        {
            _move = _move.normalized;
        }

        if (controls.isJump)
        {
            if (_groundedTimer > 0)
            {
                //AudioManager.instance.StopPasos();
                //AudioManager.instance.PlayJumpUp();
                _groundedTimer = 0;
                _verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravityValue); //saltar en realidad le da velocidad vertical nomas
                pAnims.PlayJumping();
                pAnims.StopLanding();
                controls.isJump = false;
            }
        }

        _move *= playerSpeed * _speedModifier;
        _move.y = _verticalVelocity; //sigo cargando el vector movieminto
        controller.Move(_move * Time.deltaTime); //aplico el vector movieminto al character controller, con el metodo .Move
        //print("UPDATE: estoy moviendo al player " + _move);

        if (agency)
        {
            pAnims.CheckMagnitude(_move.x + _move.z); //en el script de playerAnimations, chequea si me estoy moviendo o no
        }
    }

    public void Run()
    {
        playerSpeed = runningSpeed;
    }
    public void StopRunning()
    {
        playerSpeed = walkingSpeed;
    }
    void TPToCheckpoint(Vector3 cp)
    {
        //AudioManager.instance.PlayTPToCheckpoint();
        controller.enabled = false; //apago el character controller antes de moverlo
        transform.position = cp;
        controller.enabled = true;
    }
    public void MoveForward()
    {
        _move = transform.forward * playerSpeed * _speedModifier;
        controller.Move(_move * Time.deltaTime);
        pAnims.CheckMagnitude(_move.x + _move.z); //en el script de playerAnimations, chequea si me estoy moviendo o no
    }
    public void EnterSlow()
    {
        if (!_boostOn)
        {
            _speedModifier = 0.5f;
            jumpHeight *= jumpHeightOnSlowMultiplier;
        }
        //AudioManager.instance.TriggerSound(AudioManager.instance.sound["GeigerCounter"], 2, 0, 1, true);
    }
    public void ExitSlow()
    {
        if (!_boostOn)
        {
            _speedModifier = 1;
            jumpHeight *= (1 / jumpHeightOnSlowMultiplier);
        }
        //AudioManager.instance.TriggerSound(AudioManager.instance.sound["GeigerCounter"], 2, 0, 1, false);
    }

    public void EnterMicroGravity()
    {
        gravityValue = gravityValueOnMicroGravity;
        jumpHeight *= jumpHeightOnMicroGravityMultiplier;
        _speedModifier = speedModifierOnMicroGravity;
        //AudioManager.instance.TriggerSound(AudioManager.instance.sound["MicroGravityOn"], 0.5f, 0, 1, true);
    }

    public void ExitMicroGravity()
    {
        gravityValue = initialGravityValue;
        jumpHeight *= (1 / jumpHeightOnMicroGravityMultiplier);
        _speedModifier = 1;
        //AudioManager.instance.TriggerSound(AudioManager.instance.sound["MicroGravityOff"], 0.5f, 0, 1, true);
    }

    public void StartSpeedBoost()
    {
        if (PlayerStats.instance.SpeedBoosts > 0)
        {
            if (!_boostOn)
            {
                print("consumiste un speedboost.");
                PlayerStats.instance.SpeedBoosts--;
                CanvasManager.instance.jeringaActiveIcon.SetActive(true);
                StartCoroutine("SpeedBoost", boostDuration);
            }
            else
            {
                print("bancala un toque, ya tenes puesto el boost");
            }
        }
        else
        {
            print("no tenes speedboost para consumir");
        }
    }

    public IEnumerator SpeedBoost(float boostDuration)
    {
        float _timer = 0;
        float _initialFOV = playerCamera.fieldOfView;

        jumpHeight *= boostJumpMultiplier;
        _boostOn = true;
        //AudioManager.instance.PlayBoostOn();

        while (_timer < cameraFOVChangeDuration)
        {
            _timer += (Time.deltaTime / cameraFOVChangeDuration);
            playerCamera.fieldOfView = Mathf.Lerp(_initialFOV, _initialFOV + 60, _timer);
            _speedModifier = Mathf.Lerp(1, boostSpeedMultiplier, _timer);

            yield return null;
        }

        yield return new WaitForSeconds(boostDuration);
        _timer = 0;

        AudioManager.instance.PlayBoostOff();
        while (_timer < cameraFOVChangeDuration)
        {
            _timer += (Time.deltaTime / cameraFOVChangeDuration);
            playerCamera.fieldOfView = Mathf.Lerp(_initialFOV + 60, _initialFOV, _timer);
            _speedModifier = Mathf.Lerp(boostSpeedMultiplier, 1, _timer);

            //print(_timer);
            yield return null;
        }

        jumpHeight *= (1 / boostJumpMultiplier);
        _boostOn = false;
        CanvasManager.instance.jeringaActiveIcon.SetActive(false);

    }

    public void Transport(Vector3 v)
    {
        //print("PLAYERMOVEMENT: dispare transport " + v);
        //_move += v;
        controller.Move(v * Time.deltaTime * 0.5f);
    }

    public void EnterPlatform()
    {
        //entre a la platform
    }

    public void ExitPlatform()
    {
        //sali de la platform
    }
}
