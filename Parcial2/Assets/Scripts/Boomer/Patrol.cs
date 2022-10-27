using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Patrol : MonoBehaviour, IRalentizable, IExplotable
{
    //este es el script general del boomer. 
    //va del punto 0 al 1 (pingpong)
    //si el player entra en rango, va hacia el punto 2 y explota a mitad de camino
    //TP2 - Francisco Serra y Diego Katabian

    public NavMeshAgent miNavMeshAgent;
    public DetectPlayer detectPlayer;
    public Animator miAnimator;
    public Transform[] points;
    public float runningSpeed;
    public float timeUntilExplosionMin;
    public float timeUntilExplosionMax;

    public GameObject explosionGameObject;
    public GameObject boomerModel;

    public AudioSource walk;
    public AudioSource run;
    public AudioSource scream;

    [HideInInspector]
    public int index;

    BoomerAnimations _boomerAnims;
    BoomerSounds _boomerSounds;
    float _timeUntilStop;
    float _speedModifier;
    float _desiredPainAnimationTime = 2;
    float _initialNavMeshAgentSpeed;
    bool _yaViAlPlayer;

    void Start()
    {
        _boomerAnims = new BoomerAnimations(miAnimator);
        _boomerSounds = new BoomerSounds(this);
        _yaViAlPlayer = false;
        _initialNavMeshAgentSpeed = miNavMeshAgent.speed;

        index = 0;
        GoToPoint(points[index]);
        Walk();
    }

    void Update()
    {
        _boomerSounds.UpdateSoundsPosition();

        if (miNavMeshAgent.remainingDistance < 1 && index != 2) //si no estoy yendo al punto 2, ciclo entre punto 0 y 1
        {
            //index = (index + 1) % points.Length;
            index = 1 - index;
            GoToPoint(points[index]);
        }

        //if (miNavMeshAgent.remainingDistance < 1 && index == 2) //cuando llego al punto 2, me quedo quieto
        //{
        //    miNavMeshAgent.speed = 0;
        //}

        if (!_yaViAlPlayer && detectPlayer.playerIsInRange) //veo al player y corro hacia punto 2
        {
            walk.Stop();
            run.Play();

            _boomerAnims.StartRunning();
            miNavMeshAgent.speed = runningSpeed * _speedModifier;
            index = 2;
            GoToPoint(points[index]);
            Invoke("Stop", _timeUntilStop);
            Invoke("Explode", _timeUntilStop + _desiredPainAnimationTime);

            _yaViAlPlayer = true;
        }
    }

    public void GoToPoint(Transform point)
    {
        miNavMeshAgent.destination = point.position;
        //print("GO TO POINT " + point.position);
    }

    public void Walk()
    {
        _timeUntilStop = Random.Range(timeUntilExplosionMin, timeUntilExplosionMax);
        _speedModifier = 1;

        miNavMeshAgent.speed = _initialNavMeshAgentSpeed;

        walk.Play();
        _boomerAnims.StartWalking();
    }

    public void Stop()
    {
        miNavMeshAgent.speed = 0;

        run.Stop();
        scream.Play();
        _boomerAnims.StartPain();
    }

    public void Explode()
    {
        scream.Stop();
        AudioManager.instance.PlayZExplosion(transform.position);

        GameObject _exp = Instantiate(explosionGameObject, boomerModel.transform.position, boomerModel.transform.rotation);
        Destroy(_exp, 3);

        if (detectPlayer.playerIsInRange)
        {
            PlayerStats.instance.InstaDeath();
            Invoke("Reset", 2);

        }
        else
        {
            this.gameObject.SetActive(false);
        }

        //Invoke("Reset", 2);
    }

    public void Reset()
    {
        index = 0;
        GoToPoint(points[index]);

        Walk();
        _yaViAlPlayer = false;
    }

    public void EnterSlow()
    {
        _speedModifier = 0.5f;
        miNavMeshAgent.speed *= 0.5f;
    }

    public void ExitSlow()
    {
        _speedModifier = 1;
        miNavMeshAgent.speed *= 2;
    }

    public void ReceiveExplosion()
    {
        Explode();
    }
}
