using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SKCell;

[ExecuteInEditMode]
public sealed class StartPP : MonoSingleton<StartPP>
{
    public float maxPPStrength = 0.6f;
    public Material ppMaterial;

    public AudioSource ppAudioSource;

    private void Start()
    {
        AdjustPPStrength(0);
        AdjustPPBlackStrength(0);
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, ppMaterial);
    }

    public void AdjustPPStrength(float value)
    {
        float ovalue = ppMaterial.GetFloat("_Strength");
        float f = 0;
        CommonUtils.StartProcedure(SKCurve.QuadraticIn, f, 0.2f, (ff) =>
        {
            float v = ovalue + (value - ovalue) * ff;
            ppMaterial.SetFloat("_Strength", v);
            ppAudioSource.pitch = 1 - v*0.5f;
        });
    }
    public void AdjustPPBlackStrength(float value)
    {
        float ovalue = ppMaterial.GetFloat("_BlackStrength");
        float f = 0;
        CommonUtils.StartProcedure(SKCurve.QuadraticIn, f, 2f, (ff) =>
        {
            float v = ovalue + (value - ovalue) * ff;
            ppMaterial.SetFloat("_BlackStrength", v);
        });
    }
}
