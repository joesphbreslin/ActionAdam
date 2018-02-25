using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;

public class EndChatter : MonoBehaviour {
    StudioEventEmitter SE;
    void Start()
    {
        SE = GetComponent<StudioEventEmitter>();
        StartCoroutine(StopAudio());
    }
    IEnumerator StopAudio()
    {
        yield return new WaitForSeconds(3);
        SE.Stop();     
    }

}
