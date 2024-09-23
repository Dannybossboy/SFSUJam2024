using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public Transform levelCamPos;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(Move(cam.transform.position, levelCamPos.position, 1f));
        }
    }

    IEnumerator Move(Vector3 start, Vector3 end, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            cam.transform.position = Vector3.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        cam.transform.position = end;
    }
}
