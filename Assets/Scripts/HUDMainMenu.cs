
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDMainMenu : MonoBehaviour
{
    enum Languages
    {
        RU,
        EN
    }

    [SerializeField] Button ContinueGame;
    [SerializeField] Text languageText;

    private Languages currentLanguage = Languages.RU;
    string pathsgc;
    string pathspc;
    string pathsic;
    string pathshc;

    private void Start()
    {

#if UNITY_ANDROID && !UNITY_EDITOR
        pathsgc = Path.Combine(Application.persistentDataPath, "SaveGame.json");
        pathspc = Path.Combine(Application.persistentDataPath, "SavePlayer.json");
        pathsic = Path.Combine(Application.persistentDataPath, "SaveInventory.json");
        pathshc = Path.Combine(Application.persistentDataPath, "SaveHud.json");
#else
        pathsgc = Path.Combine(Application.dataPath, "SaveGame.json");
        pathspc = Path.Combine(Application.dataPath, "SavePlayer.json");
        pathsic = Path.Combine(Application.dataPath, "SaveInventory.json");
        pathshc = Path.Combine(Application.dataPath, "SaveHud.json");
#endif
        JSONSave.Instance.LoadLanguage();
        currentLanguage = (Languages)BbtStrings.language;
        languageText.text = currentLanguage.ToString();

        ContinueGame.interactable = File.Exists(pathsgc);
        Time.timeScale = 1.0f;
    }

    public void ContinueLevel()
    {
        SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
    }

    public void RestartLevel()
    {
        if (File.Exists(pathsgc))
        {
            File.Delete(pathsgc);
        }

        if (File.Exists(pathspc))
        {
            File.Delete(pathspc);
        }

        if (File.Exists(pathsic))
        {
            File.Delete(pathsic);
        }

        if (File.Exists(pathshc))
        {
            File.Delete(pathshc);
        }
        
        SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void SwitchLanguage()
    {
        currentLanguage++;

        if ( (int)currentLanguage >= Enum.GetNames(typeof(Languages)).Length)
        {
            currentLanguage = Languages.RU;
        }

        BbtStrings.language = (int)currentLanguage;
        languageText.text = currentLanguage.ToString();
        JSONSave.Instance.SaveLanguage();


        
    }
}
