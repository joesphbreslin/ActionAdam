using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour 
{
	public float startHealth;
	public float health;

	public TextMeshProUGUI healthText;
	public Image healthBar;

	public float backValue;

	public int XP;

	// Use this for initialization
	void Start () 
	{
		startHealth = health;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject)
		{
			Debug.Log("triggered");
		}

		if(other.gameObject.tag == "Bullet")
		{
			Vector2 direction = (transform.position - other.transform.position).normalized;
			this.transform.Translate(direction * backValue);

			health -= other.gameObject.GetComponent<Bullet>().DamageValue;
		}
	}

	void Dead()
	{
		//Sound_Effect
		//Particle_Effect

		if(this.gameObject.tag == "Enemy")
		{
			GameObject.FindWithTag("Player").GetComponent<Experience>().currentXP += XP;
		}

		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
		healthText.SetText(health.ToString());
		healthBar.fillAmount = health / startHealth;

		if(health <= 0)
		{
			Dead();
		}
	}
}
