using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{

    public bool fake = false;
    public float speed = 1;
    public Vector3 position;
    public Tracker tracker;
    private float centerY = 0;
    public float maxSize = 100; // note this may be overriden in the Unity property inspector
    public float minPitch = 175; // note this may be overriden in the Unity property inspector
    public float maxPitch = 450; // note this may be overriden in the Unity property inspector
    public float pointerPitch = 0;

    public bool isActive = false;


    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    private int framesForWhichNoPitchDetected = 0;


    // Use this for initialization
    void Start()
    {
        centerY = gameObject.transform.position.y;
        isActive = fake;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float y;
        if (!fake)
        {
            float p = tracker.pitch;
            if (p != 0)
            {
                pointerPitch = p;
                framesForWhichNoPitchDetected = 0;
            }
            else
            {
                framesForWhichNoPitchDetected++;

                float waitThisManySecondsBeforeSettingVoiceInactive = 0.2f;
                if (framesForWhichNoPitchDetected * Time.fixedDeltaTime > waitThisManySecondsBeforeSettingVoiceInactive)
                {
                    pointerPitch = 0;
                }
            }            
            
            y = MapPitchToYAxis(pointerPitch);

            isActive = pointerPitch != 0;

            UpdateOpacity();

        }
        else
        {
            y = Mathf.Clamp(gameObject.transform.position.y + Input.GetAxis("Vertical") * speed, centerY - maxSize / 2, centerY + maxSize / 2);
        }
        position = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z);
        gameObject.transform.position = Vector3.SmoothDamp(transform.position, position, ref velocity, smoothTime);
    }

    public float MapPitchToYAxis(float pitch)
    {
        float y;
        y = (pitch - minPitch)/ (maxPitch - minPitch);
        y *= maxSize;
        y -= maxSize / 2;
        y += centerY;
        return y;
    }

    private void UpdateOpacity()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, isActive ? 1f : 0.2f);
    }
}