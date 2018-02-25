using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Phone : MonoBehaviour
{
    public int credit;

	public GameObject phone;

	// Use this for initialization
	void Start ()
    {
		phone = GameObject.Find("Phone");
		phone.SetActive(false);
	}

    void Awake()
    {
    }

    void UpdateUI()
    {
    }

	void PhoneActivate()
	{
		if(Input.GetKeyDown(KeyCode.LeftShift) && phone.activeSelf == false)
		{
			phone.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.LeftShift) && phone.activeSelf == true)
        {
            phone.SetActive(false);
        }
      
	}


    // Update is called once per frame
    void Update ()
    {
		PhoneActivate();
		
        UpdateUI();
	}

	public GameObject spawnFriend;
	public void SpawnFriend()
	{
		if(credit > 0)
		{
			GameObject friendInstance;
			friendInstance = Instantiate(spawnFriend, transform.position, Quaternion.identity) as GameObject;
			credit -= 1;
		}
	}
}
