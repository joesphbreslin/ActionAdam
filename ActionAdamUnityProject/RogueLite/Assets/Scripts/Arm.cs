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

	Vector3 startRotation;
	// Use this for initialization
	void Start () 
	{
		rend = this.GetComponent<SpriteRenderer>();

		startRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
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
		if(player)
		{
			ArmMouseFollow();
		}
		else
		{
			ArmTargetFollow();
		}

		if(target == null)
		{
			Debug.Log("their is no target assigned");
		}

		if(this.transform.parent.GetComponentInChildren<AIControllerTest>().canSeePlayer == false && player == false)
		{
			target = null;

			transform.rotation = Quaternion.Euler(startRotation);
		}
	}
}
