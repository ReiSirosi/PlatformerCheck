using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text crystalGreenCountText;
    [SerializeField] private Text crystalRedCountText;
    [SerializeField] private Text crystalBlueCountText;

    private int greenCrystallCount = 0;
    private int redCrystallCount = 0;
    private int blueCrystallCount = 0;

    private void Update()
    {
        CountCrystalls();

        CrystallsToString();
    }

    private void CountCrystalls()
    {
        greenCrystallCount = Global.Instance.greenCrystallCount;
        redCrystallCount = Global.Instance.redCrystallCount;
        blueCrystallCount = Global.Instance.blueCrystallCount;
    }

    private void CrystallsToString()
    {
        crystalGreenCountText.text = greenCrystallCount.ToString();
        crystalRedCountText.text = redCrystallCount.ToString();
        crystalBlueCountText.text = blueCrystallCount.ToString();
    }
}
