using UnityEngine;

public class JoystickConroller : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("IsDone", true);
            Global.Instance.isJoystickDone = true;
        }
    }
}
