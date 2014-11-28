using UnityEngine;
using System.Collections;

public class GhostFacer : MonoBehaviour {
	public Transform player;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		//Quaternion oldRot = transform.rotation;
		transform.LookAt (player);
		//Quaternion newRot = transform.rotation;
		//transform.rotation = Quaternion.Euler (oldRot.x, newRot.y, oldRot.z);
	}
}
