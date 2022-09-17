using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemInventoryController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	public int id;
	public GameObject Object_Drag;
	private GameObject ObjectDragInstance;
	public Camera cam;
	public Vector3 offset;
	private Vector3 pos;
	public string NameItemInventory;
	public string QuestName;

	public void OnDrag(PointerEventData eventData)
	{
		InventoryController.instance.isDragging = true;
		ObjectDragInstance.GetComponent<SpriteRenderer>().sprite = Object_Drag.GetComponent<SpriteRenderer>().sprite;
		pos = Input.mousePosition + offset;
		ObjectDragInstance.transform.position = Camera.main.ScreenToWorldPoint(pos);
		Cursor.SetCursor(GameManager.instance.cursorClick, Vector2.zero, CursorMode.ForceSoftware);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		InventoryController.instance.isDragging = true;
		this.gameObject.GetComponent<Image>().enabled = false;
		ObjectDragInstance = Instantiate(Object_Drag, pos, Quaternion.identity);
		ObjectDragInstance.GetComponent<SpriteRenderer>().sprite = Object_Drag.GetComponent<SpriteRenderer>().sprite;
		pos = Input.mousePosition + offset;
		ObjectDragInstance.transform.position = Camera.main.ScreenToWorldPoint(pos);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		InventoryController.instance.isDragging = false;
        if (!InventoryController.instance.CursorOnInventory)
        {
			InventoryController.instance.InventoryMenuWait();
		}
		this.gameObject.GetComponent<Image>().enabled = true;
		Cursor.SetCursor(GameManager.instance.cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
		Destroy(ObjectDragInstance);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
		Cursor.SetCursor(GameManager.instance.cursorClick, Vector2.zero, CursorMode.ForceSoftware);
	}

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!InventoryController.instance.isDragging)
        {
			Cursor.SetCursor(GameManager.instance.cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
		}
	}
}