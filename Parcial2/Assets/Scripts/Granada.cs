using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granada : MonoBehaviour
{
    public GameObject explosionEffect;
    public float delay = 3;

    public float explosionForce = 10f;
    public float radius = 20f;

    public LayerMask objetosExplotablesLayerMask;

    void Start()
    {
        Invoke("Explode", delay);
    }
    private void Explode()
    {
        //Se fija los coliders sercanos;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, objetosExplotablesLayerMask);

        //Aplica una fuerza;
        foreach (Collider near in colliders)
        {
            Rigidbody rig = near.GetComponent<Rigidbody>();

            if (rig != null)
            rig.AddExplosionForce(explosionForce, transform.position, radius, 1f, ForceMode.Impulse);
        }


        Collider[] collidersIExplotable = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider near in collidersIExplotable)
        {
            if (near.GetComponent<IExplotable>() != null)
            {
                var elotro = near.GetComponent<IExplotable>();
                elotro.ReceiveExplosion();
            }
        }


        //Explosion effect
        GameObject _exp = Instantiate(explosionEffect, transform.position, transform.rotation);
        AudioManager.instance.PlayZExplosion(_exp.transform.position);
        Destroy(_exp, 2);

        gameObject.SetActive(false);
        Destroy(gameObject, 2.1f);
    }
}
