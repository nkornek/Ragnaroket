using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {
	public Transform Objective;
	public float offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Quaternion oldRot = transform.rotation;
		transform.LookAt (Objective);
		Quaternion newRot = transform.rotation;
		transform.rotation = Quaternion.Lerp (oldRot, newRot, 0.1f);
		transform.rotation = Quaternion.Euler (transform.rotation.x + offset, transform.rotation.y, 0);
	
	}
}
