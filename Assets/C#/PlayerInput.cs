using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerAttacking playerAttacking;
    private PlayerShooting playerShooting;

    public const string HorizontalAxis = "Horizontal";
    public const string VerticalAxis = "Vertical";
    public const string JumpButton = "Jump";
    public const string CrouchButton = "Crouch";
    public const string Fire1 = "Skill";
    public const string DashButton = "Dash";
    public const string SwordAttack = "Fire2";


    private bool isButtonCrouchEnabled = true;
    private bool isButtonShootEnabled = true;
    private bool isButtonSwordEnabled = false;
    private bool isButtonDashEnabled = true;


    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAttacking = GetComponent<PlayerAttacking>();
        playerShooting = GetComponent<PlayerShooting>();
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

        playerShooting.Shooting(isShooting);
        playerAttacking.Attacking(isAttacking);

        playerMovement.Jump(isJumpButtonPressed);
        playerMovement.Crouch(isCrouchButtonPressed);
        playerMovement.Dashin(isDashButtonPressed);
        playerMovement.Move(horizontalDirection);

        CheckPickedSword();
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

    private void CheckPickedSword()
    {
        if (Global.Instance.isSwordPicked)
        {
            SetButtonSwordEnabled(true);
        }
    }
}