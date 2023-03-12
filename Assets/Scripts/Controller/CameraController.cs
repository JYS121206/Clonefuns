using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Define.CameraMode _mode = Define.CameraMode.QuarterView;
    [SerializeField] private Vector3 _delta = new Vector3 (0, 6.0f, -5.0f);
    private Vector3 _focus = new Vector3(1.0f, 2.5f, -1.0f);
    [SerializeField] private GameObject player = null;
    public TestPopUI testPop;

    void LateUpdate()
    {
        if (_mode == Define.CameraMode.QuarterView)
        {
            if (testPop.onTrigger)
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position + _focus, 2.0f * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(30, 0, 0), 0.1f);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position + _delta, 2.0f * Time.deltaTime);
                //transform.position = player.transform.position + _delta;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(45, 0, 0), 0.1f);
                transform.LookAt(transform.position);
            }


        }
    }
}