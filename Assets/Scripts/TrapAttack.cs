using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapAttack : MonoBehaviour 
{
	private Animator ani;

	public bool Attacking { get; set; }
	public bool CanKill { get; set; }

	// Use this for initialization
	void Start () 
	{
		CanKill = true;
		ani = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!Attacking)
		{
			if (other.gameObject.tag == "mite" && CanKill)
			{
				ani.SetTrigger("attack");
				CanKill = false;
				Destroy(other.gameObject);
				GameManager.Instance.mitesAlive--;
			}	
		}
	}
}
