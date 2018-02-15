using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimation : MonoBehaviour {

    Rigidbody2D rb;
    Animator anim;
    int animState = 0;

	void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}
	
    void VelocityAnimation()
    {
        if (rb.velocity.normalized.x != 0 && rb.velocity.normalized.y == 0) 
        {
            Debug.Log("LR");
            animState = 1;
        }
        else if (rb.velocity.normalized.x == 0 && rb.velocity.normalized.y > 0)
        {
            animState = 2;
        }
        else if (rb.velocity.normalized.x == 0 && rb.velocity.normalized.y < 0)
        {
            animState = 3;
        }
        else if (rb.velocity.normalized.x == 0 && rb.velocity.normalized.y == 0)
        {
            animState = 0;
        }
    }

	// Update is called once per frame
	void Update () { 
        anim.SetInteger("Movement", animState);
        VelocityAnimation();
    }
}
