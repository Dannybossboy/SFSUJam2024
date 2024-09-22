using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMovement : TimeObject
{
    public rotationtype rotationType;

    public float rotationSpeed;
    public float acceleration;
    public enum rotationtype
    {
        Clockwise,
        CounterClockwise
    }

    //Start
    public override void Init()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = Vector2.zero;
    }

    //Rewind Behavior
    public override void RewindBehavior()
    {
        float angle = rotationType == rotationtype.Clockwise ? -acceleration : acceleration;

        rb.AddTorque(-angle, ForceMode2D.Force);
        rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -rotationSpeed, rotationSpeed);
    }

    //Normal Behavior
    public override void NormalBehavior()
    {
        float angle = rotationType == rotationtype.Clockwise ? -acceleration : acceleration;

        rb.AddTorque(angle, ForceMode2D.Force);
        rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -rotationSpeed, rotationSpeed);
    }
}
