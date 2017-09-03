using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInformation : MonoBehaviour 
{
	public string levelName;
	public string levelInfo;

	public int mitesNeeded;
	public int totalMites;
	public float timer;

	// Use this for initialization
	void Start () 
	{
		GameManager.LevelName = levelName;
		GameManager.LevelInfo = levelInfo;
		GameManager.MitesNeeded = mitesNeeded;
		GameManager.TotalMites = totalMites;
		GameManager.TotalTime = timer;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
