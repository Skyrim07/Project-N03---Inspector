using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SKCell;

[ExecuteInEditMode]
public sealed class DynamicSprites : MonoSingleton<DynamicSprites>
{
    public DayTime dayTime;
    public Color ambientColor;
    public Color morningColor, sunsetColor, eveningColor;
    private void Update()
    {
        switch (dayTime)
        {
            case DayTime.Morning:
                ambientColor = morningColor;
                break;
            case DayTime.Sunset:
                ambientColor = sunsetColor;
                break;
            case DayTime.Evening:
                ambientColor = eveningColor;
                break;
            default:
                break;
        }
        Shader.SetGlobalColor("_AmbientColor", ambientColor);
    }
}

public enum DayTime
{
    Morning,
    Sunset,
    Evening
}
