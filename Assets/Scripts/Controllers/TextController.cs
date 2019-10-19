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
