using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isRiddleActive;
    public GameObject Player;
    public GameObject Places;
    public TextAsset ta;
    public DataQuestName Dqn;
    public GameObject NPC;
    [SerializeField] public Texture2D cursorDefault;
    [SerializeField] public Texture2D cursorWalk;
    [SerializeField] public Texture2D cursorClick;

	private void Awake()
	{
        cursorDefault = UIManager.cursorDefault;
        cursorWalk = UIManager.cursorWalk;
        cursorClick = UIManager.cursorClick;
	}

	private void Start()
    {
        Places = Player.GetComponent<PlayerController>().Places;
        Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.ForceSoftware);
        Dqn = DataQuestName.Load(ta);
        instance = this;
        isRiddleActive = false;
        if (DataQuests.QuestsValues.Count == 0)
        {
            for (int i = 0; i < Dqn.Quests.Count; i++)
            {
                DataQuests.QuestsValues.Add(Dqn.Quests[i].QuestName, 0);
            }
        }     
    }
}
