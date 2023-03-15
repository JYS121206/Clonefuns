using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetMemo : MonoBehaviour
{
    [SerializeField] private Text txtMemo;
    [SerializeField] private Image imgMemo;
    [SerializeField] private Button btnMemo;
    [SerializeField] private bool board = false;
    private Color[] colors = { Color.red, Color.blue, Color.green, Color.yellow };
    private Color colorMemo;
    private UIFitfuns _UIFitfuns;

    private void Start()
    {
        if (board)
            _UIFitfuns = GameObject.FindWithTag("UI").GetComponent<UIFitfuns>();
    }

    public void SetMessage(string input)
    {
        txtMemo.text = input;

        if (!board)
            return;

        int rand = Random.Range(0, colors.Length);
        colors[rand].a = 0.25f;
        imgMemo.color = colors[rand];
        btnMemo.onClick.AddListener(() => { PopMemo(input); });
    }

    void PopMemo(string memo)
    {
        _UIFitfuns.OpenMessage(memo);
    }

    public void SetColor(float a)
    {
        colorMemo = Color.white;
        colorMemo.a = 1-a;
        txtMemo.color = colorMemo;
    }
}