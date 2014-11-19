using UnityEngine;
using System.Collections;

public class GUIResize : MonoBehaviour {
	public GUITexture cockpit;

	// Use this for initialization
	void Start () {
		cockpit.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
