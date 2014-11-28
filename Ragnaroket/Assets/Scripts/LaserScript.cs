using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {
	public float laserSpeed, lifetime;
	bool moving;

	// Use this for initialization
	void Start () {
		moving = true;
	
	}
	
	// Update is called once per frame
	void Update () {
		lifetime -= Time.deltaTime;
		if (lifetime <= 0 & moving)
		{
			Destroy(gameObject);
		}

		if (moving) 
		{
			transform.Translate (Vector3.forward * laserSpeed * Time.deltaTime);
		}
	
	}

	void OnCollisionEnter(Collision coll)
	{
		moving = false;
		gameObject.GetComponent<BoxCollider> ().enabled = false;
		gameObject.GetComponent<Light> ().enabled = false;
		foreach(SpriteRenderer s in gameObject.GetComponentsInChildren<SpriteRenderer>())
		{
			s.enabled = false;
		}
		gameObject.GetComponentInChildren<ParticleSystem> ().Emit (50);
		Invoke ("Die", 2);
	}

	void Die ()
	{
		Destroy (gameObject);
	}
}
