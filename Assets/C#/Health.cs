using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHP;
    private float currentHealth;
    private bool isAlive;

    private void Awake()
    {
        currentHealth = maxHP;
        isAlive = true;
    }

    public void DamageTake(float damage)
    {
        currentHealth -= damage;
        CheckIfALive();
    }

    private void CheckIfALive()
    {
        if (currentHealth > 0)
        {
            isAlive = true;
        }
        else
        {
            isAlive = false;
            IfDead();
        }
    }

    private void IfDead()
    {
        Destroy(gameObject);
    }
}
