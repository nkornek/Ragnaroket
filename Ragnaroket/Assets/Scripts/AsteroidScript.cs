using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {
	public int HP;
	public ParticleSystem asteroidParts;

	void OnParticleCollision(GameObject other) 
	{
		HP --;
		if (HP == 0)
		{
			gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
			gameObject.GetComponentInChildren<MeshCollider>().enabled = false;
			asteroidParts.Emit(30);
			Destroy(gameObject, 30);
		}
	}
}
