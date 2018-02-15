using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;

public class AudioManager : MonoBehaviour {

    StudioEventEmitter myEmitter;
    // Use this for initialization
    void Awake () {
        myEmitter = this.GetComponent<StudioEventEmitter>();
        myEmitter.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
