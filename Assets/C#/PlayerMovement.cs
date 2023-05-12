using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private Transform groundColliderTransform;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpOffset;
    [SerializeField] private float speed;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float dashForce = 5f;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private Collider2D firstSwordAttack;
    [SerializeField] private Collider2D SecondSwordAttack;
    [SerializeField] private Collider2D ThirdSwordAttack;

    private bool isDashing = false;
    private bool isCrouching = false;
    private bool onCollision = false;
    private bool isSkilling = false;
    private bool isAttack = false;
    private bool isRunningEnabled = true;

    private float storedDirection;

    private Animator playerAnimator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D playerRb;
    private PlayerInput playerInput;
    private Shooter shooter;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        shooter = GetComponent<Shooter>();
    }

    private void FixedUpdate()
    {
        Vector2 overlapCircleTransform = groundColliderTransform.position;
        isGrounded = Physics2D.OverlapCircle(overlapCircleTransform, jumpOffset, groundMask);
    }

    public void Attacking(bool isAttacking)
    {
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

    public void Shooting(bool isShooting)
    {
        if (isShooting && !isSkilling)
        {
            isSkilling = true;
            playerAnimator.SetBool("IsSkilling", true);
        }
        else if (!isShooting && isSkilling)
        {
            isSkilling = false;
            playerAnimator.SetBool("IsSkilling", false);
        }
    }

    public void Move(float direction, bool isJumpButtonPressed, bool isCrouchButtonPressed, bool isDashButtonPressed)
    {
        if (isJumpButtonPressed)
        {
            Jump();
            playerAnimator.SetBool("IsFloating", true);

        }

        if (Mathf.Abs(direction) > 0.01f && isRunningEnabled)
        {
            HorizontalMove(direction);
            playerAnimator.SetBool("IsRunning", true);
            FlipSwordAttackColliders(direction);
        }
        else
        {
            playerAnimator.SetBool("IsRunning", false);
        }

        CheckIsFloating();

        if (isCrouchButtonPressed&&onCollision)
        {
            isCrouching = true;
            playerAnimator.SetBool("IsSitting", isCrouching);
        }
        else
        {
            isCrouching = false;
            playerAnimator.SetBool("IsSitting", isCrouching);
        }

        if (isDashButtonPressed && !isDashing)
        {
            StartCoroutine(Dash(direction));
            StartCoroutine(WaitForDash());
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        }
    }

    private void HorizontalMove(float direction)
    {
        storedDirection = direction;

        if (direction < 0)
        {
            spriteRenderer.flipX = true;
            shooter.SetFirePointMinus();
        }
        else if (direction > 0)
        {
            spriteRenderer.flipX = false;
            shooter.SetFirePointPlus();
        }
        playerRb.velocity = new Vector2(curve.Evaluate(direction) * speed, playerRb.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        playerAnimator.SetBool("IsFloating", false);
        onCollision = true;
        isRunningEnabled = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerAnimator.SetBool("IsSitting", false);
        onCollision = false;
        playerAnimator.SetBool("IsFloating", true);
    }

    private void CheckIsFloating()
    {
        if (playerAnimator.GetBool("IsFloating") == true)
            playerInput.SetButtonCrouchEnabled(false);
        else
        {
            playerInput.SetButtonCrouchEnabled(true);
        }
    }

    private IEnumerator Dash(float direction)
    {
        isDashing = true;
        playerAnimator.SetBool("IsDashing", true);
        playerAnimator.SetBool("IsFloating", false);
        playerInput.SetButtonSwordEnabled(false);
        playerInput.SetButtonDashEnabled(false);
        Vector2 startPosition = playerRb.position;
        Vector2 endPosition = startPosition + new Vector2(direction * dashDistance, 0f);
        float dashTime = dashDistance / dashForce;

        float elapsedTime = 0f;

        while (elapsedTime < dashTime)
        {
            Vector2 currentPosition = Vector2.Lerp(startPosition, endPosition, elapsedTime / dashTime);
            playerRb.MovePosition(currentPosition);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        isDashing = false;
        playerAnimator.SetBool("IsDashing", false);
        playerAnimator.SetBool("IsFloating", true);
        playerInput.SetButtonSwordEnabled(true);
    }

    private IEnumerator WaitForDash()
    {
        yield return new WaitForSeconds(1f);
        playerInput.SetButtonDashEnabled(true);
    }
    public void SkillAnimationEnd()
    {
        if (isSkilling)
        {
            shooter.Shoot(storedDirection);
            playerAnimator.SetBool("IsSkilling", false);
            isSkilling = false;
            playerInput.SetButtonShootEnabled(false);
            StartCoroutine(SkillAnim());
        }
    }

    private IEnumerator SkillAnim()
    {
        yield return new WaitForSeconds(2);
        playerInput.SetButtonShootEnabled(true);
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
        if(!isAttack)
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

    private void FlipSwordAttackColliders(float direction)
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
