using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonMovement : TimeObject
{
    public float extentDistance = 3f;
    public float extentDuration = 1f;
    public float startOffset = 0f;

    public float extentSpeed = 5;


    public Transform target;

    public Transform toMove;

    private bool reversing;
    private float offset;
    public override void Init()
    {
        rb = toMove.GetComponent<Rigidbody2D>();

        offset = rb.position.y;

        toMove.position = Vector3.Lerp(transform.position, transform.position + new Vector3(extentDistance, 0, 0), startOffset);

    }

    public override void NormalBehavior()
    {
        //Max distance of extent
        float dist = !reversing ? (toMove.position - transform.position).magnitude : (target.position - toMove.position).magnitude; 
        //Direction of velocity
        float dir = reversing ? -1 : 1;

        //Move shaft
        if (dist < extentDistance) 
        {
            rb.MovePosition(rb.position + (Vector2)transform.right * extentDistance * dir * Time.fixedDeltaTime);
        }
        //Stop shaft and switch direction
        else if (dist >= extentDistance) 
        {
            rb.position = reversing ? transform.position : target.position;

            reversing = !reversing;
        }

        //Lock to axis Broken Help pls
        /*
        Vector2 localPos = transform.InverseTransformVector(rb.position);
        Quaternion unRot = Quaternion.Inverse(transform.rotation);
        Vector2 unRotVector = unRot * localPos;
        rb.position = transform.TransformVector(transform.rotation * new Vector2(unRotVector.x, 0));
        */
    }

    public override void RewindBehavior()
    {
        //Max distance of extent
        float dist = !reversing ? (target.position - toMove.position).magnitude : (toMove.position - transform.position).magnitude;
        //Direction of velocity
        float dir = reversing ? 1 : -1;

        //Move shaft
        if (dist < extentDistance)
        {
            rb.MovePosition(rb.position + (Vector2)transform.right * extentDistance * dir * Time.fixedDeltaTime);
        }
        //Stop shaft and switch direction
        else if (dist >= extentDistance)
        {
            rb.position = reversing ? target.position : transform.position;

            reversing = !reversing;
        }
    }
}
