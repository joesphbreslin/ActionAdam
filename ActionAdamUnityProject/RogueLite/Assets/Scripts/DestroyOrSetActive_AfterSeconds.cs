using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class DestroyOrSetActive_AfterSeconds : MonoBehaviour 
{

	public enum Type {SetActive, Destroy};
	public Type dissapearType;

	public float seconds;
	private float startSeconds;

	// Use this for initialization
	void Awake()
	{
		startSeconds = seconds;
	}

	void SetActive()
	{
		seconds -= 1 * Time.deltaTime;

		if(seconds <= 0)
		{
			this.gameObject.SetActive(false);
			seconds = startSeconds;
		}
	}

	void Destory()
	{
		Destroy(gameObject, seconds);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(dissapearType == Type.SetActive)
		{
			SetActive();
		}

		if(dissapearType == Type.Destroy)
		{
			Destory();
		}
	}
}
