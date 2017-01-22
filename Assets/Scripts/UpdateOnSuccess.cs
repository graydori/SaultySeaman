using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateOnSuccess : MonoBehaviour
{

    public int success;
    public ulong delay;
    private bool dead;

    // Update is called once per frame
    void Update()
    {
        if (!dead && UserState.success >= success)
        {
            dead = true;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<AudioSource>().Play(delay);
        }
    }

}
