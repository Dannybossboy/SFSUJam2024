using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonMovement : TimeObject
{
    public bool interactable = false;
    public bool inversed = false;

    public pistonType type = pistonType.oneshot;

    public float extentDistance = 3f;
    public float extentDuration = 1f;
    public float startOffset = 0f;

    public float extentSpeed = 5;


    public Transform target;

    public Transform toMove;

    public enum pistonType
    {
        oneshot,
        pingpong
    }

    private bool reversing;
    private float offset;
    private bool callingInverse;
    public bool interacted;
    public override void Init()
    {
        rb = toMove.GetComponent<Rigidbody2D>();

        offset = rb.position.y;

        switch(type)
        {
            case pistonType.oneshot:
                break;
            case pistonType.pingpong:
                break;
        }

        toMove.position = Vector3.Lerp(transform.position, transform.position + transform.right * extentDistance, startOffset);

    }

    public override void FixedUpdate()
    {
        if (interactable)
        {
            if (interacted)
            {
                if(inversed)
                {
                    RewindBehavior();
                } else
                {
                    NormalBehavior();
                }

            }
            else
            {
                if(inversed)
                {
                    NormalBehavior();
                } else
                {
                    RewindBehavior();
                }

            }
        } else
        {
            base.FixedUpdate();
        }
    }

    public override void NormalBehavior()
    {
        Debug.Log("Normal pills");
        if (inversed && !callingInverse)
        {
            callingInverse = true;
            RewindBehavior();
            return;
        }

        callingInverse = false;

        switch(type)
        {
            case pistonType.oneshot:
                if ((toMove.position - transform.position).magnitude < extentDistance)
                {
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    rb.MovePosition(rb.position + (Vector2)transform.right * extentDistance * Time.fixedDeltaTime);
                } else if((toMove.position - transform.position).magnitude >= extentDistance)
                {
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    toMove.position = target.position;
                }
                break;
            case pistonType.pingpong:

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
                break;
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
        Debug.Log("Reverse");

        if (inversed && !callingInverse)
        {
            callingInverse = true;
            NormalBehavior();
            return;
        }

        callingInverse = false;

        switch (type)
        {
            case pistonType.oneshot:
                if ((target.position - toMove.position).magnitude < extentDistance)
                {
                    rb.MovePosition(rb.position + (Vector2)transform.right * -extentDistance * Time.fixedDeltaTime);
                } else if ((target.position - toMove.position).magnitude >= extentDistance)
                {
                    toMove.position = transform.position;
                }
                break;
            case pistonType.pingpong:

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
                break;
        }

    }

    public void SetActive(bool active)
    {
        if(active)
        {
            interacted = true;

        } else
        {
            interacted = false;
        }
    }
}
