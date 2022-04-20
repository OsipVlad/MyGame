using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator animator;

    [SerializeField] float MoveSpeed = 1;
    
    
    // Start is called before the first frame update
    void Start()
    { 

        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        var xMov = Input.GetAxis("Horizontal");
        var yMov = Input.GetAxis("Vertical");
        Vector3 _vector = new Vector3(xMov, 0, yMov);
        transform.Translate(_vector * MoveSpeed * Time.deltaTime, Camera.main.transform);
        _vector = Vector3.zero;
        animator.SetFloat("speed", Vector3.ClampMagnitude(_vector, 1).magnitude);
        

        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    void Jump()
    {
 
    }
}
