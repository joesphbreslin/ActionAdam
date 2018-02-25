using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arm : MonoBehaviour 
{
	public int rotationOffset = 90;
	SpriteRenderer rend;

	public bool player;

	public Transform target;

	public Vector3 startRotation;
	AIVision vision;
	// Use this for initialization
	void Start () 
	{
		rend = this.GetComponent<SpriteRenderer>();

		vision = transform.parent.GetComponentInChildren<AIVision>();

		startRotation = transform.rotation.eulerAngles;
	}

	void ArmMouseFollow()
	{
		Vector3 mousePos = Input.mousePosition;

		mousePos.z = 5.23f;

		Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;

		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}

	void ArmTargetFollow()
	{
		Vector3 direction = target.position - transform.position;
		float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
	}

	// Update is called once per frame
	void Update () 
	{
		//startRotation = transform.rotation.eulerAngles;

		if(target == null && player == false)
		{
			Debug.Log("their is no target assigned");
			return;
		}

		if(player)
		{
			ArmMouseFollow();
		}
		else
		{
			ArmTargetFollow();
		}

		if(!player)
		{
			if(vision.canSeePlayer == false)
			{
				transform.rotation = Quaternion.Euler(startRotation);
			}
		}
	}
}
