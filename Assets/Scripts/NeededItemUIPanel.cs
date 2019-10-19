using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeededItemUIPanel : MonoBehaviour
{
    [SerializeField] Image iconNeededItem;
    [SerializeField] Text textCountItems;

    [SerializeField] List<int> material;
    [SerializeField] CraftVar newCraftItem;

    public Image IconNeededItem { get => iconNeededItem; set => iconNeededItem = value; }
    public Text TextCountItems { get => textCountItems; set => textCountItems = value; }
    public List<int> Material { get => material; set => material = value; }
    public CraftVar NewCraftItem { get => newCraftItem; set => newCraftItem = value; }

    private void Start()
    {
        IconNeededItem.sprite = Resources.Load<Sprite>("UI/ItemIcons/" + InventoryController.Instance.InventoryVariables[ material[0]].SpriteRef);

        if ( InventoryController.Instance.GetCountItem( Material[0] ) < Material[1] )
        {
            TextCountItems.color = Color.red;
        }
        else
        {
            TextCountItems.color = new Color32(255, 255, 255, 255);
        }

        TextCountItems.text = Material[1].ToString();
    }

}
