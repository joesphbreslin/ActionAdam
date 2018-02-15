using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AIBehaviour : MonoBehaviour 
{
    SpriteRenderer spriteRenderer;
    Animator animator;

	public enum type{friendly, enemy};
	public type aiType;

	public enum state{idle, roaming, following, attacking};
	public state aiState;

	// Use this for initialization
	void Start () 
	{
        spriteRenderer = this.GetComponent<SpriteRenderer>();
		animator = this.GetComponent<Animator>();
	}

	void Idle()
	{
        animator.SetInteger("Movement", 0);

	}

	void Attack()
	{

        animator.SetInteger("Movement", 2);
    }

    void Walk()
    {
        animator.SetInteger("Movement", 1);
    }
	
	// Update is called once per frame
	void Update () 
	{

		if(aiState == state.idle)
		{
		//	Debug.Log("idle");
		}

		if(aiState == state.attacking)
		{
           
            Debug.Log("attacking");

		}
	}
}
