using UnityEngine;

public class RedDoor : MonoBehaviour
{
    private void Awake()
    {
        if (Global.Instance.isRedDone)
        {
            Destroy(gameObject);
        }
    }
}
