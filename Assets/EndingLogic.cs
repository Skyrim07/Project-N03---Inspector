using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EndingLogic : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        }
    }
}
