using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerHP PlayerHP;
    [SerializeField] Image Image;

    private float _maxHealth;

    void Start()
    {
        _maxHealth = PlayerHP.MaxHealthGetter;
        PlayerHP.HealthChanged += OnHealthChanged;
        OnHealthChanged(PlayerHP.CurrentHealth);
    }

    void Update()
    {
        
    }

    void OnHealthChanged(float health)
    {
        Image.fillAmount = health / _maxHealth;
    }
}
