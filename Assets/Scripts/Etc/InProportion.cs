using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InProportion : MonoBehaviour
{
    private GameObject player;
    private float distance;
    private Nullable<float> num = null;
    private float ratio;
    private float trigger;
    [SerializeField] private bool _enabled = false;
    [SerializeField] private bool Memo = false;
    [SerializeField] UISetMemo UISetMemo;
    [SerializeField] private UISetVideo[] setVideo = new UISetVideo[3];

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        distance = Vector3.Distance(transform.position, player.transform.position);
        num = SetPercent(100, distance);

        if (Memo)
        {
            UISetMemo = GetComponent<UISetMemo>();
            num = SetPercent(100, 4.0f);
        }
    }

    private void Update()
    {
        if (num != null)
            distance = Vector3.Distance(transform.position, player.transform.position);

        if (player != null)
        {
            ratio = distance * (float)num;
            if (ratio <= 60)
            {
                if (Memo)
                {
                    trigger = GetPercent(SetPercent(1, 60), ratio);
                    UISetMemo.SetColor(trigger);
                }
                else
                {
                    trigger = GetPercent(SetPercent(100, 60), ratio);
                    transform.localScale = new Vector3(2.5f - (0.015f * trigger), 2.5f - (0.015f * trigger), 2.5f - (0.015f * trigger));

                    if (ratio > 30)
                    {
                        if (!_enabled)
                            return;

                        foreach (UISetVideo btn in setVideo)
                            btn.SetButton(false);
                        _enabled = false;
                    }
                    else
                    {
                        if (_enabled)
                            return;

                        Managers.Sound.Play($"Ding", 0.5f);
                        foreach (UISetVideo btn in setVideo)
                            btn.SetButton(true);
                        _enabled = true;
                    }
                }
            }
        }
    }

    float SetPercent(float standard, float target)
    {
        float value = standard / target;
        return value;
    }
    float GetPercent(float value, float target)
    {
        float percent = value * target;
        return percent;
    }
}