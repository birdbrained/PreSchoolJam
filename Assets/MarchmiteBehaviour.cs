using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchmiteBehaviour : MonoBehaviour 
{
	[SerializeField]
	protected float speed;
	public bool isFacingRight = true;
	private Collider2D rightCollider;
	private Collider2D leftCollider;


	// Use this for initialization
	void Start () 
	{
		rightCollider = transform.Find("rightCollider").GetComponent<BoxCollider2D>();
		leftCollider = transform.Find("leftCollider").GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(Vector3.forward * Time.deltaTime);
		if (isFacingRight)
			transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
		else
			transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "wall")
			isFacingRight = !isFacingRight;
	}
}
