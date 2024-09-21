using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour
{
    public List<TimeSlice> timeline = new List<TimeSlice>();


    private bool isRewinding;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRewinding = !isRewinding;
        }
    }

    private void FixedUpdate()
    {
        if(isRewinding)
        {
            Rewind();
        } else
        {
            Record();
        }

    }

    private void Rewind()
    {
        if (timeline.Count > 0)
        {
            transform.position = timeline[0].position;
            transform.rotation = timeline[0].rotation;
            timeline.RemoveAt(0);
        }

    }

    private void Record()
    {
        timeline.Insert(0, new TimeSlice(transform.position, transform.rotation));
    }
}

public class TimeSlice
{
    public Vector3 position;
    public Quaternion rotation;

    public TimeSlice(Vector3 pos, Quaternion rot)
    {
        position = pos;
        rotation = rot;
    }
}
