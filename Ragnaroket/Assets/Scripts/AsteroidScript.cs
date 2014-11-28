using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {
	public int HP;
	public ParticleSystem asteroidParts;

	float clip;
	Transform playerShip;

	void Start()
	{
		playerShip = GameObject.FindGameObjectWithTag ("Player").transform;
		clip = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<Camera> ().farClipPlane;
	}

	void Update()
	{
		gameObject.GetComponentInChildren<MeshRenderer> ().enabled = showAsteroid ();
		foreach (CapsuleCollider c in gameObject.GetComponentsInChildren<CapsuleCollider>())
		{
			c.enabled = showAsteroid();
		}
	}

	bool showAsteroid()
	{
		if (Vector3.Distance(playerShip.position, transform.position) > clip)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	void OnCollisionEnter(Collision coll) 
	{
		if (coll.transform.tag == "Laser")
		{
			print("test");
			HP --;
			if (HP == 0)
			{
				gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
				foreach (CapsuleCollider c in gameObject.GetComponentsInChildren<CapsuleCollider>())
				{
					c.enabled = false;
				}
				asteroidParts.Emit(30);
				Destroy(gameObject, 30);
			}
		}


	}
}
