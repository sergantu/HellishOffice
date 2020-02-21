
using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] Text mainLabelText;
    [SerializeField] Text continueText;
    [SerializeField] Text newgameText;
    [SerializeField] Text optionsText;
    [SerializeField] Text developersText;
    [SerializeField] Text exitText;

    [SerializeField] Text instruction;
    [SerializeField] Text developers;

    [SerializeField]
    GameObject developersWindow;

    [SerializeField]
    GameObject instructionWindow;

    [SerializeField]
    CanvasGroup introWindow;

    [SerializeField]
    AudioSource music;

    private Languages currentLanguage = Languages.RU;
    string pathsgc;
    string pathspc;
    string pathsic;
    string pathshc;

    private SaveHudClass shc = new SaveHudClass();

    private void Start()
    {

#if UNITY_ANDROID && !UNITY_EDITOR
        pathsgc = Path.Combine(Application.persistentDataPath, "saves\\SaveGame.json");
        pathspc = Path.Combine(Application.persistentDataPath, "saves\\SavePlayer.json");
        pathsic = Path.Combine(Application.persistentDataPath, "saves\\SaveInventory.json");
        pathshc = Path.Combine(Application.persistentDataPath, "saves\\SaveHud.json");
#else
        pathsgc = Path.Combine(Application.dataPath, "saves\\SaveGame.json");
        pathspc = Path.Combine(Application.dataPath, "saves\\SavePlayer.json");
        pathsic = Path.Combine(Application.dataPath, "saves\\SaveInventory.json");
        pathshc = Path.Combine(Application.dataPath, "saves\\SaveHud.json");
#endif
        JSONSave.Instance.LoadLanguage();
        currentLanguage = (Languages)BbtStrings.language;
        languageText.text = currentLanguage.ToString();

        float test1 = JSONSave.Instance.GetMusicVolume();
        music.volume = JSONSave.Instance.GetMusicVolume();

        ContinueGame.interactable = File.Exists(pathsgc);
        Time.timeScale = 1.0f;

        mainLabelText.text = BbtStrings.GetStr("mainLabelText");
        continueText.text = BbtStrings.GetStr("continueText");
        newgameText.text = BbtStrings.GetStr("newgameText");
        optionsText.text = BbtStrings.GetStr("optionsText");
        developersText.text = BbtStrings.GetStr("developersText");
        exitText.text = BbtStrings.GetStr("exitText");

        developers.text = BbtStrings.GetStr("developers");
        instruction.text = BbtStrings.GetStr("instruction");

        developersWindow.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        developersWindow.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        HideWindow(developersWindow.GetComponent<CanvasGroup>());

        instructionWindow.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        instructionWindow.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        HideWindow(instructionWindow.GetComponent<CanvasGroup>());

        introWindow.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        introWindow.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        HideWindow(introWindow);

    }

    /// <summary>
    /// Показать окно по CanvasGroup
    /// </summary>
    /// <param name="window">Окно UI для показа</param>
    public void ShowWindow(CanvasGroup window) // показать окно
    {
        window.alpha = 1f;
        window.blocksRaycasts = true;
        window.interactable = true;
    }

    /// <summary>
    /// Скрыть окно по CanvasGroup
    /// </summary>
    /// <param name="window">Окно UI для скрытия</param>
    public void HideWindow(CanvasGroup window)  //скрыть окно
    {
        window.alpha = 0f;
        window.blocksRaycasts = false;
        window.interactable = false;
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


        StartCoroutine(StartLevel1());
    }

    IEnumerator StartLevel1()
    {
        ShowWindow(introWindow);
        yield return new WaitForSeconds(8.0f);
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

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

    }

    public void SetSoundLevel(float value)
    {
        //soundLevel.value = value;
    }

    public void SetMusicLevel(float value)
    {
        //musicLevel.value = value;
    }
}
