using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class fader : MonoBehaviour {
	public bool fadeIn;
	float alpha;

	// Use this for initialization
	void Start () {
		fadeIn = true;
		alpha = 1;
	}
	
	// Update is called once per frame
	void Update () {

		if (fadeIn & alpha > 0)
		{
			alpha -= 0.05f;
		}
		if (!fadeIn & alpha < 1)
		{
			alpha += 0.05f;
		}
		gameObject.GetComponent<Image> ().color = new Color (0, 0, 0, alpha);

		if (!fadeIn & alpha == 1)
		{
			Application.LoadLevel(2);
		}
	}
}
