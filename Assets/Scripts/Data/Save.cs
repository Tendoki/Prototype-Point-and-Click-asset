using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]

public class Save
{
	public Dictionary<string, int> QuestsValues = new Dictionary<string, int>();

	public bool IsCameFromTheRight;
    public int CurrentNode;
    public string CurrentLocation;

    //public List<GameObject> ItemsInventory = new List<GameObject>();
    public List<string> ItemsNames = new List<string>();
    public Dictionary<string, bool> TookItems = new Dictionary<string, bool>();

	public bool IsOpenedRiddleSnake = false;
	public int RiddleCountLevelsComplete = 0;
	public List<bool> LevelsComplete = new List<bool>();

	public bool IsOpenedBarleyBreak = false;
	public bool IsWinBarleyBreak = false;
	public Dictionary<int, int> CellsLocation = new Dictionary<int, int>();
    public string DateSave;
    public Save()
    {
        QuestsValues = DataQuests.QuestsValues;

        IsCameFromTheRight = DataPlayer.IsCameFromTheRight;
        CurrentNode = GameManager.instance.Player.GetComponent<PlayerController>().CurrentNode.GetComponent<NodeController>().id;
        CurrentLocation = SceneManager.GetActiveScene().name;

        //ItemsInventory = DataItems.ItemsInventory;
		for (int i = 0; i < DataItems.ItemsInventory.Count; i++)
		{
            ItemsNames.Add(DataItems.ItemsInventory[i].GetComponent<ItemInventoryController>().NameItemInventory);
		}
        TookItems = DataItems.TookItems;

        IsOpenedRiddleSnake = LocationDataGarden.IsOpenedRiddleSnake;
        RiddleCountLevelsComplete = LocationDataGarden.RiddleCountLevelsComplete;
        LevelsComplete = LocationDataGarden.LevelsComplete;

        IsOpenedBarleyBreak = LocationDataInFrontOfSlotMachines.IsOpenedBarleyBreak;
        IsWinBarleyBreak = LocationDataInFrontOfSlotMachines.IsWinBarleyBreak;
        CellsLocation = LocationDataInFrontOfSlotMachines.CellsLocation;

        string DateMonth;
        int TempDateMonth = DateTime.Now.Month;
        if (TempDateMonth < 10)
            DateMonth = "0" + TempDateMonth.ToString();
        else
            DateMonth = TempDateMonth.ToString();

        string DateDay;
        int TempDateDay = DateTime.Now.Day;
        if (TempDateDay < 10)
            DateDay = "0" + TempDateDay.ToString();
        else
            DateDay = TempDateDay.ToString();

        string DateHour;
        int TempDateHour = DateTime.Now.Hour;
        if (TempDateHour < 10)
            DateHour = "0" + TempDateHour.ToString();
        else
            DateHour = TempDateHour.ToString();

        string DateMinute;
        int TempDateMinute = DateTime.Now.Minute;
        if (TempDateMinute < 10)
            DateMinute = "0" + TempDateMinute.ToString();
        else
            DateMinute = TempDateMinute.ToString();

        string DateSecond;
        int TempDateSecond = DateTime.Now.Second;
        if (TempDateSecond < 10)
            DateSecond = "0" + TempDateSecond.ToString();
        else
            DateSecond = TempDateSecond.ToString();

        DateSave = $"{DateTime.Now.Year}-{DateMonth}-{DateDay}_{DateHour}-{DateMinute}-{DateSecond}";
    }
}


