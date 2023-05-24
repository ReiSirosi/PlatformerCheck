using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damagable"))
        {
            collision.gameObject.GetComponent<HealthEnemy>().DamageTake(damage);
        }

        if (!collision.CompareTag("TriggerForPlat"))
        {
            Destroy(gameObject);
        }
    }
}
