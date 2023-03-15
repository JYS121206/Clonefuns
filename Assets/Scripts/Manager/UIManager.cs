using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager
{
    public void SetEventSystem()
    {
        GameObject go = new GameObject("@EventSystem");
        go.AddComponent<EventSystem>();
        go.AddComponent<StandaloneInputModule>();
    }

    public Dictionary<string, GameObject> uiList = new Dictionary<string, GameObject>();

    public void OpenUI(string uiName)
    {
        if (uiList.ContainsKey(uiName) == false)
        {
            GameObject uiObject = Managers.Resource.Instantiate("UI", uiName);
            uiList.Add(uiName, uiObject);
        }
        else
            uiList[uiName].SetActive(true);
    }

    public void CloseUI(string uiName)
    {
        if (uiList.ContainsKey(uiName))
            uiList[uiName].SetActive(false);
    }

    public void ClearList()
    {
        uiList.Clear();
    }
}