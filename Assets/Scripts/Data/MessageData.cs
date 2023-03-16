using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MessageData
{
    public int idx;
    public string message;
    public Vector3 messagePosition;

    public MessageData(int idx, string message, Vector3 messagePosition)
    {
        this.idx = idx;
        this.message = message;
        this.messagePosition = messagePosition;
    }
}