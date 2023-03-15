using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlaneColor : MonoBehaviour
{
    [SerializeField] private Gradient gradient;

    private GameObject player = null;
    private float x;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        x = player.transform.position.x;
        var curColor = (50 + x) / 100;

        gameObject.GetComponent<Renderer>().material.color = gradient.Evaluate(curColor);
    }
}