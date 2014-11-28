using UnityEngine;
using System.Collections;

public class SpawnWolves : MonoBehaviour {
	public GameObject wolf;
	public Transform playerShip;
	public int spawnDistance;

	public float maxTimeToSpawn, timeToSpawn, variant;

	public bool debug;

	void Start ()
	{
		timeToSpawn = maxTimeToSpawn;
	}

	bool canSpawn() {
		if (GameObject.FindGameObjectWithTag("Enemy") == null)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.P) & debug)
		{
			SpawnWolf();
		}
		if (!debug & canSpawn())
		{
			timeToSpawn -= Time.deltaTime;
			if (timeToSpawn <= 0)
			{
				SpawnWolf();
				timeToSpawn = maxTimeToSpawn + Random.Range(-variant, variant);
			}
		}
	}

	void SpawnWolf()
	{
		Vector3 spawnPos = Random.onUnitSphere * spawnDistance;
		Instantiate(wolf, playerShip.position + spawnPos, Quaternion.identity);
	}



}
