using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Controller : MonoBehaviour 
{
	public SpriteRenderer rend;
	public Rigidbody2D myRb;
	public Animator anim;


    public float inputThreshold = 0.1f;

	public float movememtSpeed;

	Vector3 characterRotation;

	// Use this for initialization
	void Start () 
	{
		characterRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        anim = GetComponent<Animator>();
        rend = this.GetComponent<SpriteRenderer>();
		myRb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Move();
	}
		
    private void Move()
    {
        if (Input.GetAxisRaw("Horizontal") > inputThreshold || Input.GetAxisRaw("Horizontal") < -inputThreshold)
        {
            anim.SetInteger("Movement", 1);
 
            myRb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movememtSpeed * Time.deltaTime, 0f);

            if (Input.GetAxisRaw("Horizontal") > inputThreshold)
            {
                rend.flipX = false;
            }
            else if (Input.GetAxisRaw("Horizontal") < -inputThreshold)
            {
                rend.flipX = true;
            }
        }
        else

        if (Input.GetAxisRaw("Vertical") > inputThreshold || Input.GetAxisRaw("Vertical") < -inputThreshold)
        {

            myRb.velocity = new Vector2(0f, Input.GetAxisRaw("Vertical") * movememtSpeed * Time.deltaTime);

            if (Input.GetAxisRaw("Vertical") > inputThreshold)
            {
            anim.SetInteger("Movement", 2);    
            }
            if (Input.GetAxisRaw("Vertical") < -inputThreshold)
            {
            anim.SetInteger("Movement", 3);
            }
        }
        else if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
        {
            myRb.velocity = Vector2.zero;
            anim.SetInteger("Movement", 0);
        }


    }
}

