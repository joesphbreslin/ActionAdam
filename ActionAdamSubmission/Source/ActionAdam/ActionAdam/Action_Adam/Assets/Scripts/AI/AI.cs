using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Seeker))]
public class AI : MonoBehaviour 
{
	public Transform target; // the AI's target

	// how many times each we will update our path
	public float updateRate = 2f;

	//caching
	private Seeker seeker;
	private Rigidbody2D myRigidBody2D;

	//The Calculated Path
	public Path path;

	//the Ai speed per Second
	public float speed = 300f;
	public ForceMode2D fMode;

	[HideInInspector]
	public bool pathIsEnded = false;

	public float nextWayPointDistance = 3;

	//the waypoint we are currently moving towords
	private int currentWayPoint = 0;

	public bool canMove;

	void Start()
	{
		seeker = GetComponent<Seeker>();
		myRigidBody2D = GetComponent<Rigidbody2D>();

		if(target == null)
		{
			Debug.LogError("No Player found PANIC");
			return;
		}

		seeker.StartPath(transform.position, target.position, OnPathComplete);

		StartCoroutine (UpdatePath());
	}

	IEnumerator UpdatePath() 
	{
		if(target == null)
		{
			//TODO: Insert a player search here
			yield return false;
		}

		seeker.StartPath(transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds(1f/updateRate);
		StartCoroutine(UpdatePath());
	}

	public void OnPathComplete(Path p)
	{
		Debug.Log("We got a path, did it have an error> " + p.error);
		if(!p.error)
		{
			path = p;
			currentWayPoint = 0; 
		}
	}

	void FixedUpdate()
	{
		if(target == null)
		{
			//TODO Insert a player search here
			return;
		}

		//TODO: Always look at player?

		if(path == null)
			return;

		if(currentWayPoint >= path.vectorPath.Count)
		{
			if(pathIsEnded)
				return;

			Debug.Log("End of path reached");
			pathIsEnded = true;
			return;
		}
		pathIsEnded = false;

		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;

		if(canMove == true)
		{
			//move the ai
			myRigidBody2D.AddForce(dir, fMode);
		}

		float dist = Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]);

		if(dist < nextWayPointDistance)
		{
			currentWayPoint++;
			return;
		}
	}
}
