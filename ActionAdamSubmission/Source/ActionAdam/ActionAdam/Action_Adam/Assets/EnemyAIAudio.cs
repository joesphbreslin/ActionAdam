using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class EnemyAIAudio : MonoBehaviour {

    StudioEventEmitter SE;
    // Use this for initialization
    void Start()
    {
        SE = GetComponent<StudioEventEmitter>();
        StartCoroutine(PlayAudio());
    }
    IEnumerator PlayAudio()
    {

        yield return new WaitForSeconds(10);
        SE.Stop();
    }

}
         
        
