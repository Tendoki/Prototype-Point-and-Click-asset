using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class LampController : MonoBehaviour
{
    public GameObject Riddle;
    public int idLine;
    public int idColumn;
    public bool isPainted;
    public bool isBlocked;

    private void Start()
    {
        Riddle = this.transform.parent.gameObject;
        Riddle = Riddle.transform.parent.gameObject;
        //if (isBlocked)
        //{
        //    Riddle.GetComponent<RiddleSnake>().CountBlocked++;
        //    Riddle.GetComponent<RiddleSnake>().CountPainted++;
        //    isPainted = true;
        //    Color color = new Color(1F, 0F, 0F, 0.347F);
        //    this.GetComponent<SpriteRenderer>().color = color;
        //}
        //else
        //{
        //    isPainted = false;
        //}
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
            if (!Riddle.GetComponent<RiddleSnake>().isFirstLampSelected && !isBlocked)
            {
                Riddle.GetComponent<AudioSource>().PlayOneShot(Riddle.GetComponent<RiddleSnake>().LampOnSound);
                Riddle.GetComponent<RiddleSnake>().FirstLamp = this.gameObject;
                Riddle.GetComponent<RiddleSnake>().isFirstLampSelected = true;
                Riddle.GetComponent<RiddleSnake>().CurrentIdLine = idLine;
                Riddle.GetComponent<RiddleSnake>().CurrentIdColumn = idColumn;
                if (idLine == 0)
                    Riddle.GetComponent<RiddleSnake>().isUpLampPainted = true;
                else
                    Riddle.GetComponent<RiddleSnake>().isUpLampPainted = false;
                if (idLine == Riddle.GetComponent<RiddleSnake>().Height - 1)
                    Riddle.GetComponent<RiddleSnake>().isDownLampPainted = true;
                else
                    Riddle.GetComponent<RiddleSnake>().isDownLampPainted = false;
                if (idColumn == 0)
                    Riddle.GetComponent<RiddleSnake>().isLeftLampPainted = true;
                else
                    Riddle.GetComponent<RiddleSnake>().isLeftLampPainted = false;
                if (idColumn == Riddle.GetComponent<RiddleSnake>().Width - 1)
                    Riddle.GetComponent<RiddleSnake>().isRightLampPainted = true;
                else
                    Riddle.GetComponent<RiddleSnake>().isRightLampPainted = false;
                Riddle.GetComponent<RiddleSnake>().CountPainted++;
                isPainted = true;
                Color color = new Color(0F, 1F, 0F, 0.347F);
                this.GetComponent<SpriteRenderer>().color = color;

            }
        }     
    }

}
