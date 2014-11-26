using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {
	public int HP;
	public ParticleSystem asteroidParts;

	void OnParticleCollision(GameObject other) 
	{
		print ("test");
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
