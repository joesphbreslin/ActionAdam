using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimation : MonoBehaviour {

    Rigidbody2D rb;
    Animator anim;
    int animState = 0;

	bool flag = false;
	AIBehaviour ai;
    SpriteRenderer rend;
    public Vector3 characterRotation;
    // Use this for initialization

    void Start () 
	{
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rend = this.GetComponent<SpriteRenderer>();
        ai = GetComponent<AIBehaviour>();

		characterRotation = transform.rotation.eulerAngles;
    }
	
    void VelocityAnimation()
    {
		int change = animState;

        float x = Mathf.Floor(Mathf.Abs(rb.velocity.x));
        float y = Mathf.Floor(Mathf.Abs(rb.velocity.y));

        if (x == 0 && y == 0)
        {
            animState = 0;
        }
        else if (x > y)
        {
            animState = 1;
            if (x > 1)
            {
                //rend.flipX = false;

				//transform.rotation = Quaternion.Euler(characterRotation.x, 180, characterRotation.z);
            }
            if (x < 1)
            {
				Debug.Log("Moving to the left");
                //rend.flipX = true;

				transform.rotation = Quaternion.Euler(characterRotation);
            }
        }
        else
        {
            if(rb.velocity.normalized.y > 0.1)
            {
                animState = 2;
            }
            else if (rb.velocity.normalized.y < 0.1)
            {
                animState = 3;
            }
        }

		if(change == animState)
		{
			flag = false;
		}
		else
		{
			flag = true;
		}

    }

	void Animate(int _animState, bool _flag)
	{
		if(_flag == true)
		{
			anim.SetInteger("Movement", _animState);
			_flag = false;
		}
		else
		{
			return;
		}
	}
	

	// Update is called once per frame
	void Update () 
	{ 
			VelocityAnimation();
			Animate(animState,flag); 

		if(this.tag == "Enemy")
		{
			if(rb.velocity.x > 5f)
			{
				transform.rotation = Quaternion.Euler(characterRotation.x, 180, characterRotation.z);
			}

			if(rb.velocity.x < 0f)
			{
				transform.rotation = Quaternion.Euler(characterRotation.x, characterRotation.y, characterRotation.z);
			}
		}

		if(this.tag == "Friendly")
		{
			if(rb.velocity.x > 5f)
			{
				transform.rotation = Quaternion.Euler(characterRotation.x, characterRotation.y, characterRotation.z);
			}

			if(rb.velocity.x < 0f)
			{
				transform.rotation = Quaternion.Euler(characterRotation.x, 180, characterRotation.z);
			}
		}
	}
}
