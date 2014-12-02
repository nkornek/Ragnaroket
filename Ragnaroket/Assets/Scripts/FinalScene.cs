using UnityEngine;
using System.Collections;

public class FinalScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("RestartGame", 10);
	
	}
	
	void RestartGame()
	{
		Application.LoadLevelAsync (0);
	}
}
