using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour 
{

	public Camera gameCamera;

	public bool shakeSwitch;

	public float shake;
	public float startShake;
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	private Vector3 originPosition;

	// Use this for initialization
	void Start () 
	{
		gameCamera = this.GetComponent<Camera>();

		startShake = shake;
		originPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z);
	}

	// Update is called once per frame
	void Update () 
	{
		if(shakeSwitch == true)
		{
			if(shake > 0)
			{
				gameCamera.transform.localPosition = transform.localPosition += Random.insideUnitSphere * shakeAmount;
					
				shake -= Time.deltaTime * decreaseFactor;

				if(shake <= 0)
				{
					this.transform.position = new Vector3(originPosition.x, originPosition.y, originPosition.z);
					shakeSwitch = false;
					shake = startShake;
				}
			}
			else
			{
				shake = 0.0f;
			}
		}
	}
}
