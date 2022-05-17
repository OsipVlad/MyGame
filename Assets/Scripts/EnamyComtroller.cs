using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnamyComtroller : MonoBehaviour
{
    [SerializeField] Transform PatrolPost;
    private NavMeshAgent _agent;
    public AudioClip shotSFX;
    public AudioSource _audioSource;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {

    }

    void Update()
    {
    
        _agent.destination = PatrolPost.position;
    }
}
