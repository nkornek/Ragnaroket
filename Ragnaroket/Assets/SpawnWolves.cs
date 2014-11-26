using UnityEngine;
using System.Collections;

public class SpawnWolves : MonoBehaviour {
	public GameObject wolf;
	public Transform playerShip;
	public int spawnDistance;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			SpawnWolf();
		}
	}

	void SpawnWolf()
	{
		Vector3 spawnPos = Random.onUnitSphere * spawnDistance;
		Instantiate(wolf, playerShip.position + spawnPos, Quaternion.identity);
	}

}
