using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LavelManager : MonoBehaviour
{

    public bool quit = true;
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quitting()
    {
        if (quit)
        {
            Application.Quit();
        }
    }
}
