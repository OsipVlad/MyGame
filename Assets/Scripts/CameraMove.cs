using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform Playerbody;
    [SerializeField] float MouseSensetiv = 1;
    float y = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var xRot = Input.GetAxis("Mouse X"); 
        var yRot = Input.GetAxis("Mouse Y");

        y -= yRot;
        y = Mathf.Clamp(y, -80, 70);
        
        Playerbody.Rotate(new Vector3(0, xRot * MouseSensetiv, 0));
        transform.localRotation = Quaternion.Euler(y * MouseSensetiv, 0, 0);
        
    }
}
