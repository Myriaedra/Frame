using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderWarp : MonoBehaviour {
	public Transform target;
	public float warpCooldown;
	public float offset;
	public Camera cam;
	public LayerMask groundMask;

	float coolDown;

	float camHalfHeight
	{
		get { return cam.orthographicSize - offset; }
	}

	float camHalfWidth
	{
		get { return cam.orthographicSize * cam.aspect - offset;}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (coolDown > 0) 
		{
			coolDown -= Time.deltaTime;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (coolDown <= 0) {
			Vector3 warpDestination = GetWarpDestination ();
			if (!Physics2D.OverlapCircle(warpDestination, 1f, groundMask))
			{
				target.position = warpDestination;
				coolDown = warpCooldown;
			}
		}
	}

	Vector3 GetWarpDestination()
	{
		Vector3 originPosition = target.position;
		Vector3 targetPosition = originPosition;

		if (originPosition.x < transform.position.x - camHalfWidth) { //Left
			targetPosition = new Vector3 (transform.position.x + camHalfWidth, targetPosition.y, originPosition.z);
		} 
		else if (originPosition.x > transform.position.x + camHalfWidth) //Right
		{
			targetPosition = new Vector3 (transform.position.x - camHalfWidth, targetPosition.y, originPosition.z);
		}

		if (originPosition.y < transform.position.y - camHalfHeight) { //Bottom
			targetPosition = new Vector3 (targetPosition.x, transform.position.y + camHalfHeight, originPosition.z);
		} 
		else if (originPosition.y + 2f > transform.position.y + camHalfHeight) //Up
		{
			targetPosition = new Vector3 (targetPosition.x, transform.position.y - camHalfHeight, originPosition.z);
		}

		return targetPosition;
	}
}
