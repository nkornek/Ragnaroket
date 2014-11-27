using UnityEngine;
using System.Collections;

public class ThrottleAnimation : MonoBehaviour {

	public Sprite[] throttleUp;
	public ShipMovement shipScript;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (shipScript.accelerating)
		{

		}
	
	}
}
