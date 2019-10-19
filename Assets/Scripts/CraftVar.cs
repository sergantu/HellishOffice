using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class CraftVar
{
    [SerializeField] string name;
    [SerializeField] string description;
    [SerializeField] List<List<int>> materials;
    [SerializeField] string spriteRef;
    [SerializeField] string eventName;
    [SerializeField] float timeForCraft;
    [SerializeField] int idCraftedItem;

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public string SpriteRef { get => spriteRef; set => spriteRef = value; }
    public List<List<int>> Materials { get => materials; set => materials = value; }
    public string EventName { get => eventName; set => eventName = value; }
    public float TimeForCraft { get => timeForCraft; set => timeForCraft = value; }
    public int IdCraftedItem { get => idCraftedItem; set => idCraftedItem = value; }

    public CraftVar(string newname, string neweventname, List<List<int>> newmaterials, string newspriteRef, string newdescription, float newtimeforcraft)
    {
        Name = newname;
        Description = newdescription;
        Materials = newmaterials;
        SpriteRef = newspriteRef;
        TimeForCraft = newtimeforcraft;
        EventName = neweventname;
    }

    public CraftVar(string newname, int newidcraftedite, List<List<int>> newmaterials, string newspriteRef, string newdescription, float newtimeforcraft)
    {
        Name = newname;
        Description = newdescription;
        Materials = newmaterials;
        SpriteRef = newspriteRef;
        TimeForCraft = newtimeforcraft;
        IdCraftedItem = newidcraftedite;
    }
}
