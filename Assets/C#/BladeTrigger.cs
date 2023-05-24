using UnityEngine;
using System.Collections;

public class BladeTrigger : MonoBehaviour
{
    [SerializeField] private GameObject sawPrefab;
    [SerializeField] private Transform spawnPoint;

    private bool hasSpawnedSaw = false;
    private bool isTriggerActive = true;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasSpawnedSaw && other.CompareTag("Player") && isTriggerActive)
        {
            SpawnNewSaw();
            hasSpawnedSaw = true;
            StartCoroutine(TriggerController());
        }
    }

    private void SpawnNewSaw()
    {
        Instantiate(sawPrefab, spawnPoint.position, Quaternion.identity);
    }

    IEnumerator TriggerController()
    {
        isTriggerActive = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(3f);

        spriteRenderer.enabled = true;
        isTriggerActive = true;
        hasSpawnedSaw = false;
    }
}
