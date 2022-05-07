using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool _isOpened; 
    [SerializeField] private Animator _animator;
    // Start is called before the first frame update



    void Start()
    {

        _animator = GetComponent<Animator>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                _animator.SetBool("isOpened", _isOpened);
                _isOpened = !_isOpened;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetBool("isOpened", _isOpened);
            _isOpened = !_isOpened;
        }
    }
}
