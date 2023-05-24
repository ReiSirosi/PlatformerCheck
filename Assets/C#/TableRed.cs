using UnityEngine;
using UnityEngine.UI;

public class TableRed : MonoBehaviour
{
    [SerializeField] private Text needMoreText;
    [SerializeField] private int forUnlockCount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int remainingCrystals = forUnlockCount - Global.Instance.redCrystallCount;
            if (remainingCrystals > 0)
            {
                needMoreText.text = "����� ��� " + remainingCrystals + " ������� ����������";
            }
            else if (remainingCrystals <= 0)
            {
                needMoreText.text = "����� F";
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
        if (needMoreText.text == "����� F" && Input.GetKeyDown(KeyCode.F))
        {
            Global.Instance.isRedDone = true;
        }

        if (Global.Instance.isRedDone)
        {
            Destroy(gameObject);
        }
    }
}