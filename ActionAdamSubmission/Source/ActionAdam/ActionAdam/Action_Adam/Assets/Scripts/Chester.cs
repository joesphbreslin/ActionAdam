using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;

public class Chester : MonoBehaviour
{
    StudioEventEmitter SE;
    // Use this for initialization
    void Start()
    {
        SE = GetComponent<StudioEventEmitter>();
    }
    IEnumerator PlayAudio()
    {
        SE.Play();
        yield return new WaitForSeconds(2);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.tag == "Player")
        {
            StartCoroutine(PlayAudio());
            this.transform.parent = other.gameObject.transform;
        }
    }

}
