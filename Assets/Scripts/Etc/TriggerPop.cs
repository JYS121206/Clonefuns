using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPop : MonoBehaviour
{
    public UIFitfuns uIMain;
    private CameraController cameraController;

    [SerializeField] private bool board = false;
    [SerializeField] private int idx = 0;
    public bool onTrigger;

    private void Start()
    {
        uIMain = GameObject.FindWithTag("UI").GetComponent<UIFitfuns>();
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        onTrigger = true;

        if (board)
        {
            Managers.Sound.Play($"Paper", 0.7f);
            cameraController.Mode = Define.CameraMode.FocusBoard;
            uIMain.CloseBtns(onTrigger);
            return;
        }

        Managers.Sound.Play( $"Pop", 0.5f);
        cameraController.Mode = Define.CameraMode.FocusUI;
        uIMain.SetPop(onTrigger, idx);
    }

    private void OnTriggerExit(Collider other)
    {
        onTrigger = false;
        cameraController.Mode = Define.CameraMode.QuarterView;

        if (board)
        {
            uIMain.CloseBtns(onTrigger);
            return;
        }

        uIMain.SetPop(onTrigger, idx);
    }
}