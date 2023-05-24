using System.Collections;
using UnityEngine;

public class HPdisplay : MonoBehaviour
{
    [SerializeField] private Canvas deadCanvas;
    [SerializeField] private GameObject takeDmgImage;
    [SerializeField] private HealthBar healthBar;

    private void OnEnable()
    {
        Health health = GetComponent<Health>();
        if (health != null)
        {
            health.HealthChanged += UpdateHealthDisplay;
            health.Dead += OnDead;
        }
    }

    private void OnDisable()
    {
        Health health = GetComponent<Health>();
        if (health != null)
        {
            health.HealthChanged -= UpdateHealthDisplay;
            health.Dead -= OnDead;
        }
    }

    private void UpdateHealthDisplay(float currentHealth, float maxHealth)
    {
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        StartCoroutine(TakeDmg());
    }

    private IEnumerator TakeDmg()
    {
        takeDmgImage.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        takeDmgImage.SetActive(false);
    }

    private void OnDead()
    {
        if (gameObject.CompareTag("Player"))
        {
            deadCanvas.gameObject.SetActive(true);
            MinusCrystalls();
            Time.timeScale = 0f;
        }
    }

    private void MinusCrystalls()
    {
        Global.Instance.greenCrystallCount -= 7;
        Global.Instance.redCrystallCount -= 5;
        Global.Instance.blueCrystallCount -= 6;

        Global.Instance.greenCrystallCount = Mathf.Max(0, Global.Instance.greenCrystallCount);
        Global.Instance.redCrystallCount = Mathf.Max(0, Global.Instance.redCrystallCount);
        Global.Instance.blueCrystallCount = Mathf.Max(0, Global.Instance.blueCrystallCount);
    }
}