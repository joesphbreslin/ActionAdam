using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AIBehaviour : MonoBehaviour 
{

	Animator animator;

	public enum type{friendly, enemy};
	public type aiType;

	public enum state{idle, roaming, following, attacking};
	public state aiState;

    bool attack = false;

	public float idleTime;
	private float startIdleTime;

	public float patrolTime;
	private float startPatrolTime;

	public bool decidedTarget;
	public float distanceFromTarget;
    StudioEventEmitter SE;
	public string previousState;

	Gun gun;
	AIVision aiVision;
	AI ai;
	LevelGenerator levelGenerator;

	// Use this for initialization
	void Start () 
	{
        SE = GetComponent<StudioEventEmitter>();
        animator = this.GetComponent<Animator>();
		gun = this.transform.GetComponentInChildren<Gun>();
		aiVision = this.transform.GetComponentInChildren<AIVision>();
		ai = this.GetComponent<AI>();
		levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();

		ai.target = GameObject.FindWithTag("Player").transform;

		startIdleTime = idleTime;

		startPatrolTime = patrolTime;

		if(gameObject.tag == "Enemy")
		{
			Decision();
		}
	}
		
	void FriendlyAttack()
	{
		ai.canMove = false;
		gun.AIFire();
	}
	void FriendlyIdle()
	{
		previousState = aiState.ToString();

		ai.canMove = false;
		if(Vector3.Distance(transform.position, ai.target.position) > distanceFromTarget && aiVision.canSeePlayer == false)
		{
			aiState = state.following;
		}
	}
	void Follow()
	{
		previousState = aiState.ToString();

		ai.canMove = true;
		ai.target = GameObject.FindWithTag("Player").transform;

		if(Vector3.Distance(transform.position, ai.target.position) <= distanceFromTarget && aiVision.canSeePlayer == false)
		{
			aiState = state.idle;
		}
	}

	void EnemyIdle()
	{
		if(aiType == type.enemy)
		{
		if(aiState == state.idle)
		{
			previousState = aiState.ToString();

			ai.canMove = false;
			ai.target = this.transform;

			animator.SetBool("Attack", false);
			animator.SetInteger("Movement", 0);

			idleTime -= 1 * Time.fixedDeltaTime;

			if(idleTime <= 0)
			{
				Decision();
			}
		}
		}
	}

	void EnemyAttack()
	{
		ai.canMove = false;
		animator.SetBool("Attack", true);
		gun.AIFire();
	}

	void EnemyPatrol()
	{
		if(aiType == type.enemy)
		{
			if(aiState == state.roaming)
			{
				previousState = aiState.ToString();

				ai.canMove = true;

				animator.SetBool("Attack", false);

				if(decidedTarget == false)
				{
					int randomTile;
					randomTile = Random.Range(0, levelGenerator.createdTiles.Count);
					this.GetComponent<AI>().target = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().createdTileGameObjects[randomTile].transform;
					decidedTarget = true;
				}

				if(Vector3.Distance(this.transform.position, ai.target.position) <= distanceFromTarget)
				{
					ai.canMove = false;
					decidedTarget = false;
					Decision();
				}
			}
		}
	}

	public void Decision()
	{
		int randomState = Random.Range(0, 2);

		if(randomState == 0)
		{
			idleTime = startIdleTime;
			aiState = state.idle;

		}
		if(randomState == 1)
		{
			aiState = state.roaming;
		}
	}

    void PlayAudio()
    {
        if (!SE.IsPlaying()) { SE.Play(); }
        
    }

    void StopAudio()
    {
        if (SE.IsPlaying()) { SE.Stop(); }
        
    }
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(aiType == type.enemy)
		{
			if(aiState == state.idle)
			{
                StopAudio();

                EnemyIdle();
			}

			if(aiState == state.roaming)
			{
                

                EnemyPatrol();
			}
			
			if(aiState == state.attacking)
			{
                PlayAudio();
                EnemyAttack();
			}
	
	
			if(aiState == state.roaming || aiState == state.idle)
			{
				previousState = aiState.ToString();
	
				if(aiVision.canSeePlayer == true)
				{
					aiState = state.attacking;
				}
			}
			if(aiVision.canSeePlayer == false)
			{
				aiState = (state) System.Enum.Parse(typeof(state), previousState);
			}
		}

		if(aiType == type.friendly)
		{
			if(aiState == state.idle)
			{
				FriendlyIdle();
			}
			if(aiState == state.following)
			{
				Follow();
			}

			if(aiVision.canSeePlayer == true)
			{
				aiState = state.attacking;
			}
			if(aiVision.canSeePlayer == false && aiState == state.attacking)
			{
				aiState = (state) System.Enum.Parse(typeof(state), previousState);
			}

			if(aiState == state.attacking)
			{
				FriendlyAttack();
			}
		}
	}
}
