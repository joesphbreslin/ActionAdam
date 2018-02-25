using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunProficiendy : MonoBehaviour 
{
	public int gunIndex;
	public GameObject[] guns;

	public float currentGunXP;
	public float maxGunXP;

	public Image gunImage;
	public Sprite[] gunImages;
	public Image gunProgressBar;

	// Use this for initialization
	void Start () 
	{
		gunImage = GameObject.Find("PlayerGunUIImage").GetComponent<Image>();
		gunProgressBar = GameObject.Find("PlayerGunProgressBar").GetComponent<Image>();
	}

	void UpdateUI()
	{
		gunImage.sprite = gunImages[gunIndex];

		gunProgressBar.fillAmount = currentGunXP / maxGunXP;
	}

	// Update is called once per frame
	void Update () 
	{
		GunXP();
		SwitchGuns();
		UpdateUI();
	}

	void GunXP()
	{
		if(currentGunXP >= maxGunXP && gunIndex <= guns.Length)
		{
			gunIndex += 1;
			currentGunXP = 0;
		}
	}

	void SwitchGuns()
	{
		for(int i = 0; i < guns.Length; i ++)
		{
			if(guns[i] == guns[gunIndex])
			{
				guns[i].gameObject.SetActive(true);
			}
			else
			{
				guns[i].gameObject.SetActive(false);
			}
		}
	}
}
