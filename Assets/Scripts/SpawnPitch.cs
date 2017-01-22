using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpawnPitch : MonoBehaviour
{

    class Spawn
    {
        public GameObject gameObject;
        public Vector2 postion;
        public bool hit;
    }

    public Pointer pointer;
    public GameObject spawn;

    public int maxCount = 20;
    
    public float horizontalMax = 14f;
    public float horizontalMin = -10f;
    public float speed = 1.5f;
    public float verticalMin = 1.5f;
    public float verticalMax = 4.5f;

    //How often we spawn
    public float frequencyOfSpawn = 0.02f;

    //Modifiers how easy something is
    public float horizontalEasy = 0.5f;
    public float verticalEasy = 0.5f;

    public int success = 0;
    public int misses = 0;

    private List<Spawn> spawns;
    public Text display;

    void Start()
    {
        spawns = new List<Spawn>();
        UpdateDisplay();
    }

    void FixedUpdate()
    {
        List<Spawn> removeMe = new List<Spawn>();
        foreach (Spawn n in spawns)
        {
            if (!n.hit)
            {
                float distance = horizontalMax - pointer.transform.position.x;
                float calculatedDistance = (Time.deltaTime / speed) * distance;
                n.gameObject.transform.position -= new Vector3(calculatedDistance, 0);

                //do we match position with the pointer?
                var localPositionB = n.gameObject.transform.InverseTransformPoint(pointer.transform.position);
            
                bool hitX = Mathf.Abs(n.gameObject.transform.position.x - pointer.transform.position.x) < horizontalEasy;
                bool hitY = Mathf.Abs(n.gameObject.transform.position.y - pointer.transform.position.y) < verticalEasy;
                if (pointer.isActive && hitX && hitY)
                {
                    //success!
                    n.hit = true;
                    pointer.GetComponent<Animator>().SetTrigger("Hit");
                    n.gameObject.GetComponent<Animator>().SetBool("Dead", true);
                    //n.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
                    success++;
                }
            }
            else
            {
                //start animating down fall
                n.gameObject.transform.position += new Vector3(0, -.3f);
               
                if (n.gameObject.transform.position.y < -7)
                {
                    Destroy(n.gameObject);
                    removeMe.Add(n);
                }
            }
            
            if (n.gameObject.transform.position.x < horizontalMin)
            {
                misses++;
                Destroy(n.gameObject);
                removeMe.Add(n);
            }
        }
        if (removeMe.Count > 0)
        {
            UpdateDisplay();
            foreach (Spawn n in removeMe) spawns.Remove(n);
        }
        PerformSpawn();
    }

    private void UpdateDisplay()
    {
        if (display != null)
        {
            display.text = "Notes Hit: " + success + "\n" +
                "Missed: " + misses;
        }
    }

    void PerformSpawn()
    {
        bool withinMax = spawns.Count < maxCount;
        float randomNumber = Random.Range(0f, 1f);
        bool shouldSpawn = randomNumber < frequencyOfSpawn;

        if (withinMax && shouldSpawn)
        {
            Spawn n = new Spawn();
            n.postion = new Vector2(horizontalMax, Random.Range(verticalMin, verticalMax));
            n.gameObject = Instantiate(spawn, n.postion, spawn.transform.rotation);
            spawns.Add(n);
        }
    }

}