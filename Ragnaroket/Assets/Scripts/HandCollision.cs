using UnityEngine;
using System.Collections;

public class HandCollision : MonoBehaviour {

	public CannonControl cannonScript;

	// Use this for initialization
	void OnCollisionEnter()
	{
		cannonScript.Fire ();
	}
}
