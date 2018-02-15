using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;

public class OneShotAudioScript : MonoBehaviour
{
    public bool playBool = false;
    StudioEventEmitter myEmitter;
    // Use this for initialization

    void Start()
    {
        myEmitter = GetComponent<StudioEventEmitter>();

    }

    public void OneShot()
    {
            myEmitter.Play();
    }

    public void StopOneShot()
    {
        myEmitter.Stop();
    }


    // Update is called once per frame
    void Update()
    {
        if (playBool)
        {
            OneShot();
            playBool = false;
        }
        
    }
}