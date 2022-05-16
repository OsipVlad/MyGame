using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvalatorController : MonoBehaviour
{
    private Animator animator;
    public AudioClip _StartGo;
    public AudioClip _FinalGo;
    public AudioSource _audioSource;

    void Start()
    {

        animator = GetComponent<Animator>();
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.F))
        {
            //_audioSource.PlayOneShot(_FinalGo);
            animator.SetBool("Up_Down", !animator.GetBool("Up_Down"));
            
            //_audioSource.PlayOneShot(_StartGo);
        }
    }

}
