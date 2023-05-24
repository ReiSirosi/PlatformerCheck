using UnityEngine;

public class FirstSceneSwordSeeOrNot : MonoBehaviour
{
    [SerializeField] private GameObject sword;

    private void Awake()
    {
        if (Global.Instance.isSwordPicked)
        {
            Destroy(sword);
        }
    }
}
