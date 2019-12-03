using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;

public class JSONSave : MonoBehaviour
{ 
    private SaveGameClass sgc = new SaveGameClass();
    private SavePlayerClass spc = new SavePlayerClass();
    private SaveInventoryClass sic = new SaveInventoryClass();
    private SaveHudClass shc = new SaveHudClass();

    private string pathsgc;
    private string pathspc;
    private string pathsic;
    private string pathshc;

    static JSONSave _instance;
    public static JSONSave Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
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
        if (File.Exists(pathsgc))
        {
            sgc = JsonUtility.FromJson<SaveGameClass>(File.ReadAllText(pathsgc));
        }
        else
        {
            sgc = new DefaultGame() as SaveGameClass;
        }

        if (File.Exists(pathspc))
        {
            spc = JsonUtility.FromJson<SavePlayerClass>(File.ReadAllText(pathspc));
        }
        else
        {
            spc = new DefaultPlayer() as SavePlayerClass;
        }

        if (File.Exists(pathsic))
        {
            sic = JsonUtility.FromJson<SaveInventoryClass>(File.ReadAllText(pathsic));
        }
        else
        {
            sic = new DefaultInventory() as SaveInventoryClass;
        }

        if (File.Exists(pathshc))
        {
            shc = JsonUtility.FromJson<SaveHudClass>(File.ReadAllText(pathshc));
        }
        else
        {
            shc = new DefaultHud() as SaveHudClass;
        }
    }

    public void SaveLanguage()
    {
        PlayerPrefs.SetInt("language", BbtStrings.language);
    }

    public void SaveGame()
    {
        sgc.ticks = GameController.Instance.ticks;

        sgc.playEvents = new List<dictionarySaved>();
        foreach (KeyValuePair<string, bool> keyValue in GameController.Instance.gameEvents)
        {
            sgc.playEvents.Add(new dictionarySaved(keyValue.Key, keyValue.Value));
        }

        File.WriteAllText(pathsgc, JsonUtility.ToJson(sgc));
    }

    public void SavePlayer()
    {
        spc.playerPosition = Player.Instance.gameObject.transform.position;
        spc.currentelevator = Player.Instance.Current_station;
        spc.playerParametres = Player.Instance.PlayerParametres;
        spc.playerDisease = Player.Instance.PlayerDisease;
        spc.progress = Player.Instance.Progress;
        

        File.WriteAllText(pathspc, JsonUtility.ToJson(spc));
    }

    public void SaveHud()
    {
        shc.soundLevel = HUD.Instance.GetSoundLevel();
        shc.musicLevel = HUD.Instance.GetMusicLevel();

        File.WriteAllText(pathshc, JsonUtility.ToJson(shc));

    }

    public void SaveInventory()
    {
        sic.playerInventory = new List<listSaved>();
        for (int i = 0; i < InventoryController.Instance.PlayerInventory.Count; i++)
        {
            sic.playerInventory.Add(new listSaved(InventoryController.Instance.PlayerInventory[i]));
        }

        File.WriteAllText(pathsic, JsonUtility.ToJson(sic));
    }

    public void LoadLanguage()
    {
        if (PlayerPrefs.HasKey("language"))
        {
            BbtStrings.language = PlayerPrefs.GetInt("language");
        }
    }

    public void LoadDataGamecontroller()
    {
        GameController.Instance.ticks = sgc.ticks;

        if (sgc.playEvents != null)
        {
            Dictionary<string, bool> gEvents = new Dictionary<string, bool>();
            for (int i = 0; i < sgc.playEvents.Count; i++)
            {
                gEvents.Add(sgc.playEvents[i].param1, sgc.playEvents[i].param2);
            }

            GameController.Instance.gameEvents = gEvents;
        }
    }

    public void LoadDataPlayer()
    {
        if (spc.playerPosition != null)
        {
            Player.Instance.gameObject.transform.position = spc.playerPosition;
        }
        if (spc.currentelevator != 0)
        {
            Player.Instance.Current_station = spc.currentelevator;
        }
        if (spc.playerParametres != null)
        {
            Player.Instance.PlayerParametres = spc.playerParametres;
        }
        if (spc.playerDisease != null)
        {
            Player.Instance.PlayerDisease = spc.playerDisease;
        }

        if (spc.progress != 0.0f)
        {
            Player.Instance.Progress = spc.progress;
        }
    }

    public void LoadDataInventory()
    {
        if (sic.playerInventory != null)
        {
            List<List<int>> PInventory = new List<List<int>>();
            for (int i = 0; i < sic.playerInventory.Count; i++)
            {
                PInventory.Add(sic.playerInventory[i].tempList);
            }
            InventoryController.Instance.PlayerInventory = PInventory;
        }
    }

    public void LoadDataHud()
    {
        HUD.Instance.SetSoundLevel(shc.soundLevel);
        HUD.Instance.SetMusicLevel(shc.musicLevel);
    }




}
[Serializable]
public class SaveGameClass
{
    public int ticks;

    public List<dictionarySaved> playEvents;

    //пока не реализовано
    public bool foundRat;
    public bool growMushroom;
    public bool craftWater;
}

[Serializable]
public class SavePlayerClass
{
    public Vector3 playerPosition;
    public int currentelevator;
    public List<float> playerParametres;
    public List<bool> playerDisease;
    public float progress;

    //статус действия
    public int statusPlayer; //0 - ничего, 1 спит, 2 проект
}

[Serializable]
public class SaveInventoryClass
{
    public List<listSaved> playerInventory;
}

[Serializable]
public class SaveHudClass
{
    public float soundLevel;
    public float musicLevel;
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

public class DefaultGame : SaveGameClass
{
    public new int ticks;
    public new List<dictionarySaved> playEvents;

    //пока не реализовано
    public new bool foundRat;
    public new bool growMushroom;
    public new bool craftWater;

    public DefaultGame()
    {
        ticks = 0;
        

        Dictionary<string, bool> gameEvents = new Dictionary<string, bool>()
        {
            { "craftsofa", false }
            ,{ "craftkey", false }
            ,{ "craftaxe", false }
            ,{ "get_keylevel5", false }
            ,{ "get_keylevel2", false }
            ,{ "get_keylevel3", false }
            ,{ "get_keylevel4", false }
        };

        playEvents = new List<dictionarySaved>();
        foreach (KeyValuePair<string, bool> keyValue in gameEvents)
        {
            playEvents.Add(new dictionarySaved(keyValue.Key, keyValue.Value));
        }

        foundRat = false;
        growMushroom = false;
        craftWater = false;
    }
}

public class DefaultPlayer : SavePlayerClass
{
    public new Vector3 playerPosition;
    public new int currentelevator;
    public new List<float> playerParametres;
    public new List<bool> playerDisease;
    public new float progress;

    //статус действия
    public new int statusPlayer; //0 - ничего, 1 спит, 2 проект

    public List<float> PlayerParametres { get => playerParametres; set => playerParametres = value; }

    public DefaultPlayer()
    {
        playerPosition = new Vector3(-7.4f, 1.18f, -18.85f);
        currentelevator = 1;
        PlayerParametres = new List<float> { 36, 36, 36, 36 };
        playerDisease = new List<bool> { false, false };
        progress = 0.0f;
        statusPlayer = 0;
    }
}

public class DefaultInventory : SaveInventoryClass
{
    public new List<listSaved> playerInventory;

    public DefaultInventory()
    {
        List<List<int>> PlayerInventory = new List<List<int>>()  //бд с текущими инвентарными предметами ( id предмета, количество, id места )
        {
            ///////////////////////-5 - торговец вирутальный
            ///////////////////////-4 - игрок вирутальный
            new List<int>(){ 1, 6, -3 },//торговец
            new List<int>(){ 2, 3, -3 },//торговец
            new List<int>(){ 3, 4, -3 },//торговец
            new List<int>(){ 4, 3, -3 },//торговец
            new List<int>(){ 5, 2, -3 },//торговец
            new List<int>(){ 6, 1, -3 },//торговец

            new List<int>(){ 1, 5, 0 },
            new List<int>(){ 2, 10, 0 },
            new List<int>(){ 3, 15, 0 },
            new List<int>(){ 4, 20, 0 },
            new List<int>(){ 6, 25, 0 },
            new List<int>(){ 7, 30, 0 },
            new List<int>(){ 8, 8, 0 },
            new List<int>(){ 9, 9, 0 },
            new List<int>(){ 10, 11, 0 },
            new List<int>(){ 11, 12, 0 },
            new List<int>(){ 13, 13, 0 },
            new List<int>(){ 14, 14, 0 },
            new List<int>(){ 15, 14, 0 },
            new List<int>(){ 16, 16, 0 },

            new List<int>(){ 8, 8, 1 },
            new List<int>(){ 5, 1, 1 },

            new List<int>(){ 7, 15, 2 },

            new List<int>(){ 7, 10, 3 },
            new List<int>(){ 8, 3, 3 },
            new List<int>(){ 0, 3, 3 },
            new List<int>(){ 1, 8, 3 },

            new List<int>(){ 7, 1, 4 },
            new List<int>(){ 8, 2, 4 },
            new List<int>(){ 0, 3, 4 },
            new List<int>(){ 1, 4, 4 },

            new List<int>(){ 7, 20, 5 },
            new List<int>(){ 8, 50, 5 },
            new List<int>(){ 0, 15, 5 },
            new List<int>(){ 1, 37, 5 }  
        
                                            //6 - крысиная ловушка
        };

        playerInventory = new List<listSaved>();
        for (int i = 0; i < PlayerInventory.Count; i++)
        {
            playerInventory.Add(new listSaved(PlayerInventory[i]));
        }
    }
}

public class DefaultHud : SaveHudClass
{
    public new float soundLevel;
    public new float musicLevel;

    public DefaultHud()
    {
        soundLevel = 0.3f;
        musicLevel = 0.3f;
    }
}