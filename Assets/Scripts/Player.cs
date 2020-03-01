using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    static private Player _instance;
    public static Player Instance
    {
        get
        {
            return _instance;
        }
    }
    private bool alignWithFloor;//выравнивать ли по ширине этажа
    public bool AlignWithFloor
    {
        get { return alignWithFloor; }
        set
        {
            if (!value)
            {
                newTarget.z = transform.position.z;
            }
            else
            {
                newTarget.z = pointInFloor;
            }

            alignWithFloor = value;
        }
    }
    private NavMeshAgent _navMeshAgent; //агент игрока
    public NavMeshAgent NavMeshAgent { get => _navMeshAgent; set => _navMeshAgent = value; }
    public Vector3 newTarget; //в какую точку движется

    private int current_station;
    public int Current_station { get => current_station; set => current_station = value; }

    public List<float> PlayerParametres = new List<float> { 36, 36, 36, 36 }; //0 вода, 1 еда, 2 энергия, 3 здоровье
    public List<bool> PlayerDisease = new List<bool> { false, false }; //рана, болезнь

    private float[] floorCoors = { -1.8f, 1.2f, 4.2f, 7.2f, 10.2f, 13.2f }; // на каком этаже находится
    private Vector3 startClkPos; //координаты точки нажатия, но не отпускания
    private bool isOverUI = false; //тап произошел поверх UI
    public bool methodReachingTargetIsCalled = true; //достижение цели произошло

    [SerializeField] float pointInFloor = -18f; //на какой ширине этажа останавливается
    [SerializeField] float distDontMove = 0.05f; //координаты смещения, после которого перестает двигаться и начинается свайпиться

    [SerializeField] private float progress = 0;
    public float Progress { get => progress; set => progress = value; }
    public float Max_progress { get => max_progress; set => max_progress = value; }

    [SerializeField] private float max_progress = 100;
    private IEnumerator coroutine;

    public float KPD = 1.0f;

    public bool playerSleep = false;
    Animator animator;

    private void Awake()
    {
        _instance = this;
        NavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        AlignWithFloor = true;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        NavMeshAgent.enabled = false;
        JSONSave.Instance.LoadDataPlayer();
        NavMeshAgent.enabled = true;

        HUD.Instance.UpdateProgressSlider(GetCountProgress());

        CameraControl.Instance.AlignCameraWithPlayer();

        UpdateDesease();
    }

    private void Update()
    {
        if (HUD.Instance.isLocked() )
        {
            return;
        }

        MovePlayer();
        
    }

    void UpdateDesease()
    {
        if (PlayerDisease[0])
        {
            HUD.Instance.travmaIcon.color = new Color(1, 1, 1, 1);
        }
        else
        {
            HUD.Instance.travmaIcon.color = new Color(1, 1, 1, 0);
        }

        if (PlayerDisease[1])
        {
            HUD.Instance.deseaseIcon.color = new Color(1, 1, 1, 1);
        }
        else
        {
            HUD.Instance.deseaseIcon.color = new Color(1, 1, 1, 0);
        }
    }

    public void SetAnimWalk()
    {
        AnimNull();
        animator.SetBool( "isWalk", true );
    }

    public void DoStepSound()
    {
        GameController.Instance.AudioManager.PlaySteps();
    }

    public void SetAnimIdle()
    {
        animator.SetBool("isWalk", false);
    }
    /// <summary>
    /// Передвижение игрока, обратботка кликов
    /// </summary>
    private void MovePlayer()
    {
        if (GameController.Instance.LockClick != LockClick.True ) //если тапы разрешены
        {
            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) //если ПК
            {
                if (Input.GetMouseButtonDown(0))
                {
                    TapDownMovePlayer();    
                }
                else if (Input.GetMouseButtonUp(0))
                {

                    if (EventSystem.current.IsPointerOverGameObject()) //если клик поверх UI
                    {
                        return;
                    }
                    TapUpMovePlayer();
                }
            }
            else if (Application.platform == RuntimePlatform.Android) //если android
            {
                if (Input.touchCount > 0 && Input.touchCount < 2 ) //перемещения производятся только один тапом
                {
                    Touch touch = Input.GetTouch(0);

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            TapDownMovePlayer();
                            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                            {
                                isOverUI = true;
                            }
                            else
                            {
                                isOverUI = false;
                            }

                            break;

                        case TouchPhase.Moved:
                            break;

                        case TouchPhase.Ended:

                            if (!isOverUI)
                            {
                                TapUpMovePlayer();
                            }
                            break;
                    }
                }
            }
        }


        if (isReachedTarget()) //достиг ли игрок пункта назначения
        {
            SetAnimIdle();
            if (!methodReachingTargetIsCalled)
            {
                GameController.Instance.LockClick = LockClick.False;
                AlignWithFloor = true;

                if (InteractController.Instance.interactClick)    //был ли это клик по UI кнопке в мире? по интерактивному объекту
                {
                    InteractController.Instance.CallEndMethod();
                }

                methodReachingTargetIsCalled = true;
            }
            
            return;
        }
        else
        {
            methodReachingTargetIsCalled = false;
        }

        if (newTarget.x != 0 && NavMeshAgent.enabled == true)   //производить поиск пути и движение игрока
        {
            NavMeshPath path = new NavMeshPath(); //если путь существует
            NavMeshAgent.CalculatePath( newTarget, path );

            if ( path.status == NavMeshPathStatus.PathComplete )
            {
                SetAnimWalk();
                NavMeshAgent.SetDestination(newTarget);
            }
        }   
    }

    /// <summary>
    /// Нажатие тапа для движения игрока
    /// </summary>
    private void TapDownMovePlayer()    //клик для перемещения
    {
        int layerMask = 1 << 9;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            startClkPos = hit.point;
        }
    }

    /// <summary>
    /// Отжатие тапа для движения игрока
    /// </summary>
    private void TapUpMovePlayer()  //поднятие нажатия
    {
        int layerMask = 1 << 9;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if ((hit.point - startClkPos).magnitude < distDontMove)
            {
                StopWork();
                StopSleep();
                InteractController.Instance.interactClick = false;
                GameController.Instance.StopTimer();
                GameController.Instance.StopTimerCraft();


                if ( AlignWithFloor )
                {
                    newTarget = new Vector3(hit.point.x, GetFloorCoor(hit.point.y), pointInFloor);
                }
                else
                {
                    newTarget = new Vector3(hit.point.x, GetFloorCoor(hit.point.y), transform.position.z);
                }
            }
        }
    }

    /// <summary>
    /// Получение координаты для текущего этажа игрока
    /// </summary>
    /// <param name="cur_y">Координата высоты точки хита от рейкаста</param>
    /// <returns></returns>
    private float GetFloorCoor( float cur_y )   //получение координаты для текущего этажа игрока
    {
        int min_dist_id = 0;
        float min_dist = 1000;

        for ( int i = 0; i < floorCoors.Length; i++ )
        {
            if (min_dist > Mathf.Abs( cur_y - floorCoors[ i ] ) )
            {
                min_dist = Mathf.Abs(cur_y - floorCoors[i]);
                min_dist_id = i;
            }
        }

        
        return floorCoors[min_dist_id];
    }

    /// <summary>
    /// Игрок дошел до цели
    /// </summary>
    /// <returns></returns>
    public bool isReachedTarget()
    {
        Vector3 tempVect = new Vector3( transform.position.x, newTarget.y, transform.position.z );
        if ((newTarget - tempVect).magnitude < 0.1f)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Переместить игрока
    /// </summary>
    /// <param name="targetPos">Точка цели</param>
    public void MovePlayerTo(Vector3 targetPos)
    {
        newTarget = targetPos;
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///ПАРАМЕТРЫ И БОНУСЫ ИГРОКА
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

   

    List<float> CoefParam = new List<float> { 0.02f, 0.04f };

    public bool GetDesease(int idDesease)
    {
        return PlayerDisease[idDesease];
    }

    public void SetDesease( int idDesease, bool status )
    {
        if(idDesease == 0)
        {
            if( status )
            {
                HUD.Instance.travmaIcon.color = new Color(1, 1, 1, 1);
            }
            else
            {
                HUD.Instance.travmaIcon.color = new Color(1, 1, 1, 0);
            }
            
        }
        else
        {
            if (status)
            {
                HUD.Instance.deseaseIcon.color = new Color(1, 1, 1, 1);
            }
            else
            {
                HUD.Instance.deseaseIcon.color = new Color(1, 1, 1, 0);
            }
        }

        if ( GetDesease(idDesease) != status)
        {
            PlayerDisease[idDesease] = status;
        }
    }

    public void AddPlayerParameter ( int type, float count )
    {
        PlayerParametres[type] = PlayerParametres[type] + count;
        if (PlayerParametres[type] > 36)
        {
            PlayerParametres[type] = 36;
        }
    }

    public void RemovePlayerParameter( int type, int count )
    {
        PlayerParametres[type] = PlayerParametres[type] - count;
    }

    public float GetCountPlayerParameter( int type )
    {
        return PlayerParametres[type];
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///ПАРАМЕТРЫ ПРОГРЕССА ИГРЫ
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

   
 
    

    public void AddProgress( float count)
    {
        Progress = Progress + count;
        HUD.Instance.UpdateProgressSlider(GetCountProgress());
    }

    public void DeleteProgress(float count)
    {
        Progress = Progress + count;
        HUD.Instance.UpdateProgressSlider(GetCountProgress());
    }

    public float GetCountProgress()
    {
        return Progress;
    }


    IEnumerator StartWorkCour()
    {
        while (true)
        {
            yield return new WaitForSeconds(GameController.Instance.TimePerHour/10);
            float allKPD = 0.09f * GetKPD();
            float part = allKPD / 10;

            if (GameController.Instance.IsEventDone("craftcomputer"))
            {
                allKPD += part;
            }

            if (GameController.Instance.IsEventDone("craftmonitor"))
            {
                allKPD += part;
            }

            if (GameController.Instance.IsEventDone("craftkreslo"))
            {
                allKPD += part;
            }

            AddProgress(allKPD);
            
            if ( CheckProgress() )
            {
                StopWork();
                HUD.Instance.ShowLevelWonWindow();
            }
        }
    }

    public void StartWork()
    {
        animator.SetBool("isType", true);
        GameController.Instance.AudioManager.StopAllSfx();
        GameController.Instance.AudioManager.PlaySoundLoop("aud_see_player_work");
        coroutine = StartWorkCour();
        StartCoroutine(coroutine);
        lIcon = Instantiate(loopIcon, GameObject.Find("ProgressButton").transform.GetChild(0).GetChild(0).gameObject.transform);
    }

    public void AnimPick(bool state)
    {
        animator.SetBool("isPickUp", state);
    }

    public void AnimEat(bool state)
    {
        animator.SetBool("isEat", state);
    }

    public void AnimDrink(bool state)
    {
        animator.SetBool("isDrink", state);
    }

    public void AnimDestroy(bool state)
    {
        animator.SetBool("isWalk", false);
        animator.SetBool("isDestroy", state);
    }

    public void AnimSleep(bool state)
    {
        animator.SetBool("isSleep", state);
    }

    public void AnimNull()
    {
        animator.SetBool("isEat", false);
        animator.SetBool("isPickUp", false);
        animator.SetBool("isDrink", false);
        animator.SetBool("isDestroy", false);
    }


    public void StopWork()
    {
        animator.SetBool("isType", false);
        if (coroutine != null)
        {
            GameController.Instance.AudioManager.StopSoundLoop("aud_see_player_work");
            StopCoroutine(coroutine);
            Destroy(lIcon.gameObject);
        }
    }

    public bool CheckProgress()
    {
        if ( Progress >= Max_progress )
        {
            return true;
        }
        return false;
    }


    public GameObject loopIcon;
    GameObject lIcon;


    private IEnumerator coroutineSleep;

    IEnumerator StartSleepCour()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            float newKpdSleep = 0.3f * GetKPD();
            if (GameController.Instance.IsEventDone("craftsofa"))
            {
                newKpdSleep *= 1.5f;
            }

            AddPlayerParameter( 2, newKpdSleep);
            if (GetCountPlayerParameter(2) >= 35.9 )
            {
                StopSleep();
            }
        }
    }

    public void StartSleep()
    {
        AnimSleep(true);
        playerSleep = true;
        coroutineSleep = StartSleepCour();
        StartCoroutine(coroutineSleep);
        GameController.Instance.AudioManager.PlaySoundLoop("aud_see_player_sleep");
        lIcon = Instantiate(loopIcon, GameObject.Find("SleepButton").transform.GetChild(0).GetChild(0).gameObject.transform);
    }

    public void StopSleep()
    {
        AnimSleep(false);
        playerSleep = false;
        if (coroutineSleep != null)
        {
            GameController.Instance.AudioManager.StopSoundLoop("aud_see_player_sleep");
            StopCoroutine(coroutineSleep);
            Destroy(lIcon.gameObject);
        }
    }

    public float GetKPD()
    {
        return KPD;
    }

    public void UpdateKPD()
    {
        KPD = 1.0f;
        float offsetKPD = 0.0f;

        for (int i = 0; i < PlayerParametres.Count; i++)
        {
            if (PlayerParametres[i] < 28 && PlayerParametres[i] > 18)//легкая необходимость
            {
                offsetKPD += -CoefParam[0];
            }
            else if (PlayerParametres[i] < 19 && PlayerParametres[i] > 0) //сильная необхожимость
            {
                offsetKPD += -CoefParam[1];
            }

        }

        for (int i = 0; i < PlayerDisease.Count; i++)
        {
            if ( PlayerDisease[i] )
            {
                offsetKPD += -0.05f;
            }
        }

        if (GameController.Instance.IsEventDone("craftfir"))
        {
            KPD += 0.1f;
        }

        if (GameController.Instance.IsEventDone("craftcondey"))
        {
            KPD += 0.1f;
        }

        KPD += offsetKPD;

    }

    private void OnDestroy()
    {
        JSONSave.Instance.SavePlayer();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            JSONSave.Instance.SavePlayer();
        }
    }

    private void OnApplicationQuit()
    {
        JSONSave.Instance.SavePlayer();
    }
}
