using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KinectMenu : MonoBehaviour {
	public bool debug;

	public ZigEngageSplitScreen playersZig;

	public Text uiText;

	public bool gameStarting;

	public ZigSkeleton viking, girl;

	bool p1In, p2In;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (debug)
		{
			if (Input.GetKeyDown(KeyCode.L) & !gameStarting)
			{
				LoadNextLevel();
				gameStarting = true;
			}
		}
		else
		{
			if (playersZig.AllUsersEngaged & !gameStarting)
			{
				uiText.text = "RAISE RIGHT HANDS TO START";
			}
			else if (!gameStarting)
			{
				uiText.text = "WAITING FOR CREW";
			}

			//raise hands to start
			if (playersZig.AllUsersEngaged)
			{

				//player 1
				if (viking.RightHand.position.y > viking.Head.position.y)
				{
					p1In = true;
				}
				else
				{
					p1In = false;
				}
				if (girl.RightHand.position.y > girl.Head.position.y)
				{
					p2In = true;
				}
				else
				{
					p2In = false;
				}
				if (p1In & p2In & !gameStarting)
				{
					gameStarting = true;
					LoadNextLevel();
				}
			}
		}

	}

	void LoadNextLevel ()
	{
		uiText.text = "LOADING";
		Application.LoadLevelAsync(1);
	}
}
