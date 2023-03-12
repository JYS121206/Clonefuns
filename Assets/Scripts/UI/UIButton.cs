using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : UIBase
{
    ResourceManager ResourceManager;
    bool isMute = false;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject buttons;
    [SerializeField] private AudioSource bgm;

    void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));


        GetImage(Images.imgIntro).gameObject.SetActive(false);
        GetImage(Images.imgMemo).gameObject.SetActive(false);
        GetText(Texts.txtPop).gameObject.SetActive(false);
        GetButton(Buttons.btnHome).onClick.AddListener(OnClickHome);
        GetButton(Buttons.btnNotion).onClick.AddListener(OnClickNotion);
        GetButton(Buttons.btnMute).onClick.AddListener(OnClickMute);
        GetButton(Buttons.btnIntro).onClick.AddListener(OnClickIntro);
        GetButton(Buttons.btnCloseIntro).onClick.AddListener(OnClickCloseIntro);
        GetButton(Buttons.btnMemo).onClick.AddListener(OnClickMemo);
        GetButton(Buttons.btnCloseMemo).onClick.AddListener(OnClickCloseMemo);
    }
    void OnClickIntro()
    {
        GetImage(Images.imgIntro).gameObject.SetActive(true);
    }
    void OnClickCloseIntro()
    {
        GetImage(Images.imgIntro).gameObject.SetActive(false);
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
        playerController.ReSetPos();
    }
    void OnClickNotion()
    {
        Application.OpenURL("https://fitfuns.notion.site/finance-in-the-funs-c1fceb332af740f4af5fe16564ea5eaf");
    }
    void OnClickMemo()
    {
        buttons.SetActive(false);
        GetImage(Images.imgMemo).gameObject.SetActive(true);
    }
    void OnClickCloseMemo()
    {
        GetImage(Images.imgMemo).gameObject.SetActive(false);
        buttons.SetActive(true);
    }
    public void SetPop(bool test)
    {
        if (test)
        {
            GetText(Texts.txtPop).gameObject.SetActive(true);
            buttons.SetActive(false);
        }
        else
        {
            GetText(Texts.txtPop).gameObject.SetActive(false);
            buttons.SetActive(true);
        }
    }
}