using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public delegate void MethodsEnd();
public delegate void CraftEnd(CraftUIButton craft);

public class InteractController : MonoBehaviour
{
    static private InteractController _instance;
    public static InteractController Instance
    {
        get
        {
            return _instance;
        }
    }
    public List<GameObject> interactObjs = new List<GameObject>(); //список объектов нажимаемых кнопок в мире
    public bool interactClick = false;  //переменная для распознавания, клик произошел на кнопку или для перемещения

    [SerializeField] Dictionary<string, float> scaleBorders = new Dictionary<string, float> //границы размера кнопки при скролле
    {
        ["forward"] = 0.7f,
        ["back"] = 1.5f
    };

    private string nameEndMethod = ""; //имя метода, который выполнится после достижения кнопки (например откроется окно подбора)

    Dictionary<string, float> interactTimes = new Dictionary<string, float> 
    {
        ["take"] = 12.0f,
        ["info"] = 3.0f,
        ["crack"] = 40.0f,
        ["elevator"] = 3.0f,
        ["dealer"] = 5.0f,
        ["craft"] = 5.0f,
        ["juice"] = 8.0f,
        ["cola"] = 10.0f,
        ["coffee"] = 7.0f,
        ["water"] = 10.0f,
        ["noodles"] = 12.0f,
        ["rat"] = 10.0f,
        ["mushrooms"] = 5.0f,
        ["energybar"] = 7.0f,
        ["cure"] = 2.0f,
        ["bandage"] = 2.0f,
        ["antibiotic"] = 2.0f,
        ["trap"] = 7.0f,

    };

    public List<Button> ElevatorButtons = new List<Button>();
    public List<GameObject> RatMineObjects = new List<GameObject>();
    public List<GameObject> WaterMineObjects = new List<GameObject>();
    public List<GameObject> MushroomsMineObjects = new List<GameObject>();

    Animator _inventoryAnimator;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _inventoryAnimator = HUD.Instance.inventoryButton;
    }

    private void FixedUpdate()
    {
        SetScaleButton();
    }

    public GameObject FindInterectObj(string name)
    {
        for (int i = 0; i < interactObjs.Count; i++)
        {
            if (interactObjs[i].name.Equals(name))
            {
                return interactObjs[i];
            }
        }

        return null;
    }

    /// <summary>
    /// Обновление размера кнопок в мире при скролле
    /// </summary>
    private void SetScaleButton()
    {
        float forwardB = CameraControl.Instance.scrollBorders["forward"];
        float backB = CameraControl.Instance.scrollBorders["back"];
        float curB = CameraControl.Instance.gameObject.transform.position.z;

        float procent = Mathf.Abs( (curB - backB) / (forwardB - backB) );
        procent = -procent;
        float newScale = Mathf.Abs( scaleBorders[ "forward" ] - scaleBorders[ "back" ] ) * procent + scaleBorders["back"];

        newScale *= 1.7f;

        foreach (GameObject obj in interactObjs)
        {
            if ( obj != null )
            {
                obj.transform.localScale = new Vector3(newScale, newScale, obj.transform.localScale.z);
            }
        }
    }

    /// <summary>
    /// Движение к кнопке в мире при клике на нее
    /// </summary>
    /// <param name="goWindow"> К какой кнопке двигаться </param>
    public void MoveToButton( GameObject goWindow )
    {
        Player.Instance.StopWork();
        Player.Instance.StopSleep();
        GameController.Instance.StopTimer();
        GameController.Instance.StopTimerCraft();

        nameEndMethod = "Open" + goWindow.name;
        Vector3 mainParent = goWindow.transform.position;
        Vector3 newCoors = new Vector3(mainParent.x, mainParent.y, Player.Instance.newTarget.z);
        Player.Instance.methodReachingTargetIsCalled = false;
        Player.Instance.MovePlayerTo(newCoors);
        interactClick = true;

    }

    /// <summary>
    /// Выполнение метода по достижению кнопки
    /// </summary>
    public void CallEndMethod()
    {
        if (nameEndMethod != "")
        {
            Invoke(nameEndMethod, 0);
            nameEndMethod = "";
        }
    }

    /// <summary>
    /// Открытие окна лифта при нажатии на 1 этаж
    /// </summary>
    private void OpenElevatorWindow1()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_button_elevator_1");
        GameController.Instance.StartTimer(ElevatorButtons[0].gameObject, interactTimes["elevator"] * Player.Instance.GetKPD(), AddElevatorWindow1);
    }

    private void AddElevatorWindow1()
    {
        HUD.Instance.ShowElevatorWindow();
    }

    /// <summary>
    /// Открытие окна лифта при нажатии на 2 этаж
    /// </summary>
    private void OpenElevatorWindow2()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_button_elevator_1");
        GameController.Instance.StartTimer(ElevatorButtons[1].gameObject, interactTimes["elevator"] * Player.Instance.GetKPD(), AddElevatorWindow1);
    }

    /// <summary>
    /// Открытие окна лифта при нажатии на 3 этаж
    /// </summary>
    private void OpenElevatorWindow3()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_button_elevator_1");
        GameController.Instance.StartTimer(ElevatorButtons[2].gameObject, interactTimes["elevator"] * Player.Instance.GetKPD(), AddElevatorWindow1);
    }

    /// <summary>
    /// Открытие окна лифта при нажатии на 4 этаж
    /// </summary>
    private void OpenElevatorWindow4()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_button_elevator_1");
        GameController.Instance.StartTimer(ElevatorButtons[3].gameObject, interactTimes["elevator"] * Player.Instance.GetKPD(), AddElevatorWindow1);
    }
   
    /// <summary>
    /// Открытие окна лифта при нажатии на 5 этаж
    /// </summary>
    private void OpenElevatorWindow5()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_button_elevator_1");
        GameController.Instance.StartTimer(ElevatorButtons[4].gameObject, interactTimes["elevator"] * Player.Instance.GetKPD(), AddElevatorWindow1);
    }

    /// <summary>
    /// Открытие окна крафта
    /// </summary>
    private void OpenCraftTableWindow()
    {
        GameController.Instance.AudioManager.PlaySound("aud_clk_trech_out");
        HUD.Instance.ShowCraftWindow();
    }

    /// <summary>
    /// Открытие торговца
    /// </summary>
    public void OpenDealerButton()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[50], interactTimes["dealer"] * Player.Instance.GetKPD(), AddDealer);
    }

    public void AddDealer()
    {
        InventoryController.Instance.CheckTrade();
        HUD.Instance.LoadTradeInventory(-3);
        HUD.Instance.ShowDealerWindow();
        InventoryController.Instance.SetOpenedPlace(-3);
    }

    /// <summary>
    /// Прогресс
    /// </summary>
    public void OpenProgressButton()
    {
        Player.Instance.StartWork();
    }

    public void OpenEatRatButton()
    {
        if (InventoryController.Instance.GetCountItem(16) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_eat_something");
            GameController.Instance.StartTimer(interactObjs[51], interactTimes["rat"] * Player.Instance.GetKPD(), AddEatRat);
            return;
        }

        _inventoryAnimator.SetTrigger("noInventory");
    }

    public void AddEatRat()
    {
        Player.Instance.AddPlayerParameter(1, 20);
        InventoryController.Instance.RemoveFromInventory(16, 1, 0);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenEatEnergybarButton()
    {
        if (InventoryController.Instance.GetCountItem(4) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_eat_something");
            GameController.Instance.StartTimer(interactObjs[52], interactTimes["energybar"] * Player.Instance.GetKPD(), AddEatEnergybar);
            return;
        }

        _inventoryAnimator.SetTrigger("noInventory");
    }

    public void AddEatEnergybar()
    {
        Player.Instance.AddPlayerParameter(1, 15);
        InventoryController.Instance.RemoveFromInventory(4, 1, 0);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenEatNoodlesButton()
    {
        if (InventoryController.Instance.GetCountItem(5) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_eat_something");
            GameController.Instance.StartTimer(interactObjs[53], interactTimes["noodles"] * Player.Instance.GetKPD(), AddEatNoodles);
            return;
        }

        _inventoryAnimator.SetTrigger("noInventory");
    }

    public void AddEatNoodles()
    {
        Player.Instance.AddPlayerParameter(1, 25);
        InventoryController.Instance.RemoveFromInventory(5, 1, 0);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenEatMushroomsButton()
    {
        if (InventoryController.Instance.GetCountItem(7) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_eat_something");
            GameController.Instance.StartTimer(interactObjs[54], interactTimes["mushrooms"] * Player.Instance.GetKPD(), AddEatMushrooms);
            return;
        }

        _inventoryAnimator.SetTrigger("noInventory");
    }

    public void AddEatMushrooms()
    {
        Player.Instance.AddPlayerParameter(1, 10);
        InventoryController.Instance.RemoveFromInventory(3, 1, 0);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenDrinkWaterButton()
    {
        if (InventoryController.Instance.GetCountItem(8) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_drink_something");
            GameController.Instance.StartTimer(interactObjs[55], interactTimes["water"] * Player.Instance.GetKPD(), AddDrinkWater);
            return;
        }

        _inventoryAnimator.SetTrigger("noInventory");
    }

    private void AddDrinkWater()
    {
        Player.Instance.AddPlayerParameter(0, 15);
        InventoryController.Instance.RemoveFromInventory(8, 1, 0);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenDrinkCoffeeButton()
    {
        if (InventoryController.Instance.GetCountItem(9) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_drink_something");
            GameController.Instance.StartTimer(interactObjs[56], interactTimes["coffee"] * Player.Instance.GetKPD(), AddDrinkCoffee);
            return;
        }

        _inventoryAnimator.SetTrigger("noInventory");
    }

    private void AddDrinkCoffee()
    {
        Player.Instance.AddPlayerParameter(0, 5);
        InventoryController.Instance.RemoveFromInventory(9, 1, 0);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenDrinkJuiceButton()
    {
        if (InventoryController.Instance.GetCountItem(10) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_drink_something");
            GameController.Instance.StartTimer(interactObjs[57], interactTimes["juice"] * Player.Instance.GetKPD(), AddDrinkJuice);
            return;
        }

        _inventoryAnimator.SetTrigger("noInventory");
    }

    private void AddDrinkJuice()
    {
        Player.Instance.AddPlayerParameter(0, 12);
        InventoryController.Instance.RemoveFromInventory(10, 1, 0);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenDrinkColaButton()
    {
        if (InventoryController.Instance.GetCountItem(11) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_drink_something");
            GameController.Instance.StartTimer(interactObjs[58], interactTimes["cola"] * Player.Instance.GetKPD(), AddDrinkCola);
            return;
        }

        _inventoryAnimator.SetTrigger("noInventory");
    }

    private void AddDrinkCola()
    {
        Player.Instance.AddPlayerParameter(0, 8);
        InventoryController.Instance.RemoveFromInventory(11, 1, 0);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenSleepButton()
    {
        Player.Instance.StartSleep();
        //звук сна
    }

    public void OpenEatCureButton()
    {
        if (InventoryController.Instance.GetCountItem(0) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
            GameController.Instance.StartTimer(interactObjs[59], interactTimes["cure"] * Player.Instance.GetKPD(), AddEatCure);
            return;
        }

        _inventoryAnimator.SetTrigger("noInventory");
    }

    public void AddEatCure()
    {
        int randomR = Random.Range(0, 100);
        if (randomR >= 50)
        {
            Player.Instance.SetDesease(1, false);
        }
        InventoryController.Instance.RemoveFromInventory(0, 1, 0);
    }

    public void OpenEatBandageButton()
    {
        if (InventoryController.Instance.GetCountItem(1) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
            GameController.Instance.StartTimer(interactObjs[60], interactTimes["bandage"] * Player.Instance.GetKPD(), AddEatBandage);
            return;
        }

        _inventoryAnimator.SetTrigger("noInventory");
    }

    public void AddEatBandage()
    {
        Player.Instance.SetDesease(0, false);
        InventoryController.Instance.RemoveFromInventory(1, 1, 0);
    }

    public void OpenEatAntibioticButton()
    {
        if (InventoryController.Instance.GetCountItem(2) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
            GameController.Instance.StartTimer(interactObjs[61], interactTimes["antibiotic"] * Player.Instance.GetKPD(), AddEatAntibiotic);
            return;
        }

        _inventoryAnimator.SetTrigger("noInventory");
    }

    public void AddEatAntibiotic()
    {
        Player.Instance.SetDesease(1, false);
        InventoryController.Instance.RemoveFromInventory(2, 1, 0);
    }

    public void OpenTrapRatButton()
    {
        if (InventoryController.Instance.GetCountItem(15) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
            GameController.Instance.StartTimer(RatMineObjects[0].transform.GetChild(0).GetChild(0).gameObject, interactTimes["trap"] * Player.Instance.GetKPD(), AddTrapRat);
        }
    }


    //ловушка крысы
    public void AddTrapRat()
    {
        GameController.Instance.TrapRatStatus = 2;
        RatMineObjects[1].SetActive(true);
    }

    public void SwipeTrapRatIcon()
    {
        GameController.Instance.TrapRatStatus = 3;
        RatMineObjects[0].SetActive(false);
        RatMineObjects[1].SetActive(false);
        RatMineObjects[2].SetActive(true);
 
    }

    public void OpenGetTrapRatButton()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(RatMineObjects[2].transform.GetChild(0).GetChild(0).gameObject, interactTimes["trap"] * Player.Instance.GetKPD(), AddGetTrapRat);
    }

    public void AddGetTrapRat()
    {
        InventoryController.Instance.AddInventoryItemPLayer(6, Random.Range(1, 3), 0);
        GameController.Instance.TrapRatStatus = 1;
        RatMineObjects[0].SetActive(true);
        RatMineObjects[2].SetActive(false);

        GameController.Instance.CallRandomDesease();
    }
    //



    //ловушка воды
    public delegate void endWaterTrapTimer();

    public void OpenTrapWaterButton()
    {
        if (InventoryController.Instance.GetCountItem(13) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
            GameController.Instance.StartTimer(WaterMineObjects[0].transform.GetChild(0).GetChild(0).gameObject, interactTimes["trap"] * Player.Instance.GetKPD(), AddTrapWater);
        }
    }

    public void AddTrapWater()
    {
        GameController.Instance.TrapWaterStatus = 2;
        endWaterTrapTimer eWTT;
        eWTT = SwipeTrapWaterIcon;
        WaterMineObjects[1].GetComponent<IndiTimer>().SetTimer(eWTT, 150.0f);
        WaterMineObjects[1].SetActive(true);
    }

    public void SwipeTrapWaterIcon()
    {
        GameController.Instance.TrapWaterStatus = 3;
        WaterMineObjects[0].SetActive(false);
        WaterMineObjects[1].SetActive(false);
        WaterMineObjects[2].SetActive(true);
        
    }

    public void OpenGetTrapWaterButton()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(WaterMineObjects[2].transform.GetChild(0).GetChild(0).gameObject, interactTimes["trap"] * Player.Instance.GetKPD(), AddGetTrapWater);
    }

    public void AddGetTrapWater()
    {
        InventoryController.Instance.AddInventoryItemPLayer(8, 2, 0);
        GameController.Instance.TrapWaterStatus = 1;
        WaterMineObjects[0].SetActive(true);
        WaterMineObjects[2].SetActive(false);

        GameController.Instance.CallRandomDesease();
    }
    //
    //ловушка Грибы
    public delegate void endMushroomsTrapTimer();

    public void OpenTrapMushroomsButton()
    {
        if (InventoryController.Instance.GetCountItem(13) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
            GameController.Instance.StartTimer(MushroomsMineObjects[0].transform.GetChild(0).GetChild(0).gameObject, interactTimes["trap"] * Player.Instance.GetKPD(), AddTrapMushrooms);
            return;
        }

        _inventoryAnimator.SetTrigger("noInventory");
    }

    public void AddTrapMushrooms()
    {
        InventoryController.Instance.RemoveFromInventory(15, 1, 0);
        GameController.Instance.TrapMushroomsStatus = 2;
        endWaterTrapTimer eWTT;
        eWTT = SwipeTrapMushroomsIcon;
        MushroomsMineObjects[1].GetComponent<IndiTimer>().SetTimer(eWTT, 200.0f);
        MushroomsMineObjects[1].SetActive(true);
    }

    public void SwipeTrapMushroomsIcon()
    {
        GameController.Instance.TrapMushroomsStatus = 3;
        MushroomsMineObjects[0].SetActive(false);
        MushroomsMineObjects[1].SetActive(false);
        MushroomsMineObjects[2].SetActive(true);

    }

    public void OpenGetTrapMushroomsButton()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(MushroomsMineObjects[2].transform.GetChild(0).GetChild(0).gameObject, interactTimes["trap"] * Player.Instance.GetKPD(), AddGetTrapMushrooms);
    }

    public void AddGetTrapMushrooms()
    {
        InventoryController.Instance.AddInventoryItemPLayer(7, 2, 0);
        GameController.Instance.TrapMushroomsStatus = 1;
        MushroomsMineObjects[0].SetActive(true);
        MushroomsMineObjects[2].SetActive(false);

        GameController.Instance.CallRandomDesease();
    }


    //разрушение

    public void OpenDestroyButton()
    {
        if (GameController.Instance.IsEventDone("craftaxe"))
        {
            GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
            GameController.Instance.StartTimer(interactObjs[30], interactTimes["crack"] * Player.Instance.GetKPD(), AddDestroy);
        }
    }

    public void AddDestroy()
    {
        GameController.Instance.CallRandomDesease();
        GameController.Instance.SetEventDone("get_destroy");
    }

    public void OpenDestroy2Button()
    {
        if (GameController.Instance.IsEventDone("craftaxe"))
        {
            GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
            GameController.Instance.StartTimer(interactObjs[31], interactTimes["crack"] * Player.Instance.GetKPD(), AddDestroy2);
        }
    }

    public void AddDestroy2()
    {
        GameController.Instance.CallRandomDesease();
        GameController.Instance.SetEventDone("get_destroy2");
    }

    public void OpenDestroy3Button()
    {
        if (GameController.Instance.IsEventDone("craftaxe"))
        {
            GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
            GameController.Instance.StartTimer(interactObjs[32], interactTimes["crack"] * Player.Instance.GetKPD(), AddDestroy3);
        }
    }

    public void AddDestroy3()
    {
        GameController.Instance.CallRandomDesease();
        GameController.Instance.SetEventDone("get_destroy3");
    }

    public void OpenDestroy4Button()
    {
        if (GameController.Instance.IsEventDone("craftaxe"))
        {
            GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
            GameController.Instance.StartTimer(interactObjs[33], interactTimes["crack"] * Player.Instance.GetKPD(), AddDestroy4);
        }
    }

    public void AddDestroy4()
    {
        Debug.Log("222");
        GameController.Instance.CallRandomDesease();
        GameController.Instance.SetEventDone("get_destroy4");
    }

    public void OpenDestroy5Button()
    {
        if (GameController.Instance.IsEventDone("craftaxe"))
        {
            GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
            GameController.Instance.StartTimer(interactObjs[34], interactTimes["crack"] * Player.Instance.GetKPD(), AddDestroy5);
        }
    }

    public void AddDestroy5()
    {
        GameController.Instance.CallRandomDesease();
        GameController.Instance.SetEventDone("get_destroy5");
    }

    public void OpenDestroy6Button()
    {
        if (GameController.Instance.IsEventDone("craftaxe"))
        {
            GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
            GameController.Instance.StartTimer(interactObjs[35], interactTimes["crack"] * Player.Instance.GetKPD(), AddDestroy6);
        }
    }

    public void AddDestroy6()
    {
        GameController.Instance.CallRandomDesease();
        GameController.Instance.SetEventDone("get_destroy6");
    }

    public void OpenDestroy7Button()
    {
        if (GameController.Instance.IsEventDone("craftaxe"))
        {
            GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
            GameController.Instance.StartTimer(interactObjs[36], interactTimes["crack"] * Player.Instance.GetKPD(), AddDestroy7);
        }
    }

    public void AddDestroy7()
    {
        GameController.Instance.CallRandomDesease();
        GameController.Instance.SetEventDone("get_destroy7");
    }

    public void OpenDestroy8Button()
    {
        if (GameController.Instance.IsEventDone("craftaxe"))
        {
            GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
            GameController.Instance.StartTimer(interactObjs[37], interactTimes["crack"] * Player.Instance.GetKPD(), AddDestroy8);
        }
    }

    public void AddDestroy8()
    {
        GameController.Instance.CallRandomDesease();
        GameController.Instance.SetEventDone("get_destroy8");
    }

    public void OpenDestroy9Button()
    {
        if (GameController.Instance.IsEventDone("craftaxe"))
        {
            GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
            GameController.Instance.StartTimer(interactObjs[38], interactTimes["crack"] * Player.Instance.GetKPD(), AddDestroy9);
        }
    }

    public void AddDestroy9()
    {
        GameController.Instance.CallRandomDesease();
        GameController.Instance.SetEventDone("get_destroy9");
    }

    public void OpenDestroy10Button()
    {
        if (GameController.Instance.IsEventDone("craftaxe"))
        {
            GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
            GameController.Instance.StartTimer(interactObjs[39], interactTimes["crack"] * Player.Instance.GetKPD(), AddDestroy10);
        }
    }

    public void AddDestroy10()
    {
        GameController.Instance.CallRandomDesease();
        GameController.Instance.SetEventDone("get_destroy10");
    }



    //подбор предметов

    public void OpenItem1Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[0], interactTimes["take"] * Player.Instance.GetKPD(), AddItem1);
    }

    public void AddItem1()
    {
        HUD.Instance.LoadNewInventory(1);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(1);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem2Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[1], interactTimes["take"] * Player.Instance.GetKPD(), AddItem2);
    }

    public void AddItem2()
    {
        HUD.Instance.LoadNewInventory(2);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(2);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem3Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[2], interactTimes["take"] * Player.Instance.GetKPD(), AddItem3);
    }

    public void AddItem3()
    {
        HUD.Instance.LoadNewInventory(3);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(3);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem4Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[3], interactTimes["take"] * Player.Instance.GetKPD(), AddItem4);
    }

    public void AddItem4()
    {
        HUD.Instance.LoadNewInventory(4);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(4);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem5Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[4], interactTimes["take"] * Player.Instance.GetKPD(), AddItem5);
    }

    public void AddItem5()
    {
        HUD.Instance.LoadNewInventory(5);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(5);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem6Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[5], interactTimes["take"] * Player.Instance.GetKPD(), AddItem6);
    }

    public void AddItem6()
    {
        HUD.Instance.LoadNewInventory(6);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(6);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem7Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[6], interactTimes["take"] * Player.Instance.GetKPD(), AddItem7);
    }

    public void AddItem7()
    {
        HUD.Instance.LoadNewInventory(7);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(7);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem8Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[7], interactTimes["take"] * Player.Instance.GetKPD(), AddItem8);
    }

    public void AddItem8()
    {
        HUD.Instance.LoadNewInventory(8);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(8);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem9Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[8], interactTimes["take"] * Player.Instance.GetKPD(), AddItem9);
    }

    public void AddItem9()
    {
        HUD.Instance.LoadNewInventory(9);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(9);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem10Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[9], interactTimes["take"] * Player.Instance.GetKPD(), AddItem10);
    }

    public void AddItem10()
    {
        HUD.Instance.LoadNewInventory(10);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(10);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem11Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[10], interactTimes["take"] * Player.Instance.GetKPD(), AddItem11);
    }

    public void AddItem11()
    {
        HUD.Instance.LoadNewInventory(11);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(11);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem12Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[11], interactTimes["take"] * Player.Instance.GetKPD(), AddItem12);
    }

    public void AddItem12()
    {
        HUD.Instance.LoadNewInventory(12);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(12);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem13Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[12], interactTimes["take"] * Player.Instance.GetKPD(), AddItem13);
    }

    public void AddItem13()
    {
        HUD.Instance.LoadNewInventory(13);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(13);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem14Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[13], interactTimes["take"] * Player.Instance.GetKPD(), AddItem14);
    }

    public void AddItem14()
    {
        HUD.Instance.LoadNewInventory(14);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(14);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem15Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[14], interactTimes["take"] * Player.Instance.GetKPD(), AddItem15);
    }

    public void AddItem15()
    {
        HUD.Instance.LoadNewInventory(15);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(15);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem16Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[15], interactTimes["take"] * Player.Instance.GetKPD(), AddItem16);
    }

    public void AddItem16()
    {
        HUD.Instance.LoadNewInventory(16);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(16);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem17Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[16], interactTimes["take"] * Player.Instance.GetKPD(), AddItem17);
    }

    public void AddItem17()
    {
        HUD.Instance.LoadNewInventory(17);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(17);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem18Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[17], interactTimes["take"] * Player.Instance.GetKPD(), AddItem18);
    }

    public void AddItem18()
    {
        HUD.Instance.LoadNewInventory(18);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(18);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem19Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[18], interactTimes["take"] * Player.Instance.GetKPD(), AddItem19);
    }

    public void AddItem19()
    {
        HUD.Instance.LoadNewInventory(19);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(19);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem20Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[19], interactTimes["take"] * Player.Instance.GetKPD(), AddItem20);
    }

    public void AddItem20()
    {
        HUD.Instance.LoadNewInventory(20);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(20);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem21Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[20], interactTimes["take"] * Player.Instance.GetKPD(), AddItem21);
    }

    public void AddItem21()
    {
        HUD.Instance.LoadNewInventory(21);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(21);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem22Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[21], interactTimes["take"] * Player.Instance.GetKPD(), AddItem22);
    }

    public void AddItem22()
    {
        HUD.Instance.LoadNewInventory(22);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(22);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem23Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[22], interactTimes["take"] * Player.Instance.GetKPD(), AddItem23);
    }

    public void AddItem23()
    {
        HUD.Instance.LoadNewInventory(23);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(23);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem24Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[23], interactTimes["take"] * Player.Instance.GetKPD(), AddItem24);
    }

    public void AddItem24()
    {
        HUD.Instance.LoadNewInventory(24);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(24);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem25Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[24], interactTimes["take"] * Player.Instance.GetKPD(), AddItem25);
    }

    public void AddItem25()
    {
        HUD.Instance.LoadNewInventory(25);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(25);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem26Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[25], interactTimes["take"] * Player.Instance.GetKPD(), AddItem26);
    }

    public void AddItem26()
    {
        HUD.Instance.LoadNewInventory(26);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(26);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem27Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[26], interactTimes["take"] * Player.Instance.GetKPD(), AddItem27);
    }

    public void AddItem27()
    {
        HUD.Instance.LoadNewInventory(27);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(27);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem28Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[27], interactTimes["take"] * Player.Instance.GetKPD(), AddItem28);
    }

    public void AddItem28()
    {
        HUD.Instance.LoadNewInventory(28);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(28);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem29Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[28], interactTimes["take"] * Player.Instance.GetKPD(), AddItem29);
    }

    public void AddItem29()
    {
        HUD.Instance.LoadNewInventory(29);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(29);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenItem30Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[29], interactTimes["take"] * Player.Instance.GetKPD(), AddItem30);
    }

    public void AddItem30()
    {
        HUD.Instance.LoadNewInventory(30);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(30);

        GameController.Instance.CallRandomDesease();
    }

    /// <summary>
    /// Открытие окна информации
    /// </summary>
    private void OpenInfoButton()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[40], interactTimes["info"] * Player.Instance.GetKPD(), AddInfo);
    }

    private void AddInfo()
    {
        TextController.Instance.SetInfoLabelText(BbtStrings.GetStr("str_info"));
        HUD.Instance.ShowInfoWindow();
    }

    private void OpenInfo1Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[41], interactTimes["info"] * Player.Instance.GetKPD(), AddInfo1);
    }

    private void AddInfo1()
    {
        TextController.Instance.SetInfoLabelText(BbtStrings.GetStr("str_info1"));
        HUD.Instance.ShowInfoWindow();
    }

    private void OpenInfo2Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[42], interactTimes["info"] * Player.Instance.GetKPD(), AddInfo2);
    }

    private void AddInfo2()
    {
        TextController.Instance.SetInfoLabelText(BbtStrings.GetStr("str_info2"));
        HUD.Instance.ShowInfoWindow();
    }

    private void OpenInfo3Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[43], interactTimes["info"] * Player.Instance.GetKPD(), AddInfo3);
    }

    private void AddInfo3()
    {
        TextController.Instance.SetInfoLabelText(BbtStrings.GetStr("str_info3"));
        HUD.Instance.ShowInfoWindow();
    }

    private void OpenInfo4Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[44], interactTimes["info"] * Player.Instance.GetKPD(), AddInfo4);
    }

    private void AddInfo4()
    {
        TextController.Instance.SetInfoLabelText(BbtStrings.GetStr("str_info4"));
        HUD.Instance.ShowInfoWindow();
    }

    private void OpenInfo5Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[45], interactTimes["info"] * Player.Instance.GetKPD(), AddInfo5);
    }

    private void AddInfo5()
    {
        TextController.Instance.SetInfoLabelText(BbtStrings.GetStr("str_info5"));
        HUD.Instance.ShowInfoWindow();
    }

    private void OpenInfo6Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[46], interactTimes["info"] * Player.Instance.GetKPD(), AddInfo6);
    }

    private void AddInfo6()
    {
        TextController.Instance.SetInfoLabelText(BbtStrings.GetStr("str_info6"));
        HUD.Instance.ShowInfoWindow();
    }

    private void OpenInfo7Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[47], interactTimes["info"] * Player.Instance.GetKPD(), AddInfo7);
    }

    private void AddInfo7()
    {
        TextController.Instance.SetInfoLabelText(BbtStrings.GetStr("str_info7"));
        HUD.Instance.ShowInfoWindow();
    }

    private void OpenInfo8Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[48], interactTimes["info"] * Player.Instance.GetKPD(), AddInfo8);
    }

    private void AddInfo8()
    {
        TextController.Instance.SetInfoLabelText(BbtStrings.GetStr("str_info8"));
        HUD.Instance.ShowInfoWindow();
    }

    private void OpenInfo9Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(interactObjs[49], interactTimes["info"] * Player.Instance.GetKPD(), AddInfo9);
    }

    private void AddInfo9()
    {
        TextController.Instance.SetInfoLabelText(BbtStrings.GetStr("str_info9"));
        HUD.Instance.ShowInfoWindow();
    }

}
