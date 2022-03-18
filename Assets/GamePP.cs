using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SKCell;

[ExecuteInEditMode]
public sealed class GamePP : MonoSingleton<GamePP>
{
    public float maxPPStrength = 0.6f;
    public Material ppMaterial, splitMaterial;

    RenderTexture rt;

    private void Start()
    {
        //AdjustPPStrength(0);
        //AdjustPPSplitStrength(0);
        //AdjustPPSplitBlackStrength(0);
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        rt = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
        Graphics.Blit(source, rt, ppMaterial);
        Graphics.Blit(rt, destination, splitMaterial);
        rt.Release();
    }

    public void AdjustPPStrength(float value)
    {
        float ovalue = ppMaterial.GetFloat("_Strength");
        float f = 0;
        CommonUtils.StartProcedure(SKCurve.QuadraticIn, f, 0.5f, (ff) =>
        {
            float v = ovalue + (value - ovalue) * ff;
            ppMaterial.SetFloat("_Strength", v);
            ConvManager.instance.bgmAudio.pitch = 1 - Mathf.Sin(v*0.3f);
        });
    }
    public void AdjustPPSplitStrength(float value)
    {
        float ovalue = splitMaterial.GetFloat("_Strength");
        float f = 0;
        CommonUtils.StartProcedure(SKCurve.QuadraticIn, f, 0.5f, (ff) =>
        {
            float v = ovalue + (value - ovalue) * ff;
            splitMaterial.SetFloat("_Strength", v);
        });
    }
    public void AdjustPPSplitBlackStrength(float value)
    {
        float ovalue = splitMaterial.GetFloat("_BlackStrength");
        float f = 0;
        CommonUtils.StartProcedure(SKCurve.QuadraticIn, f, 2f, (ff) =>
        {
            float v = ovalue + (value - ovalue) * ff;
            splitMaterial.SetFloat("_BlackStrength", v);
        });
    }
}
