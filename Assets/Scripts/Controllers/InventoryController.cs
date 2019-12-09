using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryController : MonoBehaviour
{
    static private InventoryController _instance;
    public static InventoryController Instance
    {
        get
        {
            return _instance;
        }
    }

    public List<ItemVar> InventoryVariables = new List<ItemVar>();
    [HideInInspector]
    public List<int> InventoryCost = new List<int>()   //бд с условными ценами на инвентарь у торговца
    {
  /*0*/ 5,
  /*1*/ 7,
  /*2*/ 9,
  /*3*/ 6,
  /*4*/ 4,
  /*5*/ 6,
  /*6*/ 2,
  /*7*/ 1,
  /*8*/ 1,
  /*9*/ 3,
 /*10*/ 2,
 /*11*/ 6,
 /*12*/ 15,
 /*13*/ 1,
 /*14*/ 1,
 /*15*/ 2,
 /*16*/ 4
    };

    [HideInInspector]
    public List<CraftVar> CraftVariables = new List<CraftVar>()   //бд с возможными крафтовыми предметами ( название, id или событие создаваемого, материалы, название спрайта, описание, время на создание )
    {
  /*0*/  new CraftVar( BbtStrings.GetStr("str_inv_axe"),        "craftaxe",     new List<List<int>>(){ new List<int>(){ 7, 7 } },                             "axe",      BbtStrings.GetStr("str_inv_axe_desc"),        50.0f ),
  /*1*/  new CraftVar( BbtStrings.GetStr("str_inv_bandage"),    1,              new List<List<int>>(){ new List<int>(){ 7, 7 }, new List<int>(){ 8, 11 } },   "bandage",  BbtStrings.GetStr("str_inv_bandage_desc"),    30.0f  ),
  /*3*/  new CraftVar( BbtStrings.GetStr("str_inv_sofa"),       "craftsofa",    new List<List<int>>(){ new List<int>(){ 8, 20 }, new List<int>(){ 7, 1 } },   "sofa",     BbtStrings.GetStr("str_inv_sofa_desc"),       100.0f  ),
  /*4*/  new CraftVar( BbtStrings.GetStr("str_inv_trap"),       "craftkey",     new List<List<int>>(){ new List<int>(){ 7, 10 } },                            "trap",     BbtStrings.GetStr("str_inv_trap_desc"),       70.0f  )
    };

    List<int> Places = new List<int>() //таблица с id мест, где лежат предметы
    {
        -3//торговец
        -2//игрок в торгах
        -1, //игрок при подборе
        0,//игрок
        1,
        2,
        3
    };

    [HideInInspector]
    public List<List<int>> PlayerInventory = new List<List<int>>()  //бд с текущими инвентарными предметами ( id предмета, количество, id места )
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

    [SerializeField] private InventoryUIButton choosedItem;
    public InventoryUIButton ChoosedItem { get => choosedItem; set => choosedItem = value; }

    public int OpenedPlace;

    private void Awake()
    {
        _instance = this;
        ChoosedItem = null;
        OpenedPlace = 0;
    }

    private void Start()
    {
        JSONSave.Instance.LoadDataInventory();
   
        CraftVariables[0].Name = BbtStrings.GetStr("str_inv_axe");
        CraftVariables[0].Description = BbtStrings.GetStr("str_inv_axe_desc");
        CraftVariables[1].Name = BbtStrings.GetStr("str_inv_bandage");
        CraftVariables[1].Description = BbtStrings.GetStr("str_inv_bandage_desc");
        CraftVariables[2].Name = BbtStrings.GetStr("str_inv_sofa");
        CraftVariables[2].Description = BbtStrings.GetStr("str_inv_sofa_desc");
        CraftVariables[3].Name = BbtStrings.GetStr("str_inv_trap");
        CraftVariables[3].Description = BbtStrings.GetStr("str_inv_trap_desc");

        //бд с возможными инвентарными предметами ( название, стэк, спрайт, видимость, описание, id предмета )
        /*0*/
        InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_cure"), 3, "cure", true, BbtStrings.GetStr("str_inv_cure_desc"), 0));                 
      /*1*/  InventoryVariables.Add(new ItemVar( BbtStrings.GetStr("str_inv_bandage"),        3, "bandage",          true, BbtStrings.GetStr("str_inv_bandage_desc"),        1 ));
      /*2*/  InventoryVariables.Add(new ItemVar( BbtStrings.GetStr("str_inv_antibiotic"),     1, "antibiotic",       true, BbtStrings.GetStr("str_inv_soupe_desc"),          2 ));
      /*3*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_soupe"), 2, "soupe", true, BbtStrings.GetStr("str_inv_soupe_desc"), 3));
      /*4*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_energybar"), 5, "energybar", true, BbtStrings.GetStr("str_inv_energybar_desc"), 4));
      /*5*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_noodles"), 2, "noodles", true, BbtStrings.GetStr("str_inv_noodles_desc"), 5));
      /*6*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_rat"), 3, "rat", true, BbtStrings.GetStr("str_inv_rat_desc"), 6));
      /*7*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_mushrooms"), 8, "mushrooms", true, BbtStrings.GetStr("str_inv_mushrooms_desc"), 7));
      /*8*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_water"), 8, "water", true, BbtStrings.GetStr("str_inv_water_desc"), 8));
      /*9*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_coffee"), 2, "coffee", true, BbtStrings.GetStr("str_inv_coffee_desc"), 9));
     /*10*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_juice"), 5, "juice", true, BbtStrings.GetStr("str_inv_juice_desc"), 10));
     /*11*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_cola"), 5, "cola", true, BbtStrings.GetStr("str_inv_cola_desc"), 11));
     /*12*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_axe"), 1, "axe", true, BbtStrings.GetStr("str_inv_axe_desc"), 12));
     /*13*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_stuff"), 10, "stuff", true, BbtStrings.GetStr("str_inv_stuff_desc"), 13));
     /*14*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_money"), 9999, "money", true, BbtStrings.GetStr("str_inv_money_desc"), 14));
     /*15*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_humus"), 8, "humus", true, BbtStrings.GetStr("str_inv_humus_desc"), 15));
     /*16*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_friedrat"), 3, "rat", true, BbtStrings.GetStr("str_inv_friedrat_desc"), 16));
            //добавил? добавь и в таблицу InventoryCost
    }


    public void SetOpenedPlace( int curPlace )
    {
        OpenedPlace = curPlace;
    }


    public int GetCountItem( int id )
    {
        int count = 0;

        for (int i = 0; i < PlayerInventory.Count; i++)
        {
            if (PlayerInventory[i][2] < 1 && PlayerInventory[i][0] == id)
            {
                count = PlayerInventory[i][1];
                break;
            }
        }

        return count;
    }

    public void ChangeDealerInventory()
    {
        RemoveDealerOldInventory();
        AddingDealerInventory();
    }

    private void RemoveDealerOldInventory()
    {
        List<int> arrayForRemoving = new List<int>();

        for(int i = 0; i < PlayerInventory.Count; i++)
        {
            if(PlayerInventory[i][2] == -3)
            {
                arrayForRemoving.Add(i);
            }
        }

        for (int i = (arrayForRemoving.Count - 1); i >= 0; i--)
        {
            PlayerInventory.RemoveAt(arrayForRemoving[i]);
        }
    }

    private void AddingDealerInventory()
    {
        List<List<int>> newInventory = GenerateDealerInventory();

        for ( int i = 0; i < newInventory.Count; i++)
        {
            PlayerInventory.Add(newInventory[i]);
        }
    }

    private List<List<int>> GenerateDealerInventory()
    {
        List<List<int>> newInventory = new List<List<int>>();
        int minInvCount = 3;
        int maxInvCount = 10;
        int curInvCount = UnityEngine.Random.Range(minInvCount, maxInvCount+1);

        for(int i = 0; i < curInvCount; i++)
        {
            int idNewInv = UnityEngine.Random.Range(0, InventoryVariables.Count);
            while(HasCurrentInv(newInventory, idNewInv))
            {
                idNewInv = UnityEngine.Random.Range(0, InventoryVariables.Count);
            }

            int countItem =  UnityEngine.Random.Range(1, 21);
            newInventory.Add(new List<int> { idNewInv, countItem, -3 });

        }


        return newInventory;
    }

    private bool HasCurrentInv(List<List<int>> newInventory, int id)
    {
        for (int i = 0; i < newInventory.Count; i++)
        {
            if(newInventory[i][0] == id)
            {
                return true;
            }
        }

        return false;
    }


    //перенос предметов из окошек торговли
    public void UpdateTempInventory( ) //произошла ли торговля?
    {
        for (int i = 0; i < PlayerInventory.Count; i++)
        {
            if (PlayerInventory[i][2] == -4)
            {
                int curId = PlayerInventory[i][0];
                int curCount = PlayerInventory[i][1];
                PlayerInventory.RemoveAt(i);
                AddInventoryItemPLayer(curId, curCount, 0);
                i--;
            }
        }

        for (int i = 0; i < PlayerInventory.Count; i++)
        {
            if (PlayerInventory[i][2] == -5)
            {
                int curId = PlayerInventory[i][0];
                int curCount = PlayerInventory[i][1];
                PlayerInventory.RemoveAt(i);
                AddInventoryItemPLayer(curId, curCount, -3);
                i--;
            }
        }
    }

    //переменная для определения, проводить торговлю или нет

    bool doThisTrade = false;
    //осуществить торговлю
    public void DoneTrade() 
    {
        if (doThisTrade)
        {
            for (int i = 0; i < PlayerInventory.Count; i++)
            {
                if (PlayerInventory[i][2] == -5)
                {
                    int curId = PlayerInventory[i][0];
                    int curCount = PlayerInventory[i][1];
                    PlayerInventory.RemoveAt(i);
                    AddInventoryItemPLayer(curId, curCount, 0);
                    i--;
                }
            }

            for (int i = 0; i < PlayerInventory.Count; i++)
            {
                if (PlayerInventory[i][2] == -4)
                {
                    int curId = PlayerInventory[i][0];
                    int curCount = PlayerInventory[i][1];
                    PlayerInventory.RemoveAt(i);
                    AddInventoryItemPLayer(curId, curCount, -3);
                    i--;
                }
            }

            HUD.Instance.LastUpdateInventory();
            HUD.Instance.CleanTradeWindow();
            CheckTrade();
        }
        
    }

    public void CheckTrade()
    {
        doThisTrade = false;
        int costPlayer = 0;
        int costDealer = 0;

        for (int i = 0; i < PlayerInventory.Count; i++)
        {
            if (PlayerInventory[i][2] == -4 )
            {
                costPlayer += PlayerInventory[i][1] * InventoryCost[PlayerInventory[i][0]];
            }
            else if (PlayerInventory[i][2] == -5)
            {
                costDealer += PlayerInventory[i][1] * InventoryCost[PlayerInventory[i][0]];
            }
        }

        if (costPlayer == 0 && costDealer == 0)
        {
            TextController.Instance.SetTradeLabelText("str_start_trade");
        }
        else
        {
            int result = costPlayer - costDealer;

            if ( result < 3 )
            {
                TextController.Instance.SetTradeLabelText("str_not_good_trade");
                doThisTrade = false;
            }
            else if ( result > 2 && result < 6 )
            {
                TextController.Instance.SetTradeLabelText("str_good_trade_1");
                doThisTrade = true;
            }
            else if (result > 5 && result < 10)
            {
                TextController.Instance.SetTradeLabelText("str_good_trade_2");
                doThisTrade = true;
            }
            else if (result > 9 )
            {
                TextController.Instance.SetTradeLabelText("str_good_trade_3");
                doThisTrade = true;
            }

        }
    }

    /// <summary>
    /// Добавление предмета в инвентарь
    /// </summary>
    /// <param name="itemData">Добавляемый предмет</param>
    public void AddInventoryItemPLayer( int itemID, int count, int placeID )
    {
        int idExistedItem = CheckInventoryItemExist(itemID, placeID);
        if (idExistedItem > -1)
        {
            PlayerInventory[idExistedItem][1] += count;
        }
        else
        {
            InventoryUIButton newUiButton = HUD.Instance.AddNewInventoryItem(InventoryVariables[itemID], count, placeID);
            InventoryUsedCallback callback = new InventoryUsedCallback(InventoryItemShow);
            newUiButton.Callback = callback;

            if ( placeID == 0 )
            {
                InventoryUIButton newUiButton1 = HUD.Instance.AddNewInventoryItem(InventoryVariables[itemID], count, -1);
                InventoryUsedCallback callback1 = new InventoryUsedCallback(InventoryItemShow);
                newUiButton1.Callback = callback;

                InventoryUIButton newUiButton2 = HUD.Instance.AddNewInventoryItem(InventoryVariables[itemID], count, -2);
                InventoryUsedCallback callback2 = new InventoryUsedCallback(InventoryItemShow);
                newUiButton2.Callback = callback;
            }
            

            PlayerInventory.Add(new List<int>() { itemID, count, placeID });
        }

        HUD.Instance.LastUpdateInventory();
    }

    /// <summary>
    /// Существует ли в инвентаре добавляемый предмет
    /// </summary>
    /// <param name="CrType">Тип предмета</param>
    /// <returns></returns>
    public int CheckInventoryItemExist(int itemID, int placeID)
    {
        for (int i = 0; i < PlayerInventory.Count; i++)
        {
            if (itemID == PlayerInventory[i][0] && placeID == PlayerInventory[i][2])
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Делегат нажатия на предмет в инвентаре (сделать вывод картинки, описания, названия предмета, добавление рамки, установка как выбранного
    /// </summary>
    /// <param name="item">Нажимаемая кнопка предмета в инвентаре</param>
    public void InventoryItemShow(InventoryUIButton item)
    {
        ChoosedItem = item;
        HUD.Instance.UpdateChoosedItem(item);
    }

    //перемещение предмета между окнами
    public void MoveShotItem()
    {
        if (ChoosedItem != null)
        {
            int idItem = ChoosedItem.ItemVarCur.IdItem;
            int idCurPlayInv = 0;

            if (ChoosedItem.PlaceID > 0) //перемещаем в инвентарь игрока
            {
                for (int i = 0; i < PlayerInventory.Count; i++) //ищем строку из которой удалять предмет
                {
                    if (OpenedPlace == PlayerInventory[i][2] && PlayerInventory[i][0] == idItem)
                    {
                        idCurPlayInv = i;
                        break;
                    }
                }

                PlayerInventory[idCurPlayInv][1]--;

                if (PlayerInventory[idCurPlayInv][1] <= 0)
                {
                    HUD.Instance.InvRamka.transform.parent = gameObject.transform;
                    HUD.Instance.InvRamka.transform.localScale = Vector3.zero;
                    HUD.Instance.InvRamka.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                    HUD.Instance.InvRamka.GetComponent<RectTransform>().offsetMin = Vector2.zero;

                    HUD.Instance.LastUpdateInventory();

                    PlayerInventory.RemoveAt(idCurPlayInv);
                    Destroy(ChoosedItem.gameObject);
                    ChoosedItem = null;
                }
                else
                {
                    HUD.Instance.LastUpdateInventory();
                }

                //добавление
                AddInventoryItemPLayer(idItem, 1, 0);

                HUD.Instance.LastUpdateInventory();

            }
            else        //перемещаем из инвентаря игрока
            {
                
                for (int i = 0; i < PlayerInventory.Count; i++) //ищем строку из которой удалять предмет
                {
                    if (PlayerInventory[i][2] < 1 && PlayerInventory[i][2] > -3 && PlayerInventory[i][0] == idItem)
                    {
                        idCurPlayInv = i;
                        break;
                    }
                }

                
                PlayerInventory[idCurPlayInv][1]--;
                if (PlayerInventory[idCurPlayInv][1] <= 0)
                {
                    HUD.Instance.InvRamka.transform.SetParent(gameObject.transform);
                    HUD.Instance.InvRamka.transform.localScale = Vector3.zero;
                    HUD.Instance.InvRamka.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                    HUD.Instance.InvRamka.GetComponent<RectTransform>().offsetMin = Vector2.zero;
                    HUD.Instance.LastUpdateInventory();
                    PlayerInventory.RemoveAt(idCurPlayInv);
                    ChoosedItem = null;
                }
                //добавление
                AddInventoryItemPLayer(idItem, 1, OpenedPlace);
                HUD.Instance.LastUpdateInventory();


            }
        }
    }

    public void RemoveFromInventory( int idItem, int count, int place )
    {
        int idCurPlayInv = 0;

        if ( place == -1 || place == -2 )
        {
            place = 0;
        }

        for (int i = 0; i < PlayerInventory.Count; i++) //ищем строку из которой удалять предмет
        {
            if (PlayerInventory[i][2] == place && PlayerInventory[i][0] == idItem)
            {
                idCurPlayInv = i;
                break;
            }
        }

        PlayerInventory[idCurPlayInv][1] = PlayerInventory[idCurPlayInv][1] - count;

        HUD.Instance.LastUpdateInventory();

        if (PlayerInventory[idCurPlayInv][1] <= 0)
        {
            PlayerInventory.RemoveAt(idCurPlayInv);
        }
    }

    public void TakeAllItem()
    {
        HUD.Instance.InvRamka.transform.SetParent(gameObject.transform);
        HUD.Instance.InvRamka.transform.localScale = Vector3.zero;
        HUD.Instance.InvRamka.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        HUD.Instance.InvRamka.GetComponent<RectTransform>().offsetMin = Vector2.zero;

        for (int i = 0; i < PlayerInventory.Count; i++) //ищем строку из которой удалять предмет
        {
            if (OpenedPlace == PlayerInventory[i][2])
            {
                int curCountItems = PlayerInventory[i][1];
                PlayerInventory[i][1] = 0;
                AddInventoryItemPLayer(PlayerInventory[i][0], curCountItems, 0);

                if (PlayerInventory[i][1] <= 0)
                {
                    HUD.Instance.LastUpdateInventory();

                    PlayerInventory.RemoveAt(i);
                    i--;
                    ChoosedItem = null;
                }
                else
                {
                    HUD.Instance.LastUpdateInventory();
                }


            }
        }

        HUD.Instance.LastUpdateInventory();

    }




    ////////////////////////////////////////////////////////////////////////////////////
    ///КРАФТ ПРЕДМЕТОВ
    ////////////////////////////////////////////////////////////////////////////////////
    ///

    public void CreateCraftItem(CraftUIButton craft)
    {
        HUD.Instance.HideCraftWindow();
        HUD.Instance.SetLock(false);
        GameController.Instance.StartTimerCraft(CraftVariables[craft.CraftVarCur.IdCraftedItem].TimeForCraft, EndCreateCraftItem, craft);
    }

    private void EndCreateCraftItem(CraftUIButton craft)
    {
        for (int i = 0; i < craft.CraftVarCur.Materials.Count; i++)
        {
            List<int> curMaterials = craft.CraftVarCur.Materials[i];
            RemoveFromInventory(curMaterials[0], curMaterials[1], 0);
        }

        if (craft.CraftVarCur.EventName != "")
        {
            GameController.Instance.SetEventDone(craft.CraftVarCur.EventName);
        }
        else
        {
            AddInventoryItemPLayer(craft.CraftVarCur.IdCraftedItem, 1, 0);
            HUD.Instance.LastUpdateInventory();
        }

        HUD.Instance.LoadCraftButtons();
    }

    public bool CheckNeededItemsIsDone( CraftUIButton craft )
    {
        bool isDone = true;

        for ( int i = 0; i < craft.CraftVarCur.Materials.Count; i++ )
        {
            List<int> curMaterials = craft.CraftVarCur.Materials[i];

            if ( GetCountItem(curMaterials[0]) < curMaterials[1] )
            {
                isDone = false;
                break;
            }
        }

        return isDone;
    }


    //Dealer//////////////////////////////////////////////////////////////////////////////////////

    //перемещение предмета между окнами
    public void MoveDealerItem()
    {
        if (ChoosedItem != null)
        {
            int idItem = ChoosedItem.ItemVarCur.IdItem;
            int idCurPlayInv = 0;

            if (ChoosedItem.PlaceID == -3) //перемещаем из торговца
            {
                for (int i = 0; i < PlayerInventory.Count; i++) //ищем строку из которой удалять предмет
                {
                    if (OpenedPlace == PlayerInventory[i][2] && PlayerInventory[i][0] == idItem)
                    {
                        idCurPlayInv = i;
                        break;
                    }
                }

                PlayerInventory[idCurPlayInv][1]--;

                if (PlayerInventory[idCurPlayInv][1] <= 0)
                {
                    HUD.Instance.InvRamka.transform.SetParent(gameObject.transform);
                    HUD.Instance.InvRamka.transform.localScale = Vector3.zero;
                    HUD.Instance.InvRamka.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                    HUD.Instance.InvRamka.GetComponent<RectTransform>().offsetMin = Vector2.zero;

                    HUD.Instance.LastUpdateInventory();

                    PlayerInventory.RemoveAt(idCurPlayInv);
                    Destroy(ChoosedItem.gameObject);
                    ChoosedItem = null;
                }
                else
                {
                    HUD.Instance.LastUpdateInventory();
                }

                //добавление
                AddInventoryItemPLayer(idItem, 1, -5);

                HUD.Instance.LastUpdateInventory();

            }
            else if (ChoosedItem.PlaceID == -2)       //перемещаем из инвентаря игрока
            {

                for (int i = 0; i < PlayerInventory.Count; i++) //ищем строку из которой удалять предмет
                {
                    if (PlayerInventory[i][2] < 1 && PlayerInventory[i][2] > -3 && PlayerInventory[i][0] == idItem)
                    {
                        idCurPlayInv = i;
                        break;
                    }
                }


                PlayerInventory[idCurPlayInv][1]--;
                if (PlayerInventory[idCurPlayInv][1] <= 0)
                {
                    HUD.Instance.InvRamka.transform.SetParent(gameObject.transform);
                    HUD.Instance.InvRamka.transform.localScale = Vector3.zero;
                    HUD.Instance.InvRamka.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                    HUD.Instance.InvRamka.GetComponent<RectTransform>().offsetMin = Vector2.zero;
                    HUD.Instance.LastUpdateInventory();
                    PlayerInventory.RemoveAt(idCurPlayInv);
                    ChoosedItem = null;
                }
                //добавление
                AddInventoryItemPLayer(idItem, 1, -4);
                HUD.Instance.LastUpdateInventory();


            }
            else if (ChoosedItem.PlaceID == -4)       //что предлагает игрок
            {

                for (int i = 0; i < PlayerInventory.Count; i++) 
                {
                    if (PlayerInventory[i][2] == -4 && PlayerInventory[i][0] == idItem)
                    {
                        idCurPlayInv = i;
                        break;
                    }
                }


                PlayerInventory[idCurPlayInv][1]--;
                if (PlayerInventory[idCurPlayInv][1] <= 0)
                {
                    HUD.Instance.InvRamka.transform.SetParent(gameObject.transform);
                    HUD.Instance.InvRamka.transform.localScale = Vector3.zero;
                    HUD.Instance.InvRamka.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                    HUD.Instance.InvRamka.GetComponent<RectTransform>().offsetMin = Vector2.zero;
                    HUD.Instance.LastUpdateInventory();
                    PlayerInventory.RemoveAt(idCurPlayInv);
                    ChoosedItem = null;
                }
                //добавление
                AddInventoryItemPLayer(idItem, 1, 0);
                HUD.Instance.LastUpdateInventory();
            }
            else if (ChoosedItem.PlaceID == -5)       //что предлагает игрок
            {

                for (int i = 0; i < PlayerInventory.Count; i++)
                {
                    if (PlayerInventory[i][2] == -5 && PlayerInventory[i][0] == idItem)
                    {
                        idCurPlayInv = i;
                        break;
                    }
                }


                PlayerInventory[idCurPlayInv][1]--;
                if (PlayerInventory[idCurPlayInv][1] <= 0)
                {
                    HUD.Instance.InvRamka.transform.SetParent(gameObject.transform);
                    HUD.Instance.InvRamka.transform.localScale = Vector3.zero;
                    HUD.Instance.InvRamka.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                    HUD.Instance.InvRamka.GetComponent<RectTransform>().offsetMin = Vector2.zero;
                    HUD.Instance.LastUpdateInventory();
                    PlayerInventory.RemoveAt(idCurPlayInv);
                    ChoosedItem = null;
                }
                //добавление
                AddInventoryItemPLayer(idItem, 1, -3);
                HUD.Instance.LastUpdateInventory();
            }
        }

        CheckTrade();
    }

    private void OnDestroy()
    {
        JSONSave.Instance.SaveInventory();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            JSONSave.Instance.SaveInventory();
        }
    }

    private void OnApplicationQuit()
    {
        JSONSave.Instance.SaveInventory();
    }
}
