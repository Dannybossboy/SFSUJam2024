using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMovement : TimeObject
{
    public rotationtype rotationType;

    public float rotationSpeed;
    public float acceleration;

    public Transform toRotate;
    public enum rotationtype
    {
        Clockwise,
        CounterClockwise
    }

    public AudioSource audioSource;
    private bool playing;


    private void Update()
    {
        if(!playing && Mathf.Abs(rb.angularVelocity) > 1)
        {
            audioSource.Play();
            playing = true;
        } else if(playing && Mathf.Abs(rb.angularVelocity) < 1)
        {
            audioSource.Stop();
            playing = false;
        }
    }

    //Start
    public override void Init()
    {
        rb = toRotate.GetComponent<Rigidbody2D>();
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
