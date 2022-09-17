using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class MainMenu : MonoBehaviour
{
    public int CountSaves;
    public int LastSave;
	public string LastSaveName;

	private void Awake()
	{
		LastSaveName = FindLastSave();
		UIManager.LoadCursorImage();
		Cursor.SetCursor(UIManager.cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
	}
	public void PlayButton()
    {
		LoadGame();
	}

	public void LoadGame()
	{
		if (LastSaveName != "")
		{
			Save save = BinarySavingSystem.LoadGame(LastSaveName);

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
		else
		{
			SceneManager.LoadScene("Garden");
		}
		
	}

	public string FindLastSave()
	{
        string LastDate = $"1970-{DateTime.Now.Month}-{DateTime.Now.Day}_{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}";
        LastSave = -1;
        for (int i = 0; i < CountSaves; i++)
        {
            if (File.Exists($"{Application.persistentDataPath}/Save_{i}.save"))
            {
                Save save = BinarySavingSystem.LoadGame($"Save_{i}");
				for (int j = 0; j < LastDate.Length; j++)
				{
					if (save.DateSave[j] != '-' && save.DateSave[j] != '_' && LastDate[j] != '-' && LastDate[j] != '_')
					{
						if (int.Parse(save.DateSave[j].ToString()) > int.Parse(LastDate[j].ToString()))
						{
							LastDate = save.DateSave;
                            LastSave = i;
							break;
						}
					}
				}
			}
        }
		if (LastSave != -1)
		{
			return $"Save_{LastSave}";
        }
		else
		{
			return "";
		}
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
