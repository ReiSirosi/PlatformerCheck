using System.Collections;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    private bool isAttack = false;

    private Animator playerAnimator;
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;

    [SerializeField] private Collider2D firstSwordAttack;
    [SerializeField] private Collider2D SecondSwordAttack;
    [SerializeField] private Collider2D ThirdSwordAttack;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
    }


    public void Attacking(bool isAttacking)
    {
        if (playerMovement.isDashing)
        {
            return;
        }

        if (isAttacking && !isAttack)
        {
            isAttack = true;
            playerAnimator.SetBool("IsHitting", true);
        }
        else if (!isAttacking && isAttack)
        {
            isAttack = false;
            playerAnimator.SetBool("IsHitting", false);
        }
    }

    public void SwordAnimationGo()
    {
        if (isAttack)
        {
            playerAnimator.SetBool("FirstCombo", true);
        }
        else
        {
            playerAnimator.SetBool("IsHitting", false);
            playerAnimator.SetBool("FirstCombo", false);
            StartCoroutine(SwordAnim());
        }
    }
    public void SwordAnimationCombo()
    {
        if (isAttack)
        {
            playerAnimator.SetBool("SecondCombo", true);
            playerAnimator.SetBool("FirstCombo", false);
        }
        else
        {
            playerAnimator.SetBool("IsHitting", false);
            playerAnimator.SetBool("FirstCombo", false);
            playerAnimator.SetBool("SecondCombo", false);
            StartCoroutine(SwordAnim());
        }
    }

    public void SwordAnimEnd()
    {
        if (isAttack)
        {
            playerAnimator.SetBool("SecondCombo", false);
            playerAnimator.SetBool("FirstCombo", false);
            StartCoroutine(SwordAnim());
        }
        else
        {
            playerAnimator.SetBool("IsHitting", false);
            playerAnimator.SetBool("FirstCombo", false);
            playerAnimator.SetBool("SecondCombo", false);
            StartCoroutine(SwordAnim());
        }
    }

    public void WaitForSingleAttack()
    {
        if (!isAttack)
            StartCoroutine(WaitForSingle());
    }

    private IEnumerator WaitForSingle()
    {
        playerInput.SetButtonSwordEnabled(false);
        yield return new WaitForSeconds(1f);
        playerInput.SetButtonSwordEnabled(true);
    }

    private IEnumerator SwordAnim()
    {
        playerInput.SetButtonSwordEnabled(false);

        yield return new WaitForSeconds(0.5f);

        playerInput.SetButtonSwordEnabled(true);
    }

    public void FirstSwordHit()
    {
        firstSwordAttack.gameObject.SetActive(true);
        StartCoroutine(DisableSwordAttack(firstSwordAttack));
    }

    public void SecondSwordHit()
    {
        SecondSwordAttack.gameObject.SetActive(true);
        StartCoroutine(DisableSwordAttack(SecondSwordAttack));
    }

    public void ThirdSwordHit()
    {
        ThirdSwordAttack.gameObject.SetActive(true);
        StartCoroutine(DisableSwordAttack(ThirdSwordAttack));
    }

    private IEnumerator DisableSwordAttack(Collider2D swordAttack)
    {
        yield return new WaitForSeconds(0.05f);
        swordAttack.gameObject.SetActive(false);
    }

    public void FlipSwordAttackColliders(float direction)
    {
        bool flipX = direction < 0;
        firstSwordAttack.transform.localScale = new Vector3(flipX ? -1 : 1, 1, 1);
        SecondSwordAttack.transform.localScale = new Vector3(flipX ? -1 : 1, 1, 1);
        ThirdSwordAttack.transform.localScale = new Vector3(flipX ? -1 : 1, 1, 1);
    }

    public void CheckHittingInAir()
    {
        if (playerAnimator.GetBool("IsHitting") == true)
        {
            playerAnimator.SetBool("IsFloating", false);
        }
    }
}
