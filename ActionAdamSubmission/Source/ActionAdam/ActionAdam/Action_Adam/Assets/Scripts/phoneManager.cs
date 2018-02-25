using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;

public class phoneManager : MonoBehaviour 
{

    StudioEventEmitter SE;
    void Start()
    {
        SE = GetComponent<StudioEventEmitter>();    
    }

     IEnumerator Audio(GameObject f)
    {
        SE.Play();
        yield return new WaitForSeconds(5);
        Phone phone;
        phone = GameObject.FindWithTag("Player").GetComponent<Phone>();
        phone.spawnFriend = f;
        phone.SpawnFriend();
        SE.Stop();
    }



    public void callFriend(GameObject _friend)
	{
        StartCoroutine(Audio(_friend));

       
	}


}
