using UnityEngine;

public class EvalatorController : MonoBehaviour
{
    private Animator animator;
    public AudioClip _StartGo;
    public AudioSource _audioSource;

    void Start()
    {

        animator = GetComponent<Animator>();
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            
            animator.SetBool("Up_Down", !animator.GetBool("Up_Down"));
            
            _audioSource.PlayOneShot(_StartGo);
        }
    }

}
