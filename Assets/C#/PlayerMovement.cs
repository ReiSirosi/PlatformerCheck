using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private Transform groundColliderTransform;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpOffset;
    [SerializeField] private float speed;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float dashForce = 5f;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private bool isGrounded = false;

    public bool isDashing = false;
    private bool isCrouching = false;
    private bool onCollision = false;
    private bool isRunningEnabled = true;

    private float currentDirection;
    public float storedDirection;

    private PlayerAttacking playerAttacking;
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
        playerAttacking = GetComponent<PlayerAttacking>();
    }

    private void FixedUpdate()
    {
        Vector2 overlapCircleTransform = groundColliderTransform.position;
        isGrounded = Physics2D.OverlapCircle(overlapCircleTransform, jumpOffset, groundMask);
    }

    public void Jump(bool isJumpButtonPressed)
    {
        if (isJumpButtonPressed)
        {
            Jump();
            playerAnimator.SetBool("IsFloating", true);
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        }
    }
    public void Dashin(bool isDashButtonPressed)
    {
        if (isDashButtonPressed && !isDashing)
        {
            StartCoroutine(Dash(currentDirection));
            StartCoroutine(WaitForDash());
        }
    }

    public void Crouch(bool isCrouchButtonPressed)
    {
        if (isCrouchButtonPressed && onCollision)
        {
            isCrouching = true;
            playerAnimator.SetBool("IsSitting", isCrouching);
        }
        else
        {
            isCrouching = false;
            playerAnimator.SetBool("IsSitting", isCrouching);
        }
    }

    public void Move(float direction)
    {
        currentDirection = direction;

        if (Mathf.Abs(direction) > 0.01f && isRunningEnabled)
        {
            HorizontalMove(direction);
            playerAnimator.SetBool("IsRunning", true);
            playerAttacking.FlipSwordAttackColliders(direction);
        }
        else
        {
            playerAnimator.SetBool("IsRunning", false);
        }

        CheckIsFloating();
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
      //  playerInput.SetButtonSwordEnabled(false);
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
      //  playerInput.SetButtonSwordEnabled(true);
    }

    private IEnumerator WaitForDash()
    {
        yield return new WaitForSeconds(1f);
        playerInput.SetButtonDashEnabled(true);
    }
}
