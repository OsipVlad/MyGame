using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{

    [SerializeField] float distance;
    RaycastHit hit;

    // Start is called before the first frame update
    

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F))
        {
            if(Physics.Raycast(transform.position, transform.forward,out hit, distance))
            {
                if(hit.transform.tag == "AnimatedDoor")
                {
                    Animator anim = hit.transform.GetComponent<Animator>();
                    anim.SetBool("Open", anim.GetBool("Open"));
                }
            }
        }
    }
}
