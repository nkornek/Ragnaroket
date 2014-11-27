using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ThrottleAnimation : MonoBehaviour {

	public Sprite[] throttleUp;
	public ShipMovement shipScript;
	public Image[] otherViking;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (shipScript.accelerating)
		{
			otherViking[0].enabled = false;
			otherViking[1].enabled = false;
			gameObject.GetComponent<Image>().enabled = true;
			int whichSprite;
			if (shipScript.speedMult <= 0.44)
			{
				whichSprite = (shipScript.speedMult - 0.2) * 50;
			}
			else
			{
				whichSprite = 11;
			}

			gameObject.GetComponent<Image>().sprite = throttleUp[whichSprite];
		}
		else
		{
			otherViking[0].enabled = true;
			otherViking[1].enabled = true;
			gameObject.GetComponent<Image>().enabled = false;
		}
	
	}
}
