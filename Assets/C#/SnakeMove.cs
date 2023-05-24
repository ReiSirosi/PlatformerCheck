using UnityEngine;

public class SnakeMove : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.right);
    }
}