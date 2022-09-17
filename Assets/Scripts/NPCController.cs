using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public int id;
    public bool isPlayerNear;
    public GameObject NearNode;
    public string NameNPC;
    public string QuestName;
    public InstantiateDialogue instantiateDialogue;

    private void Start()
    {
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
        else if (!GameManager.instance.isRiddleActive)
        {
            this.gameObject.GetComponent<Collider2D>().enabled = true;
        }
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
            Cursor.SetCursor(GameManager.instance.cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void OnMouseUp()
    {
        if (!GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
        {
            PlayerController.isChangeLocation = false;
            if (isPlayerNear)
            {
                instantiateDialogue.UpdateAnswers();
                instantiateDialogue.ShowDialogue = true;
                PlayerController.LockMovement = true;
            }
            else
            {
                GameManager.instance.Places.GetComponent<NodesController>().FinalNode = NearNode;
                GameManager.instance.Places.GetComponent<NodesController>().DAlg();
            }
        }
    }
}
