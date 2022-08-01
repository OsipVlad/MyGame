using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animation anim = GetComponent<Animation>();
        anim.Play("Light");
       
    }

   
}
