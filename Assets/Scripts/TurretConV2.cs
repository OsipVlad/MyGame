using UnityEngine;

public class TurretConV2 : MonoBehaviour
{
    public GameObject[] targets; //������ ���� �����
    public GameObject curTarget;
    public float towerPrice = 100.0f;
    public float attackMaximumDistance = 50.0f; //��������� �����
    public float attackMinimumDistance = 5.0f;
    public float attackDamage = 10.0f; //����
    public float reloadTimer = 1.0f; //�������� ����� ����������, ���������� ��������
    public const float reloadCooldown = 1.0f; //�������� ����� ����������, ���������
    public float rotationSpeed = 1.5f; //��������� �������� �������� �����
    public int FiringOrder = 1; //����������� �������� ��� ������� (� ��� �� �� 2)
    private int index;
    [SerializeField] private Transform[] bulletPoint;//�����, ������ ������� ��������
    [SerializeField] private BulletTurret bulletPrefab;  //������ �������
    public int BulletForce = 1;  //�������� �������
    //public AudioClip laser_sound;    //��������� ����������������� �� ����� ��������


    public Transform turretHead;

    public RaycastHit Hit;

    //���������� ���� ����� ��� �������������
    private void Start()
    {
        turretHead = transform.Find("Sphere"); //������� ����� � �������� ������ ������
    }

    //� ���� ����� ���������� ������ �����
    private void Update()
    {
        if (curTarget != null) //���� ���������� ������� ���� �� ������
        {
            float distance = Vector3.Distance(turretHead.position, curTarget.transform.position); //������ ��������� �� ���
            if (attackMinimumDistance < distance && distance < attackMaximumDistance) //���� ��������� ������ ������� ���� � ������ ��������� ��������� �����
            {
                turretHead.rotation = Quaternion.Slerp(turretHead.rotation, Quaternion.LookRotation(curTarget.transform.position - turretHead.position), rotationSpeed * Time.deltaTime); //������� ����� � ������� ����

                if (reloadTimer > 0) reloadTimer -= Time.deltaTime; //���� ������ ����������� ������ ���� - �������� ���
                if (reloadTimer < 0) reloadTimer = 0; //���� �� ���� ������ ���� - ������������� ��� � ����
                if (reloadTimer == 0) //���� �����
                {

                    GetPoint();     //��������� ������ ������� � ����������� ������� "BulletSpawnPoint"
                    Transform point = GetPoint();
                                    //laser.rotation = Quaternion.Slerp(laser.rotation, Quaternion.LookRotation(curTarget.transform.position - laser.position), rotationSpeed * Time.deltaTime);
                    BulletTurret bullet = Instantiate(bulletPrefab, point.position, Quaternion.identity);
                         //�������������� ������ ������������ ������ �� ��������� "BulletForce"
                    //audio.PlayOneShot(laser_sound);  //����������� ���� ��������

                    UnitHP mhp = curTarget.GetComponent<UnitHP>();
                    switch (FiringOrder) //�������, �� ������ ������ ��������
                    {
                        case 1:
                            if (mhp != null) mhp.Adjust(-attackDamage); //������� ����� ����
                            FiringOrder++; //����������� FiringOrder �� 1
                            break;
                        case 2:
                            if (mhp != null) mhp.Adjust(-attackDamage); //������� ����� ����
                            FiringOrder = 1; //������������� FiringOrder � ����������� �������
                            break;
                    }
                    reloadTimer = reloadCooldown; //���������� ���������� �������� � �������������� �������� �� ���������
                }
            }
        }
        else //�����
        {
            curTarget = SortTargets(); //��������� ���� � �������� �����
        }
    }

    Transform GetPoint()
    {
        if (index == bulletPoint.Length - 1) index = 0; else index++;
        return bulletPoint[index];
    }
    //����� ����������� ����� ���������� �����, ���� ������������ ��� �����������!
    public GameObject SortTargets()
    {
        float closestMobDistance = 0; //������������� ���������� ��� �������� ��������� �� ����
        GameObject nearestmob = null; //������������� ���������� ���������� ����
                                      // List<GameObject> sortingMobs = GameObject.FindGameObjectWithTag("Monster").ToList();//������� ���� ����� � ����� Monster � ������ ������ ��� ����������
        GameObject[] sortingMobs = GameObject.FindGameObjectsWithTag("Monster");
        foreach (var everyTarget in sortingMobs) //��� ������� ���� � �������
        {
            //���� ��������� �� ���� ������, ��� closestMobDistance ��� ����� ����
            if ((Vector3.Distance(everyTarget.transform.position, turretHead.position) < closestMobDistance) || closestMobDistance == 0)
            {
                closestMobDistance = Vector3.Distance(everyTarget.transform.position, turretHead.position); //������ ��������� �� ���� �� �����, ���������� � � ����������
                nearestmob = everyTarget;//������������� ��� ��� ����������
            }
        }
        return closestMobDistance > attackMaximumDistance ? null : nearestmob; //���������� ���������� ����
    }
}
