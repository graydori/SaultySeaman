using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPitch : MonoBehaviour
{

    class Spawn
    {
        public GameObject gameObject;
        public Vector2 postion;
        public bool hit;
    }

    public GameObject pointer;
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

    void Start()
    {
        spawns = new List<Spawn>();
    }

    void Update()
    {
        List<Spawn> removeMe = new List<Spawn>();
        foreach (Spawn n in spawns)
        {
            float distance = horizontalMax - pointer.transform.position.x;
            float calculatedDistance = (Time.deltaTime / speed) * distance;
            n.gameObject.transform.position -= new Vector3(calculatedDistance, 0);

            //do we match position with the pointer?
            var localPositionB = n.gameObject.transform.InverseTransformPoint(pointer.transform.position);
            if (!n.hit) {
                bool hitX = Mathf.Abs(n.gameObject.transform.position.x - pointer.transform.position.x) < horizontalEasy;
                bool hitY = Mathf.Abs(n.gameObject.transform.position.y - pointer.transform.position.y) < verticalEasy;
                if (hitX && hitY)
                {
                    //success!
                    n.hit = true;
                    n.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
                }
            }

                //cleanup this game object
            if (n.gameObject.transform.position.x < pointer.transform.position.x && n.hit)
            {
                success++;
                Destroy(n.gameObject);
                removeMe.Add(n);
            }
            if (n.gameObject.transform.position.x < horizontalMin)
            {
                misses++;
                Destroy(n.gameObject);
                removeMe.Add(n);
            }

        }

        foreach (Spawn n in removeMe) spawns.Remove(n);
        PerformSpawn();
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
            n.gameObject = Instantiate(spawn, n.postion, Quaternion.identity);
            spawns.Add(n);
        }
    }

}