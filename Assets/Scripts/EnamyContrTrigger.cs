using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnamyContrTrigger : MonoBehaviour
{
    [SerializeField] Transform PlayerPos;
    private NavMeshAgent _agent;
    public AudioClip shotSFX;
    public AudioSource _audioSource;
    public float growlRate = 1;
    private float nextGrowl = 0;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            _agent.destination = PlayerPos.position;
            while (Time.time > nextGrowl)
            {

                nextGrowl = Time.time + 1f / growlRate;
                _audioSource.PlayOneShot(shotSFX);
            }

            GetComponent<SphereCollider>().enabled = false;
        }
    }

   
}
