using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CinemachineConroller : MonoBehaviour
{
    [SerializeField] private GameObject cinemachinePlayer;
    [SerializeField] private GameObject cinemachineSkeleton;
    [SerializeField] private GameObject cinemachineSword;
    [SerializeField] private GameObject display;
    [SerializeField] private GameObject cutSceneCanvas;
    [SerializeField] private Text howToKillText;
    [SerializeField] private Text thatCanHelpText;

    [SerializeField] private PlayerInput playerInput;


    private void Start()
    {
        playerInput = GameObject.Find("RogueKnight").GetComponent<PlayerInput>();
        cinemachinePlayer.SetActive(true);
        cinemachineSkeleton.SetActive(false);
        cinemachineSword.SetActive(false);
        if (!Global.Instance.isCutSceneDone)
        {
            StartCoroutine(MiniScene());
        }
    }

    IEnumerator MiniScene()
    {
        playerInput.enabled = false;

        yield return new WaitForSeconds(1f);

        display.SetActive(false);
        cutSceneCanvas.SetActive(false);
        cinemachinePlayer.SetActive(false);
        cinemachineSkeleton.SetActive(true);

        yield return new WaitForSeconds(2f);

        cutSceneCanvas.SetActive(true);
        howToKillText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        howToKillText.gameObject.SetActive(false);
        cinemachineSword.SetActive(true);
        cinemachineSkeleton.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        thatCanHelpText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        thatCanHelpText.gameObject.SetActive(false);
        cinemachinePlayer.SetActive(true);
        cinemachineSkeleton.SetActive(false);
        cinemachineSword.SetActive(false);
        display.SetActive(true);

        playerInput.enabled = true;

        Global.Instance.isCutSceneDone = true;
    }
}
