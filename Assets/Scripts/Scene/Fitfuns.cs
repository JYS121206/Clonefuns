using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEditor.VersionControl;

public class Fitfuns : MonoBehaviour
{
    private GameObject player;
    private GameObject ui;

    private SaveData saveData = new SaveData();
    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";
    [SerializeField]
    private KeepMessage messages;

    void Awake ()
    {
        if (FindObjectOfType<PlayerController>() == false)
        {
            player = Managers.Resource.Instantiate("UnityChan");
            player.name = "Player";
        }

        if (FindObjectOfType<UIFitfuns>() == false)
            Managers.UI.OpenUI("UIFitfuns");

        if (FindObjectOfType<EventSystem>() == false)
            Managers.UI.SetEventSystem();

        SAVE_DATA_DIRECTORY = Application.dataPath + "/Save/";
        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
    }

    private void Start()
    {
        ui = GameObject.FindWithTag("UI");
        messages = GameObject.FindWithTag("UI").GetComponent<KeepMessage>();
        LoadData();
    }

    void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        List<MessageData> messageDatas = messages.GetMessageDatas();


        saveData.saveMemoDatas = messageDatas;

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);
        if (json != null)
        {
            Debug.Log("저장 완료");
            Debug.Log($"{json}");
        }
        else
            Debug.Log("저장된 데이터가 없습니다.");
    }

    void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);
            List<MessageData> messageDatas;

            for (int i = 0; i < saveData.saveMemoDatas.Count; i++)
            {
                messages.LoadToList(saveData.saveMemoDatas[i].idx, saveData.saveMemoDatas[i].message, saveData.saveMemoDatas[i].messagePosition);
            }
            Debug.Log("로드 완료");
        }
        else
            Debug.Log("불러올 데이터가 없습니다.");

    }
}