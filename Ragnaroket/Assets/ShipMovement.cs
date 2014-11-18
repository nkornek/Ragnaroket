using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {
	public float shipAccel, turnspeed;
	public bool accelerating;
	public float turnX, turnY; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!accelerating)
		{
			//will change debug keys for motion controls later
			//up/down
			if (Input.GetKey(KeyCode.UpArrow))
			{
				if (turnX < 1)
				{
					turnX = Mathf.Lerp (turnX, 1, 0.1f);
				}
			}
			else if (Input.GetKey(KeyCode.DownArrow))
			{
				if (turnX > -1)
				{
					turnX = Mathf.Lerp (turnX, -1, 0.1f);
				}
			}
			else
			{
				turnX = Mathf.Lerp (turnX, 0, 0.1f);
			}

			//left/right
			if (Input.GetKey(KeyCode.RightArrow))
			{
				if (turnY < 1)
				{
					turnY = Mathf.Lerp (turnY, 1, 0.1f);
				}
			}
			else if (Input.GetKey(KeyCode.LeftArrow))
			{
				if (turnY > -1)
				{
					turnY = Mathf.Lerp (turnY, -1, 0.1f);
				}
			}
			else
			{
				turnY = Mathf.Lerp (turnY, 0, 0.1f);
			}

			//set rotation
			transform.rotation = Quaternion.Euler(transform.rotation.x + turnX * turnspeed * Time.deltaTime, 
			                                      transform.rotation.y + turnY * turnspeed * Time.deltaTime, 
			                                      0);
		}
	
	}
}
