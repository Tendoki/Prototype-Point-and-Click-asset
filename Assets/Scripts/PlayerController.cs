using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]

public class PlayerController : MonoBehaviour
{
    public Vector2 StartPosition;
    public GameObject FirstRightLocationNode;
    public GameObject SecondRightLocationNode;
    public GameObject FirstLeftLocationNode;
    public GameObject SecondLeftLocationNode;
    public GameObject FirstLocationNode;
    public GameObject SecondLocationNode;
    public bool isStay;
    public bool isChange;
    public static bool isChangeLocation;
    public GameObject Places;
    public Animator animator;
    public float cosx;
    public float sinx;
    public GameObject CurrentNode;
    public float Speed;
    public static bool LockMovement;
    public GameObject Door;

    public AudioClip PickingUpItemMusic;
    public AudioClip FootStepsMusic;

    private void Awake()
    {
        
    }

	private void Start()
    {
        isChangeLocation = false;
        LockMovement = false;
        cosx = 0;
        sinx = -1;
        animator.SetFloat("Horizontal", cosx);
        animator.SetFloat("Vertical", sinx);
        isStay = true;
        isChange = false;
		if (!DataPlayer.IsLoadSave)
		{
            if (DataPlayer.IsCameFromTheRight)
            {
                FirstLocationNode = FirstRightLocationNode;
                SecondLocationNode = SecondRightLocationNode;
            }
            else
            {
                FirstLocationNode = FirstLeftLocationNode;
                SecondLocationNode = SecondLeftLocationNode;
            }
            transform.position = FirstLocationNode.transform.position;
            StartCoroutine(MoveToRightLocationNode());
        }
        else
		{
            FirstLocationNode = Places.GetComponent<NodesController>().Nodes[DataPlayer.CurrentNode];
            SecondLocationNode = Places.GetComponent<NodesController>().Nodes[DataPlayer.CurrentNode];
            transform.position = FirstLocationNode.transform.position;
            StartCoroutine(MoveToRightLocationNode());
        }

    }

    private IEnumerator MoveToRightLocationNode()
    {
        LockMovement = true;
        isStay = false;
        double deltaX = SecondLocationNode.transform.position.x - FirstLocationNode.transform.position.x;
        double deltaY = SecondLocationNode.transform.position.y - FirstLocationNode.transform.position.y;
        float delta = (float)(Math.Sqrt(deltaX * deltaX + deltaY * deltaY));
        cosx = (float)(deltaX) / delta;
        sinx = (float)(deltaY) / delta;
        animator.SetFloat("Horizontal", cosx);
        animator.SetFloat("Vertical", sinx);
        animator.SetBool("IsWalk", true);
        while (Vector2.Distance(transform.position, SecondLocationNode.transform.position) > 0.005f)
        {
            yield return new WaitForSeconds(0.001f);
            transform.position = Vector3.MoveTowards(transform.position, SecondLocationNode.transform.position, Speed * Time.deltaTime);
        }
        animator.SetBool("IsWalk", false);
        CurrentNode = SecondLocationNode;
        Places.GetComponent<NodesController>().CurrentNode = SecondLocationNode;
        isStay = true;
        LockMovement = false;
        
    }

    public void MovePlayer(List<GameObject> Path)
    {
        StartPosition = transform.position;
        if (isStay)
        {
            animator.SetBool("IsWalk", true);
            isStay = false;
            StartCoroutine(MovePlayerNow(Path));
        }
        else
        {
            isChange = true;
        }
    }

    private IEnumerator MovePlayerNow(List<GameObject> Path)
    {
        this.GetComponent<AudioSource>().clip = FootStepsMusic;
        this.GetComponent<AudioSource>().Play();
        for (int i = 0; i < Path.Count; i++)
        {
            //value = 0;
            double deltaX = Path[i].transform.position.x - transform.position.x;
            double deltaY = Path[i].transform.position.y - transform.position.y;
            float delta = (float)(Math.Sqrt(deltaX * deltaX + deltaY * deltaY));
            cosx = (float)(deltaX) / delta;
            sinx = (float)(deltaY) / delta;
            animator.SetFloat("Horizontal", cosx);
            animator.SetFloat("Vertical", sinx);
            //float CountInterval = delta / Step;
            //Interval = 1 / CountInterval;
            while (/*value <= 1*/Vector2.Distance(transform.position, Path[i].transform.position) > 0.005f)
            {
                yield return new WaitForSeconds(0.001f);
                //value += Interval;
                transform.position = Vector3.MoveTowards(transform.position, Path[i].transform.position, Speed * Time.deltaTime);
                //transform.position = Vector2.Lerp(Path[i].transform.position, Path[i + 1].transform.position, value);
            }
            CurrentNode = Path[i];
            if (isChange)
            {
                break;
            }
        }
        
        if (isChange)
        {
            this.GetComponent<AudioSource>().Pause();
            Places.GetComponent<NodesController>().CurrentNode = CurrentNode; 
            isChange = false;
            isStay = true;
            animator.SetBool("IsWalk", false);
            Places.GetComponent<NodesController>().DAlg();
        }
        else
        {
            this.GetComponent<AudioSource>().clip = null;
            this.GetComponent<AudioSource>().Stop();
            Places.GetComponent<NodesController>().CurrentNode = CurrentNode;
            animator.SetBool("IsWalk", false);
            isStay = true;
            if (isChangeLocation)
            {
                Door.GetComponent<DoorActivate>().ChangeLocation();
            }
        }
    }

    public void PlayPickingUpItem()
	{
        this.GetComponent<AudioSource>().PlayOneShot(PickingUpItemMusic);
    }
}
