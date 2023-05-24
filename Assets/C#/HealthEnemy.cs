using UnityEngine;
using System;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private float maxHP;

    public event Action<float, float> HealthChanged;
    public event Action Dead;

    public float currentHealth;
    private bool isAlive;

    private void Awake()
    {
        currentHealth = maxHP;
        isAlive = true;
    }

    public void DamageTake(float damage)
    {
        currentHealth -= damage;
        HealthChanged?.Invoke(currentHealth, maxHP);

        CheckIfALive();
    }

    private void CheckIfALive()
    {
        if (currentHealth <= 0 && isAlive)
        {
            isAlive = false;
            OnDead();
        }
    }

    private void OnDead()
    {
        Dead?.Invoke();
        Destroy(gameObject);
    }
}