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
    public OneShotAudioScript oneshot;

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
        if (Input.GetKey(key))
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CallRichy();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                CallJoey();
            }
        }

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

    IEnumerator PlayAudio()
    {
        oneshot.OneShot();
        yield return new WaitForSeconds(5);
        oneshot.StopOneShot();

    }
    void CallRichy()
    {
        StartCoroutine(PlayAudio());
    }

    void CallJoey()
    {
        StartCoroutine(PlayAudio());
    }

    void Update () {
        CallTheBoys(KeyCode.LeftShift);  
    }
}
