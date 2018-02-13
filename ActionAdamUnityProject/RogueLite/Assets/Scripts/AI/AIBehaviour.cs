using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour 
{

	Animator animator;

	public enum type{friendly, enemy};
	public type aiType;

	public enum state{idle, roaming, following, attacking};
	public state aiState;

	// Use this for initialization
	void Start () 
	{
		animator = this.GetComponent<Animator>();
	}

	void Idle()
	{

	}

	void Attack()
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(aiState == state.idle)
		{
			Debug.Log("idle");
		}

		if(aiState == state.attacking)
		{
			Debug.Log("attacking");
		}
	}
}
