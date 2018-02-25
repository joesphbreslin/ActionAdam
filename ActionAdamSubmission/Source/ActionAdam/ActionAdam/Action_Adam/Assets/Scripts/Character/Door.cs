using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Door : MonoBehaviour 
{
    StudioEventEmitter SE;
	public GameObject winCanvas;

	// Use this for initialization
	void Awake () 
	{
        SE = GetComponent<StudioEventEmitter>();
		winCanvas = GameObject.Find("YouWin").gameObject;
		winCanvas.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Chester")
		{

            StartCoroutine(End());
           
        }
	}

    IEnumerator End()
    {
        SE.Play();
        winCanvas.SetActive(true);
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
    }

}
