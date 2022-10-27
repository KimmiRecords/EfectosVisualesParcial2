using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelTrigger : MonoBehaviour
{
    //este script se lo adjuntas a un trigger y te lleva a otro nivel
    //por diego katabian

    public string nextLevel;
    public ScenePreLoader spl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) //player
        {
            StatsManager.instance.SaveStats();

            if (spl != null)
            {
                print("habilite el sceneactivation del scenepreloader");
                spl.asyncLoad.allowSceneActivation = true;
            }
            else
            {
                print("cargue la nueva escena de 0");
                SceneManager.LoadScene(nextLevel);
            }
        }
    }
}
