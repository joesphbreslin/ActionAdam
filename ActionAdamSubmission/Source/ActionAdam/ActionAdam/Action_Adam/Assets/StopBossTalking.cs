using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class StopBossTalking : MonoBehaviour {
 
    StudioEventEmitter SE;
void Start()
{
    SE = GetComponent<StudioEventEmitter>();
        StartCoroutine(Audio());
}

IEnumerator Audio()
{
   
    yield return new WaitForSeconds(5);
    SE.Stop();
}

// Use this for initializatio
}
