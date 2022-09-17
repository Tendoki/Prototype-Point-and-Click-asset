using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIManager
{
    [SerializeField] public static Texture2D cursorDefault;
    [SerializeField] public static Texture2D cursorWalk;
    [SerializeField] public static Texture2D cursorClick;

    public static void LoadCursorImage()
	{
        cursorDefault = Resources.Load<Texture2D>($"Cursor/CursorDefault");
        cursorWalk = Resources.Load<Texture2D>($"Cursor/CursorWalk");
        cursorClick = Resources.Load<Texture2D>($"Cursor/CursorClick");
    }

}
