using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {
	public float shipAccelSpeed, speedMult, turnspeed;
	public bool accelerating;
	public float turnX, turnZ; 

	public bool debugControls;

	public AudioSource rocketBooster;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (debugControls)
		{
			if (Input.GetKey(KeyCode.Space))
			{
				if (speedMult < 0.2f)
				{
					speedMult = 0.2f;
				}
				speedMult = Mathf.Lerp(speedMult, 0.45f, 0.1f);
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
			}
		}
		//accelerate ship
		if (accelerating)
		{
			turnX = Mathf.Lerp (turnX, 0, 0.1f);
			turnZ = Mathf.Lerp (turnZ, 0, 0.1f);
			rigidbody.AddRelativeForce(Vector3.forward * shipAccelSpeed * speedMult * Time.deltaTime);
			rocketBooster.volume = Mathf.Lerp(rocketBooster.volume, 1, 0.2f);
		}
		else
		{
			speedMult = Mathf.Lerp(speedMult, 0, 0.1f);
			rocketBooster.volume = Mathf.Lerp(rocketBooster.volume, 0, 0.2f);
		}
		//set rotation
		transform.Rotate(new Vector3(turnX * turnspeed * Time.deltaTime, 0, turnZ * turnspeed * Time.deltaTime));

	}

	void OnCollisionEnter (Collision coll) 
	{
		gameObject.GetComponent<AudioSource> ().Play ();
	}
}
