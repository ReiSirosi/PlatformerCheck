using UnityEngine;

public class PlatformMotorConroller : MonoBehaviour
{
    [SerializeField] private SliderJoint2D sliderJoint;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float upperLimit = 5f;
    [SerializeField] private float lowerLimit = -5f;

    private JointMotor2D motor;

    private void Start()
    {
        motor = sliderJoint.motor;
        motor.motorSpeed = moveSpeed;
        sliderJoint.motor = motor;

        JointTranslationLimits2D limits = sliderJoint.limits;
        limits.max = upperLimit;
        limits.min = lowerLimit;
        sliderJoint.limits = limits;
    }

    private void FixedUpdate()
    {
        if (sliderJoint.limitState == JointLimitState2D.LowerLimit)
        {
            motor.motorSpeed = moveSpeed;
            sliderJoint.motor = motor;
        }
        else if (sliderJoint.limitState == JointLimitState2D.UpperLimit)
        {
            motor.motorSpeed = -moveSpeed;
            sliderJoint.motor = motor;
        }
    }
}
