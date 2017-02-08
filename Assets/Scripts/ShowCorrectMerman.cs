using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShowCorrectMerman : MonoBehaviour {

    public Pointer pointer;
    public GameObject mermanSinging;
    public GameObject mermanNotSinging;

    // Update is called once per frame
    void Update () {

        if (pointer.isActive)
        {
            mermanSinging.GetComponent<SpriteRenderer>().enabled = true;
            mermanNotSinging.GetComponent<SpriteRenderer>().enabled = false;
            //mermanSinging.transform.position = new Vector3(mermanSinging.transform.position.x, mermanSinging.transform.position.y, -2f);
            //mermanNotSinging.transform.position = new Vector3(mermanNotSinging.transform.position.x, mermanNotSinging.transform.position.y, 2f);
        }
        else 
        {
            mermanSinging.GetComponent<SpriteRenderer>().enabled = false;
            mermanNotSinging.GetComponent<SpriteRenderer>().enabled = true;
//            mermanSinging.transform.position = new Vector3(mermanSinging.transform.position.x, mermanSinging.transform.position.y, 2f);
  //          mermanNotSinging.transform.position = new Vector3(mermanNotSinging.transform.position.x, mermanNotSinging.transform.position.y, -2f);
        }
    }
}
