using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateOnSuccess : MonoBehaviour {

    public SpawnPitch pitch;
    public int success;

	
	// Update is called once per frame
	void Update () {
        if (pitch.success >= success) GetComponent<Animator>().SetTrigger("Die"); 
	}
}
