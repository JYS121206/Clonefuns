using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoSound : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Managers.Sound.Play($"Piano{gameObject.name}", 0.1f);
    }
}