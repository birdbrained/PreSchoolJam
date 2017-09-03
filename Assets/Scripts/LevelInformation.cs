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

	[SerializeField]
	private Sprite[] rankingSprites;
	[SerializeField]
	private GameObject rankingObj;
	private Image myImage;

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
			myImage = rankingObj.GetComponent<Image>();
			if (quipText != null && quipText2 != null && rankingSprites.Length >= 5 && myImage != null)
			{
				if (GameManager.MitesSaved == GameManager.TotalMites)
				{
					quipText.text = "Got 'em all!!!";
					quipText2.text = "All of the mites are accounted for!";
					myImage.sprite = rankingSprites[0];

				} else if (GameManager.MitesSaved > GameManager.MitesNeeded)
				{
					quipText.text = "Awesome job!";
					quipText2.text = "It's alright if a few must go, but try to get more next time!";
					myImage.sprite = rankingSprites[1];
				} else if (GameManager.MitesSaved == GameManager.MitesNeeded)
				{
					quipText.text = "Just barely made it!";
					quipText2.text = "The mites mourn their losses, but you still pass!";
					myImage.sprite = rankingSprites[2];
				} else if (GameManager.MitesSaved == 0)
				{
					quipText.text = "ROCK BOTTOM.";
					quipText2.text = "...I hope for your sake that you threw that level!";
					myImage.sprite = rankingSprites[4];
				} else if (GameManager.MitesSaved < GameManager.MitesNeeded)
				{
					quipText.text = "Not quite enough...";
					quipText2.text = levelHint;
					myImage.sprite = rankingSprites[3];
				} else
				{
					quipText.text = "Wait...";
					quipText2.text = "This text should not appear. Let me know if it does.";
					myImage.sprite = rankingSprites[4];
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
