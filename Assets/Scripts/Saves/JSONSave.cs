using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;

public class JSONSave : MonoBehaviour
{
    private Save sv = new Save();
    private string path;

    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "Save.json");
#else
        path = Path.Combine(Application.dataPath, "Save.json");
#endif
        if (File.Exists(path))
        {
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
        }
    }

    private void OnApplicationPause()
    {
        SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        sv.playerPosition = Player.Instance.gameObject.transform.position;
        sv.currentelevator = Player.Instance.Current_station;
        sv.playerParametres = Player.Instance.PlayerParametres;
        sv.playerDisease = Player.Instance.PlayerDisease;
        sv.progress = Player.Instance.Progress;
        sv.ticks = GameController.Instance.ticks;
        sv.language = BbtStrings.language;

        sv.playerInventory = new List<listSaved>();
        for (int i = 0; i < InventoryController.Instance.PlayerInventory.Count; i++)
        {
            sv.playerInventory.Add(new listSaved(InventoryController.Instance.PlayerInventory[i]));
        }

        sv.playEvents = new List<dictionarySaved>();
        foreach (KeyValuePair<string, bool> keyValue in GameController.Instance.gameEvents)
        {
            sv.playEvents.Add(new dictionarySaved(keyValue.Key, keyValue.Value));
        }

        sv.soundLevel = HUD.Instance.GetSoundLevel();
        sv.musicLevel = HUD.Instance.GetMusicLevel();

        File.WriteAllText(path, JsonUtility.ToJson(sv);
    }

    public void LoadData()
    {
        if (sv.playerPosition != null)
        {
            Player.Instance.gameObject.transform.position = sv.playerPosition;
        }
        if (sv.currentelevator != 0)
        {
            Player.Instance.Current_station = sv.currentelevator;
        }
        if (sv.playerParametres != null)
        {
            Player.Instance.PlayerParametres = sv.playerParametres;
        }
        if (sv.playerDisease != null)
        {
            Player.Instance.PlayerDisease = sv.playerDisease;
        }

        if (sv.progress != 0.0f)
        {
            Player.Instance.Progress = sv.progress;
        }
        if (sv.ticks != 0)
        {
            GameController.Instance.ticks = sv.ticks;
        }
        if (sv.language != 0)
        {
            BbtStrings.language = sv.language;
        }
        if (sv.playerInventory != null)
        {
            List<List<int>> PInventory = new List<List<int>>();
            for (int i = 0; i < sv.playerInventory.Count; i++)
            {
                PInventory.Add(sv.playerInventory[i].tempList);
            }
            InventoryController.Instance.PlayerInventory = PInventory;
        }

        if (sv.playEvents != null)
        {
            Dictionary<string, bool> gEvents = new Dictionary<string, bool>();
            for (int i = 0; i < sv.playEvents.Count; i++)
            {
                gEvents.Add(sv.playEvents[i].param1, sv.playEvents[i].param2);
            }
        }

        if (sv.soundLevel != 0.0f)
        {
            HUD.Instance.SetSoundLevel(sv.soundLevel);
        }
        if (sv.musicLevel != 0.0f)
        {
            HUD.Instance.SetMusicLevel(sv.musicLevel);
        }  
    }
}
[Serializable]
public class Save
{
    public Vector3 playerPosition;
    public int currentelevator;
    public List<float> playerParametres;
    public List<bool> playerDisease;
    public float progress;
    public int ticks;

    public int language;
    public List<listSaved> playerInventory;
    public List<dictionarySaved> playEvents;

    public float soundLevel;
    public float musicLevel;


    //пока не реализовано
    public bool foundRat;
    public bool growMushroom;
    public bool craftWater;

    //статус действия
    public int statusPlayer; //0 - ничего, 1 спит, 2 проект
}

[Serializable]
public class listSaved
{
    public List<int> tempList;

    public listSaved(List<int> curList)
    {
        tempList = curList;
    }
    
}

[Serializable]
public class dictionarySaved
{
    public string param1;
    public bool param2;

    public dictionarySaved(string cur1, bool cur2)
    {
        param1 = cur1;
        param2 = cur2;
    }

}