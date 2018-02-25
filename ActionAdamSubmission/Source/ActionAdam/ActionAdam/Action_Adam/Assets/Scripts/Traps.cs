using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class Traps : MonoBehaviour
{
    StudioEventEmitter SE;
    public SpriteRenderer rend;
    public Sprite trapOff;

    Console console;

	public float damageRate;
	public float startDamageRate;
	public int damage;

	// Use this for initialization
	void Start () 
	{
        SE = GetComponent<StudioEventEmitter>();
		startDamageRate = damageRate;
	}
		
    void Awake()
    {
        console = GameObject.FindWithTag("Console").GetComponent<Console>();

        rend = this.GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && console.on_offSwitch == true)
        {
            SE.Play();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && console.on_offSwitch == true)
        {
            SE.Stop();
        }
    }


        void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "Player" && console.on_offSwitch == true)
		{
			other.attachedRigidbody.AddForce(Vector2.up * 1);

			damageRate -= 1 * Time.fixedDeltaTime;

			Health otherHealth = other.GetComponent<Health>();

			if(damageRate <= 0)
			{
				otherHealth.health -= damage;
				damageRate = startDamageRate;
			}
		}
	}

    // Update is called once per frame
    void FixedUpdate ()
    {
        if(console.on_offSwitch == false)
        {
            rend.sprite = trapOff;
            SE.Stop();
        }
	}
}
