using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCreator : MonoBehaviour
{
    public float fireRate = 1f;
    public float range = 15;
    public Camera _cam;
    public GameObject BilledPrefab;
    public float BulletVelosity = 20f;
    private float nextFire = 0f;
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            Shoot();

            
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit, range))
        {
            GameObject newBullet = Instantiate(BilledPrefab, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody>().velocity = _cam.transform.forward * BulletVelosity;
            
        }
    }
}
