using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Start()
    {
        if (SceneTransitionData.spawnPosition != Vector3.zero)
        {
            transform.position = SceneTransitionData.spawnPosition;
            SceneTransitionData.spawnPosition = Vector3.zero;
        }
    }
}