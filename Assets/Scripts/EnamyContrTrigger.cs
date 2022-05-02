using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnamyContrTrigger : MonoBehaviour
{
    [SerializeField] Transform PlayerPos;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _agent.destination = PlayerPos.position;
            GetComponent<SphereCollider>().enabled = false;
        }
    }
    void Update()
    {
        
    }
}
