using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    public bool inSaveLoadMenu;
    public bool isSaveMenu;
    public Text TextSaveOrLoad;
    public Text TextDateSaveUI;

    public GameObject pauseMenuUI;
    public GameObject SaveLoadMenuUI;
    public GameObject SaveLoadMenuHolder;
    public List<GameObject> Buttons; 

    public GameObject backgroudMusic;

	private void Awake()
	{
        GameIsPaused = false;
        foreach (Transform child in SaveLoadMenuHolder.transform)
        {
            Buttons.Add(child.gameObject);
            child.GetComponent<SaveLoadButton>().IsChangeScreenshot = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			if (GameIsPaused && inSaveLoadMenu)
			{
                SaveLoadMenuUI.SetActive(false);
                inSaveLoadMenu = false;
                pauseMenuUI.SetActive(true);
			}
            else if (GameIsPaused && !inSaveLoadMenu)
			{
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }

    public void SaveLoadMenuVisible()
	{
		for (int i = 0; i < Buttons.Count; i++)
        {
			if (Buttons[i].GetComponent<SaveLoadButton>().IsChangeScreenshot)
			{
                if (File.Exists($"{Application.persistentDataPath}/{Buttons[i].GetComponent<SaveLoadButton>().SaveName}.png"))
                {
                    byte[] UploadByte = File.ReadAllBytes($"{Application.persistentDataPath}/{Buttons[i].GetComponent<SaveLoadButton>().SaveName}.png");
                    Texture2D texture = new Texture2D(1920, 1080);
                    texture.LoadImage(UploadByte);
                    Buttons[i].GetComponent<Image>().sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                    Save save = BinarySavingSystem.LoadGame(Buttons[i].GetComponent<SaveLoadButton>().SaveName);
                    Buttons[i].GetComponent<SaveLoadButton>().TextDateSave = save.DateSave;
                }
                else
                {
                    Buttons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>($"Screenshots/Save_default");
                    Buttons[i].GetComponent<SaveLoadButton>().TextDateSave = "";
                }
                Buttons[i].GetComponent<SaveLoadButton>().IsChangeScreenshot = false;
            }
        }
        SaveLoadMenuUI.SetActive(true);
        inSaveLoadMenu = true;
        pauseMenuUI.SetActive(false);
        //this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Screenshots/{SaveName}");
    }

    public void SetInSave()
	{
        TextSaveOrLoad.text = "Save:";
        isSaveMenu = true;
	}

    public void SetInLoad()
	{
        TextSaveOrLoad.text = "Load:";
        isSaveMenu = false;
	}

    public void Continue()
    {
        pauseMenuUI.SetActive(false);
        PlayerController.LockMovement = false;
        Cursor.SetCursor(GameManager.instance.cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameManager.instance.Player.GetComponent<AudioSource>().Play();
        //backgroudMusic.GetComponent<AudioSource>().Play();
    }

    void Pause()
    {
        InventoryController.instance.HolderInventory.SetActive(false);
        PlayerController.LockMovement = true;
        GameManager.instance.Player.GetComponent<AudioSource>().Pause();
        Cursor.SetCursor(GameManager.instance.cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        //backgroudMusic.GetComponent<AudioSource>().Pause();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
