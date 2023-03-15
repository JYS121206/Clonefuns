using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMat : MonoBehaviour
{
    [SerializeField] private bool isWhite = true;
    private Material matEnter;
    private Material matDefault;
    private Color color = Color.white;
    private string[] colors = { "#EA5353", "#F89E3C", "#FFE578", "#B6E75D", "#78F6C8", "#75CEFF", "#BD86EF", "#EE86D8" };

    private void Start()
    {
        if (isWhite)
        {
            matDefault = Managers.Resource.Load<Material>("Materials/matWhite");
            matEnter = Managers.Resource.Load<Material>("Materials/matRandom");
        }
        else
        {
            matDefault = Managers.Resource.Load<Material>("Materials/matBlack");
            matEnter = Managers.Resource.Load<Material>("Materials/matRandom2");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        matEnter.color = SetRandMat();
        gameObject.GetComponent<MeshRenderer>().material = matEnter;
    }

    private void OnCollisionExit(Collision collision)
    {
        gameObject.GetComponent<MeshRenderer>().material = matDefault;
    }

    private Color SetRandMat()
    {
        int ran = Random.Range(0, colors.Length);
        var colorCode = colors[ran];
        ColorUtility.TryParseHtmlString(colorCode, out color);
        return color;
    }
}