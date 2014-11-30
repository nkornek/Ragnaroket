using UnityEngine;
using System.Collections;

public class ObjectiveReached : MonoBehaviour {

	public bool triggered, visible;
	public ObjectiveList objectives;
	public Transform playerShip;
	public float TriggerRange;

	public TextAndPortraits textStuff;

	public int showTextNum;

	void Start()
	{
		gameObject.GetComponentInChildren<MeshRenderer> ().enabled = false;
		gameObject.GetComponent<CapsuleCollider> ().enabled = false;
		gameObject.GetComponent<CapsuleCollider> ().enabled = false;
		foreach(MeshRenderer m in gameObject.GetComponentsInChildren<MeshRenderer>())
		{
			m.enabled = false;
		}
		foreach(ParticleSystem p in gameObject.GetComponentsInChildren<ParticleSystem>())
		{
			p.enableEmission = false;
		}
	}

	public void ShowObjective()
	{
		gameObject.GetComponentInChildren<MeshRenderer> ().enabled = true;
		gameObject.GetComponent<CapsuleCollider> ().enabled = true;
		gameObject.GetComponent<CapsuleCollider> ().enabled = true;
		foreach(MeshRenderer m in gameObject.GetComponentsInChildren<MeshRenderer>())
		{
			m.enabled = true;
		}
		foreach(ParticleSystem p in gameObject.GetComponentsInChildren<ParticleSystem>())
		{
			p.enableEmission = true;
		}
	}

	void Update()
	{
		if (textStuff.whoTalks[textStuff.whichText] == 2 & triggered)
		{
			gameObject.GetComponentInChildren<Animator>().SetBool("Talking", true);
		}
		else
		{
			gameObject.GetComponentInChildren<Animator>().SetBool("Talking", false);
		}
	}

	public void trigger()
	{
		triggered = true;
		gameObject.GetComponentInChildren<Animator>().SetTrigger("Appear");
		textStuff.whichText = showTextNum;
		textStuff.ShowText ();
	}

	void GoToNextObjective()
	{
		CancelInvoke ("GoToNextObjective");
		objectives.NewObjective ();
	}
}
