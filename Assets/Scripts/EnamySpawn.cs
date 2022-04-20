using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamySpawn : MonoBehaviour
{
    public GameObject prefab;
    public float TimeStep;
    
    public GameObject spawner;

    Vector3 whereToSpawn;

    IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(TimeStep);
        whereToSpawn = spawner.transform.position;

        Instantiate(prefab,whereToSpawn,Quaternion.identity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SpawnObjects());
            GetComponent<SphereCollider>().enabled = false;
        }
    }

}
