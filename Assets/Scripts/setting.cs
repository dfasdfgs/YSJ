using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class setting : MonoBehaviour
{
    public GameObject satting;
    public bool issatting;

    private void Start()
    {
        issatting = true;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (issatting)
            {
                satting.SetActive(true);
                issatting = false;
            }
            else 
            {
                satting.SetActive(false);
                issatting =true;
            }
        }
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public void MainScene()
    {
        SceneManager.LoadScene("Strat");
    }

}
