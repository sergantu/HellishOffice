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

    public List<CraftVar> CraftVariables = new List<CraftVar>()   //бд с возможными крафтовыми предметами ( название, id или событие создаваемого, материалы, название спрайта, описание, время на создание )
    {
  /*0*/  new CraftVar( BbtStrings.GetStr("str_inv_bandage"),            1,                  new List<List<int>>(){ new List<int>(){ 13, 8 }, new List<int>(){ 15, 4 } },                                                        "bandage",          BbtStrings.GetStr("str_inv_bandage_desc"),        10.0f ),
  /*1*/  new CraftVar( BbtStrings.GetStr("str_inv_friedmushrooms"),      3,                 new List<List<int>>(){ new List<int>(){ 7, 1 } },                                                                                   "friedmushrooms",   BbtStrings.GetStr("str_inv_friedmushrooms_desc"),    5.0f  ),
  /*2*/  new CraftVar( BbtStrings.GetStr("str_inv_friedrat"),           16,                 new List<List<int>>(){ new List<int>(){ 6, 1 } },                                                                                  "friedrat",         BbtStrings.GetStr("str_inv_friedrat_desc"),       5.0f  ),
  /*3*/  new CraftVar( BbtStrings.GetStr("str_inv_axe"),                "craftaxe",         new List<List<int>>(){ new List<int>(){ 33, 3 }, new List<int>(){ 34, 5 }, new List<int>(){ 13, 10 } },                              "axe",              BbtStrings.GetStr("str_inv_axe_desc"),       10.0f  ),
  /*4*/  new CraftVar( BbtStrings.GetStr("str_inv_computer"),           "craftcomputer",    new List<List<int>>(){ new List<int>(){ 34, 12 }, new List<int>(){ 13, 15 }, new List<int>(){ 19, 10 } },                              "computer",         BbtStrings.GetStr("str_inv_computer_desc"),       20.0f  ),
  /*5*/  new CraftVar( BbtStrings.GetStr("str_inv_condey"),             "craftcondey",      new List<List<int>>(){ new List<int>(){ 33, 5 }, new List<int>(){ 19, 10 }, new List<int>(){ 34, 3 }, new List<int>(){ 13, 8 } },    "condey",           BbtStrings.GetStr("str_inv_condey_desc"),       15.0f  ),
  /*6*/  new CraftVar( BbtStrings.GetStr("str_inv_crafttable"),         "craftcrafttable",  new List<List<int>>(){ new List<int>(){ 33, 12 }, new List<int>(){ 34, 8 }, new List<int>(){ 13, 11 } },                              "crafttable2",      BbtStrings.GetStr("str_inv_crafttable_desc"),       15.0f  ),
  /*7*/  new CraftVar( BbtStrings.GetStr("str_inv_fir"),                "craftfir",         new List<List<int>>(){ new List<int>(){ 19, 7 }, new List<int>(){ 33, 4 }, new List<int>(){ 1, 2 }, new List<int>(){ 15, 3 } },    "fir",              BbtStrings.GetStr("str_inv_fir_desc"),       15.0f  ),
  /*8*/  new CraftVar( BbtStrings.GetStr("str_inv_key1"),               "craftkey1",        new List<List<int>>(){ new List<int>(){ 19, 3 }, new List<int>(){ 13, 6 } },                                                        "key1",             BbtStrings.GetStr("str_inv_key1_desc"),       10.0f  ),
  /*9*/  new CraftVar( BbtStrings.GetStr("str_inv_key2"),               "craftkey2",        new List<List<int>>(){ new List<int>(){ 13, 8 }, new List<int>(){ 34, 2 } },                                                        "key2",             BbtStrings.GetStr("str_inv_key2_desc"),       5.0f  ),
  /*10*/  new CraftVar( BbtStrings.GetStr("str_inv_key4"),              "craftkey4",        new List<List<int>>(){ new List<int>(){ 13, 10 }, new List<int>(){ 33, 4 } },                                                        "key4",             BbtStrings.GetStr("str_inv_key4_desc"),       15.0f  ),
  /*11*/  new CraftVar( BbtStrings.GetStr("str_inv_key5"),              "craftkey5",        new List<List<int>>(){ new List<int>(){ 13, 12 }, new List<int>(){ 6, 1 }, new List<int>(){ 24, 2 } },                              "key5",             BbtStrings.GetStr("str_inv_key5_desc"),       20.0f  ),
  /*12*/  new CraftVar( BbtStrings.GetStr("str_inv_kreslo"),            "craftkreslo",      new List<List<int>>(){ new List<int>(){ 33, 4 }, new List<int>(){ 34, 4 }, new List<int>(){ 13, 5 } },                              "kreslo",           BbtStrings.GetStr("str_inv_kreslo_desc"),       20.0f  ),
  /*13*/  new CraftVar( BbtStrings.GetStr("str_inv_monitor"),           "craftmonitor",     new List<List<int>>(){ new List<int>(){ 19, 5 }, new List<int>(){ 34, 11 } },                                                        "monitor",          BbtStrings.GetStr("str_inv_monitor_desc"),       15.0f  ),
  /*14*/  new CraftVar( BbtStrings.GetStr("str_inv_mushtrap"),          "craftmushtrap",    new List<List<int>>(){ new List<int>(){ 24, 2 }, new List<int>(){ 15, 2 }, new List<int>(){ 8, 4 } },                              "mushtrap",         BbtStrings.GetStr("str_inv_mushtrap_desc"),       10.0f  ),
  /*15*/  new CraftVar( BbtStrings.GetStr("str_inv_sofa"),              "craftsofa",        new List<List<int>>(){ new List<int>(){ 33, 8 }, new List<int>(){ 34, 12 }, new List<int>(){ 13, 4 } },                              "sofa",             BbtStrings.GetStr("str_inv_sofa_desc"),       20.0f  ),
  /*16*/  new CraftVar( BbtStrings.GetStr("str_inv_rattrap"),           "craftrattrap",     new List<List<int>>(){ new List<int>(){ 24, 2 }, new List<int>(){ 34, 7 }, new List<int>(){ 1, 1 } },                              "rattrap",          BbtStrings.GetStr("str_inv_rattrap_desc"),       10.0f  ),
  /*17*/  new CraftVar( BbtStrings.GetStr("str_inv_watertrap"),         "craftwatertrap",   new List<List<int>>(){ new List<int>(){ 2, 1 }, new List<int>(){ 33, 4 }, new List<int>(){ 34, 8 } },                              "watertrap",        BbtStrings.GetStr("str_inv_watertrap_desc"),       15.0f  ),
  /*18*/  new CraftVar( BbtStrings.GetStr("str_inv_cure"),              0,                  new List<List<int>>(){ new List<int>(){ 15, 3 }, new List<int>(){ 7, 3 }, new List<int>(){ 11, 2 } },                              "cure",             BbtStrings.GetStr("str_inv_cure_desc"),       15.0f  ),
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

        new List<int>(){ 0, 0, 0 },

        new List<int>(){ 2, 1, 1 },
        new List<int>(){ 8, 2, 1 },
        new List<int>(){ 16, 3, 1 },
        new List<int>(){ 14, 60, 1 },

        new List<int>(){ 1, 1, 2 },
        new List<int>(){ 4, 1, 2 },
        new List<int>(){ 15, 10, 2 },

        new List<int>(){ 0, 1, 3 },
        new List<int>(){ 0, 2, 3 },
        new List<int>(){ 0, 100, 3 },

        new List<int>(){ 0, 1, 4 },
        new List<int>(){ 16, 1, 4 },
        new List<int>(){ 33, 7, 4 },

        new List<int>(){ 11, 1, 5 },
        new List<int>(){ 10, 1, 5 },
        new List<int>(){ 19, 7, 5 },
        new List<int>(){ 34, 5, 5 },

        new List<int>(){ 1, 1, 6 },
        new List<int>(){ 3, 2, 6 },
        new List<int>(){ 13, 19, 6 },

        new List<int>(){ 2, 1, 7 },
        new List<int>(){ 8, 2, 7 },
        new List<int>(){ 14, 50, 7 },

        new List<int>(){ 11, 1, 8 },
        new List<int>(){ 5, 1, 8 },
        new List<int>(){ 19, 4, 8 },
        new List<int>(){ 33, 4, 8 },

        new List<int>(){ 1, 1, 9 },
        new List<int>(){ 24, 2, 9 },
        new List<int>(){ 15, 7, 9 },

        new List<int>(){ 9, 1, 10 },
        new List<int>(){ 7, 2, 10 },
        new List<int>(){ 19, 6, 10 },
        new List<int>(){ 13, 13, 10 },

        new List<int>(){ 5, 1, 11 },
        new List<int>(){ 4, 2, 11 },
        new List<int>(){ 8, 2, 11 },
        new List<int>(){ 14, 40, 11 },

        new List<int>(){ 13, 10, 12 },
        new List<int>(){ 34, 9, 12 },
        new List<int>(){ 9, 1, 12 },

        new List<int>(){ 0, 1, 13 },
        new List<int>(){ 1, 1, 13 },
        new List<int>(){ 11, 1, 13 },
        new List<int>(){ 6, 1, 13 },

        new List<int>(){ 11, 1, 14 },
        new List<int>(){ 19, 4, 14 },
        new List<int>(){ 15, 10, 14 },
        new List<int>(){ 33, 5, 14 },

        new List<int>(){ 1, 1, 15 },
        new List<int>(){ 10, 1, 15 },
        new List<int>(){ 15, 13, 15 },
        new List<int>(){ 33, 3, 15 },

        new List<int>(){ 13, 7, 16 },
        new List<int>(){ 33, 7, 16 },
        new List<int>(){ 34, 7, 16 },
        new List<int>(){ 15, 10, 16 },

        new List<int>(){ 0, 1, 17 },
        new List<int>(){ 11, 1, 17 },
        new List<int>(){ 24, 2, 17 },

        new List<int>(){ 6, 1, 18 },
        new List<int>(){ 5, 1, 18 },
        new List<int>(){ 19, 6, 18 },
        new List<int>(){ 13, 17, 18 },

        new List<int>(){ 1, 1, 19 },
        new List<int>(){ 8, 2, 19 },
        new List<int>(){ 19, 8, 19 },
        new List<int>(){ 34, 5, 19 },

        new List<int>(){ 11, 1, 20 },
        new List<int>(){ 6, 1, 20 },
        new List<int>(){ 14, 50, 20 },
        new List<int>(){ 33, 5, 20 },

        new List<int>(){ 2, 1, 21 },
        new List<int>(){ 10, 1, 21 },
        new List<int>(){ 14, 50, 21 },

        new List<int>(){ 11, 1, 22 },
        new List<int>(){ 4, 3, 22 },
        new List<int>(){ 13, 7, 22 },

        new List<int>(){ 9, 1, 23 },
        new List<int>(){ 7, 3, 23 },
        new List<int>(){ 15, 17, 23 },
        new List<int>(){ 33, 5, 23 },

        new List<int>(){ 0, 1, 24 },
        new List<int>(){ 24, 2, 24 },
        new List<int>(){ 34, 6, 24 },

        new List<int>(){ 11, 1, 25 },
        new List<int>(){ 10, 1, 25 },
        new List<int>(){ 14, 50, 25 },

        new List<int>(){ 1, 1, 26 },
        new List<int>(){ 16, 2, 26 },
        new List<int>(){ 13, 13, 26 },

        new List<int>(){ 2, 1, 27 },
        new List<int>(){ 8, 2, 27 },
        new List<int>(){ 13, 50, 27 },
        new List<int>(){ 33, 6, 27 },

        new List<int>(){ 9, 1, 28 },
        new List<int>(){ 7, 2, 28 },
        new List<int>(){ 19, 2, 28 },
        new List<int>(){ 34, 5, 28 },

        new List<int>(){ 1, 1, 29 },
        new List<int>(){ 7, 3, 29 },
        new List<int>(){ 15, 13, 29 },
        new List<int>(){ 14, 50, 29 },

        new List<int>(){ 13, 4, 30 },
        new List<int>(){ 33, 3, 30 },
        new List<int>(){ 34, 5, 30 },
        new List<int>(){ 24, 2, 30 },
    };

    public Sprite[] inventoryIcons;

    [SerializeField] private InventoryUIButton choosedItem;
    public InventoryUIButton ChoosedItem { get => choosedItem; set => choosedItem = value; }

    public int OpenedPlace;

    [HideInInspector]
    public List<int> InventoryCost = new List<int>()   //бд с условными ценами на инвентарь у торговца
    {
  /*0*/ 12,
  /*1*/ 16,
  /*2*/ 20,
  /*3*/ 8,
  /*4*/ 8,
  /*5*/ 16,
  /*6*/ 8,
  /*7*/ 4,
  /*8*/ 8,
  /*9*/ 4,
 /*10*/ 8,
 /*11*/ 8,
 /*12*/ 9999,
 /*13*/ 2,
 /*14*/ 1,
 /*15*/ 8,
 /*16*/ 12,
 /*17*/ 9999,
 /*18*/ 9999,
 /*19*/ 8,
 /*20*/ 9999,
 /*21*/ 9999,
 /*22*/ 9999,
 /*23*/ 9999,
 /*24*/ 4,
 /*25*/ 9999,
 /*26*/ 9999,
 /*27*/ 9999,
 /*28*/ 9999,
 /*29*/ 9999,
 /*30*/ 9999,
 /*31*/ 9999,
 /*32*/ 9999,
 /*33*/ 8,
 /*34*/ 8,
    };

    private void Awake()
    {
        inventoryIcons = Resources.LoadAll<Sprite>("inventory");
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
      /*0*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_cure"),              3, "cure",              true, BbtStrings.GetStr("str_inv_cure_desc"),           0));                 
      /*1*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_bandage"),           3, "bandage",           true, BbtStrings.GetStr("str_inv_bandage_desc"),        1));
      /*2*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_antibiotic"),        1, "antibiotic",        true, BbtStrings.GetStr("str_inv_soupe_desc"),          2));
      /*3*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_friedmushrooms"),    2, "friedmushrooms",    true, BbtStrings.GetStr("str_inv_friedmushrooms_desc"), 3));
      /*4*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_energybar"),         5, "energybar",         true, BbtStrings.GetStr("str_inv_energybar_desc"),      4));
      /*5*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_noodles"),           2, "noodles",           true, BbtStrings.GetStr("str_inv_noodles_desc"),        5));
      /*6*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_rat"),               3, "rat",               true, BbtStrings.GetStr("str_inv_rat_desc"),            6));
      /*7*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_mushrooms"),         8, "mushrooms",         true, BbtStrings.GetStr("str_inv_mushrooms_desc"),      7));
      /*8*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_water"),             8, "water",             true, BbtStrings.GetStr("str_inv_water_desc"),          8));
      /*9*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_coffee"),            2, "coffee",            true, BbtStrings.GetStr("str_inv_coffee_desc"),         9));
     /*10*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_juice"),             5, "juice",             true, BbtStrings.GetStr("str_inv_juice_desc"),          10));
     /*11*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_cola"),              5, "cola",              true, BbtStrings.GetStr("str_inv_cola_desc"),           11));
     /*12*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_axe"),               1, "axe",               false, BbtStrings.GetStr("str_inv_axe_desc"),            12));
     /*13*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_stuff"),             10, "stuff",            true, BbtStrings.GetStr("str_inv_stuff_desc"),          13));
     /*14*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_money"),             9999, "money",          true, BbtStrings.GetStr("str_inv_money_desc"),          14));
     /*15*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_humus"),             8, "humus",             true, BbtStrings.GetStr("str_inv_humus_desc"),          15));
     /*16*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_friedrat"),          3, "friedrat",          true, BbtStrings.GetStr("str_inv_friedrat_desc"),       16));
     /*17*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_computer"),          1, "computer",          false, BbtStrings.GetStr("str_inv_computer_desc"),       17));
     /*18*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_crafttable"),        1, "crafttable2",        false, BbtStrings.GetStr("str_inv_crafttable_desc"),     18));
     /*19*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_wires"),             1, "wires",             true, BbtStrings.GetStr("str_inv_wires_desc"),          19));
     /*20*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_fir"),              1, "fir",              false, BbtStrings.GetStr("str_inv_fir_desc"),           20));
     /*21*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_key1"),              1, "key1",              true, BbtStrings.GetStr("str_inv_key1_desc"),           21));
     /*22*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_key2"),              1, "key2",              true, BbtStrings.GetStr("str_inv_key2_desc"),           22));
     /*23*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_condey"),         1, "condey",         false, BbtStrings.GetStr("str_inv_condey_desc"),      23));
     /*24*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_corob"),             1, "corob",             true, BbtStrings.GetStr("str_inv_corob_desc"),          24));
     /*25*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_key4"),              1, "key4",              true, BbtStrings.GetStr("str_inv_key4_desc"),           25));
     /*26*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_key5"),              1, "key5",              true, BbtStrings.GetStr("str_inv_key5_desc"),           26));
     /*27*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_kreslo"),            1, "kreslo",            false, BbtStrings.GetStr("str_inv_kreslo_desc"),         27));
     /*28*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_sofa"),             1, "sofa",             false, BbtStrings.GetStr("str_inv_sofa_desc"),          28));
     /*29*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_monitor"),           1, "monitor",           false, BbtStrings.GetStr("str_inv_monitor_desc"),        29));
     /*30*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_mushtrap"),      1, "mushtrap",      false, BbtStrings.GetStr("str_inv_mushtrap_desc"),   30));
     /*31*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_rattrap"),           1, "rattrap",           false, BbtStrings.GetStr("str_inv_rattrap_desc"),        31));
     /*32*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_watertrap"),         1, "watertrap",         false, BbtStrings.GetStr("str_inv_watertrap_desc"),      32));
     /*33*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_wood"),              1, "wood",              true, BbtStrings.GetStr("str_inv_wood_desc"),           33));
     /*34*/  InventoryVariables.Add(new ItemVar(BbtStrings.GetStr("str_inv_zapchasti"),             1, "zapchasti",             true, BbtStrings.GetStr("str_inv_zapchasti_desc"),          34));
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
            if (PlayerInventory[i][2] < 1 && PlayerInventory[i][0] == id && PlayerInventory[i][2] > -3)
            {
                count = PlayerInventory[i][1];
                break;
            }
        }

        return count;
    }

    public bool HasItemsInPlace()
    {
        for (int i = 0; i < PlayerInventory.Count; i++)
        {
            if(PlayerInventory[i][2] == OpenedPlace)
            {
                return true;
            }
        }

        return false;
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
                    HUD.Instance.InvRamka.transform.SetParent(gameObject.transform.GetChild(1).transform);
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
                    HUD.Instance.InvRamka.transform.SetParent(gameObject.transform.GetChild(1).transform);
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
