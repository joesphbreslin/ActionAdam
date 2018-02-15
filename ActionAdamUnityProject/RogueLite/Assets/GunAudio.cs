using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;


public class GunAudio : MonoBehaviour
{

    StudioEventEmitter myEmitter;
    // Use this for initialization

    void Start()
    {
        myEmitter = GetComponent<StudioEventEmitter>();

    }

    public void OneShot()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            myEmitter.Play();
        }

    }


    // Update is called once per frame
    void Update()
    {
        OneShot();

    }
}