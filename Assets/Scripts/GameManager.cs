using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	private static GameManager instance;
	public static GameManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<GameManager>();
			}
			return instance;
		}
	}

	[SerializeField]
	private Text mitesAliveText;		//text to show how many mites are currently alive
	public int mitesAlive;				//int counter of how many alive mites
	[SerializeField]
	private Text mitesNeededText;		//text to show the min. mites needed to clear the level
	public int mitesNeeded;				//int number of mites needed to clear the level
	public int totalMites;				//the total number of mites that will spawn in a level
	[SerializeField]
	private Text mitesSavedText;
	private static int mitesSaved;		//int counter of mites that have reached the finish
	public static int MitesSaved
	{
		get
		{
			return mitesSaved;
		}
		set
		{
			mitesSaved = value;
		}
	}
	[SerializeField]
	private Text miteDescriptionText;	//text to show the status of a selected mite
	public Text MiteDescriptionText
	{
		get
		{
			return miteDescriptionText;
		}
		set
		{
			miteDescriptionText = value;
		}
	}

	public int activePowerup = 0;
	public bool powerupSelected = false;
	public int[] powerupInventory;

	// Use this for initialization
	void Start() 
	{
		if (mitesAliveText != null)
			mitesAliveText.text = "Alive: " + mitesAlive.ToString();
		if (mitesNeededText != null)
			mitesNeededText.text = "Needed: " + mitesNeeded.ToString();
		if (miteDescriptionText != null)
			miteDescriptionText.text = "whee";
		if (mitesSavedText != null)
			mitesSavedText.text = "Saved: " + mitesSaved.ToString();
	}

	void CheckMouseClick()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//Debug.Log(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
			if (hit)
			{
				//Debug.Log(hit.collider.gameObject.name);
				if (hit.collider.gameObject.tag == "mite")
				{
					MarchmiteBehaviour mite = hit.collider.gameObject.GetComponent<MarchmiteBehaviour>();
					if (mite != null)
					{
						//If the mite has a power, execute it
						//If the mite does not have a power but one is currently selected, give the mite the power
						miteDescriptionText.text = mite.CurrentPower.ToString();
						if (mite.CurrentPower != MarchmiteBehaviour.SpecialPower.NONE)
						{
							mite.ExecutePower(mite.CurrentPower);
						} 
						else if (powerupSelected)
						{
							mite.CurrentPower = (MarchmiteBehaviour.SpecialPower)activePowerup;
						}
					}
				}
			}
		}
	}

	// Update is called once per frame
	void Update() 
	{
		if (mitesAliveText != null)
			mitesAliveText.text = "Alive: " + mitesAlive.ToString();
		if (mitesSavedText != null)
			mitesSavedText.text = "Saved: " + mitesSaved.ToString();
		CheckMouseClick();
	}

	public void ChangeActivePowerup(int i)
	{
		if (activePowerup == i)
		{
			powerupSelected = false;
			activePowerup = 0;
		}
		else if (powerupSelected && activePowerup != i)
		{
			activePowerup = i;
		}
		else
		{
			activePowerup = i;
			powerupSelected = true;
		}
	}

}
