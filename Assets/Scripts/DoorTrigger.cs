using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Animator animator;
    public AudioClip _openDoor;
    public AudioClip _closeDoor;
    public AudioSource _audioSource;
    void Start()
    {

        animator = GetComponent<Animator>();

    }
    
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.F))
        {
            
            animator.SetBool("Open", true);
            new WaitForSecondsRealtime(1);
            _audioSource.PlayOneShot(_openDoor);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        

        animator.SetBool("Open", false);
        new WaitForSecondsRealtime(1);
        _audioSource.PlayOneShot(_closeDoor);
    }
}
    
