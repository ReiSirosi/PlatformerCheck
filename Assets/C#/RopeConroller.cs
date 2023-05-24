using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeConroller : MonoBehaviour
{
    private void Awake()
    {
        if (Global.Instance.isJoystickDone)
        {
            Destroy(gameObject);
        }
    }
}
