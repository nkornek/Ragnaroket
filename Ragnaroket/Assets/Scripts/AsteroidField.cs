using UnityEngine;
using System.Collections;

public class AsteroidField : MonoBehaviour {
	public GameObject[] Asteroid;
	public int xRange, yRange, zRange;
	public int spaceBetween, variation, asteroidForce;

	// Use this for initialization
	void Start () {
		for (int x = -xRange; x < xRange; x++)
		{
			for (int y = -yRange; y < yRange; y++)
			{
				for (int z = -zRange; z < zRange; z++)
				{
					GameObject newAsteroid;
					float variationX = Random.Range(-variation, variation);
					float variationY = Random.Range(-variation, variation);
					float variationZ = Random.Range(-variation, variation);
					newAsteroid = Instantiate(Asteroid[Random.Range(0, Asteroid.Length)], 
					                          new Vector3(x * spaceBetween + variationX, y * spaceBetween + variationY, z * spaceBetween + variationZ), 
					                          Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))) as GameObject;
					newAsteroid.rigidbody.AddForce(new Vector3(Random.Range(-asteroidForce, asteroidForce), Random.Range(-asteroidForce, asteroidForce), Random.Range(-asteroidForce, asteroidForce)));
					newAsteroid.rigidbody.angularVelocity = Random.insideUnitSphere/asteroidForce;
					newAsteroid.transform.parent = transform;
				}
			}
		}
	}
}