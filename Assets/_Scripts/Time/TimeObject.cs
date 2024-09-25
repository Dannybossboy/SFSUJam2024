using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeObject : MonoBehaviour
{
    public List<TimeSlice> timeline = new List<TimeSlice>();

    [HideInInspector]
    public Rigidbody2D rb;

    private TimeManager timeManager;

    private void Start()
    {
        if(TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            this.rb = rb;
        }

        timeManager = TimeManager.instance;

        Init();
    }

    public virtual void FixedUpdate()
    {
        if (timeManager == null) return;

        if(timeManager.isRewinding)
        {
            RewindBehavior();
        } else
        {
            NormalBehavior();
        }

    }

    public virtual void Init() { }

    //Rewind time for object
    public virtual void RewindBehavior()
    {
        if (timeline.Count > 0)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;

            transform.position = timeline[0].position;
            transform.rotation = timeline[0].rotation;
            timeline.RemoveAt(0);
        }

    }

    //Record time for object
    public virtual void NormalBehavior()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
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
