using UnityEngine;
using System.Collections;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private GameObject snakePrefab;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    private GameObject snakeInstance;

    private void Start()
    {
        SpawnSnake();
    }

    private void SpawnSnake()
    {
        snakeInstance = Instantiate(snakePrefab, startPoint.position, Quaternion.identity);
    }

    private void Update()
    {
        if (snakeInstance != null && snakeInstance.transform.position.x >= endPoint.transform.position.x)
        {
            StartCoroutine(WaitForNextSnake());
        }
    }

    IEnumerator WaitForNextSnake()
    {
        Destroy(snakeInstance);

        yield return new WaitForSeconds(1f);

        SpawnSnake();
    }
}