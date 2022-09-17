using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public static class LocationDataGarden
{
    public static bool IsOpenedRiddleSnake = false;
    public static int RiddleCountLevelsComplete = 0;
    public static List<bool> LevelsComplete = new List<bool>();
}
