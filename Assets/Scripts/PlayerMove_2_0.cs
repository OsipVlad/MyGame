using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_2_0 : MonoBehaviour
{
    public float speed_Move;
    public float speed_Run;
    public float speed_Current;
    public float jump;
    public float gravity = 1;

    float x_Move;
    float z_Move;
    CharacterController player;
    Vector3 move_Direction;

    void Start()
    {
        player = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    void Move()
    {
        x_Move = Input.GetAxis("Horizontal");
        z_Move = Input.GetAxis("Vertical");

        if (player.isGrounded)
        {
            move_Direction = new Vector3(x_Move, 0f, z_Move);
            move_Direction = transform.TransformDirection(move_Direction);
            if (Input.GetKey(KeyCode.Space))
            {
                move_Direction.y += jump;
            }
        }
        move_Direction.y -= gravity;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed_Current = speed_Run;
        }
        else
        {
            speed_Current = speed_Move;
        }
        player.Move(move_Direction * speed_Current * Time.deltaTime);
        move_Direction = Vector3.zero;
    }
}
