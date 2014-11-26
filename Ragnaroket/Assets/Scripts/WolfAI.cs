using UnityEngine;
using System.Collections;

public class WolfAI : MonoBehaviour {

	public Transform playerShip;
	public float wolfSpeed, detectRange;
	public bool lockedOn, stunned, invuln;
	public float lifespan, maxLifespan, knockBack, stunTime, invulnTime;
	public Material[] eyes;

	// Use this for initialization
	void Start () {
		playerShip = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {

		if (Vector3.Distance (transform.position, playerShip.position) < detectRange)
		{
			lockedOn = true;
		}
		else
		{
			lockedOn = false;
		}

		if (lockedOn & !stunned)
		{
			lifespan = maxLifespan;
			Quaternion oldRot = transform.rotation;
			transform.LookAt (playerShip);
			Quaternion newRot = transform.rotation;
			transform.rotation = Quaternion.Lerp(oldRot, newRot, 0.1f);
			rigidbody.AddRelativeForce(Vector3.forward * wolfSpeed * Time.deltaTime);
		}
		else
		{
			lifespan -= Time.deltaTime;
			if (lifespan <= 0)
			{
				Destroy(gameObject);
			}
		}
	}

	void OnParticleCollision(GameObject other) 
	{
		if (!stunned & !invuln)
		{
			stunned = true;
			Invoke ("UnStun", stunTime);
			//apply force
			rigidbody.AddRelativeForce(Vector3.Normalize(transform.position - playerShip.position) * knockBack * Time.deltaTime);
			rigidbody.angularVelocity = Random.insideUnitSphere * knockBack;
		}
	}

	void UnStun ()
	{
		CancelInvoke ("UnStun");
		stunned = false;
		invuln = true;
		Invoke ("EndInvuln", invulnTime);
	}

	void EndInvuln ()
	{
		CancelInvoke ("EndInvuln");
		invuln = false;
	}
}
