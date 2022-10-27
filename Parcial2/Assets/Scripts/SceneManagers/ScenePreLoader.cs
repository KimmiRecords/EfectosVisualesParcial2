using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenePreLoader : MonoBehaviour
{
    //cuando pasas por este trigger, empieza a cargar async la escena seleccionada 
    //por diego katabian

    public string sceneName;
    //public Image black;

    [HideInInspector]
    public AsyncOperation asyncLoad;

    bool arranco = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            StartAsyncLoader();
        }
    }

    public void StartAsyncLoader()
    {
        if (!arranco)
        {
            StartCoroutine("AsyncLoader");
        }
    }

    IEnumerator AsyncLoader()
    {
        arranco = true;
        yield return new WaitForSeconds(0.02f);

        asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        print("arranque el loadSceneAsync de " + sceneName);
        asyncLoad.allowSceneActivation = false;

        yield return new WaitForSeconds(0.02f);
    }

}
