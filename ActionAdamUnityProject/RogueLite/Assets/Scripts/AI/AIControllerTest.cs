using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIControllerTest : MonoBehaviour 
{
	public enum Type {Friendly, Enemy};
	public Type aiType;

	public enum state{idle, roaming, chasing}
	public state aiState;

	public float visionRadius;
	[Range(0, 360)]
	public float visionAngle;

	public bool canSeePlayer;

	public LayerMask playerMask;
	public LayerMask obstructionMask;

	public List<Transform> playerInView = new List<Transform>();

	public bool debug;

	Arm arm;
	Gun gun;
	// Use this for initialization
	void Start () 
	{
		arm = this.transform.parent.GetComponentInChildren<Arm>();
		gun = this.transform.parent.GetComponentInChildren<Gun>();
	}

	void FieldOfView()
	{
		canSeePlayer = false;
		playerInView.Clear();
		Collider2D[] playerInTargetRadius = Physics2D.OverlapCircleAll(transform.position, visionRadius, playerMask);

		for(int i = 0; i < playerInTargetRadius.Length; i ++)
		{
			Transform player = playerInTargetRadius[i].transform;
			Vector3 directionToTarget = (player.position - transform.position).normalized;

			if(Vector2.Angle(-transform.right, directionToTarget) < visionAngle / 2)
			{
				float distanceToTarget = Vector3.Distance(transform.position, player.position);

				if(!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
				{
					playerInView.Add(player);

					canSeePlayer = true;

					arm.target = playerInView[0].transform;

					gun.AIFire();
				}
				else
				{
					canSeePlayer = false;
				}
			}
		}
	}


	public Vector3 directOfAngle(float angleConvertToDegrees, bool publicAngle)
	{
		if(!publicAngle)
		{
			angleConvertToDegrees += transform.eulerAngles.y;
		}

		return new Vector3(Mathf.Cos(angleConvertToDegrees * Mathf.Deg2Rad), Mathf.Sin(angleConvertToDegrees * Mathf.Deg2Rad), 0);
	}

	void OnDrawGizmos()
	{
		if(debug == true)
		{
			Vector3 viewAngleLeft;
			Vector3 viewAngleRight;

			viewAngleLeft = directOfAngle(-visionAngle / 2, false);
			viewAngleRight = directOfAngle(visionAngle / 2, false);

			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, visionRadius);

			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, transform.position + -viewAngleLeft * visionRadius);
			Gizmos.DrawLine(transform.position, transform.position + -viewAngleRight * visionRadius);
		}

		foreach(Transform player in playerInView)
		{
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine(transform.position, player.position);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		FieldOfView();
	}
		
}
