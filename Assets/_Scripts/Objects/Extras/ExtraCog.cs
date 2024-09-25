using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static RotateMovement;

public class ExtraCog : MonoBehaviour
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
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = Vector2.zero;
    }

    public void normalBehavior()
    {
        StartCoroutine(Move(false, .5f));
    }

    public void reverseBehavior()
    {
        StartCoroutine(Move(true, .5f));
    }

    IEnumerator Move(bool reverse, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            if(reverse)
            {
                rb.angularVelocity = Mathf.Lerp(0, -500, time / duration);
            } else
            {
                rb.angularVelocity = Mathf.Lerp(0, 500, time / duration);
            }

            time += Time.deltaTime;
            yield return null;
        }

        rb.angularVelocity = 0;
    }
}
