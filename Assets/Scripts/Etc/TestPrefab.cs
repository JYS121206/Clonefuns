using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPrefab : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = Managers.Resource.Instantiate("UnityChan");
    }
}