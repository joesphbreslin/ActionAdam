using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamControls : MonoBehaviour 
{
	public float panSpeed = 20f;

	public float panBoarderThickness = 10f;

	public Vector2 panLimitX;
	public Vector2 panLimitY;

	public float scrollSpeed = 20f;

	public float minZoom = 5;
	public float maxZoom = 20;

	// Update is called once per frame
	void Update () 
	{
		Vector3 pos = new Vector3(transform.position.x, transform.position.y, -10);

		if(Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBoarderThickness)
		{
			pos.y += panSpeed * Time.deltaTime;
		}

		if(Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBoarderThickness)
		{
			pos.y -= panSpeed * Time.deltaTime;
		}

		if(Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBoarderThickness)
		{
			pos.x -= panSpeed * Time.deltaTime;
		}

		if(Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBoarderThickness)
		{
			pos.x += panSpeed * Time.deltaTime;
		}
			
		pos.x = Mathf.Clamp(pos.x, panLimitX.x, panLimitX.y);
		pos.y = Mathf.Clamp(pos.y, panLimitY.x, panLimitY.y);

		transform.position = pos;

		Zoom();
	}

	void Zoom()
	{
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		this.GetComponent<Camera>().orthographicSize -= scroll * scrollSpeed * 100f * Time.deltaTime;
		this.GetComponent<Camera>().orthographicSize = Mathf.Clamp(this.GetComponent<Camera>().orthographicSize, minZoom, maxZoom);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
