using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextAndPortraits : MonoBehaviour {

	public Sprite[] portraits;
	public string[] whatIsSaid;
	public int[] whoTalks;
	/*
	 * 0 = girl
	 * 1 = viking
	 * 2 = spacevaettir
	 * 3 = othergirl
	 */

	public float[] showTextTime;
	public bool textVisible;
	public Image showPortrait, border;
	public Text UItext;
	public int whichText;

	public MeshRenderer arrow;
	public fader faderScript;
	public Animator tutorial;


	// Use this for initialization
	void Start () {
		ShowText();

	
	}
	
	// Update is called once per frame
	void Update () {
		if (textVisible)
		{
			arrow.enabled = false;
		}
		else
		{
			arrow.enabled = true;
		}

	}

	public void ShowText ()
	{
		textVisible = true;
		showPortrait.enabled = true;
		UItext.enabled = true;
		border.enabled = true;
		showPortrait.sprite = portraits [whoTalks[whichText]];
		UItext.text = whatIsSaid [whichText];
		Invoke("NextText", showTextTime[whichText]);
	}

	public void NextText()
	{
		whichText ++;
		if (whatIsSaid[whichText] == string.Empty)
		{
			HideText();
		}
		else
		{
			ShowText();
		}
		//tutorial
		if (whichText == 6)
		{
			tutorial.SetTrigger("Show animations");
		}
		//end
		if (whichText == 90)
		{
			faderScript.fadeIn = false;
		}
	}

	public void HideText ()
	{
		textVisible = false;
		showPortrait.enabled = false;
		UItext.enabled = false;
		border.enabled = false;
	}
}
