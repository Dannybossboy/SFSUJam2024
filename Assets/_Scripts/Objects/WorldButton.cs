using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldButton : MonoBehaviour
{
    public bool oneShot;
    public UnityEvent pressEvent;
    public UnityEvent releaseEvent;

    public Transform button;
    public SpriteRenderer buttonSprite;
    public Sprite red;
    public Sprite green;

    public AudioSource source;
    public AudioClip press;
    public AudioClip release;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb) || coll.gameObject.tag == "Obstacle")
        {
            pressEvent?.Invoke();
            moveButton(true);
            source.PlayOneShot(press);
            buttonSprite.sprite = green;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb) || coll.gameObject.tag == "Obstacle")
        {
            if (oneShot) return;

            releaseEvent?.Invoke();
            moveButton(false);
            source.PlayOneShot(release);
            buttonSprite.sprite = red;
        }
    }

    //Move Visual
    public void moveButton(bool pressed)
    {
        if(pressed)
        {
            button.transform.position -= transform.rotation * new Vector3(0,.1f, 0);
        } else
        {
            button.transform.position += transform.rotation * new Vector3(0, .1f, 0);
        }
    }

}
