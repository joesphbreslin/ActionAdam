using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{

    public bool on_offSwitch = true;

	public GameObject canvas;

	// Use this for initialization
	void Start ()
    {
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
			canvas.SetActive(true);
        }
    }

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			other.attachedRigidbody.AddForce(Vector2.up * 1);

			if(Input.GetKeyDown(KeyCode.F) && other.GetComponent<Phone>().credit > 0)
			{
				on_offSwitch = false;
				other.GetComponent<Phone>().credit -= 1;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			canvas.SetActive(false);
		}
	}

    // Update is called once per frame
    void Update () {
		
	}
}
