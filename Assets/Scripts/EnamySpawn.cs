using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnamySpawn : MonoBehaviour
{
    [SerializeField] Transform PatrolPost;
    private NavMeshAgent _agent;
    public GameObject prefab;
    public float TimeStep;
    
    public GameObject spawner;

    Vector3 whereToSpawn;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
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
            _agent.destination = PatrolPost.position;

            GetComponent<SphereCollider>().enabled = false;
            //StopCoroutine(SpawnObjects());
        }
    }

}
