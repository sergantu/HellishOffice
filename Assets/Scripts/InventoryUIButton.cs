using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIButton : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text label;
    [SerializeField] string description;
    [SerializeField] Text count;

    [SerializeField] ItemVar itemVarCur;
    [SerializeField] int newCount;
    [SerializeField] int placeID;

    private InventoryUsedCallback callback;
    public InventoryUsedCallback Callback { get => callback; set => callback = value; }
    public Image Image1 { get => image; set => image = value; }
    public Text Label { get => label; set => label = value; }
    public string Description { get => description; set => description = value; }
    public ItemVar ItemVarCur { get => itemVarCur; set => itemVarCur = value; }
    public Text Count { get => count; set => count = value; }
    public int PlaceID { get => placeID; set => placeID = value; }
    public int NewCount { get => newCount; set => newCount = value; }

    private void Start()
    {
        Image1.sprite = InventoryController.Instance.inventoryIcons.Single(s => s.name == ItemVarCur.SpriteRef);
        Description = ItemVarCur.Description;
        Label.text = ItemVarCur.Name;
        Count.text = NewCount.ToString();
        gameObject.GetComponent<Button>().onClick.AddListener(() => callback(this));
    }

}
