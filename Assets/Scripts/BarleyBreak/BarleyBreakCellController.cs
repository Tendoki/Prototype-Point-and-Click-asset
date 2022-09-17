using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class BarleyBreakCellController : MonoBehaviour
{
    public AudioClip clickAudio;
    public Vector2 StartPos;
    public Vector2 MousePos;
    public GameObject CurrentPlace;
    public GameObject PossibleNextPlace;
    public int id;

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

    private void OnMouseDown()
    {
        if (GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
		{
            StartPos = transform.position;
            CurrentPlace.GetComponent<BarleyBreakPlaceController>().isFilled = false;
            PossibleNextPlace = CurrentPlace;
        }
    }

    private void OnMouseDrag()
    {
        if (GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
		{
            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Mathf.Abs(MousePos.x - StartPos.x) > Mathf.Abs(MousePos.y - StartPos.y))
            {
                if (MousePos.x - StartPos.x > 0)
                {
                    if (CurrentPlace.GetComponent<BarleyBreakPlaceController>().RightPlace != null && !CurrentPlace.GetComponent<BarleyBreakPlaceController>().RightPlace.GetComponent<BarleyBreakPlaceController>().isFilled)
                    {
                        if (MousePos.x < CurrentPlace.GetComponent<BarleyBreakPlaceController>().RightPlace.transform.position.x)
                        {
                            transform.position = new Vector3(MousePos.x, StartPos.y);
                        }
                        else
                        {
                            transform.position = new Vector3(CurrentPlace.GetComponent<BarleyBreakPlaceController>().RightPlace.transform.position.x, StartPos.y);
                        }
                        if (Mathf.Abs(transform.position.x - StartPos.x) > Mathf.Abs(transform.position.x - CurrentPlace.GetComponent<BarleyBreakPlaceController>().RightPlace.transform.position.x))
                        {
                            PossibleNextPlace = CurrentPlace.GetComponent<BarleyBreakPlaceController>().RightPlace;
                            //CurrentPlace = PossibleNextPlace;
                            //StartPos = CurrentPlace.transform.position;
                        }
                        else
                        {
                            PossibleNextPlace = CurrentPlace;
                        }
                    }
                    else
                    {
                        if (transform.position.x < StartPos.x)
                        {
                            if (MousePos.x < StartPos.x)
                            {
                                transform.position = new Vector3(MousePos.x, StartPos.y);
                            }
                            else
                            {
                                transform.position = new Vector3(StartPos.x, StartPos.y);
                            }
                        }
                    }
                }
                else if (MousePos.x - StartPos.x < 0)
                {
                    if (CurrentPlace.GetComponent<BarleyBreakPlaceController>().LeftPlace != null && !CurrentPlace.GetComponent<BarleyBreakPlaceController>().LeftPlace.GetComponent<BarleyBreakPlaceController>().isFilled)
                    {
                        if (MousePos.x > CurrentPlace.GetComponent<BarleyBreakPlaceController>().LeftPlace.transform.position.x)
                        {
                            transform.position = new Vector3(MousePos.x, StartPos.y);
                        }
                        else
                        {
                            transform.position = new Vector3(CurrentPlace.GetComponent<BarleyBreakPlaceController>().LeftPlace.transform.position.x, StartPos.y);
                        }
                        if (Mathf.Abs(transform.position.x - StartPos.x) > Mathf.Abs(transform.position.x - CurrentPlace.GetComponent<BarleyBreakPlaceController>().LeftPlace.transform.position.x))
                        {
                            PossibleNextPlace = CurrentPlace.GetComponent<BarleyBreakPlaceController>().LeftPlace;
                            //CurrentPlace = PossibleNextPlace;
                            //StartPos = CurrentPlace.transform.position;
                        }
                        else
                        {
                            PossibleNextPlace = CurrentPlace;
                        }
                    }
                    else
                    {
                        if (transform.position.x > StartPos.x)
                        {
                            if (MousePos.x > StartPos.x)
                            {
                                transform.position = new Vector3(MousePos.x, StartPos.y);
                            }
                            else
                            {
                                transform.position = new Vector3(StartPos.x, StartPos.y);
                            }
                        }
                    }
                }

            }
            else
            {
                if (MousePos.y - StartPos.y > 0)
                {
                    if (CurrentPlace.GetComponent<BarleyBreakPlaceController>().UpPlace != null && !CurrentPlace.GetComponent<BarleyBreakPlaceController>().UpPlace.GetComponent<BarleyBreakPlaceController>().isFilled)
                    {
                        if (MousePos.y < CurrentPlace.GetComponent<BarleyBreakPlaceController>().UpPlace.transform.position.y)
                        {
                            transform.position = new Vector3(StartPos.x, MousePos.y);
                        }
                        else
                        {
                            transform.position = new Vector3(StartPos.x, CurrentPlace.GetComponent<BarleyBreakPlaceController>().UpPlace.transform.position.y);
                        }
                        if (Mathf.Abs(transform.position.y - StartPos.y) > Mathf.Abs(transform.position.y - CurrentPlace.GetComponent<BarleyBreakPlaceController>().UpPlace.transform.position.y))
                        {
                            PossibleNextPlace = CurrentPlace.GetComponent<BarleyBreakPlaceController>().UpPlace;
                            //CurrentPlace = PossibleNextPlace;
                            //StartPos = CurrentPlace.transform.position;
                        }
                        else
                        {
                            PossibleNextPlace = CurrentPlace;
                        }
                    }
                    else
                    {
                        if (transform.position.y < StartPos.y)
                        {
                            if (MousePos.y < StartPos.y)
                            {
                                transform.position = new Vector3(StartPos.x, MousePos.y);
                            }
                            else
                            {
                                transform.position = new Vector3(StartPos.x, StartPos.y);
                            }
                        }
                    }
                }
                else if (MousePos.y - StartPos.y < 0)
                {
                    if (CurrentPlace.GetComponent<BarleyBreakPlaceController>().DownPlace != null && !CurrentPlace.GetComponent<BarleyBreakPlaceController>().DownPlace.GetComponent<BarleyBreakPlaceController>().isFilled)
                    {
                        if (MousePos.y > CurrentPlace.GetComponent<BarleyBreakPlaceController>().DownPlace.transform.position.y)
                        {
                            transform.position = new Vector3(StartPos.x, MousePos.y);
                        }
                        else
                        {
                            transform.position = new Vector3(StartPos.x, CurrentPlace.GetComponent<BarleyBreakPlaceController>().DownPlace.transform.position.y);
                        }
                        if (Mathf.Abs(transform.position.y - StartPos.y) > Mathf.Abs(transform.position.y - CurrentPlace.GetComponent<BarleyBreakPlaceController>().DownPlace.transform.position.y))
                        {
                            PossibleNextPlace = CurrentPlace.GetComponent<BarleyBreakPlaceController>().DownPlace;
                            //CurrentPlace = PossibleNextPlace;
                            //StartPos = CurrentPlace.transform.position;
                        }
                        else
                        {
                            PossibleNextPlace = CurrentPlace;
                        }
                    }
                    else
                    {
                        if (transform.position.y > StartPos.y)
                        {
                            if (MousePos.y > StartPos.y)
                            {
                                transform.position = new Vector3(StartPos.x, MousePos.y);
                            }
                            else
                            {
                                transform.position = new Vector3(StartPos.x, StartPos.y);
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnMouseUp()
    {
        if (GameManager.instance.isRiddleActive && !PauseMenu.GameIsPaused)
		{
            this.GetComponent<AudioSource>().PlayOneShot(clickAudio);
            if (PossibleNextPlace != null)
            {
                CurrentPlace = PossibleNextPlace;
                CurrentPlace.GetComponent<BarleyBreakPlaceController>().isFilled = true;
                transform.position = CurrentPlace.transform.position;
                StartPos = CurrentPlace.transform.position;
                LocationDataInFrontOfSlotMachines.CellsLocation[id] = CurrentPlace.GetComponent<BarleyBreakPlaceController>().id;
            }
            BarleyBreakController.instance.CheckVictory();
        }
    }
}
