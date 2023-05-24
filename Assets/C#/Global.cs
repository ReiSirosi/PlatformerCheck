using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global Instance;

    public float HP;

    public int greenCrystallCount;
    public int redCrystallCount;
    public int blueCrystallCount;

    public bool isBlueDone = false;
    public bool isRedDone = false;
    public bool isGreenDone = false;
    public bool isJoystickDone = false;
    public bool isSwordPicked = false;
    public bool isCutSceneDone = false;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayer()
    {
        Global.Instance.HP = HP;
        Global.Instance.isSwordPicked = isSwordPicked;
        Global.Instance.greenCrystallCount = greenCrystallCount;
        Global.Instance.redCrystallCount = redCrystallCount;
        Global.Instance.blueCrystallCount = blueCrystallCount;
        Global.Instance.isBlueDone = isBlueDone;
        Global.Instance.isJoystickDone = isJoystickDone;
        Global.Instance.isRedDone = isRedDone;
        Global.Instance.isGreenDone = isGreenDone;
        Global.Instance.isCutSceneDone = isCutSceneDone;
    }

    private void Start()
    {
        HP = Global.Instance.HP;
        isSwordPicked = Global.Instance.isSwordPicked;
        greenCrystallCount = Global.Instance.greenCrystallCount;
        blueCrystallCount = Global.Instance.blueCrystallCount;
        redCrystallCount = Global.Instance.redCrystallCount;
        isBlueDone = Global.Instance.isBlueDone;
        isJoystickDone = Global.Instance.isJoystickDone;
        isRedDone = Global.Instance.isRedDone;
        isGreenDone = Global.Instance.isGreenDone;
        isCutSceneDone = Global.Instance.isCutSceneDone;
    }
}
