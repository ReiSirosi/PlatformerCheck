using System.Collections;
using UnityEngine;

public class CollectSword : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(WaitTextWhenCollect());
        }
    }

    IEnumerator WaitTextWhenCollect()
    {
        spriteRenderer.enabled = false;
        canvas.gameObject.SetActive(true);
        Global.Instance.isSwordPicked = true;
        yield return new WaitForSeconds(1.5f);

        canvas.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}