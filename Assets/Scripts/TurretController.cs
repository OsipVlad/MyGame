using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class TurretController : MonoBehaviour
{
    [SerializeField] private BulletTurret bulletPrefab;//префаб снаряда, если его нет, стреляем лучем
    [SerializeField] private float fireRate = 1;//скорострельность
    [SerializeField] private float smooth = 1;//сглаживание движение башни
    [SerializeField] private float rayOffset = 1;//поисковый луч, немного больше области видимости
    [SerializeField] private float damage = 10;//повреждение(при стрельбе лучем)
    [SerializeField] private Transform[] bulletPoint;//точки, откуда ведется стрельба
    [SerializeField] Transform turretRotation;//обьект вращения, башня турели
    [SerializeField] private Transform center;//центр между пушками, для поискового луча
    [SerializeField] private LayerMask layerMask;//фильтр коллайдера по маске слоя
    [Header("Лимиты по осям башни: ")]
    [SerializeField] private bool useLimits;
    [SerializeField][Range(0, 180)] private float limitX = 50;
    [SerializeField][Range(0, 180)] private float limitY = 30;
    private SphereCollider turretTrigger;
    private Transform target;
    private Vector3 offset;
    private int index;
    private float curFireRate;
    private Quaternion defaultRot = Quaternion.identity;


    private void Awake()
    {
        turretTrigger = GetComponent<SphereCollider>();
        turretTrigger.isTrigger = true;
        offset = turretTrigger.center;
        curFireRate = fireRate;
        turretTrigger.enabled = true;
        enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckLayerMask(other.gameObject, layerMask))
        {
            target = other.transform;
            turretTrigger.enabled = false;
            enabled = true;
        }
    }

    Transform FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + offset, turretTrigger.radius, layerMask);

        Collider currentCollider = null;
        float dist = Mathf.Infinity;

        foreach(Collider coll in colliders)
        {
            float currentDist = Vector3.Distance(transform.position + offset, coll.transform.position);

            if(currentDist < dist)
            {
                currentCollider = coll;
                dist = currentDist;
            }
        }
        return (currentCollider != null) ? currentCollider.transform : null;
    }

    Vector3 CalculateNegativeValues(Vector3 eulerAngels)
    {
        eulerAngels.y = (eulerAngels.y > 180) ? eulerAngels.y - 360 : eulerAngels.y;
        eulerAngels.x = (eulerAngels.x > 180) ? eulerAngels.x - 360 : eulerAngels.x;
        eulerAngels.z = (eulerAngels.z > 180) ? eulerAngels.z - 360 : eulerAngels.z;
        return eulerAngels;
    }

    bool Search()//разварот вашни на цель
    {
        if (rayOffset < 0) rayOffset = 0;
        float dist = Vector3.Distance(transform.position + offset, target.position);
        Vector3 lookPos = target.position - turretRotation.position;
        Debug.DrawRay(turretRotation.position, center.forward * (turretTrigger.radius + rayOffset), Color.red);
        Vector3 rotation = Quaternion.Lerp(turretRotation.rotation, Quaternion.LookRotation(lookPos), smooth * Time.deltaTime).eulerAngles;

        if (useLimits)
        {
            rotation = CalculateNegativeValues(rotation);
            rotation.y = Mathf.Clamp(rotation.y, -limitY, limitY);
            rotation.x = Mathf.Clamp(rotation.x, -limitX, limitX);
        }
        rotation.z = 0;
        turretRotation.eulerAngles = rotation;

        if (IsRaycastHit(center)) return true;

        return false;
    }

    bool CheckLayerMask(GameObject obj, LayerMask layers)
    {
        if(((1 << obj.layer) & layers) != 0)
        {
            return true;
        }
        return false;
    }

    bool IsRaycastHit(Transform point)
    {
        RaycastHit hit;
        Ray ray = new Ray(point.position, point.forward);
        if(Physics.Raycast(ray, out hit, turretTrigger.radius + rayOffset))
        {
            if(CheckLayerMask(hit.transform.gameObject, layerMask))
            {
                return true;
            }
        }
        return false ;
    }

    void Shot()
    {
        if(!Search()) return;

        curFireRate += Time.deltaTime;
        if(curFireRate > fireRate)
        {
            Transform point = GetPoint();
            curFireRate = 0;

            if(bulletPrefab != null)
            {
                BulletTurret bullet = Instantiate(bulletPrefab, point.position, Quaternion.identity);
                bullet.SetBullet(layerMask, point.forward);
            }
            else if (IsRaycastHit(point))
            {
                target.GetComponent<UnitHP>().Adjust(-damage);
            }
        }
    }

    void Choice()
    {
        curFireRate = fireRate;
        target = FindTarget();

        turretRotation.rotation = Quaternion.Lerp(turretRotation.rotation, defaultRot, smooth * Time.deltaTime);
        if(Quaternion.Angle(turretRotation.rotation, defaultRot) == 0)
        {
            turretRotation.rotation = defaultRot;
            turretTrigger.enabled = true;
            enabled = false;
        }
    }

    Transform GetPoint()
    {
        if (index == bulletPoint.Length - 1) index = 0; else index++;
        return bulletPoint[index];
    }

    private void LateUpdate()
    {
        if (target != null) Shot(); else Choice();
    }
}
