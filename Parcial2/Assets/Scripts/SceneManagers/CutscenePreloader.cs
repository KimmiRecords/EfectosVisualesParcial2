using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutscenePreloader : MonoBehaviour
{
    public string cutsceneName;
    public FinalUSB finalUsb; //el item cuyo interact dispara mi startasycloader
    public Image black;

    [HideInInspector]
    public AsyncOperation asyncLoad;

    private void Start()
    {
        if (finalUsb != null)
        {
            finalUsb.OnFinalUSBPickup += StartAsyncLoader;
        }
    }
    public void StartAsyncLoader()
    {
        StartCoroutine("AsyncLoader");
    }

    IEnumerator AsyncLoader()
    {
        black.color = new Color(0, 0, 0, 1);
        AudioManager.instance.PlayDerrumbe();

        yield return new WaitForSeconds(0.25f);

        asyncLoad = SceneManager.LoadSceneAsync(cutsceneName, LoadSceneMode.Single);
        print("arranque el loadSceneAsync de " + cutsceneName);
        asyncLoad.allowSceneActivation = false;

        yield return new WaitForSeconds(0.25f);

        black.color = new Color(0, 0, 0, 0);
    }
}
