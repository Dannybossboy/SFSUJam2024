using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveablePlatform : MonoBehaviour
{
    public Transform toMove;
    public float lerpSpeed = 10f;

    public Vector3 end;
    private Vector3 start;

    public UnityEvent active;
    public UnityEvent notActive;


    // Start is called before the first frame update
    void Start()
    {
        start = toMove.position;
    }


    public void toggle(bool active)
    {
        if(active)
        {
            StartCoroutine(Move(start, end, .5f));
            this.active.Invoke();
        } else
        {
            StartCoroutine(Move(end, start, .5f));
            this.notActive.Invoke();
        }
    }

    IEnumerator Move(Vector3 start, Vector3 end, float duration)
    {
        float time = 0;
        float dist = Vector3.Distance(start, end);
        float offset = Vector3.Distance(start, toMove.position);
        Debug.Log(offset + " " + dist);
        offset = offset / dist;
        Debug.Log(offset);

        while(time < duration)
        {
            toMove.position = Vector3.Lerp(start, end, (time / duration) + offset);
            time += Time.deltaTime;
            yield return null;
        }

        toMove.position = end;
    }
}
