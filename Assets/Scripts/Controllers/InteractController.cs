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
        ["drinkWater"] = 2.00f,
        ["eat"] = 2.0f,
        ["sleep"] = 2.0f,
        ["crack"] = 2.0f,
        ["elevator"] = 2.0f,
        ["bint"] = 2.0f,
        ["take"] = 2.0f,
        ["info"] = 2.0f,
        ["dealer"] = 2.0f,
        ["trapWater"] = 10.0f,
    };

    public List<Button> ElevatorButtons = new List<Button>();
    public List<GameObject> RatMineObjects = new List<GameObject>();
    public List<GameObject> WaterMineObjects = new List<GameObject>();
    public List<GameObject> MushroomsMineObjects = new List<GameObject>();

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        SetScaleButton();
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
    /// Открытие окна информации
    /// </summary>
    private void OpenInfoButton()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(GameObject.Find("InfoButton").transform.GetChild(0).GetChild(0).gameObject, interactTimes["info"] * Player.Instance.GetKPD(), AddInfo);
    }

    private void AddInfo()
    {
        TextController.Instance.SetInfoLabelText("Помогите! \n Меня держат здесь насильно!");
        HUD.Instance.ShowInfoWindow();
    }







    /// <summary>
    /// Открытие торговца
    /// </summary>
    public void OpenDealerButton()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(GameObject.Find("DealerButton").transform.GetChild(0).GetChild(0).gameObject, interactTimes["dealer"] * Player.Instance.GetKPD(), AddDealer);
    }

    public void AddDealer()
    {
        InventoryController.Instance.CheckTrade();
        HUD.Instance.LoadTradeInventory(-3);
        HUD.Instance.ShowDealerWindow();
        InventoryController.Instance.SetOpenedPlace(-3);
    }








    /// <summary>
    /// Открытие подбора предмета для ящика 1
    /// </summary>
    public void OpenItem1Button( )
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(GameObject.Find("Item1Button").transform.GetChild(0).GetChild(0).gameObject, interactTimes["eat"] * Player.Instance.GetKPD(), AddItem1);
    }

    public void AddItem1()
    {
        HUD.Instance.LoadNewInventory(1);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(1);

        GameController.Instance.CallRandomDesease();
    }

    /// <summary>
    /// Открытие подбора предмета для ящика 2
    /// </summary>
    public void OpenItem2Button()
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
        GameController.Instance.StartTimer(GameObject.Find("Item2Button").transform.GetChild(0).GetChild(0).gameObject, interactTimes["eat"] * Player.Instance.GetKPD(), AddItem2);
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
        GameController.Instance.StartTimer(GameObject.Find("Item3Button").transform.GetChild(0).GetChild(0).gameObject, interactTimes["eat"] * Player.Instance.GetKPD(), AddItem3);
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
        GameController.Instance.StartTimer(GameObject.Find("Item4Button").transform.GetChild(0).GetChild(0).gameObject, interactTimes["eat"] * Player.Instance.GetKPD(), AddItem4);
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
        GameController.Instance.StartTimer(GameObject.Find("Item5Button").transform.GetChild(0).GetChild(0).gameObject, interactTimes["eat"] * Player.Instance.GetKPD(), AddItem5);
    }

    public void AddItem5()
    {
        HUD.Instance.LoadNewInventory(5);
        HUD.Instance.ShowSwitchItemsWindow();
        InventoryController.Instance.SetOpenedPlace(5);

        GameController.Instance.CallRandomDesease();
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
            GameController.Instance.StartTimer(GameObject.Find("EatRatButton").transform.GetChild(0).GetChild(0).gameObject, interactTimes["eat"] * Player.Instance.GetKPD(), AddEatRat);
        }
    }

    public void AddEatRat()
    {
        Player.Instance.AddPlayerParameter(1, 10);
        InventoryController.Instance.RemoveFromInventory(16, 1, 0);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenEatEnergybarButton()
    {
        if (InventoryController.Instance.GetCountItem(4) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_eat_something");
            GameController.Instance.StartTimer(GameObject.Find("EatEnergybarButton").transform.GetChild(0).GetChild(0).gameObject, interactTimes["eat"] * Player.Instance.GetKPD(), AddEatEnergybar);
        }
    }

    public void AddEatEnergybar()
    {
        Player.Instance.AddPlayerParameter(1, 10);
        InventoryController.Instance.RemoveFromInventory(4, 1, 0);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenEatNoodlesButton()
    {
        if (InventoryController.Instance.GetCountItem(5) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_eat_something");
            GameController.Instance.StartTimer(GameObject.Find("EatNoodlesButton").transform.GetChild(0).GetChild(0).gameObject, interactTimes["eat"] * Player.Instance.GetKPD(), AddEatNoodles);
        }
    }

    public void AddEatNoodles()
    {
        Player.Instance.AddPlayerParameter(1, 10);
        InventoryController.Instance.RemoveFromInventory(5, 1, 0);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenEatMushroomsButton()
    {
        if (InventoryController.Instance.GetCountItem(7) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_eat_something");
            GameController.Instance.StartTimer(GameObject.Find("EatMushroomsButton").transform.GetChild(0).GetChild(0).gameObject, interactTimes["eat"] * Player.Instance.GetKPD(), AddEatMushrooms);
        }
    }

    public void AddEatMushrooms()
    {
        Player.Instance.AddPlayerParameter(1, 10);
        InventoryController.Instance.RemoveFromInventory(7, 1, 0);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenDrinkWaterButton()
    {
        if (InventoryController.Instance.GetCountItem(8) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_drink_something");
            GameController.Instance.StartTimer(GameObject.Find("DrinkWaterButton").transform.GetChild(0).GetChild(0).gameObject, interactTimes["drinkWater"] * Player.Instance.GetKPD(), AddDrinkWater);
        }
    }

    private void AddDrinkWater()
    {
        Player.Instance.AddPlayerParameter(0, 10);
        InventoryController.Instance.RemoveFromInventory(8, 1, 0);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenDrinkCoffeeButton()
    {
        if (InventoryController.Instance.GetCountItem(9) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_drink_something");
            GameController.Instance.StartTimer(GameObject.Find("DrinkCoffeeButton").transform.GetChild(0).GetChild(0).gameObject, interactTimes["drinkWater"] * Player.Instance.GetKPD(), AddDrinkCoffee);
        }
    }

    private void AddDrinkCoffee()
    {
        Player.Instance.AddPlayerParameter(0, 10);
        InventoryController.Instance.RemoveFromInventory(9, 1, 0);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenDrinkJuiceButton()
    {
        if (InventoryController.Instance.GetCountItem(10) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_drink_something");
            GameController.Instance.StartTimer(GameObject.Find("DrinkJuiceButton").transform.GetChild(0).GetChild(0).gameObject, interactTimes["drinkWater"] * Player.Instance.GetKPD(), AddDrinkJuice);
        }
    }

    private void AddDrinkJuice()
    {
        Player.Instance.AddPlayerParameter(0, 10);
        InventoryController.Instance.RemoveFromInventory(10, 1, 0);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenDrinkColaButton()
    {
        if (InventoryController.Instance.GetCountItem(11) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_drink_something");
            GameController.Instance.StartTimer(GameObject.Find("DrinkColaButton").transform.GetChild(0).GetChild(0).gameObject, interactTimes["drinkWater"] * Player.Instance.GetKPD(), AddDrinkCola);
        }
    }

    private void AddDrinkCola()
    {
        Player.Instance.AddPlayerParameter(0, 10);
        InventoryController.Instance.RemoveFromInventory(11, 1, 0);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenSleepButton()
    {
        Player.Instance.StartSleep();
        //звук сна
    }

    public void OpenDestroyButton()
    {
        if (GameController.Instance.IsEventDone( "craftaxe" ) )
        {
            GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
            GameController.Instance.StartTimer(GameObject.Find("DestroyButton").transform.GetChild(0).GetChild(0).gameObject, interactTimes["crack"] * Player.Instance.GetKPD(), AddDestroy);
        }
    }

    public void AddDestroy()
    {
        interactObjs[13].gameObject.transform.parent.GetComponent<NavMeshObstacle>().enabled = false;
        Destroy(interactObjs[13].gameObject);

        GameController.Instance.CallRandomDesease();
    }

    public void OpenEatCureButton()
    {
        if (InventoryController.Instance.GetCountItem(0) > 0.9)
        {
            GameController.Instance.AudioManager.PlaySFX("aud_clk_trech_out");
            GameController.Instance.StartTimer(GameObject.Find("EatCureButton").transform.GetChild(0).GetChild(0).gameObject, interactTimes["bint"] * Player.Instance.GetKPD(), AddEatCure);
        }
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
            GameController.Instance.StartTimer(GameObject.Find("EatBandageButton").transform.GetChild(0).GetChild(0).gameObject, interactTimes["bint"] * Player.Instance.GetKPD(), AddEatBandage);
        }
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
            GameController.Instance.StartTimer(GameObject.Find("EatAntibioticButton").transform.GetChild(0).GetChild(0).gameObject, interactTimes["bint"] * Player.Instance.GetKPD(), AddEatAntibiotic);
        }
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
            GameController.Instance.StartTimer(RatMineObjects[0].transform.GetChild(0).GetChild(0).gameObject, interactTimes["bint"] * Player.Instance.GetKPD(), AddTrapRat);
        }
    }


    //ловушка крысы
    public void AddTrapRat()
    {
        InventoryController.Instance.RemoveFromInventory(15, 1, 0);
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
        GameController.Instance.StartTimer(RatMineObjects[2].transform.GetChild(0).GetChild(0).gameObject, interactTimes["eat"] * Player.Instance.GetKPD(), AddGetTrapRat);
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
            GameController.Instance.StartTimer(WaterMineObjects[0].transform.GetChild(0).GetChild(0).gameObject, interactTimes["bint"] * Player.Instance.GetKPD(), AddTrapWater);
        }
    }

    public void AddTrapWater()
    {
        InventoryController.Instance.RemoveFromInventory(13, 3, 0);
        GameController.Instance.TrapWaterStatus = 2;
        endWaterTrapTimer eWTT;
        eWTT = SwipeTrapWaterIcon;
        WaterMineObjects[1].GetComponent<IndiTimer>().SetTimer(eWTT, interactTimes["trapWater"]);
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
        GameController.Instance.StartTimer(WaterMineObjects[2].transform.GetChild(0).GetChild(0).gameObject, interactTimes["eat"] * Player.Instance.GetKPD(), AddGetTrapWater);
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
            GameController.Instance.StartTimer(MushroomsMineObjects[0].transform.GetChild(0).GetChild(0).gameObject, interactTimes["bint"] * Player.Instance.GetKPD(), AddTrapMushrooms);
        }
    }

    public void AddTrapMushrooms()
    {
        InventoryController.Instance.RemoveFromInventory(15, 3, 0);
        GameController.Instance.TrapMushroomsStatus = 2;
        endWaterTrapTimer eWTT;
        eWTT = SwipeTrapMushroomsIcon;
        MushroomsMineObjects[1].GetComponent<IndiTimer>().SetTimer(eWTT, interactTimes["trapWater"]);
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
        GameController.Instance.StartTimer(MushroomsMineObjects[2].transform.GetChild(0).GetChild(0).gameObject, interactTimes["eat"] * Player.Instance.GetKPD(), AddGetTrapMushrooms);
    }

    public void AddGetTrapMushrooms()
    {
        InventoryController.Instance.AddInventoryItemPLayer(7, 2, 0);
        GameController.Instance.TrapMushroomsStatus = 1;
        MushroomsMineObjects[0].SetActive(true);
        MushroomsMineObjects[2].SetActive(false);

        GameController.Instance.CallRandomDesease();
    }

}
