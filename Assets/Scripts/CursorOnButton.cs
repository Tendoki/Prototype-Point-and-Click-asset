using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorOnButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public void OnPointerEnter(PointerEventData eventData)
	{
		Vector2 offset = new Vector2(10, 0);
		Cursor.SetCursor(UIManager.cursorClick, offset, CursorMode.ForceSoftware);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Cursor.SetCursor(UIManager.cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
	}
}
