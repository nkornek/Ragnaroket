using UnityEngine;
using System.Collections;

public class GirlAnimationController : MonoBehaviour {

	public ShipMovement shipSteering;

	// Update is called once per frame
	void Update () {
		//forward
		if (shipSteering.turnX > 0.2f)
		{
			gameObject.GetComponent<Animator>().SetBool("Forward", true);
		}
		else
		{
			gameObject.GetComponent<Animator>().SetBool("Forward", false);
		}
		//backward
		if (shipSteering.turnX < -0.2f)
		{
			gameObject.GetComponent<Animator>().SetBool("Backward", true);
		}
		else
		{
			gameObject.GetComponent<Animator>().SetBool("Backward", false);
		}
		//left
		if (shipSteering.turnZ > 0.2f)
		{
			gameObject.GetComponent<Animator>().SetBool("Left", true);
		}
		else
		{
			gameObject.GetComponent<Animator>().SetBool("Left", false);
		}
		//right
		if (shipSteering.turnZ < -0.2f)
		{
			gameObject.GetComponent<Animator>().SetBool("Right", true);
		}
		else
		{
			gameObject.GetComponent<Animator>().SetBool("Right", false);
		}
	}
}
