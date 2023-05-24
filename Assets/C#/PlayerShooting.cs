using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Shooter shooter;
    private PlayerInput playerInput;
    private Animator playerAnimator;
    private PlayerMovement playerMovement;

    private bool isSkilling = false;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        shooter = GetComponent<Shooter>();
        playerMovement = GetComponent<PlayerMovement>();
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

    public void SkillAnimationEnd()
    {
        if (isSkilling)
        {
            shooter.Shoot(playerMovement.storedDirection);
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
}
