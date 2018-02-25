using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Experience : MonoBehaviour 
{
	public TextMeshProUGUI xpText;
	public GameObject canvas;

	public float currentXP;
	public float maxXP;

	public int currentLevel;
	public int maxLevel;

	public float force;

	public TextMeshProUGUI levelText;
	public Image xpBar;

	// Use this for initialization
	void Start () 
	{
		levelText = GameObject.Find("PlayerLevelText").GetComponent<TextMeshProUGUI>();
		xpBar = GameObject.Find("PlayerXPBar").GetComponent<Image>();
	}

	void UpdateUI()
	{
		levelText.SetText("Level: " + currentLevel.ToString());

		xpBar.fillAmount = currentXP / maxXP;
	}

	public void XPText(int xp)
	{
		TextMeshProUGUI xpTextInstance;
		xpTextInstance = Instantiate(xpText, canvas.transform.localPosition, Quaternion.identity) as TextMeshProUGUI;
		xpTextInstance.SetText("+" + xp.ToString() + "XP");

		Rigidbody2D rb = xpTextInstance.GetComponent<Rigidbody2D>();
		rb.AddForce(canvas.transform.up * force);

		xpTextInstance.transform.SetParent(canvas.transform, false);
	}

	void LevelUp()
	{
		if(currentXP >= maxXP)
		{
			currentLevel += 1;
			maxXP += maxXP / 2;
			currentXP = 0;
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		UpdateUI();
		LevelUp();
	}
}
