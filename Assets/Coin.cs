using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Coin : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    public void SetStatus(bool status)
    {
        indicator.SetActive(status);
    }
}
