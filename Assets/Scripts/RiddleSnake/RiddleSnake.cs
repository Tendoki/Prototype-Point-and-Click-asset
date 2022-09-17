using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class RiddleSnake : MonoBehaviour
{
    public List<GameObject> Lamps;
    public GameObject FirstLamp;
    public bool isFirstLampSelected;
    public GameObject[,] TableLamps;
    public int Width;
    public int Height;
    public int CurrentIdLine;
    public int CurrentIdColumn;
    public bool isUpLampPainted;
    public bool isRightLampPainted;
    public bool isDownLampPainted;
    public bool isLeftLampPainted;
    public int CountPainted;
    public int CountBlocked;
    public float TimeRestart;
    public GameObject LampsList;
    public List<GameObject> Levels;
    public int CurrentLevel;
    public GameObject BirdFoodInventory;
    public string ItemGiveName;

    public AudioClip LampOnSound;

    void Start()
    {
        isFirstLampSelected = false;
        foreach (Transform child in LampsList.transform)
        {
            Lamps.Add(child.gameObject);
        }
        TableLamps = new GameObject[Height, Width]; 
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                TableLamps[i, j] = Lamps[Width * i + j];
                TableLamps[i, j].GetComponent<LampController>().idLine = i;
                TableLamps[i, j].GetComponent<LampController>().idColumn = j;
            }
        }

        CurrentLevel = 0;
        for (int i = 0; i < Levels.Count; i++)
        {
            if (Levels[i].GetComponent<RiddleSnakeLevels>().isComplete)
			{
                Color colorLvl = new Color(0F, 1F, 0F, 1F);
                Levels[i].GetComponent<SpriteRenderer>().color = colorLvl;
            }
            else
			{
                Color colorLvl = new Color(1F, 0F, 0F, 1F);
                Levels[i].GetComponent<SpriteRenderer>().color = colorLvl;
            }
            Levels[i].GetComponent<RiddleSnakeLevels>().id = i;
        }
        if (Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().isComplete)
        {
            LevelComplete();
        }
        else
        {
            Color colorLvl = new Color(1F, 1F, 0F, 1F);
            Levels[CurrentLevel].GetComponent<SpriteRenderer>().color = colorLvl;
            RestartLevel();
        }
    }

    private void Update()
    {
        if (isFirstLampSelected && !Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().isComplete)
        {
            if (Input.GetKeyDown("right"))
            {
                if (CurrentIdColumn < Width - 1 && !TableLamps[CurrentIdLine, CurrentIdColumn + 1].GetComponent<LampController>().isPainted)
				{
                    this.GetComponent<AudioSource>().PlayOneShot(LampOnSound);
                }
                for (int i = CurrentIdColumn + 1; i < Width; i++)
                {
                    if (!TableLamps[CurrentIdLine, i].GetComponent<LampController>().isPainted)
                    {
                        CurrentIdColumn = i;
                        Color color = new Color(1F, 1F, 0F, 0.347F);
                        TableLamps[CurrentIdLine, i].GetComponent<SpriteRenderer>().color = color;
                        TableLamps[CurrentIdLine, i].GetComponent<LampController>().isPainted = true;
                        CountPainted++;
                    }
                    else
                    {
                        CheckNearPainted();
                        CheckDeadEnd();
                        break;
                    }
                    if (i == Width - 1)
                    {
                        CheckNearPainted();
                        CheckDeadEnd();
                    }
                }

            }

            if (Input.GetKeyDown("left"))
            {
                if (CurrentIdColumn > 0 && !TableLamps[CurrentIdLine, CurrentIdColumn - 1].GetComponent<LampController>().isPainted)
                {
                    this.GetComponent<AudioSource>().PlayOneShot(LampOnSound);
                }
                for (int i = CurrentIdColumn - 1; i >= 0; i--)
                {
                    if (!TableLamps[CurrentIdLine, i].GetComponent<LampController>().isPainted)
                    {
                        CurrentIdColumn = i;
                        Color color = new Color(1F, 1F, 0F, 0.347F);
                        TableLamps[CurrentIdLine, i].GetComponent<SpriteRenderer>().color = color;
                        TableLamps[CurrentIdLine, i].GetComponent<LampController>().isPainted = true;
                        CountPainted++;
                    }
                    else
                    {
                        CheckNearPainted();
                        CheckDeadEnd();
                        break;
                    }
                    if (i == 0)
                    {
                        CheckNearPainted();
                        CheckDeadEnd();
                    }
                }
            }

            if (Input.GetKeyDown("down"))
            {
                if (CurrentIdLine < Height-1 && !TableLamps[CurrentIdLine + 1, CurrentIdColumn].GetComponent<LampController>().isPainted)
                {
                    this.GetComponent<AudioSource>().PlayOneShot(LampOnSound);
                }
                for (int i = CurrentIdLine + 1; i < Height; i++)
                {
                    if (!TableLamps[i, CurrentIdColumn].GetComponent<LampController>().isPainted)
                    {
                        CurrentIdLine = i;
                        Color color = new Color(1F, 1F, 0F, 0.347F);
                        TableLamps[i, CurrentIdColumn].GetComponent<SpriteRenderer>().color = color;
                        TableLamps[i, CurrentIdColumn].GetComponent<LampController>().isPainted = true;
                        CountPainted++;
                    }
                    else
                    {
                        CheckNearPainted();
                        CheckDeadEnd();
                        break;
                    }
                    if (i == Height - 1)
                    {
                        CheckNearPainted();
                        CheckDeadEnd();
                    }
                }
            }

            if (Input.GetKeyDown("up"))
            {
                if (CurrentIdLine > 0 && !TableLamps[CurrentIdLine - 1, CurrentIdColumn].GetComponent<LampController>().isPainted)
                {
                    this.GetComponent<AudioSource>().PlayOneShot(LampOnSound);
                }
                for (int i = CurrentIdLine - 1; i >= 0; i--)
                {
                    if (!TableLamps[i, CurrentIdColumn].GetComponent<LampController>().isPainted)
                    {
                        CurrentIdLine = i;
                        Color color = new Color(1F, 1F, 0F, 0.347F);
                        TableLamps[i, CurrentIdColumn].GetComponent<SpriteRenderer>().color = color;
                        TableLamps[i, CurrentIdColumn].GetComponent<LampController>().isPainted = true;
                        CountPainted++;
                    }
                    else
                    {
                        CheckNearPainted();
                        CheckDeadEnd();
                        break;
                    }
                    if (i == 0)
                    {
                        CheckNearPainted();
                        CheckDeadEnd();
                    }
                }
            }


        }
    }

    public void CheckDeadEnd()
    {
        if (isUpLampPainted && isDownLampPainted && isLeftLampPainted && isRightLampPainted)
        {
            isUpLampPainted = false;
            isDownLampPainted = false;
            isLeftLampPainted = false;
            isRightLampPainted = false;
            if (CountPainted != Width * Height)
            {
                RestartLevel();
            }
            else if (!Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().isComplete)
            {
                LevelComplete();
            }
        }
    }

    public void CheckNearPainted()
    {
        if (CurrentIdLine == 0 || TableLamps[CurrentIdLine - 1, CurrentIdColumn].GetComponent<LampController>().isPainted)
            isUpLampPainted = true;
        else
            isUpLampPainted = false;
        if (CurrentIdLine == Height - 1 || TableLamps[CurrentIdLine + 1, CurrentIdColumn].GetComponent<LampController>().isPainted)
            isDownLampPainted = true;
        else
            isDownLampPainted = false;
        if (CurrentIdColumn == 0 || TableLamps[CurrentIdLine, CurrentIdColumn - 1].GetComponent<LampController>().isPainted)
            isLeftLampPainted = true;
        else
            isLeftLampPainted = false;
        if (CurrentIdColumn == Width - 1 || TableLamps[CurrentIdLine, CurrentIdColumn + 1].GetComponent<LampController>().isPainted)
            isRightLampPainted = true;
        else
            isRightLampPainted = false;
    }

    public void RestartLevel()
    {
        StartCoroutine(WaitRestartLevel());
    }

    public void LevelComplete()
    {
        StartCoroutine(WaitLevelComplete());
    }


    private IEnumerator WaitLevelComplete()
    {
        if (!Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().isComplete)
        {
            LocationDataGarden.RiddleCountLevelsComplete++;
        }
        Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().isComplete = true;
        LocationDataGarden.LevelsComplete[CurrentLevel] = true;
        Color colorLvl = new Color(0F, 1F, 0F, 1F);
        Levels[CurrentLevel].GetComponent<SpriteRenderer>().color = colorLvl;
        if (LocationDataGarden.RiddleCountLevelsComplete == 6)
        {
            LocationDataGarden.RiddleCountLevelsComplete++;
            InventoryController.instance.InventoryMenuWait();
            DataItems.ItemsInventory.Add(BirdFoodInventory);
            Instantiate(BirdFoodInventory, InventoryController.instance.HolderInventoryTransform);

            if (DataQuests.QuestsValues[BirdFoodInventory.GetComponent<ItemInventoryController>().QuestName] == 1)
            {
                DataQuests.QuestsValues[BirdFoodInventory.GetComponent<ItemInventoryController>().QuestName] = 2;
            }
        }
        yield return new WaitForSeconds(TimeRestart);
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                Color color = new Color(0F, 1F, 0F, 0.347F);
                TableLamps[i, j].GetComponent<SpriteRenderer>().color = color;
                TableLamps[i, j].GetComponent<LampController>().isPainted = false;
                TableLamps[i, j].GetComponent<LampController>().isBlocked = false;
            }
        }
        for (int i = 0; i < Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().LineBlockedLamps.Count; i++)
        {
            TableLamps[Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().LineBlockedLamps[i], Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().ColumnBlockedLamps[i]].GetComponent<LampController>().isBlocked = true;
            TableLamps[Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().LineBlockedLamps[i], Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().ColumnBlockedLamps[i]].GetComponent<LampController>().isPainted = true;
            Color color = new Color(1F, 0F, 0F, 0.347F);
            TableLamps[Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().LineBlockedLamps[i], Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().ColumnBlockedLamps[i]].GetComponent<SpriteRenderer>().color = color;
        }
    }

    private IEnumerator WaitRestartLevel()
    {
        yield return new WaitForSeconds(TimeRestart);
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                Color color = new Color(0F, 0F, 0F, 0F);
                TableLamps[i, j].GetComponent<SpriteRenderer>().color = color;
                TableLamps[i, j].GetComponent<LampController>().isPainted = false;
                TableLamps[i, j].GetComponent<LampController>().isBlocked = false;
            }
        }


        for (int i = 0; i < Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().LineBlockedLamps.Count; i++)
        {
            TableLamps[Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().LineBlockedLamps[i], Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().ColumnBlockedLamps[i]].GetComponent<LampController>().isBlocked = true;
            TableLamps[Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().LineBlockedLamps[i], Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().ColumnBlockedLamps[i]].GetComponent<LampController>().isPainted = true;
            Color color = new Color(1F, 0F, 0F, 0.347F);
            TableLamps[Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().LineBlockedLamps[i], Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().ColumnBlockedLamps[i]].GetComponent<SpriteRenderer>().color = color;
        }
        CountBlocked = Levels[CurrentLevel].GetComponent<RiddleSnakeLevels>().LineBlockedLamps.Count;
        CountPainted = CountBlocked;
        isFirstLampSelected = false;
    }
}
