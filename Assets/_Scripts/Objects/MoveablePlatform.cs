using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatform : MonoBehaviour
{
    public float lerpSpeed = 10f;

    public Vector3 end;
    private Vector3 start;


    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
    }


    public void toggle(bool active)
    {
        if(active)
        {
            StartCoroutine(Move(start, end, .5f));
        } else
        {
            StartCoroutine(Move(end, start, .5f));
        }
    }

    IEnumerator Move(Vector3 start, Vector3 end, float duration)
    {
        float time = 0;

        while(time < duration)
        {
            transform.position = Vector3.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
    }
}
