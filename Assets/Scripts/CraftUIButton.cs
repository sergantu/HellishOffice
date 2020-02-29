using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CraftUIButton : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text label;
    [SerializeField] Text description;
    [SerializeField] Button craftButton;
    [SerializeField] GameObject craftPanel; 

    [SerializeField] CraftVar craftVarCur;

    private CraftUsedCallback callback;
    public CraftUsedCallback Callback { get => callback; set => callback = value; }
    public Image Image { get => image; set => image = value; }
    public Text Label { get => label; set => label = value; }
    public Text Description { get => description; set => description = value; }
    public Button CraftButton { get => craftButton; set => craftButton = value; }
    public GameObject CraftPanel { get => craftPanel; set => craftPanel = value; }
    public CraftVar CraftVarCur { get => craftVarCur; set => craftVarCur = value; }

    private void Start()
    {
        Image.sprite = InventoryController.Instance.inventoryIcons.Single(s => s.name == craftVarCur.SpriteRef);
        Description.text = craftVarCur.Description;
        Label.text = craftVarCur.Name;
        craftButton.onClick.AddListener(() => callback(this));

        craftButton.interactable = InventoryController.Instance.CheckNeededItemsIsDone(this);
    }
}
