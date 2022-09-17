using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarleyBreakController : MonoBehaviour
{
    public GameObject Places;
    public GameObject Cells;
    public List<GameObject> PlacesList;
    public List<GameObject> CellsList;
    public static BarleyBreakController instance;
    public GameObject TicketInventory;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        foreach (Transform child in Places.transform)
        {
            PlacesList.Add(child.gameObject);
        }

        foreach (Transform child in Cells.transform)
        {
            CellsList.Add(child.gameObject);
        }

        for (int i = 0; i < PlacesList.Count; i++)
        {
            PlacesList[i].GetComponent<BarleyBreakPlaceController>().id = i;
        }

        for (int i = 0; i < CellsList.Count; i++)
        {
            CellsList[i].GetComponent<BarleyBreakCellController>().id = i;            
        }
		if (!LocationDataInFrontOfSlotMachines.IsOpenedBarleyBreak)
		{
            RestartRiddle();
            LocationDataInFrontOfSlotMachines.IsOpenedBarleyBreak = true;
		}
        else
		{
            RecoveryRiddle();
		}
    }

    public void RecoveryRiddle()
	{
        for (int i = 0; i < PlacesList.Count; i++)
        {
            PlacesList[i].GetComponent<BarleyBreakPlaceController>().isFilled = false;
        }

        for (int i = 0; i < CellsList.Count; i++)
		{
            CellsList[i].GetComponent<BarleyBreakCellController>().CurrentPlace = PlacesList[LocationDataInFrontOfSlotMachines.CellsLocation[i]];
            CellsList[i].transform.position = PlacesList[LocationDataInFrontOfSlotMachines.CellsLocation[i]].transform.position;
            PlacesList[LocationDataInFrontOfSlotMachines.CellsLocation[i]].GetComponent<BarleyBreakPlaceController>().isFilled = true;
        }
	}

    public void RestartRiddle()
    {
        for (int i = 0; i < PlacesList.Count; i++)
        {
            PlacesList[i].GetComponent<BarleyBreakPlaceController>().isFilled = false;
        }

        for (int i = 0; i < CellsList.Count; i++)
        {
            int rand = Random.Range(0, 9);
            while (PlacesList[rand].GetComponent<BarleyBreakPlaceController>().isFilled)
            {
                rand = Random.Range(0, 9);
            }
            CellsList[i].GetComponent<BarleyBreakCellController>().CurrentPlace = PlacesList[rand];
            CellsList[i].transform.position = PlacesList[rand].transform.position;
            PlacesList[rand].GetComponent<BarleyBreakPlaceController>().isFilled = true;
        }
    }

    public void CheckVictory()
    {
        bool isWinTemp = true;
        for (int i = 0; i < CellsList.Count; i++)
        {
            if (CellsList[i].GetComponent<BarleyBreakCellController>().id != CellsList[i].GetComponent<BarleyBreakCellController>().CurrentPlace.GetComponent<BarleyBreakPlaceController>().id)
            {
                isWinTemp = false;
                break;
            }
        }
        if (isWinTemp)
        {
            if (!LocationDataInFrontOfSlotMachines.IsWinBarleyBreak)
            {
                LocationDataInFrontOfSlotMachines.IsWinBarleyBreak = true;
                InventoryController.instance.InventoryMenuWait();
                DataItems.ItemsInventory.Add(TicketInventory);
                Instantiate(TicketInventory, InventoryController.instance.HolderInventoryTransform);
            }
        }
    }
}
