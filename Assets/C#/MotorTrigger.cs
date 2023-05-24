using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorTrigger : MonoBehaviour
{
    private SliderJoint2D platformSJ;

    [SerializeField] private GameObject otherObject;

    private void Start()
    {
        platformSJ = otherObject.GetComponent<SliderJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            JointMotor2D motor = platformSJ.motor;
            motor.motorSpeed *= -1;
            platformSJ.motor = motor;
        }
    }
}
