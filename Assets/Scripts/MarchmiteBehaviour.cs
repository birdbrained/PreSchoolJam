using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchmiteBehaviour : MonoBehaviour 
{
	private Rigidbody2D rb;
	private Collider2D coll;
	[SerializeField]
	protected float speed;
	protected bool isFacingRight = true;
	private bool isGrounded = false;
	//private Collider2D rightCollider;
	//private Collider2D leftCollider;

	public enum SpecialPower
	{
		NONE,
		JUMP,
		UNFURL
	};
	private SpecialPower currentPower = SpecialPower.NONE;
	public SpecialPower CurrentPower
	{
		get
		{
			return currentPower;
		}
		set
		{
			currentPower = value;
		}
	}

	// Use this for initialization
	void Start() 
	{
		//rightCollider = transform.Find("rightCollider").GetComponent<BoxCollider2D>();
		//leftCollider = transform.Find("leftCollider").GetComponent<BoxCollider2D>();
		rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
	}

	void ExecutePower(SpecialPower power)
	{
		//Debug.Log("Power: " + power);
		if (power == SpecialPower.JUMP)
		{
			//Debug.Log("Jumping");
			rb.AddForce(new Vector2(0, 500));
		}
	}
	
	// Update is called once per frame
	void Update() 
	{
		if (isGrounded) 
		{
			//transform.Translate(Vector3.forward * Time.deltaTime);
			if (isFacingRight)
				transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
			else
				transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
		}
		else 
		{
			//transform.Translate(Vector3.down * Time.deltaTime * (speed * 1.5f), Space.World);
		}
	}

	void CheckIfGrounded()
	{
		Vector2 below = transform.TransformDirection(Vector2.down);
		if (Physics.Raycast(transform.position, below, 0.1f))
		{
			isGrounded = true;
		} else
			isGrounded = false;
	}

	void FixedUpdate()
	{
		//CheckIfGrounded();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log("Other's tag: " + other.gameObject.tag);
		Collider2D otherCol = other.gameObject.GetComponent<Collider2D>();
		//if (other.gameObject.tag == "wall")
			//isFacingRight = !isFacingRight;
		//else if (other.gameObject.tag == "floor")
			//isGrounded = true;
		if (other.gameObject.tag == "mite")
		{
			Physics2D.IgnoreCollision(otherCol, coll);
		}
		else if (transform.position.y >= other.gameObject.transform.position.y + (otherCol.bounds.size.y / 2 - coll.bounds.size.y / 2) &&
			(transform.position.x + coll.bounds.size.x / 2 < other.gameObject.transform.position.x - otherCol.bounds.size.x / 2 || transform.position.x - coll.bounds.size.x / 2 > other.gameObject.transform.position.x + otherCol.bounds.size.x / 2))
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + coll.bounds.size.y / 2, transform.position.z);
		}
		else if (other.contacts.Length > 0)
		{
			ContactPoint2D c = other.contacts[0];
			if (Vector2.Dot(c.normal, Vector2.up) > 0.5)
			{
				isGrounded = true;
			}
			if (Mathf.Abs(Vector2.Dot(c.normal, Vector2.right)) > 0.5)
			{
				isFacingRight = !isFacingRight;
			}
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		/*if (other.gameObject.tag == "floor")
			isGrounded = false;
		else if (other.gameObject.tag == "wall")
			isGrounded = true;*/
	}
}
