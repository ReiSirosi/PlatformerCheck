using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float knockbackForce;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            rb.freezeRotation = true;
            collision.gameObject.GetComponent<Health>().DamageTake(damage);

            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            Vector2 knockbackDirection = Vector2.zero;
            Vector2 contactPoint = collision.contacts[0].point;
            Vector2 enemyPosition = transform.position;

            if (contactPoint.x < enemyPosition.x)
                knockbackDirection.x = -1f;
            else if (contactPoint.x > enemyPosition.x)
                knockbackDirection.x = 1f;

            if (contactPoint.y > enemyPosition.y)
                knockbackDirection.y = 1f;
            else if (contactPoint.y < enemyPosition.y)
                knockbackDirection.y = -1f;

            knockbackDirection.Normalize();

            playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(WaitConstraints());
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<HealthEnemy>().DamageTake(damage);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.freezeRotation = true;
        }
    }

    private IEnumerator WaitConstraints()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        rb.freezeRotation = true;

        yield return new WaitForSeconds(0.2f);

        rb.constraints = RigidbodyConstraints2D.None;
        rb.freezeRotation = true;
    }
}
