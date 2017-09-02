using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchmiteBehaviour : MonoBehaviour 
{
	private Rigidbody2D rb;
	private Collider2D coll;
	private SpriteRenderer rend;
	[SerializeField]
	protected float speed;
	protected bool isFacingRight = true;
	private bool isGrounded = false;
	//private Collider2D rightCollider;
	//private Collider2D leftCollider;
	[SerializeField]
	private GameObject unfurlPlatformObj;

	public enum SpecialPower
	{
		NONE = 0,
		JUMP = 1,
		UNFURL = 2
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
	private bool usingJump = false;

	// Use this for initialization
	void Start() 
	{
		//rightCollider = transform.Find("rightCollider").GetComponent<BoxCollider2D>();
		//leftCollider = transform.Find("leftCollider").GetComponent<BoxCollider2D>();
		rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
		rend = GetComponent<SpriteRenderer>();
	}

	//Execute the power within
	public void ExecutePower(SpecialPower power)
	{
		//Debug.Log("Power: " + power);
		switch (power)
		{
		case SpecialPower.JUMP:
			//Debug.Log("Jumping");
			//***TO DO*** Make all mites in radius jump
			Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 3.0f);
			foreach (Collider2D hit in hitColliders)
			{
				if (hit.gameObject.tag == "mite")
				{
					MarchmiteBehaviour mite = hit.gameObject.GetComponent<MarchmiteBehaviour>();
					if (mite.isGrounded)
					{
						mite.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
						//hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
						mite.usingJump = true;
					}
				}
			}
			break;
		case SpecialPower.UNFURL:
			Debug.Log("Unfurling");
			GameObject platform = (GameObject)Instantiate(unfurlPlatformObj, new Vector3(isFacingRight ? transform.position.x + 3 : transform.position.x - 3, transform.position.y - coll.bounds.size.y / 2), Quaternion.identity);
			GameManager.Instance.mitesAlive--;
			Destroy(gameObject);
			break;
		default:
			Debug.Log("No power!");
			break;
		}
	}

	//Dirty way of changing the mite's color. Probably should delete this later.
	void ColorManagement()
	{
		switch (currentPower)
		{
		case SpecialPower.JUMP:
			rend.color = Color.yellow;
			break;
		case SpecialPower.UNFURL:
			rend.color = Color.magenta;
			break;
		default:
			rend.color = Color.white;
			break;
		}
	}
	
	// Update is called once per frame
	void Update() 
	{
		ColorManagement();
		if (isGrounded || usingJump) 
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

	void FixedUpdate()
	{
		if (rb.velocity.y < 0.0f)
		{
			isGrounded = false;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		//Debug.Log("Other's tag: " + other.gameObject.tag);
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
				usingJump = false;
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

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "goal")
		{
			GameManager.MitesSaved++;
			GameManager.Instance.mitesAlive--;
			Destroy(gameObject);
		}
	}
}
