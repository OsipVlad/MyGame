using System;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] public float health = 100;
    
    public Action<float> HealthChanged;
    
    private float _currentHealth;

    public float MaxHealthGetter => health;
    public float CurrentHealth => _currentHealth;



    private void Awake()
    {
        _currentHealth = health;
    }
    public void Adjust(float value)
    {
        _currentHealth -= value;
        HealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
