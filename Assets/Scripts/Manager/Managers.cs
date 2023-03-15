using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    #region Singletone
    private static Managers _instance = null;

    /// <summary> get: _instance (Singletone) </summary>
    static Managers Instance
    { 
        get 
        {
            if (_instance == null)
            {
                GameObject go = GameObject.Find("@Managers");
                if (go == null)
                {
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<Managers>();
                }
                DontDestroyOnLoad(go);
                _instance = go.GetComponent<Managers>();
                _instance._sound.Init();
            }

            return _instance;
        } 
    }
    #endregion

    ResourceManager _resource = new ResourceManager();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();

    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
}