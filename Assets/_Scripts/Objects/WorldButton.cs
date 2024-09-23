using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldButton : MonoBehaviour
{
    public UnityEvent pressEvent;
    public UnityEvent releaseEvent;

    public Transform button;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb) || coll.gameObject.tag == "Obstacle")
        {
            pressEvent?.Invoke();
            moveButton(true);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb) || coll.gameObject.tag == "Obstacle")
        {
            releaseEvent?.Invoke();
            moveButton(false);
        }
    }

    public void moveButton(bool pressed)
    {
        if(pressed)
        {
            button.transform.position -= transform.rotation * new Vector3(0,.5f, 0);
        } else
        {
            button.transform.position += transform.rotation * new Vector3(0, .5f, 0);
        }
    }

    private void Update()
    {
        
    }

}
