using UnityEngine;
using System.Collections;

public class BladeConroller : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = -100f;

    private bool endForBlade = false;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0f);

        StartCoroutine(EndBlade());
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    IEnumerator EndBlade()
    {
        yield return new WaitForSeconds(3.5f);

        Destroy(gameObject);
    }
}