using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class LoadButton : MonoBehaviour
{
    public string SaveName;

	private void Awake()
	{
		if (File.Exists($"Screenshots/{SaveName}"))
		{
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Screenshots/{SaveName}");
        }
		else
		{
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Screenshots/Save_default");
        }
	}

	public void LoadGame()
	{
		Save save = BinarySavingSystem.LoadGame(SaveName);

        DataQuests.QuestsValues = save.QuestsValues;

        DataPlayer.IsCameFromTheRight = save.IsCameFromTheRight;
        DataPlayer.CurrentNode = save.CurrentNode;
        DataPlayer.IsLoadSave = true;

        //ItemsInventory = DataItems.ItemsInventory;
        DataItems.ItemsInventory.Clear();
        for (int i = 0; i < save.ItemsNames.Count; i++)
		{
            DataItems.ItemsInventory.Add(Resources.Load<GameObject>($"Items/{save.ItemsNames[i] + " Inventory Item"}"));
		}
        DataItems.TookItems = save.TookItems;

        LocationDataGarden.IsOpenedRiddleSnake = save.IsOpenedRiddleSnake;
        LocationDataGarden.RiddleCountLevelsComplete = save.RiddleCountLevelsComplete;
        LocationDataGarden.LevelsComplete = save.LevelsComplete;

        LocationDataInFrontOfSlotMachines.IsOpenedBarleyBreak = save.IsOpenedBarleyBreak;
        LocationDataInFrontOfSlotMachines.IsWinBarleyBreak = save.IsWinBarleyBreak;
        LocationDataInFrontOfSlotMachines.CellsLocation = save.CellsLocation;

        Time.timeScale = 1f;
        PlayerController.LockMovement = false;
        SceneManager.LoadScene(save.CurrentLocation);
        
    }
}
