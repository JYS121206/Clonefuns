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
                GameObject go = new GameObject("@Managers");
                _instance = go.AddComponent<Managers>();

                DontDestroyOnLoad(go);
            }

            return _instance;
        } 
    }
    #endregion

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    SoundManager _sound = new SoundManager();

    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }

    private void Update()
    {
        Input.OnUpdate();
    }
}