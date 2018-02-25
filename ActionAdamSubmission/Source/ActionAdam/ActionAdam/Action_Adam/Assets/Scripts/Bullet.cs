using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public string characterTag;
	public string characterName;
	public int DamageValue;

	void Start()
	{
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == this.tag)
		{
			Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}

		if(other.gameObject.tag != characterTag && other.gameObject.tag != this.gameObject.tag)
		{
			Destroy(gameObject);
		}

		/*if(other.gameObject.tag != "Player")
		{
			Destroy(gameObject);
		}*/
	}
}
