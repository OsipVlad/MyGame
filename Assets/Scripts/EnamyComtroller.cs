using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnamyComtroller : MonoBehaviour
{
    [SerializeField] Transform PatrolPost;
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
    void Update()
    {
        _agent.destination = PatrolPost.position;
    }
}
