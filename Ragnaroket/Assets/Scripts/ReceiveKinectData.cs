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
	public float shootThreshhold, armthreshhold;
	public Texture2D[] crosshairs;
	public GUITexture crossSprite;

	public Vector3 oldPos;
	

	// Use this for initialization
	void Start () {
		oldPos = girlSkele.LeftHand.position;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!ship.debugControls)
		{
			//throttle control
			kneeUp = (Mathf.Abs(vikingSkele.LeftHip.localPosition.y - vikingSkele.LeftKnee.localPosition.y) <= threshholdKneeLift 
							& vikingSkele.LeftHip.localPosition.z - vikingSkele.LeftKnee.localPosition.z >= threshholdKneeSide);

			footOut = (kneeUp & vikingSkele.LeftFoot.localPosition.z - vikingSkele.LeftKnee.localPosition.z <= threshholdFootOut);
			ship.accelerating = footOut;

			if (footOut)
			{
				float footDist = vikingSkele.LeftFoot.localPosition.z - vikingSkele.LeftKnee.localPosition.z;
				footDist = Mathf.Abs(footDist);
				ship.speedMult = Mathf.Lerp(ship.speedMult, footDist, 0.1f);
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

			//new kinect position stuff
			if (Vector3.Distance(vikingSkele.RightHand.position, girlSkele.LeftHand.position) < shootThreshhold
			    & Vector3.Distance(vikingSkele.RightHand.position, vikingSkele.Torso.position) > armthreshhold)
			{
				if (canShoot & Vector3.Distance(vikingSkele.RightHand.position, girlSkele.LeftHand.position) < Vector3.Distance(vikingSkele.RightHand.position, oldPos))
				{
					cannonScript.Fire();
					canShoot = false;
				}
			}
			if (Vector3.Distance(vikingSkele.RightHand.position, girlSkele.LeftHand.position) > Vector3.Distance(vikingSkele.RightHand.position, oldPos))
			{
				canShoot = true;
			}
			oldPos = girlSkele.LeftHand.position;

			/*
			//debug
			if (Input.GetKeyDown(KeyCode.O))
			{
				print(Vector3.Distance(girlSkele.LeftHand.position, vikingSkele.RightHand.position));
			}

			if (Vector3.Distance(vikingSkele.RightHand.position, vikingSkele.Torso.position) > armthreshhold)
			{
				print ("ok");
			}
			if (Input.GetKey(KeyCode.U))
			{
				print (Vector3.Distance(vikingSkele.RightHand.position, vikingSkele.Torso.position));
			}
			*/
		}
	}
}
