using UnityEngine;
using System.Collections;

public class ReceiveKinectData : MonoBehaviour {
	public ZigSkeleton vikingSkele, girlSkele;
	public ZigEngageSingleUser userEngage;
	public ShipMovement ship;

	//accel stuff
	public float threshholdKneeLift, threshholdKneeSide, threshholdFootOut;
	bool kneeUp, footOut;

	//steer stuff
	public float steerZ;
	public float steerX;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!ship.debugControls)
		{
			//throttle control
			kneeUp = (vikingSkele.LeftHip.localPosition.y - vikingSkele.LeftKnee.localPosition.y <= threshholdKneeLift 
							& vikingSkele.LeftHip.localPosition.x - vikingSkele.LeftKnee.localPosition.x >= threshholdKneeSide);

			footOut = (kneeUp & vikingSkele.LeftFoot.localPosition.x - vikingSkele.LeftKnee.localPosition.x <= threshholdFootOut);
			ship.accelerating = footOut;

			if (footOut)
			{
				float footDist = vikingSkele.LeftFoot.localPosition.x - vikingSkele.LeftKnee.localPosition.x;
				footDist = Mathf.Abs(footDist);
				ship.speedMult = footDist;
			}


			//steering control
			//will later swap out for player 2
			if (!ship.accelerating)
			{
				steerX = vikingSkele.Torso.localRotation.x * 3;
				steerZ = vikingSkele.Torso.localRotation.z * 3;
				ship.turnX = steerX;
				ship.turnZ = steerZ;
			}
		}
	}
}
