using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    //todo lo pertinente a sonidos y sus metodos
    //por diego katabian

    public static AudioManager instance;

    //public FinalUSB finalUsb;

    AudioSource[] allSounds;
    public bool isRunning;

    float volumenDeseadoScreamer;
    bool jumpDownIsReady;

    int _explosionCycleIndex = 0;
    int _tosCycleIndex = 0;

    IEnumerator fadeOutScreamerCoroutine;

    public string thisLevelBgm;

    [HideInInspector]
    public Dictionary<string, AudioSource> sound = new Dictionary<string, AudioSource>();

    void Awake()
    {
        if (instance) //esto es para que audiomanager sea unico. puse uno en cada escena, pero a traves de las escenas se mantiene vivo uno solo.
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);

        allSounds = GetComponentsInChildren<AudioSource>(); //construyo mi array con todos los audiosource

        for (int i = 0; i < allSounds.Length; i++) //lleno el diccionario de pares string-audiosource
        {
            string s = allSounds[i].ToString(); //convierto a string
            s = s.Substring(0, s.Length - 26); //formateo para que no me quede (UnityEngine.AudioSource) en cada nombre
            sound.Add(s, allSounds[i]);
            //print("agregue el key " + s + " con value " + allSounds[i]+ " al diccionario");
        }

        volumenDeseadoScreamer = sound["Screamer1SFX"].volume; //ojo, esto significa que los 2 screamers tendran el mismo volumen

    }

    public void PlayByName(string clipName) //el mas groso. le das el string y te da play a ese audio. muy global y sencillo.
    {
        AudioSource sound;
        sound = this.sound[clipName];
        sound.Play();
    }
    public void StopByName(string clipName)
    {
        AudioSource sound;
        sound = this.sound[clipName];
        sound.Stop();
    }
    public void PlayByNamePitch(string clipName, float pitch)
    {
        AudioSource sound;
        sound = this.sound[clipName];
        sound.pitch = pitch;
        sound.Play();
    }

    //BACKGROUNDMUSIC
    public void PlayBGM()
    {
        print("reproduje el sonido " + thisLevelBgm);
        sound[thisLevelBgm].Play();
    }
    public void StopBGM()
    {
        sound[thisLevelBgm].Stop();
    }
    public void FadeInBGM(float fadetime)
    {
        float timer = Time.time / fadetime;
        sound[thisLevelBgm].volume = Mathf.Lerp(0, 1, timer);
    }
    public void FadeOutBGM(float fadetime)
    {
        float timer = Time.time / fadetime;
        sound[thisLevelBgm].volume = Mathf.Lerp(1, 0, timer);
    }

    //MAIN MENU MUSIC
    public void PlayMainMenuMusic()
    {
        sound["MainMenuMusic"].Play();
    }
    public void StopMainMenuMusic()
    {
        sound["MainMenuMusic"].Stop();
    }

    //ALARMAS
    public void PlayAlarmaNorway()
    {
        sound["AlarmaNorway"].Play();
    }
    public void StopAlarmaNorway()
    {
        sound["AlarmaNorway"].Stop();
    }
    public void PlayAlarmaTriple()
    {
        sound["AlarmaTriple"].Play();
    }
    public void StopAlarmaTriple()
    {
        sound["AlarmaTriple"].Stop();
    }
    public void TurnOnFinalAlarm()
    {
        StopByName("AlarmaNorway");
        PlayByName("AlarmaTriple");
        PlayByName("SelfDestructionSequenceInitiated");
        PlayByName("StunnedSFX");

    }


    //SFX
    public void PlayDerrumbe()
    {
        switch (_explosionCycleIndex)
        {
            case 0:
                sound["Derrumbe01"].Play();
                break;

            case 1:
                sound["Derrumbe02"].Play();
                break;

            case 2:
                sound["Derrumbe03"].Play();
                break;

            default:
                break;
        }

        _explosionCycleIndex = (_explosionCycleIndex + 1) % 3;
    }
    public void PlayTos()
    {
        bool anyTosIsPlaying = false;

        if (sound["Tos0"].isPlaying || sound["Tos1"].isPlaying ||
            sound["Tos2"].isPlaying || sound["Tos3"].isPlaying)
        {
            anyTosIsPlaying = true;
        }

        if (!anyTosIsPlaying)
        {
            float randomPitch = Random.Range(0.9f, 1.1f);

            switch (_tosCycleIndex)
            {
                case 0:
                    sound["Tos0"].pitch = randomPitch;
                    sound["Tos0"].Play();
                    break;

                case 1:
                    sound["Tos1"].pitch = randomPitch;
                    sound["Tos1"].Play();
                    break;

                case 2:
                    sound["Tos2"].pitch = randomPitch;
                    sound["Tos2"].Play();
                    break;

                case 3:
                    sound["Tos3"].pitch = randomPitch;
                    sound["Tos3"].Play();
                    break;

                default:
                    break;
            }
            _tosCycleIndex = (_tosCycleIndex + 1) % 4;
        }
    }
  

    public void PlayHollowRoar(Vector3 pos, float delayTime, float p)
    {
        if (!sound["HollowRoarSFX"].isPlaying)
        {
            sound["HollowRoarSFX"].pitch = p;
            sound["HollowRoarSFX"].gameObject.transform.position = pos; //muevo al audiosource
            sound["HollowRoarSFX"].PlayDelayed(delayTime);
        }
    }
    public void PlayPickup(float p)
    {
        sound["PickupSFX"].pitch = p;
        sound["PickupSFX"].Play();
    }
    public void PlayTPToCheckpoint()
    {
        sound["TPToCheckpoint"].Play();
    }
    public void PlayAccessDenied()
    {
        sound["AccessDeniedSFX"].Play();
    }
    public void PlayHeavyBreathing()
    {
        float randomPitch = Random.Range(0.95f, 1.05f);
        if (!sound["HeavyBreathing"].isPlaying)
        {
            sound["HeavyBreathing"].volume = 0.8f;
            sound["HeavyBreathing"].pitch = randomPitch;
            sound["HeavyBreathing"].Play();
            StartCoroutine(FadeAudioSource.StartFade(sound["HeavyBreathing"], 15, 0.8f, 0));
        }
    }

    //PRESSURE PLATE SFX
    public void PlayPPlateOn(Vector3 pos)
    {
        sound["PPlateOn"].gameObject.transform.position = pos; //muevo al audiosource
        sound["PPlateOn"].Play();
    }
    public void PlayPPlateOff(Vector3 pos)
    {
        sound["PPlateOff"].gameObject.transform.position = pos;
        sound["PPlateOff"].Play();
    }
    public void PlayBigLightSwitch()
    {
        float randomPitch = Random.Range(0.9f, 1.1f);
        sound["BigLightSwitchSFX"].pitch = randomPitch;
        sound["BigLightSwitchSFX"].Play();
    }

    //PUERTAS
    public void PlayDoorOpen(Vector3 pos)
    {
        float randomPitch = Random.Range(0.95f, 1.05f);
        sound["DoorOpenSFX"].pitch = randomPitch;
        sound["DoorOpenSFX"].gameObject.transform.position = pos;
        sound["DoorOpenSFX"].Play();
    }
    public void PlayDoorClose(Vector3 pos)
    {
        float randomPitch = Random.Range(0.95f, 1.05f);
        sound["DoorCloseSFX"].pitch = randomPitch;
        sound["DoorCloseSFX"].gameObject.transform.position = pos;
        sound["DoorCloseSFX"].Play();
    }

    //SCREAMER
    public void PlayScreamer(int screamerID)
    {
        sound["Screamer1SFX"].Stop();
        sound["Screamer2SFX"].Stop();
        switch (screamerID)
        {
            case 1:
                sound["Screamer1SFX"].volume = volumenDeseadoScreamer;   //para resetear el volumen en caso de que otro metodo lo haya alterado
                sound["Screamer1SFX"].Play();
                break;

            case 2:
                sound["Screamer2SFX"].volume = volumenDeseadoScreamer;   //para resetear el volumen en caso de que otro metodo lo haya alterado
                sound["Screamer2SFX"].Play();
                break;

            default:
                break;
        }
    }
    public void FadeOutScreamer(int screamerID, float fadetime)
    {

        fadeOutScreamerCoroutine = FadeOutScreamerCoroutine(screamerID, fadetime);

        StartCoroutine(fadeOutScreamerCoroutine);
    }

    public IEnumerator FadeOutScreamerCoroutine(int screamerID, float fadetime)
    {
        float timer = 0;
        string screamerName = "";

        switch (screamerID)
        {
            case 1:
                screamerName = "Screamer1SFX";
                break;

            case 2:
                screamerName = "Screamer2SFX";
                break;

            default:
                break;
        }

        while (sound[screamerName].volume > 0)
        {
            timer += Time.time / fadetime;
            sound[screamerName].volume = Mathf.Lerp(1, 0, timer);
            yield return null;
        }
    }
    public void StopScreamer(int screamerID)
    {
        switch (screamerID)
        {
            case 1:
                sound["Screamer1SFX"].Stop();
                break;

            case 2:
                sound["Screamer2SFX"].Stop();
                break;

            default:
                break;
        }
    }

    //LINTERNA ON/OFF
    public void PlayLinternaOn()
    {
        sound["LinternaOnSFX"].Play();
    }
    public void PlayLinternaOff()
    {
        sound["LinternaOffSFX"].Play();
    }

    //PASOS
    public void PlayPasos()
    {
        if (!isRunning)
        {
            if (sound["Pasos1"].isPlaying == false && sound["Pasos2"].isPlaying == false) //solo da play si no estaba sonando ya
            {
                float randomPitch = Random.Range(0.95f, 1.05f); // para un poquito de variedad
                int randomClip = Random.Range(0, 2); // 50/50 chances de reproducir uno o el otro
                if (randomClip == 0)
                {
                    sound["Pasos1"].pitch = randomPitch;
                    sound["Pasos1"].Play();
                }
                else
                {
                    sound["Pasos2"].pitch = randomPitch;
                    sound["Pasos2"].Play();
                }
            }
        }
        else
        {
            if (sound["Pasos1"].isPlaying == false && sound["Pasos2"].isPlaying == false) //solo da play si no estaba sonando ya
            {
                float randomPitch = Random.Range(1.1f, 1.2f); // para un poquito de variedad
                int randomClip = Random.Range(0, 2); // 50/50 chances de reproducir uno o el otro
                if (randomClip == 0)
                {
                    sound["Pasos1"].pitch = randomPitch;
                    sound["Pasos1"].Play();
                }
                else
                {
                    sound["Pasos2"].pitch = randomPitch;
                    sound["Pasos2"].Play();
                }
            }
        }
    }
    public void StopPasos()
    {
        sound["Pasos1"].Stop();
        sound["Pasos2"].Stop();
    }
    public void ChangePitchPasos(bool keyDown) //asi cuando corres suenan distinto
    {
        if (keyDown)
        {
            float randomPitch = Random.Range(1.1f, 1.2f);
            sound["Pasos1"].pitch = randomPitch;
            sound["Pasos2"].pitch = randomPitch;
        }
        else
        {
            float randomPitch = Random.Range(0.95f, 1.05f);
            sound["Pasos1"].pitch = randomPitch;
            sound["Pasos2"].pitch = randomPitch;
        }
    }

    //SALTO
    public void PlayJumpUp()
    {
        if (!sound["JumpUpSFX"].isPlaying)
        {
            float randomPitch = Random.Range(0.95f, 1.05f);
            sound["JumpUpSFX"].pitch = randomPitch;
            sound["JumpUpSFX"].Play();
            jumpDownIsReady = true;
        }
    }
    public void PlayJumpDown()
    {
        if (jumpDownIsReady)
        {
            if (!sound["JumpDownSFX"].isPlaying)
            {
                float randomPitch = Random.Range(0.95f, 1.05f);
                sound["JumpDownSFX"].pitch = randomPitch;
                sound["JumpDownSFX"].Play();
                jumpDownIsReady = false;
            }
        }
    }

    //SPEED BOOST

    public void PlayBoostOn()
    {
        sound["BoostOn"].Play();
    }
    public void PlayBoostOff()
    {
        sound["BoostOff"].Play();
    }
    
    //ZOMBIE EXPLOSIVO
    public void PlayZExplosion(Vector3 position)
    {
        sound["ExplosionBoomer"].transform.position = position;
        sound["ExplosionBoomer"].Play();
    }
    public void PlayZIdle()
    {
        sound["ZombieIdleSFX"].Play();
    }
    public void StopZIdle()
    {
        sound["ZombieIdleSFX"].Stop();
    }
    public void PlayZRun()
    {
        sound["ZombieRun"].Play();
    }
    public void StopZRun()
    {
        sound["ZombieRun"].Stop();
    }
    public void PlayZPainScream()
    {
        sound["ZombiePainScream"].Play();
    }
    public void StopZPainScream()
    {
        sound["ZombiePainScream"].Stop();
    }

    //INVENTORY
    public void PlayInventoryOpen()
    {
        sound["InventoryOpen"].Play();
    }
    public void PlayInventoryClose()
    {
        sound["InventoryClose"].Play();
    }

    //OTROS
    public void TriggerSound(AudioSource auso, float fadeDuration, float initialVolume, float finalVolume, bool isPlay)
    {
        if (isPlay)
        {
            auso.Play();
            StartCoroutine(FadeAudioSource.StartFade(auso, fadeDuration, initialVolume, finalVolume));
        }
        else
        {
            auso.Stop();
        }
    }
    public void PlayAtMoment(AudioSource sound, float moment)
    {
        sound.time = moment;
        sound.Play();
    }
    public void StopAll()
    {
        for (int i = 0; i < allSounds.Length; i++)
        {
            if (allSounds[i].isPlaying)
            {
                allSounds[i].Stop();
            }
        }
    }
}
