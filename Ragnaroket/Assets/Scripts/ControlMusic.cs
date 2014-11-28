using UnityEngine;
using System.Collections;

public class ControlMusic : MonoBehaviour {
	public AudioSource actionMusic, calmMusic;

	public Transform playerShip;
	public GameObject wolf;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (wolf)
		{
			if (Vector3.Distance (wolf.transform.position, playerShip.position) < 1000)
			{
				float adjustedDist = Mathf.Abs(Vector3.Distance (wolf.transform.position, playerShip.position) - 1000) / 1000;
				float inverseDist = Vector3.Distance (wolf.transform.position, playerShip.position) / 1000;
				actionMusic.volume = Mathf.Lerp(actionMusic.volume, adjustedDist, 0.1f);
				calmMusic.volume = Mathf.Lerp(calmMusic.volume, inverseDist, 0.1f);
			}
		}
	
	}
}
