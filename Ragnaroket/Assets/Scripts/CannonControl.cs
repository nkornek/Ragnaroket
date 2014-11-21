using UnityEngine;
using System.Collections;

public class CannonControl : MonoBehaviour {
	public bool DebugControl;

	public ParticleSystem[] laser;
	public Transform crosshair;

	public Vector2 toCam;

	public int AmmoMax, AmmoCurrent;

	public GUITexture reloadText;

	void Start() {
		Reload ();
	}

	// Update is called once per frame
	void Update () {
		if (DebugControl)
		{
			//translate mouse position into screen coordinates for GUI
			crosshair.position = new Vector2 (Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
			if (Input.GetMouseButtonDown(0))
			{
				Fire();
			}
		}
		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Ray ray = Camera.main.ScreenPointToRay(new Vector3 (crosshair.position.x * Screen.width, crosshair.position.y * Screen.height, 100));
		laser[0].transform.LookAt(ray.GetPoint(100));
		laser[1].transform.LookAt(ray.GetPoint(100));

		//show reload text
		if (AmmoCurrent > 0)
		{
			reloadText.enabled = false;
		}
		else
		{
			reloadText.enabled = true;
		}
		
	}

	void Reload ()
	{
		AmmoCurrent = AmmoMax;
	}

	public void Fire()
	{
		//fire lasers
		if (crosshair.localPosition.x >= 0.5f || crosshair.localPosition.x <= -0.5f || crosshair.localPosition.y >= 0.5f || crosshair.localPosition.y <= -0.5f)
		{
			Reload();
		}
		else if (AmmoCurrent > 0)
		{
			laser[0].Emit(1);
			laser[1].Emit(1);
			AmmoCurrent --;
		}
	}
}
