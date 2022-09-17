using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]

public static class DataItems
{
    public static List<GameObject> ItemsInventory = new List<GameObject>();
    public static Dictionary<string, bool> TookItems = new Dictionary<string, bool>();
}
