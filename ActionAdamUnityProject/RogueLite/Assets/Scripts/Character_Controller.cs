using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Controller : MonoBehaviour 
{
	public SpriteRenderer rend;
	public Rigidbody2D myRb;

	public Sprite upSprite;
	public Sprite downSprite;
	public Sprite leftSprite;
	public Sprite rightSprite;

	public float movememtSpeed;

	Vector3 characterRotation;

	// Use this for initialization
	void Start () 
	{
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
		if(Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
		{
			transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * movememtSpeed * Time.deltaTime, 0f, 0f));

			if(Input.GetAxisRaw("Horizontal") > 0.5f)
			{
				rend.flipX = false;
			}
			else if(Input.GetAxisRaw("Horizontal") < -0.5f)
			{
				rend.flipX = true;
			}
		}

		if(Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
		{
			transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * movememtSpeed * Time.deltaTime, 0f));

			if(Input.GetAxisRaw("Vertical") > 0.5f)
			{
				rend.sprite = upSprite;
			}

			if(Input.GetAxisRaw("Vertical") < -0.5f)
			{
				rend.sprite = downSprite;
			}
		}
	}
}
