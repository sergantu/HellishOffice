﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState { Play, Pause }
public enum LockClick { True, False }

public delegate void InventoryUsedCallback(InventoryUIButton item); // делегат на нажатия инвентаря (сделать показать инфы по предмету, обводка иконкой и установка как выбранный)
public delegate void CraftUsedCallback(CraftUIButton craft); // делегат на нажатия инвентаря (сделать показать инфы по предмету, обводка иконкой и установка как выбранный)



public class GameController : MonoBehaviour
{
    static GameController _instance;
    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                // GameObject gameController = Instantiate(Resources.Load("Prefabs/GameController") as GameObject);
                //_instance = gameController.GetComponent<GameController>();
            }

            return _instance;
        }
    } //синглтон

    public GameState state;
    public GameState State //свойство установки на паузу
    {
        get { return state; }
        set
        {
            if (value == GameState.Play)
            {
                Time.timeScale = 1.0f;
            }
            else
            {
                Time.timeScale = 0.0f;
            }
            state = value;
        }
    }

    private LockClick lockClick; //
    public LockClick LockClick
    {
        get { return lockClick; }
        set
        {
            lockClick = value;
        }
    } //свойство установки на блокировку клика

    public float TimePerHour { get => timePerHour; set => timePerHour = value; }

    public Dictionary<string, bool> gameEvents = new Dictionary<string, bool>()
    {
        { "craftsofa", false }
        ,{ "craftkey", false }
        ,{ "craftaxe", false }
        ,{ "get_keylevel5", false }
        ,{ "get_keylevel2", false }
        ,{ "get_keylevel3", false }
        ,{ "get_keylevel4", false }
    };

    [SerializeField] private Audio audioManager;
    public Audio AudioManager { get => audioManager; set => audioManager = value; }

    [SerializeField] GameObject TimerIcon;

    public SaveLoad saveLoad;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        //DontDestroyOnLoad(gameObject);

        State = GameState.Play;
        LockClick = LockClick.False;
        InitializeAudioManager();

        saveLoad = new SaveLoad();
        saveLoad.SaveData( "string1", "teststring" );
        saveLoad.SaveData("intttt", 5);
        saveLoad.SaveChanges();

    }

    private void Start()
    {
        LoadEvents();

        //StartNewLevel();

        AddTickAndUpdate();
        StartCoroutine(StartDateTime());
        AudioManager.PlayMusic(true);

        InventoryController.Instance.UpdateTempInventory();
    }

    public bool IsEventDone(string eventName)
    {
        return gameEvents[eventName];
    }

    public void SetEventDone(string eventName)
    {
        gameEvents[eventName] = true;
        Invoke(eventName + "_end", 0);
    }

    private void LoadEvents()
    {

        foreach (var pair in gameEvents)
        {
            if (pair.Value)
            {
                Invoke(pair.Key + "_end", 0);
            }
        }

    }

    /// <summary>
    /// Начало уровня
    /// </summary>
    public void StartNewLevel()
    {
        State = GameState.Play;
        LockClick = LockClick.False;
    }

    /// <summary>
    /// Рестарт уровня
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
    }

    /// <summary>
    /// Переход в галвное меню
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Игра пройдена
    /// </summary>
    public void LevelWon()
    {
        HUD.Instance.ShowLevelWonWindow();
    }

    /// <summary>
    /// Игра проиграна
    /// </summary>
    public void GameOver()
    {
        HUD.Instance.ShowLevelLoseWindow();
    }


    //////////////////////////////////////////////////////////////////////////////////////////////
    ///GAME EVENTS
    //////////////////////////////////////////////////////////////////////////////////////////////

    private void craftsofa_end()
    {

    }

    private void craftaxe_end()
    {

    }

    private void craftkey_end()
    {
        Button curButton = GameObject.Find("btn_elevator4").GetComponent<Button>();
        curButton.interactable = true;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////




    //////////////////////////////////////////////////////////////////////////////////////////////
    ///DateTime
    //////////////////////////////////////////////////////////////////////////////////////////////

    int ticks;
    string month;
    int date;
    public int time;
    [SerializeField] float timePerHour = 30f;
    [SerializeField] GameObject SunLight;
    [SerializeField] int base_station = 1;

    private bool statusIsNight = false;
    public bool StatusIsNight { get => statusIsNight; set => statusIsNight = value; }
    [SerializeField] Transform PlayerSpawn;

    private void ResetDateTime()
    {
        ticks = -1;
        month = "декабря";
        date = 27;
        time = 9;
        Player.Instance.Current_station = 1;
    }

    IEnumerator StartDateTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimePerHour);
            ticks++;
            AddTickAndUpdate();
        }
    }

    void AddTickAndUpdate()
    {
        if (ticks > 110)
        {
            month = "января";
        }
        else
        {
            month = "декабря";
        }

        time = ((ticks + 9) % 24);

        StatusIsNight = false;

        if ( time > 17 || time < 9 )
        {
            StatusIsNight = true;
        }

        date = (27 + (ticks + 9) / 24);
        if (date > 31)
        {
            date = date - 31;
        }

        string finalDate = date.ToString() + " " + month + " " + time.ToString() + ":00";

        for (int i = 0; i < Player.Instance.PlayerParametres.Count; i++)
        {
            if (i == 2 && Player.Instance.playerSleep)
            {
                continue;
            }

            if ( i == 3 && !( Player.Instance.GetDesease(0) || Player.Instance.GetDesease(1)) )
            {
                Player.Instance.AddPlayerParameter(i, 1);
                continue;
            }
            Player.Instance.RemovePlayerParameter(i, 1);
        }


        if (StatusIsNight)
        {
            SunLight.transform.rotation = Quaternion.Euler(190, 63, 0);
        }
        else
        {
            SunLight.transform.rotation = Quaternion.Euler(50, 63, 0);
        }

        HUD.Instance.UpdateDateTime(finalDate);
        HUD.Instance.UpdateMainParamIcons();

        CallRandomDesease();

        if (TrapRatStatus == 2 && StartRandomTrapRat())
        {
            InteractController.Instance.SwipeTrapRatIcon();
        }

        //проигрыш по истечению времени
        if (ticks > 720)
        {
            //проигрыш
            HUD.Instance.ShowLevelLoseWindow();
        }

        Player.Instance.UpdateKPD();

        if ( time > 8 && time < 18 && Player.Instance.Current_station != base_station)
        {
            Player.Instance.NavMeshAgent.enabled = false;
            Player.Instance.Current_station = base_station;
            Player.Instance.gameObject.transform.position = PlayerSpawn.position;
            Player.Instance.NavMeshAgent.enabled = true;
            CameraControl.Instance.AlignCameraWithPlayer();

            //наказание!
        }

        if (time == 12)
        {
            ShowEverydayEvent();
        }

        if ( time != 12 )
        {
            int rnd = Random.Range(0, 24);
            if(rnd == 0)
            {
                ShowRandomEvent();
            }
        }

    }

    private bool StartRandomTrapRat()
    {
        int result = Random.Range(0, 100);

        if (result < 11)
        {
            return true;
        }

        return false;
    }

    public float chanceDesiasePercent = 1.0f;

    public void CallRandomDesease()
    {
        float randomForDesiase = Random.Range(0.0f, 100.0f);
        int numberDesiase = Random.Range(0, 1);
        if (randomForDesiase < chanceDesiasePercent)
        {
            Player.Instance.SetDesease(numberDesiase, true);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////


    private void InitializeAudioManager()
    {
        audioManager.SourceSFX = gameObject.AddComponent<AudioSource>();
        audioManager.SourceMusic = gameObject.AddComponent<AudioSource>();
        audioManager.SourceRandomPitchSFX = gameObject.AddComponent<AudioSource>();
        gameObject.AddComponent<AudioSource>();
    }


    //////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////
    private IEnumerator corTimer;
    private GameObject currentPlaceDo;
    //TimerIcon
    //куда и время
    IEnumerator StartTimerCourutine(GameObject place, float time, MethodsEnd func_end)
    {
        currentPlaceDo = place;
        GameObject timerIcon = Instantiate(TimerIcon, place.transform);
        timerIcon.GetComponent<ThisTimer>().time = time;

        yield return new WaitForSeconds(time);

        func_end();
    }

    public void StartTimer(GameObject place, float time, MethodsEnd func_end)
    {
        corTimer = StartTimerCourutine(place, time, func_end);
        StartCoroutine(corTimer);
    }

    public void StopTimer()
    {
        if ( corTimer != null )
        {
            if (currentPlaceDo != null && currentPlaceDo.transform.childCount > 0 )
            {
                Destroy(currentPlaceDo.transform.GetChild(0).gameObject);
                AudioManager.StopAllSfx();
                StopCoroutine(corTimer);
                corTimer = null;
            }
        }  
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////////////////

    //куда и время
    IEnumerator StartCraftCourutine(float time, CraftEnd func_end, CraftUIButton craft)
    {
        currentPlaceDo = GameObject.Find("UseCraftTable");
        GameObject timerIcon = Instantiate(TimerIcon, currentPlaceDo.transform);
        timerIcon.GetComponent<ThisTimer>().time = time;

        yield return new WaitForSeconds(time);

        func_end(craft);
    }

    public void StartTimerCraft(float time, CraftEnd func_end, CraftUIButton craft)
    {
        corTimer = StartCraftCourutine(time, func_end, craft);
        StartCoroutine(corTimer);
    }

    public void StopTimerCraft()
    {
        if (corTimer != null)
        {
            if (currentPlaceDo != null && currentPlaceDo.transform.childCount > 0)
            {
                Destroy(currentPlaceDo.transform.GetChild(0).gameObject);
                AudioManager.StopAllSfx();
                StopCoroutine(corTimer);
                corTimer = null;
            }
        }
    }


    public int TrapRatStatus = 0; //0 - не создан, 1 - создан и не заряжен, 2 - заряжен, 3 - получена крыса
    public int TrapWaterStatus = 0; //0 - не создан, 1 - создан и не заряжен, 2 - заряжен, 3 - получена вода
    public int TrapMushroomsStatus = 0; //0 - не создан, 1 - создан и не заряжен, 2 - заряжен, 3 - получена грибы


    //зачем это??
    int[][,] nums = new int[3][,]
    {
        new int[,] { {1,2}, {3,4} },
        new int[,] { {1,2}, {3,6} },
        new int[,] { {1,2}, {3,5}, {8, 13} }
    };
    //



    //система рандомных событий

    //таблица рандомных ивентов
    public int[][,] RandomEventEffects = new int[14][,]
    {
          new int[,] { { 2, -1, -3, 1 } }
        , new int[,] { { 1, 6, 1, 1 } }
        , new int[,] { { 2, -1, 2, 1 }, { 1, 5, 1, 1 } }
        , new int[,] { { 0, 4, 1, 1 }, { 0, 11, 1, 1 } }
        , new int[,] { { 1, 2, 10, 0 } }
        , new int[,] { { 1, 2, 8, 0 } }
        , new int[,] { { 1, 1, 20, 1 } }
        , new int[,] { { 0, 4, 1, 1 }, { 0, 10, 1, 1 } }
        , new int[,] { { 0, 5, 1, 0 } }
        , new int[,] { { 0, 8, 2, 0 } }
        , new int[,] { { 1, 2, 50, 1 } }
        , new int[,] { { 0, 8, 3, 1 } }
        , new int[,] { { 0, 7, 3, 1 } }
        , new int[,] { { 0, 14, 100, 1 } }
    };

    //таблица ежедневных ивентов с ответом да
    //typeEffect 0 - добавлен предмет, 1 - добавлен параметр игрока(5 - травма), 2 - добавлен проект
    //тип, id, count, isadd
    public int[][,] YesEverydayEventEffects = new int[14][,]
    {
          new int[,] { { 0, 7, 5, 0 }, { 0, 1, 1, 1 } }
        , new int[,] { { 2, 0, 2, 1 } }
        , new int[,] { { 1, 5, 1, 1 } }
        , new int[,] { { 0, 8, 5, 1 } }
        , new int[,] { { 2, 0, 2, 0 } }
        , new int[,] { { 0, 6, 1, 1 }, { 0, 8, 2, 1 }, { 0, 0, 1, 1 }, { 1, 2, 10, 0 } }
        , new int[,] { { 1, 5, 0, 1 } }
        , new int[,] { { 1, 2, 10, 1 } }
        , new int[,] { { 0, 8, 5, 1 }, { 0, 0, 1, 1 } }
        , new int[,] { { 1, 5, 0, 1 } }
        , new int[,] { { 0, 5, 1, 1 }, { 0, 0, 1, 0 } }
        , new int[,] { { 0, 14, 50, 1 } }
        , new int[,] { { 2, 0, 1, 1 }, { 1, 2, 5, 0 } }
        , new int[,] { { 0, 14, 50, 0 } }
    };

    //таблица ежедневных ивентов с ответом нет
    public int[][,] NoEverydayEventEffects = new int[14][,]
    {
          new int[,] { { 1, 2, 10, 0 } }
        , new int[,] { { 1, 2, 10, 0 } }
        , new int[,] { { 0, 5, 1, 0 } }
        , new int[,] { { 1, 1, 10, 0 } }
        , new int[,] { { 2, 0, 2, 1 } }
        , new int[,] { { 1, 2, 20, 1 } }
        , new int[,] { { 0, 4, 2, 0 } }
        , new int[,] { { 1, 0, 10, 0 } }
        , new int[,] { { 2, 0, 2, 1 } }
        , new int[,] { { 1, 0, 5, 0 } }
        , new int[,] { { 2, 0, 1, 1 } }
        , new int[,] { { 1, 2, 5, 0 } }
        , new int[,] { { 1, 2, 10, 1 } }
        , new int[,] { { 2, 0, 1, 0 } }
    };

    //таблица ивентов в лифте
    public int[][,] ElevatorEventEffects = new int[7][,]
    {
          new int[,] { { 1, 2, 5, 0 } }
        , new int[,] { { 1, 2, 1, 0 } }
        , new int[,] { { 1, 2, 5, 1 } }
        , new int[,] { { 1, 1, 5, 1 } }
        , new int[,] { { 1, 5, 0, 1 } }
        , new int[,] { { 1, 2, 10, 1 } }
        , new int[,] { { 1, 1, 10, 1 } }
    };

    private string textInfo = "";


    public void ShowRandomEvent()
    {
        textInfo = "";
        int rnd = Random.RandomRange(0, 14);

        HUD.Instance.GetWindow("EventWindow").transform.GetChild(0).GetComponent<Text>().text = BbtStrings.GetStr("str_random_event_" + rnd);
        HUD.Instance.ShowEventWindow();
        HUD.Instance.SetLock(true);

        for (int i = 0; i < RandomEventEffects[rnd].GetLength(0); i++)
        {
            RealiseEventEffects(RandomEventEffects[rnd][i, 0], RandomEventEffects[rnd][i, 1], RandomEventEffects[rnd][i, 2], RandomEventEffects[rnd][i, 3]);
        }

        HUD.Instance.GetWindow("InfoWindow").transform.GetChild(0).GetComponent<Text>().text = textInfo.ToString();
    }

    //сохранить
    private int rndEverydayEvent = 0;

    public void ShowEverydayEvent()
    {
        rndEverydayEvent = Random.Range(0, 14);

        HUD.Instance.GetWindow("DialogWindow").transform.GetChild(0).GetComponent<Text>().text = BbtStrings.GetStr("str_everyday_event_" + rndEverydayEvent);
        HUD.Instance.ShowDialogWindow();
        HUD.Instance.SetLock(true);
    }

    public void RealizeEverydayEvent(bool answer)
    {
        textInfo = "";
        if (answer)
        {
            for (int i = 0; i < YesEverydayEventEffects[rndEverydayEvent].GetLength(0); i++)
            {
                RealiseEventEffects(YesEverydayEventEffects[rndEverydayEvent][i, 0], YesEverydayEventEffects[rndEverydayEvent][i, 1], YesEverydayEventEffects[rndEverydayEvent][i, 2], YesEverydayEventEffects[rndEverydayEvent][i, 3]);
            }
        }
        else
        {
            for (int i = 0; i < NoEverydayEventEffects[rndEverydayEvent].GetLength(0); i++)
            {
                RealiseEventEffects(NoEverydayEventEffects[rndEverydayEvent][i, 0], NoEverydayEventEffects[rndEverydayEvent][i, 1], NoEverydayEventEffects[rndEverydayEvent][i, 2], NoEverydayEventEffects[rndEverydayEvent][i, 3]);
            }
        }

        HUD.Instance.GetWindow("InfoWindow").transform.GetChild(0).GetComponent<Text>().text = textInfo.ToString();
    }

    public void ShowElevatorEvent()
    {
        textInfo = "";
        int rnd = Random.RandomRange(0, 7);

        HUD.Instance.GetWindow("EventWindow").transform.GetChild(0).GetComponent<Text>().text = BbtStrings.GetStr("str_elevator_event_" + rnd);
        HUD.Instance.ShowEventWindow();
        HUD.Instance.SetLock(true);

        for (int i = 0; i < ElevatorEventEffects[rnd].GetLength(0); i++)
        {
            RealiseEventEffects(ElevatorEventEffects[rnd][i, 0], ElevatorEventEffects[rnd][i, 1], ElevatorEventEffects[rnd][i, 2], ElevatorEventEffects[rnd][i, 3]);
        }

        HUD.Instance.GetWindow("InfoWindow").transform.GetChild(0).GetComponent<Text>().text = textInfo.ToString();
    }

    //typeEffect 0 - добавлен предмет, 1 - добавлен параметр игрока(5 - травма), 2 - добавлен проект, 3 добавить?
    private void RealiseEventEffects( int typeEffect, int id_param, int count, int IsAdd )
    {
        switch(typeEffect)
        {
            case 0: //предмет

                if ( IsAdd == 1 )
                {
                    textInfo += "+ ";
                    InventoryController.Instance.AddInventoryItemPLayer(id_param, count, 0);
                }
                else
                {
                    textInfo += "- ";
                    InventoryController.Instance.RemoveFromInventory(id_param, count, 0);
                }
                textInfo += count + " " + InventoryController.Instance.InventoryVariables[id_param].Name;
                break;

            case 1: //параметры игрока

                if ( id_param != 5 )
                {
                    if (IsAdd == 1)
                    {
                        textInfo += "+ ";
                        Player.Instance.AddPlayerParameter(id_param, count);
                    }
                    else
                    {
                        textInfo += "- ";
                        Player.Instance.RemovePlayerParameter(id_param, count);
                    }
                    textInfo += count + " " + BbtStrings.GetStr("str_param_" + id_param);
                }
                else//травма
                {
                    if ( IsAdd == 1 )
                    {
                        textInfo += "+ " + BbtStrings.GetStr("str_desease_0");
                        Player.Instance.SetDesease(0, true);
                        textInfo += count + " " + InventoryController.Instance.InventoryVariables[id_param].Name;
                    }
                }
                break;

            case 2: //проект

                if (IsAdd == 1)
                {
                    textInfo += "+ ";
                    Player.Instance.AddProgress(count);
                }
                else
                {
                    textInfo += "- ";
                    Player.Instance.DeleteProgress(count);
                }

                textInfo += count + " " + BbtStrings.GetStr("str_project");

                break;

            default:
                break;
        }

        textInfo += System.Environment.NewLine;

    }



    //вызов рандомного метода


}

