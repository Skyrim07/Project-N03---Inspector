using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SKCell;
public sealed class StartLogic : MonoBehaviour
{
    public bool started = false;
    float f;
    [SerializeField] SKSlider slider;
    [SerializeField] GameObject text;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !started)
        {
            started = true;
            slider.gameObject.SetActive(true);
            text.SetActive(false);

            CommonUtils.StartProcedure(SKCurve.CubicDoubleIn, f, 2.5f, (ff) =>
           {
               slider.SetValue(ff);
           }, (ff) =>
           {
               CommonUtils.InvokeAction(1f, () =>
               {
                   StartPP.instance.AdjustPPBlackStrength(1);
                   CommonUtils.InvokeAction(2f, () =>
                   {
                       UnityEngine.SceneManagement.SceneManager.LoadScene("Level01");
                   });
               });
           });
            StartPP.instance.AdjustPPStrength(1);
        }
    }

}
