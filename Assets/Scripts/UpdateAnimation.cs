using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAnimation : MonoBehaviour {

    public Pointer pointer;

	
	// Update is called once per frame
	void Update () {
        GetComponent<Animator>().SetBool("Singing", pointer.isActive); 
	}
}
