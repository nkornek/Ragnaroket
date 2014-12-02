using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LockOnIndicator : MonoBehaviour {
	public Image indicator;
	public RectTransform indicPos;
	public CannonControl cannonScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		indicPos.position = Vector2.Lerp (indicPos.position, new Vector2 (cannonScript.crosshair.position.x * Screen.width, cannonScript.crosshair.position.y * Screen.height), 0.9f);
		indicator.fillAmount = cannonScript.currentFocus / cannonScript.focusTime;
	
	}
}
