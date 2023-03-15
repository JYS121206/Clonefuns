using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UISetVideo : MonoBehaviour
{
    [SerializeField] private VideoPlayer _player;
    [SerializeField] private RawImage _texture;
    [SerializeField] private UIFitfuns UIMain;
    [SerializeField] private Button btnVideo;
    void Start()
    {
        UIMain = GameObject.FindWithTag("UI").GetComponent<UIFitfuns>();
        _player = GetComponentInChildren<VideoPlayer>();
        _texture = GetComponentInChildren<RawImage>();
        btnVideo = _texture.GetComponent<Button>();
        btnVideo.onClick.AddListener(SetThisVideo);
        SetButton(false);
    }

    void SetThisVideo()
    {
        Managers.Sound.Play($"Pop2", 0.3f);
        UIMain.SetUIVideo(_texture, _player);
    }

    public void SetButton(bool isTrue)
    { 
        btnVideo.enabled = isTrue;
        if(isTrue) _player.Play();
        else _player.Stop();
    }
}