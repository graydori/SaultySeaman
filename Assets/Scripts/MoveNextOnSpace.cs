using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNextOnSpace : MonoBehaviour {

    public Tracker Track;

    private void MoveNext()
    {
        if (Application.loadedLevel < 2)
            Application.LoadLevel(Application.loadedLevel + 1);
        else
            Application.LoadLevel(1);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        
        if (/*Track != null && Track.pitch > 0 || Track == null && */ Input.GetKeyDown(KeyCode.Space)) MoveNext();


    }
}
