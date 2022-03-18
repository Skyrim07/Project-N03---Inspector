using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Square : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (GetComponent<DayTimeSwitch>())
            {
                if (GetComponent<DayTimeSwitch>().active)
                {
                    ConvManager.instance.OnCollectSquare(this);
                    gameObject.SetActive(false);
                }
                return;
            }
            ConvManager.instance.OnCollectSquare(this);
            gameObject.SetActive(false);
        }
    }
}
