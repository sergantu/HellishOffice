using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    static private HUD _instance;
    public static HUD Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField] List<GameObject> windowsUI;  //список UI окон
    [SerializeField] InventoryUIButton inventoryItemPrefab; //перфаб кнопки в инвентаре
    [SerializeField] CraftUIButton craftButtonPrefab;
    [SerializeField] NeededItemUIPanel neededItemPrefab;
    [SerializeField] Transform inventoryContainer;  //контейнер для кнопок инвентаря в окне инвентаря игрока

    [SerializeField] Transform inventorySwitchContainer; //контейнер для кнопок инвентаря в окне обмена вещами справа (вещи игрока)
    [SerializeField] Transform inventoryNewItems;  //контейнер для кнопок инвентаря в окне обмена вещами слева (подбираемые вещи)

    [SerializeField] Transform invTradePlayer; 
    [SerializeField] Transform invTradeDealer;  
    [SerializeField] Transform resTradePlayer;
    [SerializeField] Transform resTradeDealer; 

    [SerializeField] Transform craftContainer;

    //params
    [SerializeField] Transform mainParamPanel;
    [SerializeField] IconParam prefabPlayerParam;
    [SerializeField] string[] iconsNameParam = new string[] { "energyParam", "healthParam", "waterParam", "foodParam" };

    //в инвентаре игрока
    [SerializeField] Image invImage;
    [SerializeField] Text invLabel;
    [SerializeField] Text invDescription;

    //в инвентаре обмена
    [SerializeField] Image switchImage;
    [SerializeField] Text switchLabel;
    [SerializeField] Text switchDescription;

    [SerializeField] Image invRamka;
    public Image InvRamka { get => invRamka; set => invRamka = value; }

    [SerializeField] Slider progressSlider;

    [SerializeField] GameObject warningLabel;

    [SerializeField] Slider soundLevel;
    [SerializeField] Slider musicLevel;

    public enum WindowState
    {
        PlayerInventory,
        SwitchInventory,
        TradeInventory,
        Closed
    }

    public WindowState OpenedWindow;

    private void Awake()
    {
        _instance = this;
        InitWindowsUI();
        OpenedWindow = WindowState.Closed;
        invImage.transform.parent.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        switchImage.transform.parent.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        InvRamka.transform.localScale = Vector3.zero;
        InvRamka.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        InvRamka.GetComponent<RectTransform>().offsetMin = Vector2.zero;
    }

    private void Start()
    {
        JSONSave.Instance.LoadDataHud();
        LoadInventory(); //загруза инвентарных окон кнопками инвентаря
        //LoadCraftButtons();
    }

    public float GetSoundLevel()
    {
        return soundLevel.value;
    }

    public void SetSoundLevel(float value)
    {
        soundLevel.value = value;
    }

    public float GetMusicLevel()
    {
        return musicLevel.value;
    }

    public void SetMusicLevel(float value)
    {
        musicLevel.value = value;
    }

    ////////////////////////////////////////////////////////////////////////////////////
    ///ОБНОВЛЕНИЕ ПРОГРЕССА
    ////////////////////////////////////////////////////////////////////////////////////

    public void UpdateProgressSlider( float progress )
    {
        progressSlider.value = progress / Player.Instance.Max_progress;
    }

    ////////////////////////////////////////////////////////////////////////////////////
    ///ОБНОВЛЕНИЕ ДАТЫ И ВРЕМЕНИ
    ////////////////////////////////////////////////////////////////////////////////////

    [SerializeField] Text datePanel;

    public void UpdateDateTime( string doneDate )
    {
        datePanel.text = doneDate;

        for ( int i = 0; i < InteractController.Instance.ElevatorButtons.Count; i++ )
        {
            InteractController.Instance.ElevatorButtons[i].interactable = GameController.Instance.StatusIsNight;
        }

        if ( GameController.Instance.time > 5 && GameController.Instance.time < 9 )
        {
            warningLabel.SetActive(true);
        }
        else
        {
            warningLabel.SetActive(false);
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////


    ////////////////////////////////////////////////////////////////////////////////////
    ///ОБНОВЛЕНИЕ ИКОНОК ПАРАМЕТРОВ ИГРОКА
    ////////////////////////////////////////////////////////////////////////////////////

    public void UpdateMainParamIcons()
    {
        for ( int i = 0; i < Player.Instance.PlayerParametres.Count; i++ )
        {
            GameObject curParamObj = IconIsCreated(i);

            if (Player.Instance.PlayerParametres[i] > 27) //нет необходимости
            {
                curParamObj.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            }
            else if (Player.Instance.PlayerParametres[i] < 28 && Player.Instance.PlayerParametres[i] > 18)//легкая необходимость
            {
                curParamObj.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            else if (Player.Instance.PlayerParametres[i] < 19 && Player.Instance.PlayerParametres[i] > 0) //сильная необхожимость
            {
                curParamObj.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            }
            else //умер
            {
                curParamObj.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                ShowLevelLoseWindow();
            }

        }
    }

    private GameObject IconIsCreated( int idPar )
    {
        for (int i = 0; i < mainParamPanel.childCount; i++)
        {
            GameObject curObjParam = mainParamPanel.GetChild(i).gameObject;

            if ( idPar == curObjParam.GetComponent<IconParam>().PlaceID)
            {
                curObjParam.GetComponent<Image>().color = Color.red;
                return curObjParam;
            }
        }

        return null;
    }
    
    ////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////////////
    ///МЕТОДЫ БЛОКИРОВАНИЯ КЛИКОВ
    ////////////////////////////////////////////////////////////////////////////////////


    /// <summary>
    /// Установить блокировку кликов
    /// </summary>
    /// <param name="isLocked">bool поставить или нет</param>
    public void SetLock(bool isLocked)  // блокировка нажатия не на UI
    {
        if (isLocked)
        {
            GameController.Instance.State = GameState.Pause;
        }
        else
        {
            GameController.Instance.State = GameState.Play;
        }
    }

    /// <summary>
    /// Заблокированы ли нажатия?
    /// </summary>
    /// <returns>bool: true-заблокировано, false-не заблокировано</returns>
    public bool isLocked()  //блокированы ли клики?
    {
        if (GameController.Instance.State == GameState.Pause)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

 ////////////////////////////////////////////////////////////////////////////////////
 ///МЕТОДЫ ОБРАБОТКИ ИНВЕНТАРЕЙ
 ////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Создать кнопку инвентаря, добавить в окнока инвентаря игрока
    /// </summary>
    /// <param name="itemData">экземпляр InventoryItem</param>
    /// <returns>Кнопка со значениями</returns>
    public InventoryUIButton AddNewInventoryItem(ItemVar newItemVar, int count, int placeID)  //создать кнопку инвентаря, добавить в окнока инвентаря игрока, вернуть объект этой кнопки
    {
        InventoryUIButton newItem = Instantiate(inventoryItemPrefab) as InventoryUIButton;
        if ( placeID == 0 )
        {
            newItem.transform.SetParent(inventoryContainer);
            newItem.ItemVarCur = newItemVar;
            newItem.NewCount = count;
            newItem.PlaceID = placeID;
        }
        else if ( placeID == -1 )
        {
            newItem.transform.SetParent(inventorySwitchContainer);
            newItem.ItemVarCur = newItemVar;
            newItem.NewCount = count;
            newItem.PlaceID = placeID;
        }
        else if (placeID == -2)
        {
            newItem.transform.SetParent(invTradePlayer);
            newItem.ItemVarCur = newItemVar;
            newItem.NewCount = count;
            newItem.PlaceID = placeID;
        }
        else if (placeID == -3)
        {
            newItem.transform.SetParent(invTradeDealer);
            newItem.ItemVarCur = newItemVar;
            newItem.NewCount = count;
            newItem.PlaceID = placeID;
        }
        else if (placeID == -4)
        {
            newItem.transform.SetParent(resTradePlayer);
            newItem.ItemVarCur = newItemVar;
            newItem.NewCount = count;
            newItem.PlaceID = placeID;
        }
        else if (placeID == -5)
        {
            newItem.transform.SetParent(resTradeDealer);
            newItem.ItemVarCur = newItemVar;
            newItem.NewCount = count;
            newItem.PlaceID = placeID;
        }
        else
        {
            newItem.transform.SetParent(inventoryNewItems);
            newItem.ItemVarCur = newItemVar;
            newItem.NewCount = count;
            newItem.PlaceID = placeID;
        }
        
        
        return newItem;
    }

    /// <summary>
    /// Загрузка инвентаря в инвентарь игрока
    /// </summary>
    public void LoadInventory() // загрущка инвентаря, назначение делегата на нажатие кнопки
    {
        InventoryUsedCallback callback = new InventoryUsedCallback(InventoryController.Instance.InventoryItemShow);
        for (int i = 0; i < InventoryController.Instance.PlayerInventory.Count; i++)
        {
            if (InventoryController.Instance.PlayerInventory[i][2] == 0)
            {
                try //заебала ошибку выдавать, хуй знает в чем проблема пусть будет так
                {
                    ItemVar newItemVar = InventoryController.Instance.InventoryVariables[InventoryController.Instance.PlayerInventory[i][0]];

                    InventoryUIButton newItem = AddNewInventoryItem(newItemVar, InventoryController.Instance.PlayerInventory[i][1], 0);
                    newItem.Callback = callback;

                    InventoryUIButton newItem1 = AddNewInventoryItem(newItemVar, InventoryController.Instance.PlayerInventory[i][1], -1);
                    newItem1.Callback = callback;

                    InventoryUIButton newItem2 = AddNewInventoryItem(newItemVar, InventoryController.Instance.PlayerInventory[i][1], -2);
                    newItem2.Callback = callback;
                }
                catch
                {

                }
                
            }
        }
    }


    /// <summary>
    /// Загрузка предметов в левую часть(в подбираемые предметы) во время подбора
    /// </summary>
    /// <param name="itemData">Массив с предметами для добавления</param>
    public void LoadNewInventory(int placeID)  //загрузка предметов в левой части (то, что можно подобрать)
    {
        if (inventoryNewItems.transform.childCount > 0)
        {
            for (int i = 0; i < inventoryNewItems.transform.childCount; i++)
            {
                if (inventoryNewItems.GetChild(i).GetComponent<InventoryUIButton>() != null)
                {
                    Destroy(inventoryNewItems.GetChild(i).gameObject);
                }
            }
        }

        InventoryUsedCallback callback = new InventoryUsedCallback(InventoryController.Instance.InventoryItemShow);

        for ( int i = 0; i < InventoryController.Instance.PlayerInventory.Count; i++ )
        {
            if (InventoryController.Instance.PlayerInventory[i][2] == placeID )
            {
                ItemVar tempItem = InventoryController.Instance.InventoryVariables[InventoryController.Instance.PlayerInventory[i][0]];
                InventoryUIButton newItem = AddNewInventoryItem(tempItem, InventoryController.Instance.PlayerInventory[i][1], InventoryController.Instance.PlayerInventory[i][2]);
                newItem.Callback = callback;
            }
        }
    }


    /// <summary>
    /// Загрузка предметов в окно торговца
    /// </summary>
    /// <param name="itemData">Массив с предметами для добавления</param>
    public void LoadTradeInventory(int placeID)  //загрузка предметов в левой части (то, что можно подобрать)
    {
        if (invTradeDealer.transform.childCount > 0)
        {
            for (int i = 0; i < invTradeDealer.transform.childCount; i++)
            {
                if (invTradeDealer.GetChild(i).GetComponent<InventoryUIButton>() != null)
                {
                    Destroy(invTradeDealer.GetChild(i).gameObject);
                }
            }
        }

        InventoryUsedCallback callback = new InventoryUsedCallback(InventoryController.Instance.InventoryItemShow);

        for (int i = 0; i < InventoryController.Instance.PlayerInventory.Count; i++)
        {
            if (InventoryController.Instance.PlayerInventory[i][2] == placeID)
            {
                ItemVar tempItem = InventoryController.Instance.InventoryVariables[InventoryController.Instance.PlayerInventory[i][0]];
                InventoryUIButton newItem = AddNewInventoryItem(tempItem, InventoryController.Instance.PlayerInventory[i][1], -3);
                newItem.Callback = callback;
            }
        }

        //временно
        if (resTradeDealer.transform.childCount > 0)
        {
            for (int i = 0; i < resTradeDealer.transform.childCount; i++)
            {
                if (resTradeDealer.GetChild(i).GetComponent<InventoryUIButton>() != null)
                {
                    Destroy(resTradeDealer.GetChild(i).gameObject);
                }
            }
        }

        InventoryUsedCallback callback1 = new InventoryUsedCallback(InventoryController.Instance.InventoryItemShow);

        for (int i = 0; i < InventoryController.Instance.PlayerInventory.Count; i++)
        {
            if (InventoryController.Instance.PlayerInventory[i][2] == -5)
            {
                ItemVar tempItem = InventoryController.Instance.InventoryVariables[InventoryController.Instance.PlayerInventory[i][0]];
                InventoryUIButton newItem = AddNewInventoryItem(tempItem, InventoryController.Instance.PlayerInventory[i][1], -5);
                newItem.Callback = callback1;
            }
        }


        if (resTradePlayer.transform.childCount > 0)
        {
            for (int i = 0; i < resTradePlayer.transform.childCount; i++)
            {
                if (resTradePlayer.GetChild(i).GetComponent<InventoryUIButton>() != null)
                {
                    Destroy(resTradePlayer.GetChild(i).gameObject);
                }
            }
        }

        InventoryUsedCallback callback2 = new InventoryUsedCallback(InventoryController.Instance.InventoryItemShow);

        for (int i = 0; i < InventoryController.Instance.PlayerInventory.Count; i++)
        {
            if (InventoryController.Instance.PlayerInventory[i][2] == -4)
            {
                ItemVar tempItem = InventoryController.Instance.InventoryVariables[InventoryController.Instance.PlayerInventory[i][0]];
                InventoryUIButton newItem = AddNewInventoryItem(tempItem, InventoryController.Instance.PlayerInventory[i][1], -4);
                newItem.Callback = callback2;
            }
        }

        //

    }

    public void LastUpdateInventory()
    {
        for ( int i = 0; i < InventoryController.Instance.PlayerInventory.Count; i++ )
        {
            if (inventoryNewItems.transform.childCount > 0 && InventoryController.Instance.PlayerInventory[i][2] > 0)
            {
                for (int j = 0; j < inventoryNewItems.transform.childCount; j++)
                {
                    if (inventoryNewItems.GetChild(j).GetComponent<InventoryUIButton>() != null)
                    {
                        InventoryUIButton curButton = inventoryNewItems.GetChild(j).GetComponent<InventoryUIButton>();

                        if (InventoryController.Instance.PlayerInventory[i][0] == curButton.ItemVarCur.IdItem && InventoryController.Instance.PlayerInventory[i][2] == curButton.PlaceID)
                        {
                            if (InventoryController.Instance.PlayerInventory[i][1] > 0)
                            {
                                curButton.Count.text = InventoryController.Instance.PlayerInventory[i][1].ToString();
                            }
                            else
                            {
                                Destroy(curButton.gameObject);
                            }
                        }
                    }
                }
            }
            if (invTradeDealer.transform.childCount > 0 && InventoryController.Instance.PlayerInventory[i][2] == -3 )
            {
                for (int j = 0; j < invTradeDealer.transform.childCount; j++)
                {
                    if (invTradeDealer.GetChild(j).GetComponent<InventoryUIButton>() != null)
                    {
                        InventoryUIButton curButton = invTradeDealer.GetChild(j).GetComponent<InventoryUIButton>();


                        if (InventoryController.Instance.PlayerInventory[i][0] == curButton.ItemVarCur.IdItem && InventoryController.Instance.PlayerInventory[i][2] == -3)
                        {
                            if (InventoryController.Instance.PlayerInventory[i][1] > 0)
                            {
                                curButton.Count.text = InventoryController.Instance.PlayerInventory[i][1].ToString();
                            }
                            else
                            {
                                Destroy(curButton.gameObject);
                            }
                        }
                    }
                }
            }

            if (resTradeDealer.transform.childCount > 0 && InventoryController.Instance.PlayerInventory[i][2] == -5)
            {
                for (int j = 0; j < resTradeDealer.transform.childCount; j++)
                {
                    if (resTradeDealer.GetChild(j).GetComponent<InventoryUIButton>() != null)
                    {
                        InventoryUIButton curButton = resTradeDealer.GetChild(j).GetComponent<InventoryUIButton>();
                        if (InventoryController.Instance.PlayerInventory[i][0] == curButton.ItemVarCur.IdItem && InventoryController.Instance.PlayerInventory[i][2] == -5)
                        {
                            if (InventoryController.Instance.PlayerInventory[i][1] > 0)
                            {
                                curButton.Count.text = InventoryController.Instance.PlayerInventory[i][1].ToString();
                            }
                            else
                            {
                                Destroy(curButton.gameObject);
                            }
                        }
                    }
                }
            }

            if (resTradePlayer.transform.childCount > 0 && InventoryController.Instance.PlayerInventory[i][2] == -4)
            {
                for (int j = 0; j < resTradePlayer.transform.childCount; j++)
                {
                    if (resTradePlayer.GetChild(j).GetComponent<InventoryUIButton>() != null)
                    {
                        InventoryUIButton curButton = resTradePlayer.GetChild(j).GetComponent<InventoryUIButton>();

                        if (InventoryController.Instance.PlayerInventory[i][0] == curButton.ItemVarCur.IdItem && InventoryController.Instance.PlayerInventory[i][2] == -4)
                        {
                            if (InventoryController.Instance.PlayerInventory[i][1] > 0)
                            {
                                curButton.Count.text = InventoryController.Instance.PlayerInventory[i][1].ToString();
                            }
                            else
                            {
                                Destroy(curButton.gameObject);
                            }
                        }
                    }
                }
            }

            if (inventoryContainer.transform.childCount > 0 && InventoryController.Instance.PlayerInventory[i][2] == 0)
            {
                for (int j = 0; j < inventoryContainer.transform.childCount; j++)
                {
                    if (inventoryContainer.GetChild(j).GetComponent<InventoryUIButton>() != null)
                    {
                        InventoryUIButton curButton = inventoryContainer.GetChild(j).GetComponent<InventoryUIButton>();
                        InventoryUIButton curButton2 = inventorySwitchContainer.GetChild(j).GetComponent<InventoryUIButton>();
                        InventoryUIButton curButton3 = invTradePlayer.GetChild(j).GetComponent<InventoryUIButton>();

                        if (InventoryController.Instance.PlayerInventory[i][0] == curButton.ItemVarCur.IdItem && InventoryController.Instance.PlayerInventory[i][2] == curButton.PlaceID)
                        {
                            if (InventoryController.Instance.PlayerInventory[i][1] > 0)
                            {
                                curButton.Count.text = InventoryController.Instance.PlayerInventory[i][1].ToString();
                                curButton2.Count.text = InventoryController.Instance.PlayerInventory[i][1].ToString();
                                curButton3.Count.text = InventoryController.Instance.PlayerInventory[i][1].ToString();
                            }
                            else
                            {
                                Destroy(curButton.gameObject);
                                Destroy(curButton2.gameObject);
                                Destroy(curButton3.gameObject);
                            }
                        }
                    }
                }
            }
        }
    }

    public void CleanTradeWindow() //очищение после успешной торговли
    { 
        if (resTradeDealer.transform.childCount > 0 )
        {
            for (int j = 0; j < resTradeDealer.transform.childCount; j++)
            {
                Destroy(resTradeDealer.GetChild(j).gameObject);
            }
        }

        if (resTradePlayer.transform.childCount > 0)
        {
            for (int j = 0; j < resTradePlayer.transform.childCount; j++)
            {
                Destroy(resTradePlayer.GetChild(j).gameObject);
            }
        }
    }

    public void UpdateChoosedItem( InventoryUIButton item )
    {
        if ( OpenedWindow == WindowState.PlayerInventory )
        {
            invLabel.transform.parent.gameObject.GetComponent<CanvasGroup>().alpha = 1;
            invImage.sprite = Resources.Load<Sprite>( "UI/ItemIcons/" + item.ItemVarCur.SpriteRef);
            invLabel.text = item.ItemVarCur.Name;
            invDescription.text = item.ItemVarCur.Description;
            invRamka.transform.SetParent(item.gameObject.transform);
            InvRamka.transform.localScale = Vector3.one;
        }
        else if ( OpenedWindow == WindowState.SwitchInventory )
        {
            switchLabel.transform.parent.gameObject.GetComponent<CanvasGroup>().alpha = 1;
            switchImage.sprite = Resources.Load<Sprite>("UI/ItemIcons/" + item.ItemVarCur.SpriteRef);
            switchLabel.text = item.ItemVarCur.Name;
            switchDescription.text = item.ItemVarCur.Description;
            invRamka.transform.SetParent(item.gameObject.transform);
            InvRamka.transform.localScale = Vector3.one;
        }
        else if (OpenedWindow == WindowState.TradeInventory)
        {
            invRamka.transform.SetParent(item.gameObject.transform);
            InvRamka.transform.localScale = Vector3.one;
        }
        else
        {
            invLabel.transform.parent.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            switchLabel.transform.parent.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            InventoryController.Instance.ChoosedItem = null;
            invRamka.transform.SetParent(gameObject.transform);
            InvRamka.transform.localScale = Vector3.zero;
        }
        InvRamka.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        InvRamka.GetComponent<RectTransform>().offsetMin = Vector2.zero;
    }


    ////////////////////////////////////////////////////////////////////////////////////
    ///МЕТОДЫ ОБРАБОТКИ КРАФТОВЫХ КНОПОК
    ////////////////////////////////////////////////////////////////////////////////////

    public void LoadCraftButtons() 
    {
        if (craftContainer.transform.childCount > 0)
        {
            for (int i = 0; i < craftContainer.transform.childCount; i++)
            {
                if (craftContainer.GetChild(i).GetComponent<CraftUIButton>() != null)
                {
                    Destroy(craftContainer.GetChild(i).gameObject);
                }
            }
        }
        
        CraftUsedCallback callback = new CraftUsedCallback(InventoryController.Instance.CreateCraftItem);
        
        for (int i = 0; i < InventoryController.Instance.CraftVariables.Count; i++)
        {
            string eventName = InventoryController.Instance.CraftVariables[i].EventName;

            if (eventName != "" && GameController.Instance.IsEventDone(eventName) )
            {
                //создать метод обработки выполненого крафта
                continue;
            }

            CraftVar tempCraft = InventoryController.Instance.CraftVariables[i];
            CraftUIButton newCraft = AddCraftItemButton(tempCraft);
            newCraft.Callback = callback;
            LoadNeededItems( newCraft );
        }
    }

    private void LoadNeededItems( CraftUIButton newCraft )
    {
        for ( int i = 0; i < newCraft.CraftVarCur.Materials.Count ; i++ )
        {
            NeededItemUIPanel newNeededItem = AddNeededItemUI(newCraft, newCraft.CraftVarCur.Materials[i]);
        }
    }

    public CraftUIButton AddCraftItemButton(CraftVar newCraftVar)  //создать кнопку инвентаря, добавить в окнока инвентаря игрока, вернуть объект этой кнопки
    {
        CraftUIButton newCraft = Instantiate(craftButtonPrefab) as CraftUIButton;

        newCraft.transform.SetParent(craftContainer);
        newCraft.CraftVarCur = newCraftVar;

        return newCraft;
    }

    public NeededItemUIPanel AddNeededItemUI(CraftUIButton newCraft, List<int> material)  //создать кнопку инвентаря, добавить в окнока инвентаря игрока, вернуть объект этой кнопки
    {
        NeededItemUIPanel newNeededItem = Instantiate(neededItemPrefab) as NeededItemUIPanel;

        newNeededItem.transform.SetParent(newCraft.CraftPanel.transform);
        newNeededItem.Material = material;
        newNeededItem.NewCraftItem = newCraft.CraftVarCur;

        return newNeededItem;
    }

    ////////////////////////////////////////////////////////////////////////////////////
    ///МЕТОДЫ ОБРАБОТКИ НАЖАТИЙ КНОПОК
    ////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Переход в главное меню
    /// </summary>
    public void ButtonMainMenu() // переход в галвное меню
    {
        GameController.Instance.LoadMainMenu();
    }

    ////////////////////////////////////////////////////////////////////////////////////
    ///МЕТОДЫ ОБРАБОТКИ UI ОКОН
    ////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Cкрытие всех окон и центрирование их на экране
    /// </summary>
    private void InitWindowsUI()  //скрытие всех окон и центрование их на экране
    {
        if ( windowsUI != null )
        {
            for ( int i = 0; i < windowsUI.Count; i++ )
            {
                windowsUI[i].GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                windowsUI[i].GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
                HideWindow(windowsUI[i].GetComponent<CanvasGroup>());
            }
        }
    }

    /// <summary>
    /// Получить объект окна UI по названию
    /// </summary>
    /// <param name="nameWindow">string Название объекта окна</param>
    /// <returns></returns>
    public GameObject GetWindow( string nameWindow ) //получить объект окна по его названию из списка
    {
        return windowsUI.Find(x => x.name.Contains(nameWindow));
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
        SetLock(true);
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

    /// <summary>
    /// Показать окно победы
    /// </summary>
    public void ShowLevelWonWindow() //показать окно победы
    {
        GameController.Instance.AudioManager.PlaySound("aud_clk_button_menu_1");
        ShowWindow(GetWindow("LevelWonWindow").GetComponent<CanvasGroup>());
    }

    /// <summary>
    /// Показать окно проигрыша
    /// </summary>
    public void ShowLevelLoseWindow() //показать окно поражения
    {
        GameController.Instance.AudioManager.PlaySound("aud_clk_button_menu_1");
        ShowWindow(GetWindow("LevelLoseWindow").GetComponent<CanvasGroup>());
    }

    /// <summary>
    /// Показать окно лифта
    /// </summary>
    public void ShowElevatorWindow() //показать окно выбора лифта
    {
        ShowWindow(GetWindow("ElevatorWindow").GetComponent<CanvasGroup>());
        Player.Instance.NavMeshAgent.enabled = false;
    }

    /// <summary>
    /// Скрыть окно лифта
    /// </summary>
    public void HideElevatorWindow()    //скрыть окно выбора лифта
    {
        HideWindow(GetWindow("ElevatorWindow").GetComponent<CanvasGroup>());
        Player.Instance.NavMeshAgent.enabled = true;
    }

    /// <summary>
    /// Показать окно крафта
    /// </summary>
    public void ShowCraftWindow()   //показать окно крафта
    {
        LoadCraftButtons();
        ShowWindow(GetWindow("CraftWindow").GetComponent<CanvasGroup>());
    }

    /// <summary>
    /// Скрыть окно крафта
    /// </summary>
    public void HideCraftWindow()   //скрыть окно крафта
    {
        HideWindow(GetWindow("CraftWindow").GetComponent<CanvasGroup>());
    }

    /// <summary>
    /// Показать окно инвентаря игрока
    /// </summary>
    public void ShowSwitchItemsWindow() //показать окно обмена предметами
    {
        OpenedWindow = WindowState.SwitchInventory;
        ShowWindow(GetWindow("SwitchItemsWindow").GetComponent<CanvasGroup>());
    }

    /// <summary>
    /// Скрыть окно обмена предметами
    /// </summary>
    public void HideSwitchItemsWindow() //скрыть окно обмена предметами
    {
        OpenedWindow = WindowState.Closed;
        UpdateChoosedItem(null);
        HideWindow(GetWindow("SwitchItemsWindow").GetComponent<CanvasGroup>());
        InventoryController.Instance.SetOpenedPlace(0);

    }



    /// <summary>
    /// Показать окно торговца
    /// </summary>
    public void ShowDealerWindow() //показать окно обмена предметами
    {
        OpenedWindow = WindowState.TradeInventory;
        ShowWindow(GetWindow("DealerWindow").GetComponent<CanvasGroup>());
    }

    /// <summary>
    /// Скрыть окно торговца
    /// </summary>
    public void HideDealerWindow() //скрыть окно обмена предметами
    {
        OpenedWindow = WindowState.Closed;
        UpdateChoosedItem(null);
        HideWindow(GetWindow("DealerWindow").GetComponent<CanvasGroup>());
        InventoryController.Instance.SetOpenedPlace(0);
        InventoryController.Instance.UpdateTempInventory();
    }



    /// <summary>
    /// Показать окно обмена предметами
    /// </summary>
    public void ShowInventoryItemsWindow() //показать окно обмена предметами
    {
        OpenedWindow = WindowState.PlayerInventory;
        ShowWindow(GetWindow("InventoryWindow").GetComponent<CanvasGroup>());
    }

    /// <summary>
    /// Скрыть окно обмена предметами
    /// </summary>
    public void HideInventoryItemsWindow() //скрыть окно обмена предметами
    {
        OpenedWindow = WindowState.Closed;
        UpdateChoosedItem(null);
        HideWindow(GetWindow("InventoryWindow").GetComponent<CanvasGroup>());  
    }

    /// <summary>
    /// Показать окно информации
    /// </summary>
    public void ShowInfoWindow()    //показать окно вывода информации
    {
        ShowWindow(GetWindow("InfoWindow").GetComponent<CanvasGroup>());
    }

    /// <summary>
    /// Показать окно информации
    /// </summary>
    public void ShowEventWindow()    //показать окно вывода информации
    {
        ShowWindow(GetWindow("EventWindow").GetComponent<CanvasGroup>());
    }

    /// <summary>
    /// Скрыть окно информации
    /// </summary>
    public void HideEventWindow()    //скрыть окно вывода информации
    {
        HideWindow(GetWindow("EventWindow").GetComponent<CanvasGroup>());
    }

    /// <summary>
    /// Скрыть окно информации
    /// </summary>
    public void HideInfoWindow()    //скрыть окно вывода информации
    {
        HideWindow(GetWindow("InfoWindow").GetComponent<CanvasGroup>());
    }

    /// <summary>
    /// Показать окно информации
    /// </summary>
    public void ShowDialogWindow()    //показать окно вывода информации
    {
        ShowWindow(GetWindow("DialogWindow").GetComponent<CanvasGroup>());
    }

    /// <summary>
    /// Скрыть окно информации
    /// </summary>
    public void HideDialogWindow()    //скрыть окно вывода информации
    {
        HideWindow(GetWindow("DialogWindow").GetComponent<CanvasGroup>());
    }

    /// <summary>
    /// Показать окно игрока
    /// </summary>
    public void ShowPlayerWindow()    //показать окно вывода информации
    {
        if ( !CameraControl.Instance.AlignCameraWithPlayer() )
        {
            ShowWindow(GetWindow("PlayerWindow").GetComponent<CanvasGroup>());
        }
    }

    /// <summary>
    /// Скрыть окно информации
    /// </summary>
    public void HidePlayerWindow()    //скрыть окно вывода информации
    {
        HideWindow(GetWindow("PlayerWindow").GetComponent<CanvasGroup>());
    }

    public void SetSoundVolume(Slider slider)
    {
        GameController.Instance.AudioManager.SfxVolume = slider.value;
    }

    public void SetMusicVolume(Slider slider)
    {
        GameController.Instance.AudioManager.MusicVolume = slider.value;
    }

    private void OnDestroy()
    {
        JSONSave.Instance.SaveHud();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            JSONSave.Instance.SaveHud();
        }
    }

    private void OnApplicationQuit()
    {
        JSONSave.Instance.SaveHud();
    }
}
