using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimAudio : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] footsteps;
    public void Footstep()
    {
        source.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)]);
    }
}
