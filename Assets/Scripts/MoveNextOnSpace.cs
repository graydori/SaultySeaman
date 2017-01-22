using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNextOnSpace : MonoBehaviour {

    public Tracker Track;

    private void MoveNext()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    // Update is called once per frame
    void Update () {
        if (Track != null && Track.pitch > 0 || Track == null && Input.GetKeyDown(KeyCode.Space)) MoveNext(); 
    }
}
