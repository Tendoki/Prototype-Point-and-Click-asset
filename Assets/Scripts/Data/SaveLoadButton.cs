using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public string SaveName;
	public GameObject PauseMenu;
	public Sprite img;
	public string TextDateSave;
	public bool IsChangeScreenshot;

	private void Awake()
	{
		if (File.Exists($"{Application.persistentDataPath}/{SaveName}.png"))
		{
			//LoadScreenshot();
			//this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Screenshots/{SaveName}");
			//Sprite spriteScreenshot = LoadScreenshot();
			//this.gameObject.GetComponent<Image>().sprite = spriteScreenshot;
		}
		else
		{
			this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Screenshots/Save_default");
			TextDateSave = "";
		}
	}

	public void LoadScreenshot()
	{
		if (File.Exists($"{Application.persistentDataPath}/{SaveName}.png"))
		{
			byte[] UploadByte = File.ReadAllBytes($"{Application.persistentDataPath}/{SaveName}.png");
			Texture2D texture = new Texture2D(1920, 1080);
			texture.LoadImage(UploadByte);
			this.gameObject.GetComponent<Image>().sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
			Save save = BinarySavingSystem.LoadGame(SaveName);
			TextDateSave = save.DateSave;
		}
		else
		{
			this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Screenshots/Save_default");
			TextDateSave = "";
		}

	}

	public void SaveOrLoadGame()
	{
		if (PauseMenu.GetComponent<PauseMenu>().isSaveMenu)
		{
			SaveGame();
		}
		else
		{
			LoadGame();
		}
	}

	public void SaveGame()
	{
		BinarySavingSystem.SaveGame(SaveName);

		PauseMenu.GetComponent<PauseMenu>().SaveLoadMenuUI.SetActive(false);
		PauseMenu.GetComponent<PauseMenu>().inSaveLoadMenu = false;
		PauseMenu.GetComponent<PauseMenu>().Continue();
		
		ScreenShotGameSave.TakeScreenshotGameScreen(SaveName);
		IsChangeScreenshot = true;
		//LoadScreenshot();
		//this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Screenshots/{SaveName}");
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
		PauseMenu.GetComponent<PauseMenu>().SaveLoadMenuUI.SetActive(false);
		PauseMenu.GetComponent<PauseMenu>().inSaveLoadMenu = false;
		PauseMenu.GetComponent<PauseMenu>().Continue();
		SceneManager.LoadScene(save.CurrentLocation);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		PauseMenu.GetComponent<PauseMenu>().TextDateSaveUI.text = TextDateSave;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		PauseMenu.GetComponent<PauseMenu>().TextDateSaveUI.text = "";
	}
}
