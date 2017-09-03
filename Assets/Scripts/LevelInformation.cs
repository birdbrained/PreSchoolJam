using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInformation : MonoBehaviour 
{
	public bool repopulateInformation;

	public string levelName;
	public string levelInfo;
	public string levelHint;

	public int mitesNeeded;
	public int totalMites;
	public float timer;

	[SerializeField]
	private Text quipText;
	[SerializeField]
	private Text quipText2;

	// Use this for initialization
	void Start () 
	{
		if (repopulateInformation)
		{
			GameManager.LevelName = levelName;
			GameManager.LevelInfo = levelInfo;
			GameManager.MitesNeeded = mitesNeeded;
			GameManager.TotalMites = totalMites;
			GameManager.TotalTime = timer;
		}
		else
		{
			if (quipText != null && quipText2 != null)
			{
				if (GameManager.MitesSaved == GameManager.TotalMites)
				{
					quipText.text = "Got 'em all!!!";
					quipText2.text = "All of the mites are accounted for!";
				} else if (GameManager.MitesSaved > GameManager.MitesNeeded)
				{
					quipText.text = "Awesome job!";
					quipText2.text = "It's alright if a few must go, but try to get more next time!";
				} else if (GameManager.MitesSaved == GameManager.MitesNeeded)
				{
					quipText.text = "Just barely made it!";
					quipText2.text = "The mites mourn their losses, but you still pass!";
				} else if (GameManager.MitesSaved == 0)
				{
					quipText.text = "ROCK BOTTOM.";
					quipText2.text = "...I hope for your sake that you threw that level!";
				} else if (GameManager.MitesSaved < GameManager.MitesNeeded)
				{
					quipText.text = "Not quite enough...";
					quipText2.text = levelHint;
				} else
				{
					quipText.text = "Wait...";
					quipText2.text = "This text should not appear. Let me know if it does.";
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
