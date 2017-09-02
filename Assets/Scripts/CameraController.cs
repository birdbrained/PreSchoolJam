using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	[SerializeField]
	private float movementSpeed;
	[SerializeField]
	private float maxY, minY, maxX, minX;

	// Use this for initialization
	void Start() 
	{
		
	}

	void HandleInput()
	{
		if (Input.GetKey(KeyCode.W))
			transform.Translate(new Vector2(0, movementSpeed * Time.deltaTime));
		if (Input.GetKey(KeyCode.A))
			transform.Translate(new Vector2(-movementSpeed * Time.deltaTime, 0));
		if (Input.GetKey(KeyCode.S))
			transform.Translate(new Vector2(0, -movementSpeed * Time.deltaTime));
		if (Input.GetKey(KeyCode.D))
			transform.Translate(new Vector2(movementSpeed * Time.deltaTime, 0));
	}

	// Update is called once per frame
	void Update() 
	{
		HandleInput();
	}

	void LateUpdate()
	{
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
	}
		
}
