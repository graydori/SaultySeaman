using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedPlay : MonoBehaviour {
    public ulong delay = 44100;

	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().Play(delay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
