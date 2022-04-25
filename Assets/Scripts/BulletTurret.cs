using System.Collections; 
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class BulletTurret : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float bulledSpeed = 5;
    private LayerMask layer;

    public void SetBullet(LayerMask layerMask, Vector3 direction)
    {
        layer = layerMask;
        Rigidbody body = GetComponent<Rigidbody>();
        body.useGravity = false;
        body.velocity = direction * bulledSpeed;
        transform.forward = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            if(((1 << other.gameObject.layer) & layer) != 0)
            {
                other.GetComponent<UnitHP>().Adjust(-damage);
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

}
