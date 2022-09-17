using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemController : MonoBehaviour
{
    public int id;
    public GameObject ItemInventory;
    public bool isPlayerNear;
    public GameObject NearNode;
    public string NameItem;
    public string QuestName;

    private void Awake()
    {
        isPlayerNear = false;
        if (!DataItems.TookItems.ContainsKey(this.GetComponent<ItemController>().NameItem))
        {
            DataItems.TookItems.Add(this.GetComponent<ItemController>().NameItem, false);
        }
        else
        {
            if (DataItems.TookItems[this.GetComponent<ItemController>().NameItem])
            {
                Destroy(this.gameObject);
            }
        }
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

    private void OnMouseOver()
    {
        if (isPlayerNear && !GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
		{
            Vector2 offset = new Vector2(10, 0);
            Cursor.SetCursor(GameManager.instance.cursorClick, offset, CursorMode.ForceSoftware);
        }
    }

    private void OnMouseEnter()
    {
        if (isPlayerNear && !GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
		{
            Vector2 offset = new Vector2(10, 0);
            Cursor.SetCursor(GameManager.instance.cursorClick, offset, CursorMode.ForceSoftware);
        }
    }

    private void OnMouseExit()
    {
        if (isPlayerNear && !GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
            Cursor.SetCursor(GameManager.instance.cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
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
        if (isPlayerNear && !GameManager.instance.isRiddleActive)
        {
            GameManager.instance.Player.GetComponent<PlayerController>().PlayPickingUpItem();
            DataItems.TookItems[NameItem] = true;
            InventoryController.instance.InventoryMenuWait();
            DataItems.ItemsInventory.Add(ItemInventory);
            Instantiate(ItemInventory, InventoryController.instance.HolderInventoryTransform);
            if (QuestName != "")
            {
                if (DataQuests.QuestsValues[QuestName] == 1)
                {
                    DataQuests.QuestsValues[QuestName] = 2;
                }
            }
            Cursor.SetCursor(GameManager.instance.cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
            Destroy(this.gameObject);
        }      
    }

    
}
