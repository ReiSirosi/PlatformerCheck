using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCrystallCollect : MonoBehaviour
{
    private Animator crystalAnimator;

    private int crystallCount = 0;

    private bool isCollected = false;

    private void Awake()
    {
        crystalAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            StartCoroutine(CollectWait());
        }
    }

    IEnumerator CollectWait()
    {
        crystalAnimator.SetTrigger("Collect");
        crystallCount++;

        yield return new WaitForEndOfFrame();

        Global.Instance.blueCrystallCount += crystallCount;

        Destroy(gameObject);
    }
}
