using UnityEngine;

public class GreenDoor : MonoBehaviour
{
    private void Awake()
    {
        if (Global.Instance.isGreenDone)
        {
            Destroy(gameObject);
        }
    }
}
