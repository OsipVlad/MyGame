using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class TurretController : MonoBehaviour
{
    [SerializeField] Transform AmmonHead;
    [SerializeField] float VisionRadius;

    [SerializeField] float CooldownTime;
    [SerializeField] Transform Joint;
    [SerializeField] Transform TestObject;
    [SerializeField] float RotationSpeed;
    [SerializeField] GameObject BulletTurrentPrefab;

    private SphereCollider _visionCollider;
    //private Transform _playerPos;
    private float _currentTime;

    private void Awake()
    {
        _visionCollider = GetComponent<SphereCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        VisionRadius = _visionCollider.radius;
        _currentTime = CooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(TestObject != null)
        {
            _currentTime += Time.deltaTime;
            var rotDirection = TestObject.position - Joint.position;
            //Joint.rotation = Quaternion.LookRotation(rotDirection.normalized);

            var targetRotation = Quaternion.LookRotation(rotDirection.normalized);
            Joint.rotation = Quaternion.Lerp(Joint.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TestObject = other.gameObject.transform;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && _currentTime >= CooldownTime)
        {
            var bullet = Instantiate(BulletTurrentPrefab, AmmonHead.position, transform.rotation);
            bullet.GetComponent<BulletTurret>().TargetPos = TestObject.position;
            _currentTime = 0;
        }
    }


}
