using UnityEngine;
using System.Collections;

public class CannonControl : MonoBehaviour {
	public bool DebugControl;

	public Transform[] spotlights, cannons;
	public Transform crosshair;

	public GameObject laserFire;

	public Vector2 toCam;

	public int AmmoMax, AmmoCurrent;

	public GUITexture reloadText;

	public float focusTime, currentFocus, focusRange;

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
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(new Vector3 (crosshair.position.x * Screen.width, crosshair.position.y * Screen.height, 100));
		cannons [0].LookAt (ray.GetPoint (100));
		cannons [1].LookAt (ray.GetPoint (100));
		spotlights[0].LookAt(ray.GetPoint(100));
		spotlights[1].LookAt(ray.GetPoint(100));

		if (Physics.Raycast (ray, out hit)) 
		{
			if (hit.transform.tag == "Objective" & Vector3.Distance(hit.transform.position, gameObject.transform.position) <= focusRange)
			{
				currentFocus += Time.deltaTime;
				if (currentFocus >= focusTime)
				{
					currentFocus = focusTime;
					//put stuff to trigger spacevaettir;
				}
			}
			else
			{
				currentFocus = Mathf.Lerp(currentFocus, 0, 0.2f);
			}
		}



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
			gameObject.GetComponent<AudioSource>().Play ();
			Instantiate(laserFire, cannons[0].position, cannons[0].rotation);
			Instantiate(laserFire, cannons[1].position, cannons[1].rotation);
			AmmoCurrent --;
		}
	}
}
