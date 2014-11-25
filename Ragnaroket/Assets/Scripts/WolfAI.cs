using UnityEngine;
using System.Collections;

public class WolfAI : MonoBehaviour {

	public Transform playerShip;
	public float wolfSpeed, detectRange;
	public bool lockedOn;
	public float lifespan;

	// Use this for initialization
	void Start () {
		playerShip = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {

		if (Vector3.Distance (transform.position, playerShip.position) < detectRange)
		{
			lockedOn = true;
		}
		else
		{
			lockedOn = false;
		}

		if (lockedOn)
		{
			Quaternion oldRot = transform.rotation;
			transform.LookAt (playerShip);
			Quaternion newRot = transform.rotation;
			transform.rotation = Quaternion.Lerp(oldRot, newRot, 0.1f);
			rigidbody.AddRelativeForce(Vector3.forward * wolfSpeed * Time.deltaTime);
		}	
	}
}
