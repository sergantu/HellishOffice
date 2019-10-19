using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class ItemVar
{
    [SerializeField] string name;
    [SerializeField] string description;
    [SerializeField] int stackSize;
    [SerializeField] string spriteRef;
    [SerializeField] bool visible;
    [SerializeField] int idItem;

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public int StackSize { get => stackSize; set => stackSize = value; }
    public string SpriteRef { get => spriteRef; set => spriteRef = value; }
    public bool Visible { get => visible; set => visible = value; }
    public int IdItem { get => idItem; set => idItem = value; }

    public ItemVar( string newname, int newstackSize, string newspriteRef, bool newvisible, string newdescription, int newiditem )
    {
        Name = newname;
        Description = newdescription;
        StackSize = newstackSize;
        SpriteRef = newspriteRef;
        Visible = newvisible;
        IdItem = newiditem;
    }
}
