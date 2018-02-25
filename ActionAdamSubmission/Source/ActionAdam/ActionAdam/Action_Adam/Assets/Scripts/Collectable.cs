using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;

public class Collectable : MonoBehaviour
{
    public enum type {Phone, Ammo, Health };
    public type collectableType;
    public int resourceAmount;
    StudioEventEmitter SE;
	// Use this for initialization
	void Start ()
    {
        SE = GetComponent<StudioEventEmitter>();
	}
    IEnumerator PlayAudio()
    {
        SE.Play();
        yield return new WaitForSeconds(2);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collectableType == type.Phone)
            {
                
                collision.gameObject.GetComponent<Phone>().credit += resourceAmount;
                StartCoroutine(PlayAudio());
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            if (collectableType == type.Health)
            {
                collision.gameObject.GetComponent<Health>().health += resourceAmount;
                StartCoroutine(PlayAudio());
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            if (collectableType == type.Ammo)
            {
                collision.gameObject.GetComponentInChildren<Gun>().ammoAmount += resourceAmount;
                StartCoroutine(PlayAudio());
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
