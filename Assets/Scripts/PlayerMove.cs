using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //private Animator animator;

    [SerializeField] float MoveSpeed = 1f;
    [SerializeField] float MoveRunSpeed = 2f;
    [SerializeField] float jumpPower = 200f;
    public bool ground;
    public Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    { 

        //animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        var xMov = Input.GetAxis("Horizontal");
        var yMov = Input.GetAxis("Vertical");
        Vector3 _vector = new Vector3(xMov, 0, yMov);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(_vector * MoveRunSpeed * Time.deltaTime);
        }
        transform.Translate(_vector * MoveSpeed * Time.deltaTime);
        
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

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            ground = true;
            
        }
    }
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        ground = false;
            

    //    }
    //}

}
