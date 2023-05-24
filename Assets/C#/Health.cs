using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public float maxHP;

    public event Action<float, float> HealthChanged;
    public event Action Dead;

    public float currentHealth;
    private bool isAlive;

    private void Awake()
    {
        currentHealth = Global.Instance.HP;
        isAlive = true;
        HealthChanged?.Invoke(currentHealth, maxHP);
    }

    public void DamageTake(float damage)
    {
        currentHealth -= damage;
        Global.Instance.HP = currentHealth;
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