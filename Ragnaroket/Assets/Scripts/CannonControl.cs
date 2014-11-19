﻿using UnityEngine;
using System.Collections;

public class CannonControl : MonoBehaviour {
	public ParticleSystem[] laser;
	public Transform crosshair;

	public Vector2 toCam;

	// Update is called once per frame
	void Update () {
		//translate mouse position into screen coordinates for GUI
		crosshair.position = new Vector2 (Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		laser[0].transform.LookAt(ray.GetPoint(100));
		laser[1].transform.LookAt(ray.GetPoint(100));
			//fire lasers
			if (Input.GetMouseButtonDown(0))
			{
				laser[0].Emit(1);
				laser[1].Emit(1);
			}
	}
}
