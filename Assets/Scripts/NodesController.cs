using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesController : MonoBehaviour
{
    public GameObject Player;
    public int CountNodes;
    public GameObject StartNode;
    public GameObject CurrentNode;
    public GameObject CurrentDAlgNode;
    public int CurrentNodeInd;
    public GameObject FinalNode;
    public List<GameObject> Nodes;
    public List<GameObject> NeighboringNodes;
    public List<int> IntNodes;
    public List<bool> BoolNodes;
    public List<GameObject> Path;
    public bool isStay;
    public float speed;

	private void Awake()
	{
        foreach (Transform child in this.transform) Nodes.Add(child.gameObject);
        speed = 0.5f;
        isStay = true;
        IntNodes = new List<int>(new int[Nodes.Count]);
        BoolNodes = new List<bool>(new bool[Nodes.Count]);
        for (int i = 0; i < IntNodes.Count; i++)
        {
            Nodes[i].GetComponent<NodeController>().id = i;
            IntNodes[i] = 1000;
            BoolNodes[i] = false;
        }
    }
	public void Start()
    {
        
    }

    public void DAlg()
    {
        Path = new List<GameObject>();
        StartNode = CurrentNode;
        if (CurrentDAlgNode != StartNode)
        {
            IntNodes = new List<int>(new int[Nodes.Count]);
            BoolNodes = new List<bool>(new bool[Nodes.Count]);
            for (int i = 0; i < IntNodes.Count; i++)
            {
                IntNodes[i] = 1000;
                BoolNodes[i] = false;
            }
            CurrentDAlgNode = StartNode;
            NeighboringNodes = CurrentDAlgNode.GetComponent<NodeController>().NeighboringNodes;
            CurrentNodeInd = StartNode.GetComponent<NodeController>().id;
            IntNodes[CurrentNodeInd] = 0;
            BoolNodes[CurrentNodeInd] = true;

            for (int i = 0; i < IntNodes.Count; i++)
            {
                foreach (GameObject NeighboringNode in NeighboringNodes)
                {
                    int NeighboringNodeInd = NeighboringNode.GetComponent<NodeController>().id;
                    if (IntNodes[NeighboringNodeInd] > IntNodes[CurrentNodeInd] + 1)
                    {
                        IntNodes[NeighboringNodeInd] = IntNodes[CurrentNodeInd] + 1;
                    }
                }
                int minNodeInd = CurrentNodeInd;
                int minNodeP = 1001;
                for (int j = 0; j < IntNodes.Count; j++)
                {
                    if (BoolNodes[j] == false && IntNodes[j] < minNodeP)
                    {
                        minNodeP = IntNodes[j];
                        minNodeInd = j;
                    }
                }
                BoolNodes[minNodeInd] = true;
                CurrentNodeInd = minNodeInd;
                CurrentDAlgNode = Nodes[minNodeInd];
                NeighboringNodes = CurrentDAlgNode.GetComponent<NodeController>().NeighboringNodes;
            }
        }
        
        Path.Insert(0, FinalNode);
        int FinalNodeInd = FinalNode.GetComponent<NodeController>().id;
        CurrentNodeInd = FinalNodeInd;
        int Value = IntNodes[CurrentNodeInd];
        CurrentDAlgNode = Nodes[CurrentNodeInd];
        NeighboringNodes = CurrentDAlgNode.GetComponent<NodeController>().NeighboringNodes;
        int ind = 0;
        while (ind < 1000)
        {
            foreach (GameObject NeighboringNode in NeighboringNodes)
            {
                int NeighboringNodeInd = NeighboringNode.GetComponent<NodeController>().id;
                if (IntNodes[CurrentNodeInd] - 1 == IntNodes[NeighboringNodeInd])
                {
                    Value -= 1;
                    CurrentNodeInd = NeighboringNodeInd;
                    CurrentDAlgNode = Nodes[CurrentNodeInd];
                    Path.Insert(0, CurrentDAlgNode);
                    NeighboringNodes = CurrentDAlgNode.GetComponent<NodeController>().NeighboringNodes;
                    break;
                }
            }
            if (Value == 0)
                break;
            ind++;
        }
        isStay = false;
        Player.GetComponent<PlayerController>().MovePlayer(Path);
    }
}