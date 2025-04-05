using UnityEngine;
using System;

public class HealthController : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    public float CurrentHealth { get; private set; }

    public bool IsDead => CurrentHealth <= 0;

    // Olaylar
    public event Action OnDeath;
    public event Action<float> OnHealthChanged;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(" in here " + 5 + transform.name + " damage");
        if (IsDead) return;

        CurrentHealth -= 5;
        Debug.Log(CurrentHealth);
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

      

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }

}