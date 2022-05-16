using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //private Animator animator;

    [SerializeField] float MoveSpeed = 1f;
    [SerializeField] float MoveRunSpeed = 2f;
    [SerializeField] float jumpPower = 200f;

    public bool ground = true;
    public Rigidbody rb;

    private CapsuleCollider _player;
    void Start()
    {
        _player = GetComponent<CapsuleCollider>();
       
        //animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        var xMov = Input.GetAxis("Horizontal");
        var yMov = Input.GetAxis("Vertical");
        Vector3 _vector = new Vector3(xMov, 0, yMov);
        transform.Translate(_vector * MoveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(_vector * MoveRunSpeed * Time.deltaTime);
        }
        
        
        //animator.SetFloat("speed", Vector3.ClampMagnitude(_vector, 1).magnitude);
        _vector = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(ground == true)
            {
                rb.AddForce(transform.up * jumpPower);
                ground = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(ground == true)
            {
                _player.height = 1.4f;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (ground == true)
            {
                _player.height = 1.8f;
            }
        }


    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            ground = true;
            
        }
    }
}
