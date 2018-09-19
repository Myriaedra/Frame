using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	Rigidbody2D rB;
	public float speed;


	void Start () 
	{
		rB = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () 
	{
		float horizontal = Input.GetAxis ("CamHorizontal");
		float vertical = Input.GetAxis ("CamVertical");

		rB.AddForce(new Vector2 (speed * horizontal, speed * vertical));
	}
}
