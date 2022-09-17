using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarleyBreakActivate : MonoBehaviour
{
    public GameObject Riddle;
    public bool isPlayerNear;
    public GameObject NearNode;
    public bool HaveItem;
    public InstantiateDialogue instantiateDialogue;
    public GameObject NPC;
    public string NameNeedItem;

    private void Start()
    {
        //HaveItem = LocationDataGarden.IsOpenedRiddleSnake;
        //if (!LocationDataGarden.IsOpenedRiddleSnake)
        //{
        //    for (int i = 0; i < Riddle.GetComponent<RiddleSnake>().Levels.Count; i++)
        //    {
        //        LocationDataGarden.LevelsComplete.Add(false);
        //        Riddle.GetComponent<RiddleSnake>().Levels[i].GetComponent<RiddleSnakeLevels>().isComplete = LocationDataGarden.LevelsComplete[i];
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < Riddle.GetComponent<RiddleSnake>().Levels.Count; i++)
        //    {
        //        Riddle.GetComponent<RiddleSnake>().Levels[i].GetComponent<RiddleSnakeLevels>().isComplete = LocationDataGarden.LevelsComplete[i];
        //    }
        //}
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
    }

    private void OnMouseEnter()
    {
		if (!PauseMenu.GameIsPaused && !GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
		{
            Cursor.SetCursor(GameManager.instance.cursorClick, Vector2.zero, CursorMode.ForceSoftware);
        } 
    }

    private void OnMouseExit()
    {
		if (!PauseMenu.GameIsPaused && !GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
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
                if (HaveItem)
                {
                    GameManager.instance.Player.GetComponent<BoxCollider2D>().enabled = false;
                    Riddle.SetActive(true);
                    GameManager.instance.isRiddleActive = true;
                    PlayerController.LockMovement = true;
                    if (NPC != null)
                    {
                        NPC.GetComponent<BoxCollider2D>().enabled = false;
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
