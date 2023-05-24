using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Canvas deadCanvas;

    public void RestartLvl()
    {
        Global.Instance.HP = 100f;
        deadCanvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}