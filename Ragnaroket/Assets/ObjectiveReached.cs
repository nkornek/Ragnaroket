using UnityEngine;
using System.Collections;

public class ObjectiveReached : MonoBehaviour {

	public bool isActive, triggered;
	public ObjectiveList objectives;
	public Transform playerShip;
	public float TriggerRange;

	public void ShowObjective()
	{
		gameObject.GetComponentInChildren<MeshRenderer> ().enabled = true;
		gameObject.GetComponent<CapsuleCollider> ().enabled = true;
		isActive = true;
	}

	void Update()
	{
		if (triggered)
		{
			//put stuff for spacevaettir text;
		}
	}

	void GoToNextObjective()
	{
		CancelInvoke ("GoToNextObjective");
		objectives.NewObjective ();
	}
}
