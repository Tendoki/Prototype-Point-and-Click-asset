using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateDialogue : MonoBehaviour
{
    public TextAsset ta;
    public DialogueController dialog;
    public int CurrentNode;
    public bool ShowDialogue;

    public GUISkin skin;
    public List<Answer> answers = new List<Answer>();
    public string ItemGiveName;
    public GameObject ItemGet;

    private void Awake()
    {
        dialog = DialogueController.Load(ta);
        //UpdateAnswers();
    }


    public void UpdateAnswers()
    {
        answers.Clear();
        if (dialog.nodes[CurrentNode].answers != null)
        {
            for (int i = 0; i < dialog.nodes[CurrentNode].answers.Length; i++)
            {
                if (dialog.nodes[CurrentNode].answers[i].QuestName == null || dialog.nodes[CurrentNode].answers[i].NeedQuestValue == DataQuests.QuestsValues[dialog.nodes[CurrentNode].answers[i].QuestName])
                {
                    answers.Add(dialog.nodes[CurrentNode].answers[i]);
                }
            }
        }
    }

    private void OnGUI()
    {
        GUI.skin = skin;
        GUI.skin.label.fontSize = (int)(0.024f * Screen.height);
        GUI.skin.button.fontSize = (int)(0.0185f * Screen.height);
        if (ShowDialogue && !PauseMenu.GameIsPaused)
        {
            UpdateAnswers();
            GUI.Box(new Rect(0.05f * Screen.width, Screen.height - 0.26f * Screen.height, 0.9f * Screen.width, 0.24f * Screen.height), "");
            GUI.Label(new Rect(0.072f * Screen.width, Screen.height - 0.194f * Screen.height, 0.42f * Screen.width, 0.24f * Screen.height), dialog.nodes[CurrentNode].NpcText);
            for (int i = 0; i < answers.Count; i++)
            {
                if (GUI.Button(new Rect(Screen.width / 2, Screen.height - 0.194f * Screen.height + 0.037f * Screen.height * i, 0.42f * Screen.width, 0.0324f * Screen.height), answers[i].text))
                {
                    if (answers[i].QuestValue > 0)
                    {
                        DataQuests.QuestsValues[answers[i].QuestName] = answers[i].QuestValue;
                    }

                    if (answers[i].QuestValue == 1)
                    {
                        foreach (GameObject item in DataItems.ItemsInventory)
                        {
                            if(item.GetComponent<ItemInventoryController>().QuestName == answers[i].QuestName)
                            {
                                DataQuests.QuestsValues[answers[i].QuestName] = 2;
                                break;
                            }
                        }
                    }

                    if (ItemGiveName != "")
                    {
                        if (answers[i].QuestValue == 3)
                        {
                            foreach (GameObject item in DataItems.ItemsInventory)
                            {
                                if (item.GetComponent<ItemInventoryController>().NameItemInventory == ItemGiveName)
                                {
                                    DataItems.ItemsInventory.Remove(item);
                                    break;
                                }
                            }
                            foreach (Transform child in InventoryController.instance.HolderInventoryTransform)
                            {
                                if (child.GetComponent<ItemInventoryController>().NameItemInventory == ItemGiveName)
                                {
                                    Destroy(child.gameObject);
                                    break;
                                }
                            }
                            if (ItemGet != null)
                            {
                                InventoryController.instance.InventoryMenuWait();
                                DataItems.ItemsInventory.Add(ItemGet);
                                Instantiate(ItemGet, InventoryController.instance.HolderInventoryTransform);
                            }
                        }
                    }

                    if (answers[i].end == "true")
                    {
                        PlayerController.LockMovement = false;
                        ShowDialogue = false;
                    }
                    CurrentNode = answers[i].nextNode;
                    UpdateAnswers();
                }
            }
        }  
    }
}
