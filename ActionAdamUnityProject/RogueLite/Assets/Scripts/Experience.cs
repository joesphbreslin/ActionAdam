using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Experience : MonoBehaviour 
{
	//public Text levelText;
	//public Text currentXPText;

	public TextMeshProUGUI levelText;
	public TextMeshProUGUI currentXPText;

	public int currentXP;
	public int maxXP;

	public int currentLevel;
	public int maxLevel;

	// Use this for initialization
	void Start () 
	{
		
	}

	void UpdateUI()
	{
		levelText.SetText("Player Level " + currentLevel.ToString());
		currentXPText.SetText("Experience Points " + currentXP.ToString() + " / " + maxXP.ToString());

		//levelText.text = ("Player Level ") + currentLevel.ToString();
		//currentXPText.text = ("Experience Points ") + currentXP.ToString() + " / " + maxXP.ToString();
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
	void Update () 
	{
		LevelUp();
		UpdateUI();
	}
}
