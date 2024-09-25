using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitSound : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] hitSounds;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        source.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
    }
}
