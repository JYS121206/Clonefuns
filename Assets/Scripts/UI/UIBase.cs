using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    #region EnumOption

    protected enum GameObjects
    {
        ButtonGroup,
        Introduce,
        Memo,
        Pop,
        Message,
        Video,
        PlayBar,
    }

    protected enum Buttons
    {
        btnHome,
        btnIntro,
        btnMute,
        btnNotion,
        btnMemo,
        btnCloseIntro,
        btnCloseMemo,
        btnSaveMemo,
        btnExit,
        btnPlay,
        btnBackward,
        btnForward,
        btnCloseVideo,
        btnFull,
    }
    protected enum Images
    {
        imgProfile,
    }

    protected enum Texts
    {
        txtIntro,
        txtName,
        txtAbout,
        txtMessage,
        txtPlayTime,
    }

    #endregion

    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to bind ({names[i]})");
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    protected GameObject GetGameObject(GameObjects gameObjects) { return Get<GameObject>((int)gameObjects); }
    protected Button GetButton(Buttons buttons) { return Get<Button>((int)buttons); }
    protected Image GetImage(Images images) { return Get<Image>((int)images); }
    protected Text GetText(Texts texts) { return Get<Text>((int)texts); }
}