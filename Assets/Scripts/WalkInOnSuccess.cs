using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkInOnSuccess : MonoBehaviour
{
    
    public Animator animate;
    public int successMin;
    public int successMax;
    public Vector3 position1;
    public Vector3 position2;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private bool movingToPosOne = true;


    // Update is called once per frame
    void Update()
    {
        Vector3 dest = movingToPosOne ? position1 : position2;
        if (UserState.success >= successMin && UserState.success < successMax)
        {
            transform.position = Vector3.SmoothDamp(
                transform.position,
                dest, 
                ref velocity, smoothTime);
            if (Vector3.Distance(dest, gameObject.transform.position) < 0.5) 
            {
                movingToPosOne = !movingToPosOne;
                animate.SetBool("Angry", !movingToPosOne);
                transform.localScale = new Vector3(transform.localScale.x * -1,1,1);
            }
            
        }
    }
}
