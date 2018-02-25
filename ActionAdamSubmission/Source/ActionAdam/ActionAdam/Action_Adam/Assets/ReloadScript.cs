using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ReloadScript : MonoBehaviour {

    StudioEventEmitter SE;
	// Use this for initialization
	void Start () {
        SE = GetComponent<StudioEventEmitter>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SE.Play();
        }
	}
}
