using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class LocationDataInFrontOfSlotMachines
{
    public static bool IsOpenedBarleyBreak = false;
    public static bool IsWinBarleyBreak = false;
    public static Dictionary<int, int> CellsLocation = new Dictionary<int, int>();
}
