using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public int DamageValue;

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag != "Player")
		{
			Destroy(gameObject);
		}
	}
}
