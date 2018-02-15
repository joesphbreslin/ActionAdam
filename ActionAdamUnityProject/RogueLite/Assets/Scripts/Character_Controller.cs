using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Controller : MonoBehaviour 
{
	SpriteRenderer rend;
    SpriteRenderer[] sp;
	public Rigidbody2D myRb;
    Animator anim;
	public Sprite upSprite;
	public Sprite downSprite;
	public Sprite leftSprite;
	public Sprite rightSprite;
    public float inputThreshold = 0.1f;
	public float movememtSpeed;
    public bool MainCharacter = true;
	Vector3 characterRotation;

	// Use this for initialization
	void Start () 
	{
        sp = GetComponentsInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();  // Integer 0 = idle, 1 = walking, 2 = Up, 3 = down
		characterRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);

		rend = this.GetComponent<SpriteRenderer>();
		myRb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Movement();
	}

	void Movement()
	{
		if(Input.GetAxisRaw("Horizontal") > inputThreshold || Input.GetAxisRaw("Horizontal") < -inputThreshold)
        {
            if(this.gameObject.name != "ActionRichie") { 
            foreach (SpriteRenderer spriteRend in sp)
            {

                spriteRend.sortingOrder = 2;

            }
            rend.sortingOrder = 1;
                }
            if (MainCharacter)
            {
                anim.SetInteger("Movement", 1);
            }
            myRb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movememtSpeed * Time.deltaTime, 0f);
          

			if(Input.GetAxisRaw("Horizontal") > inputThreshold)
			{
				rend.flipX = false;
			}
			else if(Input.GetAxisRaw("Horizontal") < -inputThreshold)
			{
				rend.flipX = true;
			}
		}else

		if(Input.GetAxisRaw("Vertical") > inputThreshold || Input.GetAxisRaw("Vertical") < -inputThreshold)
		{

            myRb.velocity = new Vector2(0f,Input.GetAxisRaw("Vertical") * movememtSpeed * Time.deltaTime);

            if (Input.GetAxisRaw("Vertical") > inputThreshold)
            {
               
                foreach (SpriteRenderer spriteRend in sp)
                {
                    if (this.gameObject.name != "ActionRichie")
                    {
                        spriteRend.sortingOrder = 0;
                    }
                }
                if (this.gameObject.name != "ActionRichie")
                {
                    rend.sortingOrder = 1;
                }

                if (MainCharacter)
                {
                    anim.SetInteger("Movement", 2);
                }
                }
                else
                {
                    foreach (SpriteRenderer spriteRend in sp)
                    {
                    if (this.gameObject.name != "ActionRichie")
                    {
                        spriteRend.sortingOrder = 2;
                    }

                    }
                if (this.gameObject.name != "ActionRichie")
                {
                    rend.sortingOrder = 1;
                }
                }
           
            

			if(Input.GetAxisRaw("Vertical") < -inputThreshold)
			{
                if (MainCharacter)
                {
                    anim.SetInteger("Movement", 3);
                }
            }
		}
        else if(Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
        {
            myRb.velocity = Vector2.zero;
            if (MainCharacter)
            {
                anim.SetInteger("Movement", 0);
            }
        }


    }

 
}
