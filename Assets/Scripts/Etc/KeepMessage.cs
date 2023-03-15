using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepMessage : MonoBehaviour
{
    private GameObject player;
    private GameObject Board;
    public List<string> _inputMessage;
    private int idx = 0;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        Board = GameObject.FindWithTag("Board");
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

        idx++;
    }
}