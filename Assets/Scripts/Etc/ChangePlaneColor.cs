using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlaneColor : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    Color color;
    float x;
    float dx;
    float speed = 0.05f;

    ColorState colorState;

    enum ColorState
    {
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
    }

    void Update()
    {
        x = player.transform.position.x;
        dx = 50 + x;

        if (dx <= 20.0f)
            colorState = ColorState.Red;
        else if (dx <= 40.0f)
            colorState = ColorState.Orange;
        else if (dx <= 60.0f)
            colorState = ColorState.Yellow;
        else if (dx <= 80.0f)
            colorState = ColorState.Green;
        else if (dx <= 100.0f)
            colorState = ColorState.Blue;

        switch (colorState)
        {
            case ColorState.Red:
                ColorUtility.TryParseHtmlString("#FA8F9F", out color);
                ColorChange(color, 0f);
                break;
            case ColorState.Orange:
                ColorUtility.TryParseHtmlString("#FFB073", out color);
                ColorChange(color, 0f);
                break;
            case ColorState.Yellow:
                ColorUtility.TryParseHtmlString("#FFD67C", out color);
                ColorChange(color, 0f);
                break;
            case ColorState.Green:
                ColorUtility.TryParseHtmlString("#AFE58E", out color);
                ColorChange(color, 0f);
                break;
            case ColorState.Blue:
                ColorUtility.TryParseHtmlString("#8DE5D4", out color);
                ColorChange(color, 0f);
                break;
            default:
                break;
        }


        void ColorChange(Color afterColor, float time)
        {
            while (time < 1)
            {
                time += speed;
                gameObject.GetComponent<Renderer>().material.color = Color.Lerp(gameObject.GetComponent<Renderer>().material.color, color, time * Time.deltaTime);
            }
        }


    }
}