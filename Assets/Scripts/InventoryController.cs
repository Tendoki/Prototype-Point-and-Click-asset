using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public Transform HolderInventoryTransform;
    public GameObject HolderInventory;
    public float WaitTimeInventory;
    public bool isDragging;
    public static InventoryController instance;
    public bool CursorOnInventory;
    public float HideOffsetColliderY;
    public float VisibleOffsetColliderY;
    public float HideSizeColliderY;
    public float VisibleSizeColliderY;

    private void Awake()
    {
        CursorOnInventory = false;
        isDragging = false;
        instance = this;
        for (int i = 0; i < DataItems.ItemsInventory.Count; i++)
        {
            Instantiate(DataItems.ItemsInventory[i], HolderInventoryTransform);
        }
    }

    public void InventoryMenuWait()
    {
        StartCoroutine(WaitInventory());
    }

    private IEnumerator WaitInventory()
    {
        HolderInventory.SetActive(true);
        yield return new WaitForSeconds(WaitTimeInventory);
        if (isDragging == false && !CursorOnInventory)
        {
            this.GetComponent<BoxCollider2D>().offset = new Vector2(this.GetComponent<BoxCollider2D>().offset.x, HideOffsetColliderY);
            this.GetComponent<BoxCollider2D>().size = new Vector2(this.GetComponent<BoxCollider2D>().size.x, HideSizeColliderY);
            HolderInventory.SetActive(false);
        }
    }

    private void OnMouseEnter()
    {
        this.GetComponent<BoxCollider2D>().offset = new Vector2(this.GetComponent<BoxCollider2D>().offset.x, VisibleOffsetColliderY);
        this.GetComponent<BoxCollider2D>().size = new Vector2(this.GetComponent<BoxCollider2D>().size.x, VisibleSizeColliderY);

        if (!PauseMenu.GameIsPaused)
		{
            CursorOnInventory = true;
            if (isDragging == false)
            {
                HolderInventory.SetActive(true);
            }
        }
    }

    private void OnMouseExit()
    {
        if (!PauseMenu.GameIsPaused)
		{
            CursorOnInventory = false;
            if (isDragging == false)
            {
                StartCoroutine(WaitInventory());
                Cursor.SetCursor(GameManager.instance.cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
            }
        }  
    }
}
