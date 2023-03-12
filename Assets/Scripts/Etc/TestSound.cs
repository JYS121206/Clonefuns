using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    public AudioClip clip;

    private void OnCollisionEnter(Collision collision)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(clip);
    }
}