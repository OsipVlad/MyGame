using UnityEngine;

public class UnitHP : MonoBehaviour
{
    [SerializeField] public float health = 100;


    

   
    public void Adjust(float value)
    {
        health -= value;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
