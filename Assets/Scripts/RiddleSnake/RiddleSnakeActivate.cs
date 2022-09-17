using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleSnakeActivate : MonoBehaviour
{
    public GameObject Riddle;
    public bool isPlayerNear;
    public GameObject NearNode;
    public InstantiateDialogue instantiateDialogue;
    public string NameNeedItem;

    private void Start()
    {
        if (!LocationDataGarden.IsOpenedRiddleSnake)
        {
            LocationDataGarden.LevelsComplete.Clear();
            for (int i = 0; i < Riddle.GetComponent<RiddleSnake>().Levels.Count; i++)
            {
                LocationDataGarden.LevelsComplete.Add(false);
                Riddle.GetComponent<RiddleSnake>().Levels[i].GetComponent<RiddleSnakeLevels>().isComplete = LocationDataGarden.LevelsComplete[i];
            }
        }
        else
        {
            Debug.Log("Levels.Count" + Riddle.GetComponent<RiddleSnake>().Levels.Count);
            for (int i = 0; i < Riddle.GetComponent<RiddleSnake>().Levels.Count; i++)
            {
                Riddle.GetComponent<RiddleSnake>().Levels[i].GetComponent<RiddleSnakeLevels>().isComplete = LocationDataGarden.LevelsComplete[i];
            }
        }
        isPlayerNear = false;
    }

    private void Update()
    {
        if (GameManager.instance.Player.GetComponent<PlayerController>().CurrentNode == NearNode && GameManager.instance.Player.GetComponent<PlayerController>().isStay)
        {
            isPlayerNear = true;
        }
        else
        {
            isPlayerNear = false;
        }

        if (PauseMenu.GameIsPaused)
        {
            this.gameObject.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<Collider2D>().enabled = true;
        }
        //if (Input.GetKeyDown("escape") && GameManager.instance.isRiddleActive)
        //{
        //    Riddle.SetActive(false);
        //    GameManager.instance.isRiddleActive = false;
        //    PlayerController.LockMovement = false;
        //    if (NPC != null)
        //    {
        //        NPC.GetComponent<BoxCollider2D>().enabled = true;
        //    }
        //}
    }

    private void OnMouseEnter()
    {
        if (!GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
        {
            Vector2 offset = new Vector2(10, 0);
            Cursor.SetCursor(GameManager.instance.cursorClick, offset, CursorMode.ForceSoftware);
        }
    }

    private void OnMouseExit()
    {
        if (!GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
        {
            Cursor.SetCursor(GameManager.instance.cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    private void OnMouseUp()
    {
        if (!PlayerController.LockMovement && !GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
        {
            if (!isPlayerNear)
            {
                GameManager.instance.Places.GetComponent<NodesController>().FinalNode = NearNode;
                GameManager.instance.Places.GetComponent<NodesController>().DAlg();

            }
            else
            {
                if (!LocationDataGarden.IsOpenedRiddleSnake)
                {
                    foreach (GameObject item in DataItems.ItemsInventory)
                    {
                        if (item.GetComponent<ItemInventoryController>().NameItemInventory == NameNeedItem)
                        {
                            LocationDataGarden.IsOpenedRiddleSnake = true;
                            DataItems.ItemsInventory.Remove(item);
                            break;
                        }
                    }

                    foreach (Transform child in InventoryController.instance.HolderInventoryTransform)
                    {
                        if (child.GetComponent<ItemInventoryController>().NameItemInventory == NameNeedItem)
                        {
                            Destroy(child.gameObject);
                            break;
                        }
                    }
                }
                if (LocationDataGarden.IsOpenedRiddleSnake)
                {
                    Riddle.SetActive(true);
                    for (int i = 0; i < LocationDataGarden.LevelsComplete.Count; i++)
                    {
                        Riddle.GetComponent<RiddleSnake>().Levels[i].GetComponent<RiddleSnakeLevels>().isComplete = LocationDataGarden.LevelsComplete[i];
                    }
                    Cursor.SetCursor(GameManager.instance.cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
                    GameManager.instance.isRiddleActive = true;
                    PlayerController.LockMovement = true;
                    if (GameManager.instance.NPC != null)
                    {
                        GameManager.instance.NPC.GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
                else
                {
                    instantiateDialogue.UpdateAnswers();
                    instantiateDialogue.ShowDialogue = true;
                    PlayerController.LockMovement = true;
                }
            }
        }
    }

}