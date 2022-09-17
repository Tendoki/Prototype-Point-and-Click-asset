using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOutside : MonoBehaviour
{
	public GameObject Riddle;
    public GameObject RiddleHotspot;

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
	private void OnMouseUp()
	{
        if (GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
        {
            GameManager.instance.Player.GetComponent<BoxCollider2D>().enabled = true;
            Riddle.SetActive(false);
            GameManager.instance.isRiddleActive = false;
            PlayerController.LockMovement = false;
            if (GameManager.instance.NPC != null)
            {
                GameManager.instance.NPC.GetComponent<BoxCollider2D>().enabled = true;
            }     
        }
    }
}
