using UnityEngine.SceneManagement;
using UnityEngine;

public static class SceneTransitionData
{
    public static Vector3 spawnPosition;
}

public class SceneTransitionManager : MonoBehaviour
{
    public CharacterSpawnData spawnData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneTransitionData.spawnPosition = GetSpawnPosition(spawnData.corridorID);
            SceneManager.LoadScene(spawnData.sceneID);
        }
    }

    private Vector3 GetSpawnPosition(int corridorID)
    {
        CorridorController[] corridors = FindObjectsOfType<CorridorController>();

        foreach (CorridorController corridor in corridors)
        {
            if (corridor.corridorID == corridorID)
            {
                return corridor.transform.position;
            }
        }

        return Vector3.zero;
    }
}