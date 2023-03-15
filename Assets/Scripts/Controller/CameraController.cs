using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private GameObject playerMesh;

    [SerializeField] private Define.CameraMode _mode = Define.CameraMode.QuarterView;

    private Vector3 _delta = new Vector3 (0, 6.0f, -5.0f);
    private Vector3 _focusUI = new Vector3(1.0f, 2.5f, -1.0f);
    private Vector3 _focusBoard = new Vector3(-3.25f, 1.45f, 4.2f);

    public Define.CameraMode Mode { set { _mode = value; } }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerMesh = player.GetComponentsInChildren<Transform>()[2].gameObject;
    }

    void LateUpdate()
    {
        switch (_mode)
        {
            case Define.CameraMode.QuarterView:
                transform.position = Vector3.Lerp(transform.position, player.transform.position + _delta, 2.0f * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(45, 0, 0), 0.1f);
                transform.LookAt(transform.position);
                playerMesh.SetActive(true);
                break;
            case Define.CameraMode.FocusUI:
                transform.position = Vector3.Lerp(transform.position, player.transform.position + _focusUI, 2.0f * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(30, 0, 0), 0.1f);
                playerMesh.SetActive(true);
                break;
            case Define.CameraMode.FocusBoard:
                transform.position = Vector3.Lerp(transform.position, _focusBoard, 2.0f * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.1f);
                playerMesh.SetActive(false);
                break;
        }
    }
}