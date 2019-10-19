using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDMainMenu : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
