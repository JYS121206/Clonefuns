using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class KeepMessage : MonoBehaviour
{
    private GameObject player;
    private GameObject Board;
    private int idx = 0;

    [SerializeField] public List<string> _inputMessage;
    [SerializeField] public List<MessageData> messageDatas = new List<MessageData>();

    private void Awake()
    {
        Board = GameObject.FindWithTag("Board");
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void AddMessage(string input)
    {
        _inputMessage.Add(input);

        GameObject memo = Managers.Resource.Instantiate("Memo");
        memo.GetComponent<UISetMemo>().SetMessage(_inputMessage[idx]);
        memo.transform.position = player.transform.position;

        GameObject objMemo = Managers.Resource.Instantiate("objMemo");
        objMemo.GetComponent<UISetMemo>().SetMessage(_inputMessage[idx]);
        objMemo.transform.parent = Board.transform;
        float x = Random.Range(-0.4f, 0.4f);
        float y = Random.Range(-0.35f, 0.35f);
        objMemo.transform.localPosition = new Vector3(x, y, -0.8f);

        messageDatas.Add(new MessageData(idx, input, memo.transform.position));
        idx++;
    }

    public List<MessageData> GetMessageDatas() { return messageDatas; }

    public void LoadToList(int idx, string message, Vector3 messagePosition)
    {
        _inputMessage.Add(message);

        GameObject memo = Managers.Resource.Instantiate("Memo");
        memo.GetComponent<UISetMemo>().SetMessage(_inputMessage[idx]);
        memo.transform.position = messagePosition;

        GameObject objMemo = Managers.Resource.Instantiate("objMemo");
        objMemo.GetComponent<UISetMemo>().SetMessage(_inputMessage[idx]);
        objMemo.transform.parent = Board.transform;
        float x = Random.Range(-0.4f, 0.4f);
        float y = Random.Range(-0.35f, 0.35f);
        objMemo.transform.localPosition = new Vector3(x, y, -0.8f);

        messageDatas.Add(new MessageData(idx, message, memo.transform.position));
    }
}