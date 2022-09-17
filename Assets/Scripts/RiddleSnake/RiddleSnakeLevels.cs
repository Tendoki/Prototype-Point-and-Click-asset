using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleSnakeLevels : MonoBehaviour
{
    public GameObject Riddle;
    public int id;
    public List<int> LineBlockedLamps;
    public List<int> ColumnBlockedLamps;
    public bool isComplete;

    private void Start()
    {
        Riddle = this.transform.parent.gameObject;
        Riddle = Riddle.transform.parent.gameObject;
    }

	private void Update()
	{
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
        if (GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
        {
            Vector2 offset = new Vector2(10, 0);
            Cursor.SetCursor(GameManager.instance.cursorClick, offset, CursorMode.ForceSoftware);
        }
    }

    private void OnMouseExit()
    {
        if (GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
        {
            Cursor.SetCursor(GameManager.instance.cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    private void OnMouseUp()
    {
		if (GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
		{
            if (!Riddle.GetComponent<RiddleSnake>().Levels[Riddle.GetComponent<RiddleSnake>().CurrentLevel].GetComponent<RiddleSnakeLevels>().isComplete)
            {
                Color tempColorLvl = new Color(1F, 0F, 0F, 1F);
                Riddle.GetComponent<RiddleSnake>().Levels[Riddle.GetComponent<RiddleSnake>().CurrentLevel].GetComponent<SpriteRenderer>().color = tempColorLvl;
            }
            if (!isComplete)
            {
                Color colorLvl = new Color(1F, 1F, 0F, 1F);
                this.GetComponent<SpriteRenderer>().color = colorLvl;
            }
            Riddle.GetComponent<RiddleSnake>().CurrentLevel = id;
            if (isComplete)
            {
                Riddle.GetComponent<RiddleSnake>().LevelComplete();
            }
            else
            {
                Riddle.GetComponent<RiddleSnake>().RestartLevel();
            }
        }
    }
}