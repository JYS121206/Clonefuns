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
        _profiles.Add(new Profile("��� �񵵸�", "�Ϸ�� 24�ð��̰� ���� �� �ڽ��� 24���̴�. ���� �쿬�ϱ�?", "�����1"));
        _profiles.Add(new Profile("���� ��", "���� �����. �ʿ��� �Կ��츱 ����̰� �ִ�.", "�����2"));
    }
}