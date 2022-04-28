using UnityEngine;

public class TurretConV2 : MonoBehaviour
{
    public GameObject[] targets; //массив всех целей
    public GameObject curTarget;
    public float towerPrice = 100.0f;
    public float attackMaximumDistance = 50.0f; //дистанция атаки
    public float attackMinimumDistance = 5.0f;
    public float attackDamage = 10.0f; //урон
    public float reloadTimer = 1.0f; //задержка между выстрелами, изменяемое значение
    public const float reloadCooldown = 1.0f; //задержка между выстрелами, константа
    public float rotationSpeed = 1.5f; //множитель скорости вращения башни
    public int FiringOrder = 1; //очередность стрельбы для стволов (у нас же их 2)
    private int index;
    [SerializeField] private Transform[] bulletPoint;//точки, откуда ведется стрельба
    [SerializeField] private BulletTurret bulletPrefab;  //Префаб патрона
    public int BulletForce = 1;  //Скорость патрона
    //public AudioClip laser_sound;    //Аудиоклип воспроизводящийся во время стрельбы


    public Transform turretHead;

    public RaycastHit Hit;

    //используем этот метод для инициализации
    private void Start()
    {
        turretHead = transform.Find("Sphere"); //находим башню в иерархии частей модели
    }

    //а этот метод вызывается каждый фрейм
    private void Update()
    {
        if (curTarget != null) //если переменная текущей цели не пустая
        {
            float distance = Vector3.Distance(turretHead.position, curTarget.transform.position); //меряем дистанцию до нее
            if (attackMinimumDistance < distance && distance < attackMaximumDistance) //если дистанция больше мертвой зоны и меньше дистанции поражения пушки
            {
                turretHead.rotation = Quaternion.Slerp(turretHead.rotation, Quaternion.LookRotation(curTarget.transform.position - turretHead.position), rotationSpeed * Time.deltaTime); //вращаем башню в сторону цели

                if (reloadTimer > 0) reloadTimer -= Time.deltaTime; //если таймер перезарядки больше нуля - отнимаем его
                if (reloadTimer < 0) reloadTimer = 0; //если он стал меньше нуля - устанавливаем его в ноль
                if (reloadTimer == 0) //став нулем
                {

                    GetPoint();     //Генерится префаб патрона в координатах объекта "BulletSpawnPoint"
                    Transform point = GetPoint();
                                    //laser.rotation = Quaternion.Slerp(laser.rotation, Quaternion.LookRotation(curTarget.transform.position - laser.position), rotationSpeed * Time.deltaTime);
                    BulletTurret bullet = Instantiate(bulletPrefab, point.position, Quaternion.identity);
                         //Сгенерированый патрон отправляется вперед со скоростью "BulletForce"
                    //audio.PlayOneShot(laser_sound);  //проигрываем звук выстрела

                    UnitHP mhp = curTarget.GetComponent<UnitHP>();
                    switch (FiringOrder) //смотрим, из какого ствола стрелять
                    {
                        case 1:
                            if (mhp != null) mhp.Adjust(-attackDamage); //наносим дамаг цели
                            FiringOrder++; //увеличиваем FiringOrder на 1
                            break;
                        case 2:
                            if (mhp != null) mhp.Adjust(-attackDamage); //наносим дамаг цели
                            FiringOrder = 1; //устанавливаем FiringOrder в изначальную позицию
                            break;
                    }
                    reloadTimer = reloadCooldown; //возвращаем переменной задержки её первоначальное значение из константы
                }
            }
        }
        else //иначе
        {
            curTarget = SortTargets(); //сортируем цели и получаем новую
        }
    }

    Transform GetPoint()
    {
        if (index == bulletPoint.Length - 1) index = 0; else index++;
        return bulletPoint[index];
    }
    //Очень примитивный метод сортировки целей, море возможностей для модификации!
    public GameObject SortTargets()
    {
        float closestMobDistance = 0; //инициализация переменной для проверки дистанции до моба
        GameObject nearestmob = null; //инициализация переменной ближайшего моба
                                      // List<GameObject> sortingMobs = GameObject.FindGameObjectWithTag("Monster").ToList();//находим всех мобов с тегом Monster и создаём массив для сортировки
        GameObject[] sortingMobs = GameObject.FindGameObjectsWithTag("Monster");
        foreach (var everyTarget in sortingMobs) //для каждого моба в массиве
        {
            //если дистанция до моба меньше, чем closestMobDistance или равна нулю
            if ((Vector3.Distance(everyTarget.transform.position, turretHead.position) < closestMobDistance) || closestMobDistance == 0)
            {
                closestMobDistance = Vector3.Distance(everyTarget.transform.position, turretHead.position); //Меряем дистанцию от моба до пушки, записываем её в переменную
                nearestmob = everyTarget;//устанавливаем его как ближайшего
            }
        }
        return closestMobDistance > attackMaximumDistance ? null : nearestmob; //возвращаем ближайшего моба
    }
}
