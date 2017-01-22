using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionOnSuccess : MonoBehaviour {

    public SpawnPitch pitch;
    public int successMax;
    public float dest;
    private Vector3 start;
    private float distance;

    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        start = transform.position;
        distance = dest - start.y;
    }

    // Update is called once per frame
 
	void Update () {
        if (pitch.success < successMax)
        {
            float amountOfSuccess = (float)pitch.success / successMax;
            var destPost = start + new Vector3(0, amountOfSuccess * distance);
            transform.position = transform.position = Vector3.SmoothDamp(
                transform.position,
                destPost,
                ref velocity, smoothTime);
        }
	}
}
