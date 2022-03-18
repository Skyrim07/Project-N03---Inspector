using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ObjectTag : MonoBehaviour
{
    public int level;
    public string[] tags;
    public string lookat;
    public bool hasKey;

    private bool hasRemoved = false;

    public GameObject objectAppearWhenRemove;
    public void AddTag()
    {
        ConvManager.instance.tagList.Add(this);
    }

    public void Remove()
    {
        if(objectAppearWhenRemove && !hasRemoved)
            objectAppearWhenRemove.SetActive(true);
        hasRemoved = true;
        gameObject.SetActive(false);
    }
}
