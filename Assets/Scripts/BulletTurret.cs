using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class BulletTurret : MonoBehaviour
{

    [SerializeField] private float damage = 10;
    [SerializeField] private float bulledSpeed = 5;
    private LayerMask layer;
    private bool isActive = true;

    public void SetBullet(LayerMask layerMask, Vector3 direction)
    {
        layer = layerMask;
        Rigidbody body = GetComponent<Rigidbody>();
        body.useGravity = false;
        body.velocity = direction * bulledSpeed;
        transform.forward = direction;
    }

 

    private void OnCollisionEnter(Collision collision)
    {
        if (!isActive) return;
        isActive = false;

        
        UnitHP unitHP = collision.gameObject.GetComponent<UnitHP>();
        Destroy(gameObject);
        if (unitHP != null)
        {
            unitHP.Adjust(damage);
            Debug.Log(collision.gameObject.name);
            Debug.Log(unitHP.health);
            
        }
     
    }

}
