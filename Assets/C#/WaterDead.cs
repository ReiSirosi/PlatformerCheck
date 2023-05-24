using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDead : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().DamageTake(damage);
        }
    }
}
