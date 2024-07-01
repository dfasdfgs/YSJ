using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BTN : MonoBehaviour
{
    public GameObject gameObject;
    public void StartBtn()
    {
        SceneManager.LoadScene("DAY1");
    }

    public void setting()
    {
        gameObject.SetActive(true);
    }
    public void setting2()
    {
        gameObject.SetActive(false);
    }
}
