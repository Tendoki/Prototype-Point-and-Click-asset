using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    public int id;
    public List<GameObject> NeighboringNodes;
    public bool isSelected;
    public Transform myParent;

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

	private void Start()
    {
        myParent = this.gameObject.transform.parent;
    }
    private void OnMouseDown()
    {
        if (!PlayerController.LockMovement && !GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
        {
            PlayerController.isChangeLocation = false;
            myParent.GetComponent<NodesController>().FinalNode = this.gameObject;
            PlayerController.isChangeLocation = false;
            myParent.GetComponent<NodesController>().DAlg();
        }
    }

    private void OnMouseEnter()
    {
        if (!GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
        {
            Vector2 offset = new Vector2(60, 0);
            Cursor.SetCursor(GameManager.instance.cursorWalk, offset, CursorMode.ForceSoftware);
        }    
    }

    private void OnMouseExit()
    {
        if (!GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
            Cursor.SetCursor(GameManager.instance.cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == 6)
    //    {
    //        myParent.GetComponent<NodesController>().CurrentNode = this.gameObject;
    //    }
    //}
}
