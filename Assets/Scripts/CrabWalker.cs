using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CrabWalker : MonoBehaviour {

    public float _UpDown;
    public float _Angle;
    public float _Period;

    private float _Time;

    // Update is called once per frame
    void Update () {
        _Time = _Time + Time.deltaTime;
        float phase = Mathf.Sin(_Time / _Period);
        transform.position += new Vector3(Math.Sign(phase) * _UpDown, 0, 0);
    }
}
