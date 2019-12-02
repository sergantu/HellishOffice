
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDMainMenu : MonoBehaviour
{
    [SerializeField] Button ContinueGame;

    private void Start()
    {
        JSONSave.Instance.LoadLanguage();



        ContinueGame.interactable = JSONSave.Instance.LoadIsStartedGame();
        Time.timeScale = 1.0f;
    }

    public void ContinueLevel()
    {
        SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
    }

    public void RestartLevel()
    {
        JSONSave.Instance.SaveStartedGame(false);
        SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
