using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Animations : MonoBehaviour {
	private Animator animator;
	private float v,h,run;

	void Start () {
		animator = GetComponent <Animator> ();
	}
	
	void Update () {
		v=Input.GetAxis("Vertical");
		h=Input.GetAxis("Horizontal");
		if (animator.GetFloat("Run")==0.2f){
				if (Input.GetKeyDown("space")){
						animator.SetBool("Jump",true);
				}
		}
		Sprinting();
	}

	void FixedUpdate (){
			animator.SetFloat("Walk",v);
			animator.SetFloat("Run",run);
			animator.SetFloat("Turn",h);
	}

	void Sprinting(){
		if (Input.GetKey(KeyCode.LeftShift))
			run=0.2f;
		else
			run=0.0f;
	}
}