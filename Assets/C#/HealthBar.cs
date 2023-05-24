using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform barFill;
    [SerializeField] private float barWidth = 1f;

    private float savedFillScaleX;
    private float newScaleX;
    private float fillPercentage;

    private void Awake()
    {
        savedFillScaleX = barFill.localScale.x;
    }

    public void UpdateHealthBar(float currentHP, float maxHP)
    {
        fillPercentage = Mathf.Clamp01(currentHP / maxHP);
        newScaleX = savedFillScaleX * fillPercentage * barWidth;
        barFill.localScale = new Vector3(newScaleX, barFill.localScale.y, barFill.localScale.z);
    }
}
