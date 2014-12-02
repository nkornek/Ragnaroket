using UnityEngine;
using System.Collections;

public class ObjectiveList : MonoBehaviour {

	public GameObject[] objectiveList;
	public int currentObjective;
	public ArrowScript arrow;

	void Start()
	{
		objectiveList [currentObjective].GetComponent<ObjectiveReached> ().ShowObjective ();
	}

	public void NewObjective()
	{
		if (currentObjective < objectiveList.Length)
		{
			currentObjective ++;
		}
		arrow.Objective = objectiveList [currentObjective].transform;
		objectiveList [currentObjective].GetComponent<ObjectiveReached> ().ShowObjective ();
	}
}