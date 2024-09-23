using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonMovement : TimeObject
{
    public float extentDistance = 3f;
    public float extentDuration = 1f;

    public float extentSpeed = 5;

    public Transform toMove;

    private bool reversing;
    public override void Init()
    {
        rb = toMove.GetComponent<Rigidbody2D>();
    }

    public override void NormalBehavior()
    {
        float dist = (toMove.position - transform.position).magnitude;

        if(!reversing)
        {
            if (dist < extentDistance)
            {
                rb.velocity = transform.right * extentSpeed;
            }
            else if (dist >= extentDistance)
            {
                reversing = true;
                rb.velocity = Vector2.zero;
            }
        }


        if(reversing)
        {
            if(dist > .5f)
            {
                rb.velocity = -transform.right * extentSpeed;
            } else if(dist <= .5f)
            {
                reversing = false;
                rb.velocity = Vector2.zero;
            }

        }

        
    }

    public override void RewindBehavior()
    {
    }
}
