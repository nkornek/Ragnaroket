using UnityEngine;
using System.Collections;

public class ReceiveKinectData : MonoBehaviour {

	public ZigSkeleton vikingSkele, girlSkele;
	public ShipMovement ship;

	//accel stuff
	public float threshholdKneeLift, threshholdKneeSide, threshholdFootOut;
	bool kneeUp, footOut;

	//steer stuff
	public float steerZ;
	public float steerX;

	//aim & shoot stuff
	public CannonControl cannonScript;
	public bool canShoot;
	public float shootThreshhold;
	public Texture2D[] crosshairs;
	public GUITexture crossSprite;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!ship.debugControls)
		{
			//throttle control
			kneeUp = (Mathf.Abs(vikingSkele.LeftHip.localPosition.y - vikingSkele.LeftKnee.localPosition.y) <= threshholdKneeLift 
							& vikingSkele.LeftHip.localPosition.x - vikingSkele.LeftKnee.localPosition.x >= threshholdKneeSide);

			footOut = (kneeUp & vikingSkele.LeftFoot.localPosition.x - vikingSkele.LeftKnee.localPosition.x <= threshholdFootOut);
			ship.accelerating = footOut;

			if (footOut)
			{
				float footDist = vikingSkele.LeftFoot.localPosition.x - vikingSkele.LeftKnee.localPosition.x;
				footDist = Mathf.Abs(footDist);
				ship.speedMult = footDist;
				print ("footdist" + footDist);
			}


			//steering control
			//will later swap out for player 2
			if (!ship.accelerating)
			{
				steerX = girlSkele.Torso.localRotation.x * 3;
				steerZ = girlSkele.Torso.localRotation.z * 3;
				ship.turnX = steerX;
				ship.turnZ = steerZ;
			}
		}
		if (!cannonScript.DebugControl)
		{
			//Aiming Control
			cannonScript.crosshair.position = Vector2.Lerp(cannonScript.crosshair.position, vikingSkele.RightHand.localPosition * 2, 0.1f);

			//debug
			if (Input.GetKeyDown(KeyCode.O))
			{
				print(Vector3.Distance(girlSkele.LeftHand.position, vikingSkele.RightHand.position));
			}
		}
	}
}
