using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchmiteBehaviour : MonoBehaviour 
{
	[SerializeField]
	protected float speed;
	protected bool isFacingRight = true;
	private bool isGrounded = false;
	//private Collider2D rightCollider;
	//private Collider2D leftCollider;


	// Use this for initialization
	void Start () 
	{
		//rightCollider = transform.Find("rightCollider").GetComponent<BoxCollider2D>();
		//leftCollider = transform.Find("leftCollider").GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isGrounded) 
		{
			//transform.Translate(Vector3.forward * Time.deltaTime);
			if (isFacingRight)
				transform.Translate (Vector3.right * Time.deltaTime * speed, Space.World);
			else
				transform.Translate (Vector3.left * Time.deltaTime * speed, Space.World);
		}
		else 
		{
			transform.Translate (Vector3.down * Time.deltaTime * (speed * 1.5f), Space.World);
		}
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "wall")
			isFacingRight = !isFacingRight;
		else if (other.gameObject.tag == "floor")
			isGrounded = true;
	}

	void OnCollisionExit2D (Collision2D other)
	{
		if (other.gameObject.tag == "floor")
			isGrounded = false;
	}
}
