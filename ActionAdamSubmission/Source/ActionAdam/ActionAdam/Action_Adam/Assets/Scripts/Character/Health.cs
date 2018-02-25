using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour 
{

    public GameObject EnemyDeathPrefab, PlayerDeathPrefab;

    public float startHealth;

	public float health;

	public TextMeshProUGUI healthText;

	public Image healthBar;
    
    public float backValue;

	public int XP;

	public GameObject loseCanvas;

	public Color[] healthBarColors;
	public int[] healthValues;

	// Use this for initialization
	void Start () 
	{
		startHealth = health;
		if(this.tag == "Player")
		{
			loseCanvas = GameObject.Find("YouLose").gameObject;
			loseCanvas.SetActive(false);

			healthText = GameObject.Find("PlayerHealthText").GetComponent<TextMeshProUGUI>();
			healthBar = GameObject.Find("PlayerHealthBar").GetComponent<Image>();
		}
    }

	void UpdateUI()
	{
		if(health > healthValues[0])
		{
			healthBar.color = new Color(healthBarColors[0].r, healthBarColors[0].g, healthBarColors[0].b);
		}
		else if(health > healthValues[1])
		{
			healthBar.color = new Color(healthBarColors[1].r, healthBarColors[1].g, healthBarColors[1].b);
		}
		else if(health > healthValues[2])
		{
			healthBar.color = new Color(healthBarColors[2].r, healthBarColors[2].g, healthBarColors[2].b);
		}

		healthText.SetText(health.ToString() + "/" + startHealth.ToString());
		healthBar.fillAmount = health / startHealth;
	}

    void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject)
		{
			Debug.Log("triggered");
		}

		if(other.gameObject.tag == "Bullet")
		{
			if(other.gameObject.GetComponent<Bullet>().characterTag != this.gameObject.tag)
			{
				Vector2 direction = (transform.position - other.transform.position).normalized;
				this.transform.Translate(direction * backValue);
				health -= other.gameObject.GetComponent<Bullet>().DamageValue;
			}
		}
	}

    void Dead()
	{
        
		//Particle_Effect

		if(this.gameObject.tag == "Enemy")
		{
            GameObject go = Instantiate(EnemyDeathPrefab) as GameObject;

            GameObject.FindWithTag("Player").GetComponent<Experience>().currentXP += XP;

			GameObject.FindWithTag("Player").GetComponent<Experience>().XPText(XP);

			GameObject.FindWithTag("Player").GetComponent<GunProficiendy>().currentGunXP += 1;
		}

		if(gameObject.tag == "Player")
		{
            GameObject go = Instantiate(PlayerDeathPrefab) as GameObject;
            loseCanvas.SetActive(true);

			Debug.Log("You lose");
		}

		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateUI();

		if(health <= 0)
		{
			Dead();
		}
	}
}
