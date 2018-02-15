using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;

public class MiniGunAudioScript : MonoBehaviour {

    [Range(0,1)]
    public float miniGunValue;
    public string parameterString;
    StudioEventEmitter emitter;
    public bool setParamOff;
    public bool playMiniGunSound = false;


    void Start()
    {
        emitter = GetComponent<StudioEventEmitter>();
        MiniGunOff();
    }

    void MiniGunOn()
    {

    }

    void MiniGunOff()
    {
        emitter.SetParameter(parameterString, miniGunValue);
    }

 
    // Update is called once per frame
    void Update()
    {
        if (setParamOff)
        {
            MiniGunOff();
            setParamOff = false;
        }
        if (playMiniGunSound)
        {
            //MiniGunOff();
            emitter.Play();
            playMiniGunSound = false;
        }
    
    

    }
}
