using UnityEngine;
using UnityEngine.UI;

public class TableGreen : MonoBehaviour
{
    [SerializeField] private Text needMoreText;
    [SerializeField] private int forUnlockCount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int remainingCrystals = forUnlockCount - Global.Instance.greenCrystallCount;
            if (remainingCrystals > 0)
            {
                needMoreText.text = "Нужно ещё " + remainingCrystals + " зеленых кристаллов";
            }
            else if (remainingCrystals <= 0)
            {
                needMoreText.text = "Нажми F";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            needMoreText.text = "";
        }
    }

    private void Update()
    {
        if (needMoreText.text == "Нажми F" && Input.GetKeyDown(KeyCode.F))
        {
            Global.Instance.isGreenDone = true;
        }

        if (Global.Instance.isGreenDone)
        {
            Destroy(gameObject);
        }
    }
}
