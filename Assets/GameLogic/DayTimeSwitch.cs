using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SKCell;

public sealed class DayTimeSwitch : MonoBehaviour
{
    public DayTime dayTime;

    private DayTime curDayTime;

    private SpriteRenderer sr;
    public GameObject interactiveGO;
    public bool active = false;
    private void Start()
    {
        sr=GetComponent<SpriteRenderer>();  
    }
    private void Update()
    {
        curDayTime = DynamicSprites.instance.dayTime;
        if(sr)
        sr.enabled= active=dayTime == curDayTime;

        CommonUtils.SetActiveEfficiently(interactiveGO, dayTime == curDayTime);
    }
}
