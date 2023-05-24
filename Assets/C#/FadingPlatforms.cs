using System.Collections;
using UnityEngine;

public class FadingPlatforms : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DisablePlatform());
        }
    }

    IEnumerator DisablePlatform()
    {
        yield return new WaitForSeconds(2f);

        spriteRenderer.enabled = false;
        boxCollider.enabled = false;

        yield return new WaitForSeconds(2f);

        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
    }
}
