using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateOnSuccess : MonoBehaviour {
    
    public int success;

	// Update is called once per frame
	void Update () {
        if (UserState.success >= success) GetComponent<Animator>().SetTrigger("Die"); 
	}
}
