using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldButton : MonoBehaviour
{
    public UnityEvent pressEvent; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player" || collision.collider.tag == "Obstacle")
        {
            print("It's a hit!");
        }   
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.name == "Player" || collision.collider.tag == "Obstacle")
        {
            print("Goodbye!");
        }
    }

}
