using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Weapon : MonoBehaviour
{
    public float damage = 20;
    public float fireRate = 1;
    public float range = 15;
    public float force = 155;
    public ParticleSystem muzzleFlash;
    public AudioClip shotSFX;
    public AudioSource _audioSource;
    public Camera _cam;
    public Transform bulletSpawn;
    private float nextFire = 0;
    //public GameObject hitEffect, hitEffect_2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            Shoot();
        }

    }

    void Shoot()
    {


        _audioSource.PlayOneShot(shotSFX);
        muzzleFlash.Play();
        //Instantiate(muzzleFlash, bulletSpawn.position, bulletSpawn.rotation);
        RaycastHit hit;
        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit, range))
        {

            if (hit.rigidbody != null)
            {
                //GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                //Destroy(impact, 2f);

                hit.rigidbody.AddForce(-hit.normal * force);
                UnitHP unitHP = hit.collider.GetComponent<UnitHP>();
                unitHP.Adjust(damage);

                Debug.Log(unitHP.health);
            }
            //else
            //{
                //GameObject impact_2 = Instantiate(hitEffect_2, hit.point, Quaternion.LookRotation(hit.normal));
                //Destroy(impact_2, 2f);
            //}
        }
    }
}
