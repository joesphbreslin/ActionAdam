using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour 
{
	public enum Type {SetActive, Destroy};
	public Type dissapearType;

	[Space(10)]

	[Header("Speed of Fading Low = Slow / High = Fast")]
	public float fadeSpeed;

	[Space(10)]

	SpriteRenderer rend;

	[Header("SpriteRenderer Color Values")]
	public float alpha;
	public float r;
	public float g;
	public float b;

	// Use this for initialization
	void Start () 
	{
		rend = this.GetComponent<SpriteRenderer>();
		alpha = rend.color.a;
		r = rend.color.r;
		g = rend.color.g;
		b = rend.color.b;
	}

	void Destroy()
	{
		Destroy(gameObject);
	}

	void SetActive()
	{
		this.gameObject.SetActive(false);
	}

	void FadeOutObject()
	{
		float a = alpha -= fadeSpeed * Time.deltaTime;
		rend.color = new Color(r, g, b, a);

		if(a <= 0)
		{
			if(dissapearType == Type.Destroy)
			{
				Destroy();
			}

			if(dissapearType == Type.SetActive)
			{
				SetActive();
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		FadeOutObject();
	}
}
