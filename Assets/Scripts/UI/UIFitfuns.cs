using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIFitfuns : UIBase
{
    [SerializeField] private AudioSource bgm;
    [SerializeField] private VideoPlayer videoPlayer;

    private bool isMute = false;
    private bool isFull = false;

    private PlayerController playerController;
    private KeepMessage inputMessage;
    private InputField inputField;
    private ProfileList profileList;
    private RawImage myVideo;
    private Slider playBar;

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        profileList = ProfileList.Instance;
        inputField = GetComponentInChildren<InputField>();
        inputMessage = GetComponent<KeepMessage>();
        bgm = GetComponent<AudioSource>();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));

        myVideo = GetGameObject(GameObjects.Video).GetComponent<RawImage>();
        playBar = GetGameObject(GameObjects.PlayBar).GetComponent<Slider>();

        GetGameObject(GameObjects.Introduce).SetActive(false);
        GetGameObject(GameObjects.Memo).SetActive(false);
        GetGameObject(GameObjects.Pop).SetActive(false);
        GetGameObject(GameObjects.Message).SetActive(false);
        GetGameObject(GameObjects.Video).SetActive(false);

        GetButton(Buttons.btnHome).onClick.AddListener(OnClickHome);
        GetButton(Buttons.btnNotion).onClick.AddListener(OnClickNotion);
        GetButton(Buttons.btnMute).onClick.AddListener(OnClickMute);
        GetButton(Buttons.btnIntro).onClick.AddListener(OnClickIntro);
        GetButton(Buttons.btnCloseIntro).onClick.AddListener(OnClickCloseIntro);
        GetButton(Buttons.btnMemo).onClick.AddListener(OnClickMemo);
        GetButton(Buttons.btnCloseMemo).onClick.AddListener(OnClickCloseMemo);
        GetButton(Buttons.btnSaveMemo).onClick.AddListener(OnSaveMemo);
        GetButton(Buttons.btnExit).onClick.AddListener(OnCloseMessage); 

        GetButton(Buttons.btnPlay).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Sprite/imgPause");
        GetButton(Buttons.btnForward).onClick.AddListener(() => { ChangeVideoTime(5); });
        GetButton(Buttons.btnBackward).onClick.AddListener(() => { ChangeVideoTime(-5); });
        GetButton(Buttons.btnPlay).onClick.AddListener(PlayVideo);
        GetButton(Buttons.btnCloseVideo).onClick.AddListener(CloseVideo);
        GetButton(Buttons.btnFull).onClick.AddListener(SetFullScreen);
        videoPlayer.loopPointReached += CheckOver;
    }
    private void Update()
    {
        EnterSaveMemo();

        if (!GetGameObject(GameObjects.Video).activeSelf)
            return;

        playBar.value = (float)videoPlayer.time;
        GetText(Texts.txtPlayTime).text = $"{((int)videoPlayer.time) / 60}:{((int)videoPlayer.time) % 60}/{((int)videoPlayer.clip.length) / 60}:{((int)videoPlayer.clip.length) % 60}";

    }
    void OnClickIntro()
    {
        GetGameObject(GameObjects.Introduce).SetActive(true);
    }
    void OnClickCloseIntro()
    {
        GetGameObject(GameObjects.Introduce).SetActive(false);
    }
    void OnClickMute()
    {
        if (!isMute)
        {
            GetButton(Buttons.btnMute).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>($"Sprite/imgMute2");
            bgm.Pause();
            isMute = true;
        }
        else
        {
            GetButton(Buttons.btnMute).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>($"Sprite/imgMute");
            bgm.Play();
            isMute = false;
        }
    }
    void OnClickHome()
    {
        playerController.ResetPos();
    }
    void OnClickNotion()
    {
        Application.OpenURL("https://fitfuns.notion.site/finance-in-the-funs-c1fceb332af740f4af5fe16564ea5eaf");
    }
    void OnClickMemo()
    {
        GetGameObject(GameObjects.ButtonGroup).SetActive(false);
        GetGameObject(GameObjects.Memo).SetActive(true);
    }
    void OnClickCloseMemo()
    {
        GetGameObject(GameObjects.Memo).SetActive(false);
        GetGameObject(GameObjects.ButtonGroup).SetActive(true);
    }
    void OnSaveMemo()
    {
        if (inputField.text == "")
        {
            Managers.Sound.Play($"SCH", 0.7f);
            Debug.Log($"메시지를 입력하세요.");
            return;
        }

        Managers.Sound.Play($"Bell", 0.7f);
        inputMessage.AddMessage(inputField.text);
        inputField.text = "";
        OnClickCloseMemo();
    }
    void EnterSaveMemo()
    {
        if (!GetGameObject(GameObjects.Memo).activeSelf)
            return;

        if(Input.GetKeyDown(KeyCode.Return))
            OnSaveMemo();
    }
    public void SetPop(bool test, int idx)
    {
        profileList.SetProfileIdx = idx;
        GetGameObject(GameObjects.Pop).SetActive(test);
        GetText(Texts.txtName).text = profileList.Profile._name;
        GetText(Texts.txtAbout).text = profileList.Profile._about;
        GetImage(Images.imgProfile).sprite = Managers.Resource.Load<Sprite>($"Sprite/{profileList.Profile._img}");
        GetImage(Images.imgProfile).preserveAspect = true;
        CloseBtns(test);
    }
    public void CloseBtns(bool test)
    {
        GetGameObject(GameObjects.ButtonGroup).SetActive(!test);
    }
    void OnCloseMessage()
    {
        GetGameObject(GameObjects.Message).SetActive(false);
    }
    public void OpenMessage(string memo)
    {
        Managers.Sound.Play($"Bell", 0.7f);
        GetGameObject(GameObjects.Message).SetActive(true);
        GetText(Texts.txtMessage).text = memo;

    }
    public void SetUIVideo(RawImage _texture, VideoPlayer _player)
    {
        GetGameObject(GameObjects.Video).SetActive(true);
        myVideo.texture = _texture.texture;
        videoPlayer = _player;
        playBar.maxValue = (float)videoPlayer.clip.length;
        videoPlayer.time = 0;
        if (!videoPlayer.isPlaying)
            videoPlayer.Play();
        videoPlayer.isLooping = false;
        videoPlayer.SetDirectAudioMute(0, false);

        if (!isMute)
        {
            GetButton(Buttons.btnMute).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>($"Sprite/imgMute2");
            bgm.Pause();
        }

    }
    void PlayVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            GetButton(Buttons.btnPlay).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Sprite/imgplay");
        }
        else
        {
            videoPlayer.Play();
            GetButton(Buttons.btnPlay).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Sprite/imgPause");
        }
    }
    void ChangeVideoTime(float time)
    {
        var curTime = videoPlayer.time;

        videoPlayer.time += time;
        curTime += time;

        if (curTime <= 0.1f)
            videoPlayer.time = 0;
    }
    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        GetButton(Buttons.btnPlay).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Sprite/imgplay");
    }
    void CloseVideo()
    {
        GetGameObject(GameObjects.Video).gameObject.SetActive(false);
        videoPlayer.isLooping = true;
        videoPlayer.SetDirectAudioMute(0, true);
        if (!videoPlayer.isPlaying)
            videoPlayer.Play();

        if (!isMute)
        {
            GetButton(Buttons.btnMute).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>($"Sprite/imgMute");
            bgm.Play();
        }
    }
    void SetFullScreen()
    {
        if (!isFull)
        {
            myVideo.rectTransform.offsetMin = new Vector2(0, 0);
            myVideo.rectTransform.offsetMax = new Vector2(0, 0);
            isFull = true;
        }
        else
        {
            myVideo.rectTransform.offsetMin = new Vector2(400, 225);
            myVideo.rectTransform.offsetMax = new Vector2(-400, -225);
            isFull = false;
        }
    }
}