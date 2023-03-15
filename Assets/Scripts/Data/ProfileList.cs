using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Profile
{
    public string _name;
    public string _about;
    public string _img;

    public Profile(string _name, string _about, string img)
    {
        this._name = _name;
        this._about = _about;
        this._img = img;
    }
}

public class ProfileList : MonoBehaviour
{
    #region Singletone
    private static ProfileList _instance = null;

    /// <summary> get: _instance (Singletone) </summary>
    public static ProfileList Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("@ProfileList");
                _instance = go.AddComponent<ProfileList>();

                DontDestroyOnLoad(go);
            }

            return _instance;
        }
    }
    #endregion

    private List<Profile> _profiles;
    private int _idx;

    public Profile Profile  { get { return _profiles[_idx]; } }
    public int SetProfileIdx { set { _idx = value; } }

    private void Start()
    {
        _profiles = new List<Profile>();
        _profiles.Add(new Profile("노란 목도리", "하루는 24시간이고 맥주 한 박스는 24병이다. 과연 우연일까?", "눈사람1"));
        _profiles.Add(new Profile("빨간 코", "돈을 벌어라. 너에겐 먹여살릴 고양이가 있다.", "눈사람2"));
    }
}