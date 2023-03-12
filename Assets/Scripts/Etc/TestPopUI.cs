using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPopUI : MonoBehaviour
{
    public bool onTrigger;
    public UIButton uIButton;
    private void OnTriggerEnter(Collider other)
    {
        onTrigger = true;
        uIButton.SetPop(onTrigger);
    }

    private void OnTriggerExit(Collider other)
    {
        onTrigger = false;
        uIButton.SetPop(onTrigger);
    }
}