using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class SaveLoad
{
    /*
     *  что сохранять
     *  xyz персонажа
     *  этаж
     *  уровень сна
     *  уровень здоровья
     *  уровень сытости
     *  уровень жажды
     *  травмы
     *  болезни
     *  прогресс
     *  время(шаг, 1 цифра)
     *  язык
     *  инвентарь игрока
     *  инвентарь торговца
     *  инвентарь всех точек с предметами
     *  события
     *  уровень звука
     *  уровень музыки
     *  статус поиска крыс
     *  статус выращивания грибов
     *  статус фильтра воды
     *  статус выполнения действия
     * */




    string gameSavesName = "GameSaves";
    
    XmlNode userNode;
    XmlAttribute attribute;
    XmlElement element;
    XmlDocument xmlDoc;
    XmlNode rootNode;

    public SaveLoad()
    {
        xmlDoc = new XmlDocument();
        rootNode = xmlDoc.CreateElement("GameSaves");
        xmlDoc.AppendChild(rootNode);
    }

    private void OnDestroy()
    {
        SaveChanges();
    }

    public void SaveChanges()
    {
        xmlDoc.Save(Application.dataPath + "/" + gameSavesName + ".xml");
    }

    public void SaveData( string key_var, bool value )
    {
        element = xmlDoc.CreateElement(key_var);
        element.SetAttribute("value", value.ToString());
        rootNode.AppendChild(element);
    }

    public void SaveData(string key_var, float value)
    {
        element = xmlDoc.CreateElement(key_var);
        element.SetAttribute("value", value.ToString());
        rootNode.AppendChild(element);
    }

    public void SaveData(string key_var, int value)
    {
        element = xmlDoc.CreateElement(key_var);
        element.SetAttribute("value", value.ToString());
        rootNode.AppendChild(element);
    }

    public void SaveData(string key_var, string value)
    {
        element = xmlDoc.CreateElement(key_var);
        element.SetAttribute("value", value.ToString());
        rootNode.AppendChild(element);
    }

    public void SaveArray()
    {
       /* userNode = xmlDoc.CreateElement("KeysArray");
        for (int i = 0; i < saveArray.Length; i++)
        {
            element = xmlDoc.CreateElement("key");
            element.SetAttribute("value", saveArray[i].ToString());
            userNode.AppendChild(element);
        }
        rootNode.AppendChild(userNode);*/
    }

    public void SaveXML()
    {
        userNode = xmlDoc.CreateElement("Info");
        attribute = xmlDoc.CreateAttribute("Unity");
        attribute.Value = Application.unityVersion;
        userNode.Attributes.Append(attribute);
        userNode.InnerText = "Company Name: " + Application.companyName + " :: Product Name: " + Application.productName;
        rootNode.AppendChild(userNode);
    }




   /* public void LoadXML()
    {
        try
        {
            List<string> tmp = new List<string>();
            XmlTextReader reader = new XmlTextReader(Application.dataPath + "/" + fileName + ".xml");
            while (reader.Read())
            {
                if (reader.IsStartElement("key_int"))
                {
                    int k;
                    if (int.TryParse(reader.GetAttribute("value"), out k)) loadInt = k;
                }
                if (reader.IsStartElement("key_float"))
                {
                    float k;
                    if (float.TryParse(reader.GetAttribute("value"), out k)) loadFloat = k;
                }
                if (reader.IsStartElement("key_bool"))
                {
                    bool k;
                    if (bool.TryParse(reader.GetAttribute("value"), out k)) loadBool = k;
                }
                if (reader.IsStartElement("KeysArray"))
                {
                    while (reader.ReadToFollowing("key"))
                    {
                        tmp.Add(reader.GetAttribute("value"));
                    }
                }
            }

            loadArray = new string[tmp.Count];
            for (int i = 0; i < tmp.Count; i++)
            {
                loadArray[i] = tmp[i];
            }

            reader.Close();
        }

        catch (System.Exception)
        {
            Debug.Log("Ошибка чтения файла!");
        }
    }*/
}
