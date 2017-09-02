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
	private int mitesSaved;				//int counter of mites that have reached the finish
	[SerializeField]
	private Text miteDescriptionText;	//text to show the status of a selected mite

	private int activePowerup = 1;

	// Use this for initialization
	void Start() 
	{
		if (mitesAliveText != null)
			mitesAliveText.text = "Alive: " + mitesAlive.ToString();
		if (mitesNeededText != null)
			mitesNeededText.text = "Needed: " + mitesNeeded.ToString();
		if (miteDescriptionText != null)
			miteDescriptionText.text = "whee";
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
						mite.ExecutePower(mite.CurrentPower);
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
		if (miteDescriptionText != null)
			miteDescriptionText.text = "whee";
		CheckMouseClick();
	}

	public void ChangeActivePowerup(int i)
	{
		activePowerup = i;
	}

}
