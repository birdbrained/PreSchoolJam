using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
	[SerializeField]
	private float spawnDelay;
	private int mitesCurrentlySpawned;
	[SerializeField]
	private GameObject miteObj;

	// Use this for initialization
	void Start() 
	{
		if (spawnDelay > 1.0f)
			spawnDelay = 1.0f;
		else if (spawnDelay < 0.1f)
			spawnDelay = 0.1f;
		StartCoroutine(SpawnMites());
	}
	
	// Update is called once per frame
	void Update() 
	{
		
	}

	private IEnumerator SpawnMites()
	{
		while (mitesCurrentlySpawned < GameManager.Instance.totalMites)
		{
			mitesCurrentlySpawned++;
			GameManager.Instance.mitesAlive++;
			Debug.Log("Spawning mite #" + mitesCurrentlySpawned.ToString());
			if (miteObj != null)
			{
				GameObject mite = (GameObject)Instantiate(miteObj, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
			}
			yield return new WaitForSeconds(2.0f - (spawnDelay * 2));
		}
	}
}
