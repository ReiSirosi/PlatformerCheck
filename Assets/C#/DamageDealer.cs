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
            collision.gameObject.GetComponent<Health>().DamageTake(damage);
        }
        Destroy(gameObject);
    }
}
