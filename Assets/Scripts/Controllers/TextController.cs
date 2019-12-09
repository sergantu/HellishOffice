using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    static private TextController _instance;
    public static TextController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField] Text infoLabel;    //label текста в окне InfoUI
    [SerializeField] Text tradeFrase; //фраза о успешности торговли

    [SerializeField] Text winLabel;
    [SerializeField] Text winMenu;
    [SerializeField] Text yesDialog;
    [SerializeField] Text noDialog;
    [SerializeField] Text itemsCraft;
    [SerializeField] Text labelMenu;
    [SerializeField] Text optionsMenu;
    [SerializeField] Text menuMenu;
    [SerializeField] Text backMenu;
    [SerializeField] Text labelLose;
    [SerializeField] Text menuLose;
    [SerializeField] Text soundLabel;
    [SerializeField] Text muteSound;
    [SerializeField] Text musicLabel;
    [SerializeField] Text muteMusic;
    [SerializeField] Text chooseElevator;
    [SerializeField] Text dealerLabel;
    [SerializeField] Text playerDealer;
    [SerializeField] Text newitemsLabel;
    [SerializeField] Text playerItems;
    [SerializeField] Text playerInfo;
    [SerializeField] Text craftLabel;


    private void Start()
    {
        winLabel.text = BbtStrings.GetStr("str_winLabel");
        winMenu.text = BbtStrings.GetStr("str_winMenu");
        yesDialog.text = BbtStrings.GetStr("str_yesDialog");
        noDialog.text = BbtStrings.GetStr("str_noDialog");
        itemsCraft.text = BbtStrings.GetStr("str_itemsCraft");
        labelMenu.text = BbtStrings.GetStr("str_labelMenu");
        optionsMenu.text = BbtStrings.GetStr("str_optionsMenu");
        menuMenu.text = BbtStrings.GetStr("str_menuMenu");
        backMenu.text = BbtStrings.GetStr("str_backMenu");
        labelLose.text = BbtStrings.GetStr("str_labelLose");
        menuLose.text = BbtStrings.GetStr("str_menuLose");
        soundLabel.text = BbtStrings.GetStr("str_soundLabel");
        muteSound.text = BbtStrings.GetStr("str_muteSound");
        musicLabel.text = BbtStrings.GetStr("str_musicLabel");
        muteMusic.text = BbtStrings.GetStr("str_muteMusic");
        chooseElevator.text = BbtStrings.GetStr("str_chooseElevator");
        dealerLabel.text = BbtStrings.GetStr("str_dealerLabel");
        playerDealer.text = BbtStrings.GetStr("str_playerDealer");
        newitemsLabel.text = BbtStrings.GetStr("str_newitemsLabel");
        playerItems.text = BbtStrings.GetStr("str_playerItems");
        playerInfo.text = BbtStrings.GetStr("str_playerInfo");
        craftLabel.text = BbtStrings.GetStr("str_craftLabel");
    }








    /// <summary>
    /// Установка значения текста в окне InfoUI 
    /// </summary>
    /// <param name="mainText">Добавляемый текст</param>
    public void SetInfoLabelText(string mainText)
    {
        infoLabel.text = BbtStrings.GetStr(mainText);
    }

    public void SetTradeLabelText(string mainText)
    {
        tradeFrase.text = BbtStrings.GetStr(mainText);
    }
}
