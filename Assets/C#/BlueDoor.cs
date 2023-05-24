using UnityEngine;

public class BlueDoor : MonoBehaviour
{
    private void Awake()
    {
        if (Global.Instance.isBlueDone)
        {
            Destroy(gameObject);
        }
    }
}
