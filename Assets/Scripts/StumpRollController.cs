using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StumpRollController : MonoBehaviour {

    public float speed = 1.0f;
    Vector2 v2 = new Vector2(-1, 0);
    Rigidbody2D rb2d;
    Animator animator;

    bool init = false;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		if (animator.GetBool("Init"))
        {
            rb2d.velocity = v2.normalized * speed;
        }
	}

    // Collsion enter
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Monkey")
        {
            this.speed = 0f;
        }
    }
    
}
