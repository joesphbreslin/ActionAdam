using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RenderPhone : MonoBehaviour {
    Image[] img;
    TMP_Text[] txt;
    Image parentImg;
    public float alphaSpeed = 0.5f;
	// Use this for initialization
	void Start () {
        parentImg = this.GetComponent<Image>();
        img = GetComponentsInChildren<Image>();
        txt = GetComponentsInChildren<TMP_Text>();
        foreach (Image image in img)
        {
            parentImg.CrossFadeAlpha(0, alphaSpeed, false);
            image.CrossFadeAlpha(0, alphaSpeed, false);
        }
        foreach (TMP_Text text in txt)
        {
            text.CrossFadeAlpha(0, alphaSpeed, false);
        }

    }
    
    void CallTheBoys(KeyCode key)
    {
        foreach (TMP_Text text in txt)
        {
            if (Input.GetKey(key))
            {
                text.CrossFadeAlpha(1, alphaSpeed, false);
            }
            else
            {
                text.CrossFadeAlpha(0, alphaSpeed, false);
            }
                
        }
        foreach (Image image in img)
        {
            if (Input.GetKey(key))
            {
                parentImg.CrossFadeAlpha(1, alphaSpeed, false);
                image.CrossFadeAlpha(1, alphaSpeed, false);
            }
            else
            {
                parentImg.CrossFadeAlpha(0, alphaSpeed, false);
                image.CrossFadeAlpha(0, alphaSpeed, false);
            }
        }
    
    }
	
	// Update is called once per frame
	void Update () {
        CallTheBoys(KeyCode.LeftShift);

    }
}
