using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public const string HorizontalAxis = "Horizontal";
    public const string VerticalAxis = "Vertical";
    public const string JumpButton = "Jump";
    public const string CrouchButton = "Crouch";
    public const string Fire1 = "Skill";
    public const string DashButton = "Dash";
    public const string SwordAttack = "Fire2";

    private bool isButtonCrouchEnabled = true;
    private bool isButtonShootEnabled = true;
    private bool isButtonSwordEnabled = true;
    private bool isButtonDashEnabled = true;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        float horizontalDirection = Input.GetAxis(HorizontalAxis); ;
        bool isJumpButtonPressed = Input.GetButtonDown(JumpButton);
        bool isCrouchButtonPressed = false;
        bool isDashButtonPressed = false;
        bool isShooting = false;
        bool isAttacking = false;

        if (isButtonDashEnabled)
        {
           isDashButtonPressed = Input.GetButtonDown(DashButton);
        }

        if (isButtonSwordEnabled)
        {
            isAttacking = Input.GetButton(SwordAttack);
        }

        if (isButtonCrouchEnabled)
        {
            isCrouchButtonPressed = Input.GetButton(CrouchButton);
        }

        if (isButtonShootEnabled)
        {
            isShooting = Input.GetButton(Fire1);
        }

        playerMovement.Shooting(isShooting);
        playerMovement.Attacking(isAttacking);
        playerMovement.Move(horizontalDirection, isJumpButtonPressed, isCrouchButtonPressed, isDashButtonPressed);
    }

    public void SetButtonCrouchEnabled(bool isEnabled)
    {
        isButtonCrouchEnabled = isEnabled;
    }

    public void SetButtonShootEnabled(bool isEnabled)
    {
        isButtonShootEnabled = isEnabled;
    }

    public void SetButtonSwordEnabled(bool isEnabled)
    {
        isButtonSwordEnabled = isEnabled;
    }

    public void SetButtonDashEnabled(bool isEnabled)
    {
        isButtonDashEnabled = isEnabled;
    }
}