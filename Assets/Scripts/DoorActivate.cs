using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoorActivate : MonoBehaviour
{
    public string NextLocation;
    public bool NextLocationIsRight;
    public bool NeedPass;
    public bool isPlayerNear;
    public GameObject NearNode;
    public bool HaveKey;
    public InstantiateDialogue instantiateDialogue;

    private void Start()
    {
        HaveKey = false;
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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == 6)
    //    {
    //        isPlayerNear = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == 6)
    //    {
    //        isPlayerNear = false;
    //    }
    //}

    private void OnMouseUp()
    {
        if (!PlayerController.LockMovement && !GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
        {
            PlayerController.isChangeLocation = true;
            if (!isPlayerNear)
            {
                GameManager.instance.Places.GetComponent<NodesController>().FinalNode = NearNode;
                if (NeedPass)
                {
                    foreach (GameObject item in DataItems.ItemsInventory)
                    {
                        if (item.GetComponent<ItemInventoryController>().NameItemInventory == "Key")
                        {
                            HaveKey = true;
                            break;
                        }
                    }
                }
                GameManager.instance.Places.GetComponent<NodesController>().DAlg();
            }
            else
            {
                ChangeLocation();
            }
        }
    }

    public void ChangeLocation()
    {
        if (NeedPass)
        {
            foreach (GameObject item in DataItems.ItemsInventory)
            {
                if (item.GetComponent<ItemInventoryController>().NameItemInventory == "Key")
                {
                    HaveKey = true;
                    break;
                }
            }

            if (!HaveKey)
            {
                instantiateDialogue.UpdateAnswers();
                instantiateDialogue.ShowDialogue = true;
                PlayerController.LockMovement = true;
                PlayerController.isChangeLocation = false;
            }
            else
            {
                DataPlayer.IsLoadSave = false;
                DataPlayer.IsCameFromTheRight = !NextLocationIsRight;
                SceneManager.LoadScene(NextLocation);
            }
        }
        else
        {
            DataPlayer.IsLoadSave = false;
            DataPlayer.IsCameFromTheRight = !NextLocationIsRight;
            SceneManager.LoadScene(NextLocation);
        }
    }
}
