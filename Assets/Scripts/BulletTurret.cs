using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class BulletTurret : MonoBehaviour
{
    [SerializeField] private float BulletSpeed;
    
    public Vector3 TargetPos
    {
        get => _targetPos;
        set
        {
            _isShooted = true;
            _targetPos = value;
        }

    }
    private bool _isShooted = false;
    private Vector3 _targetPos;

    // Update is called once per frame
    void Update()
    {
        if (_isShooted)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, BulletSpeed * Time.deltaTime);
           
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

}
