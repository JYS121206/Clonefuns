using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fitfuns : MonoBehaviour
{
    private GameObject player;

    void Awake ()
    {
        if (GameObject.FindWithTag("Player") != null)
        { player = GameObject.FindWithTag("Player"); }
        else
        { player = Managers.Resource.Instantiate("UnityChan"); }

        player.name = "Player";

        if (FindObjectOfType<EventSystem>() == false)
        { Managers.UI.SetEventSystem(); }

        if (GameObject.FindWithTag("UI") == null)
        { Managers.UI.OpenUI("UIFitfuns"); }
    }
}