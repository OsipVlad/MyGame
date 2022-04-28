using UnityEngine;

public class UnitHP : MonoBehaviour
{
    [SerializeField] public float health = 100;

    //public float currentHealth
    //{
    //    get { return health; }
    //}

    public void Adjust(float value)
    {
        health -= value;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
