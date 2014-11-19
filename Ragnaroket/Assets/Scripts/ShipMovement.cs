using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {
	public float shipAccel, turnspeed;
	public bool accelerating;
	public float turnX, turnZ; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space))
		{
			accelerating = true;
		}
		else
		{
			accelerating = false;
		}
		if (!accelerating)
		{
			//will change debug keys for motion controls later
			//up/down
			if (Input.GetKey(KeyCode.W))
			{
				if (turnX < 1)
				{
					turnX = Mathf.Lerp (turnX, 1, 0.1f);
				}
			}
			else if (Input.GetKey(KeyCode.S))
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
			if (Input.GetKey(KeyCode.A))
			{
				if (turnZ < 1)
				{
					turnZ = Mathf.Lerp (turnZ, 1, 0.1f);
				}
			}
			else if (Input.GetKey(KeyCode.D))
			{
				if (turnZ > -1)
				{
					turnZ = Mathf.Lerp (turnZ, -1, 0.1f);
				}
			}
			else
			{
				turnZ = Mathf.Lerp (turnZ, 0, 0.1f);
			}

			//set rotation
		}
		//accelerate ship
		else
		{
			turnX = Mathf.Lerp (turnX, 0, 0.1f);
			turnZ = Mathf.Lerp (turnZ, 0, 0.1f);
			rigidbody.AddRelativeForce(Vector3.forward * shipAccel * Time.deltaTime);
		}
		transform.Rotate(new Vector3(turnX * turnspeed * Time.deltaTime, 0, turnZ * turnspeed * Time.deltaTime));

	}
}
