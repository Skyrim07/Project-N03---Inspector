using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SKCell;
public sealed class FlowManager : MonoSingleton<FlowManager>
{
    public int startLevel = 1;
    public int curLevel = 1;
    public int[] levelCoinCount;
    private string[] levelTexts = new string[] { "", "Trying to remember...", "When the sun sets.", "Lift the curse", "Break the reality"};

    private void Start()
    {
        levelCoinCount = new int[] { 0, 7, 6, 5, 1};
        LoadLevel(startLevel);
    }
    public void LoadLevel(int level)
    {
        curLevel = level;

        ConvManager.instance.LoadScene(level);  
        ConvManager.instance.LoadSquares(levelCoinCount[level]);
        ConvManager.instance.LoadLevelText(levelTexts[level]);
        ConvManager.instance.LoadCharacter();
    }
    public void NextLevel()
    {
        LoadLevel(curLevel == levelCoinCount.Length - 1 ? 1 : curLevel + 1);
    }
    private static void SetBlack(int ending)
    {
        CommonUtils.InvokeAction(1.5f, () =>
        {
            GamePP.instance.AdjustPPSplitBlackStrength(1);
            CommonUtils.InvokeAction(2f, () =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Ending"+ending.ToString("d2"));
            });
        });
    }
    public void Ending1()
    {
        SetBlack(1);
    }

    public void Ending2()
    {
        SetBlack(2);
    }

}
