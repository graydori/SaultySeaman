using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {

    public bool fake = false;
    public float speed = 1;
    public Vector3 position;
    public Tracker tracker;
    public float maxPitch = 150;
    private float centerY = 0;
    public float maxSize = 100;

	// Use this for initialization
	void Start () {
      centerY = gameObject.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        float y;
        if (!fake)
        {
            y = tracker.pitch / maxPitch;
            y *= maxSize;
            y -= maxSize / 2;
            y += centerY;
            UpdateOpacity(tracker.pitch != 0);

        } else {
            y = Mathf.Clamp( gameObject.transform.position.y + Input.GetAxis("Vertical") * speed,  centerY - maxSize / 2, centerY + maxSize /2 );
        }
        position = new Vector3(gameObject.transform.position.x , y);
        gameObject.transform.position = position;
	}

    private void UpdateOpacity(bool active)
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, active? 1f: 0.5f);
    }
}