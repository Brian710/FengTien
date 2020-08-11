using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinYanGame.Core;

public class ThreeDBtnManager : MonoBehaviour
{
    public Material mat;
    public Color hovercolor;
    public Color defaultColor;

    public void ChangMatEmisColor(bool value)
    {
        if (mat)
        {
            Color color;
            color = value ? hovercolor : defaultColor;
            mat.SetColor("_EmissionColor", color);
        }
    }

    public void ChangMatColor(bool value)
    {
        if (mat)
        {
            Color color;
            color = value ? hovercolor : defaultColor;
            mat.color = color;
        }
    }
}
