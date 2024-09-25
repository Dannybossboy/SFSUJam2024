using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonMovement : TimeObject
{
    public float extentDistance = 3f;
    public float extentDuration = 1f;
    public float startOffset = 0f;

    public float extentSpeed = 5;

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
        rb.position = new Vector2(rb.position.x, offset);

    }

    public override void RewindBehavior()
    {
        float dist = (toMove.position - transform.position).magnitude;

        if (reversing)
        {
            if (dist < extentDistance)
            {
                rb.velocity = transform.right * extentSpeed;
            }
            else if (dist >= extentDistance)
            {
                reversing = false;
                rb.velocity = Vector2.zero;
            }
        }


        if (!reversing)
        {
            if (dist > .5f)
            {
                rb.velocity = -transform.right * extentSpeed;
            }
            else if (dist <= .5f)
            {
                reversing = true;
                rb.velocity = Vector2.zero;
            }


        }
        rb.position = new Vector2(rb.position.x, offset);
    }
}
