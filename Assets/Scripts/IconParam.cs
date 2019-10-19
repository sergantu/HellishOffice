using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconParam : MonoBehaviour
{
    [SerializeField] int placeID;

    public int PlaceID { get => placeID; set => placeID = value; }
}
