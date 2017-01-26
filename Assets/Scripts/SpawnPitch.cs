using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public static class UserState {
    public static int success;
    public static int misses;
    public static void init()
    {
        success = 0;
        misses = 0;
    }
}

public class SpawnPitch : MonoBehaviour
{

    class Spawn
    {
        public GameObject gameObject;
        public Vector2 position;
        public bool hit;
        public float noteFrequency;
    }

    public Pointer pointer;
    public GameObject spawn;
    public bool useLevelTwoNotes;

    public AudioSource forGodsSakeSing;
    bool forGodsSakeSingPlayed = false;
    public AudioSource successNoise;
    bool successNoisePlayed = false;
    private int countDownFramesTillSuccessNoisePlays = 10;

    public int maxCount = 20;

    public float horizontalMax = 14f;
    public float horizontalMin = -10f;
    public float speed = 1.5f;


    //How often we spawn
    public float frequencyOfSpawn = 0.036f;

    //Modifiers how easy something is
    public float horizontalEasy = 0.5f;
    public float verticalEasy = 0.5f;

    

    private List<Spawn> spawns;
    public Text display;
    public Text instructionsDisplay;

    // Pitch related stuff
    public float verticalMin = 1.5f;
    public float verticalMax = 4.5f;
    private SongGenerator songGenerator = new SongGenerator();

    private float timeOfLastSpawn;

    void Start()
    {
        spawns = new List<Spawn>();
        UserState.init();
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
                    UserState.success++;
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
                UserState.misses++;
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


        if (UserState.success >= 24) // ASSUMES successmax=24 for positionOnSuccess
        {
            if (!successNoisePlayed && (--countDownFramesTillSuccessNoisePlays == 0))
            {
                successNoise.Play();
                successNoisePlayed = true;
            }
        }
    }

        // during jam, no time to set the number of birds per sailor properly so instead hacked it here. should match Success Max in inspector for  positionOnSuccess.successMax, that's the
        // thing which triggers the transition to the next level when you finish;
        // successmax = 24 => use / 8, of 3 . also the number of birds that need to be hit to take out a pirate 
        // also needs to be set in UpdateOnSuccess.Success for Ship/Pirate1,2,Duchess. 
    private float Score()    {        return (float) UserState.success / 8; }
    private int NumberOfSailors() { return 3; }

    private void UpdateDisplay()
    {
        if (instructionsDisplay != null)
        {

            if (Time.timeSinceLevelLoad > 8 && Score() < 0.2)
            {
                if (!forGodsSakeSingPlayed)
                {
                    forGodsSakeSing.Play();
                    forGodsSakeSingPlayed = true;
                }
            }

            if (Time.timeSinceLevelLoad > 8 && Score() < 0.2)
            {
                instructionsDisplay.text = "Use your singing pitch (the shell) to hit the notes (the birds)\n";
            }
            else
            {
                instructionsDisplay.text = "";
            }

            if (Microphone.devices.Length == 0)
            {
                pointer.fake = true;
                
                instructionsDisplay.text += "\nNO MICROPHONE HUH? YOU CAN USE THE ARROW KEYS            ";
            }

        }

        if (display != null)
        {
            display.text = "Sailors seduced : " + string.Format("{0:F1}", Score())  + " of " + NumberOfSailors(); 
        }
    }
     
    float PlaceSpawnVertically(float noteFrequency)
    {
        Debug.Log("PlaceSpawnVertically noteFrequency = " + noteFrequency);

        // float y = Random.Range(verticalMin, verticalMax); // 1.5,4.5        
        float y = pointer.MapPitchToYAxis(noteFrequency);
        return y;
    }
    void PerformSpawn()
    {
        bool withinMax = spawns.Count < maxCount;
        float randomNumber = Random.Range(0f, 1f);
        
        bool shouldSpawn = randomNumber < frequencyOfSpawn;

        if (Time.timeSinceLevelLoad < 2.3)
        {
            // give people some time to read the instructions
            shouldSpawn = false;
        }
            

        if ((Time.timeSinceLevelLoad - timeOfLastSpawn) < 0.1)
        {
            //Debug.Log("Avoiding spawning really close together");
            shouldSpawn = false;
        }

        if (withinMax && shouldSpawn)
        {
            Spawn n = new Spawn();
            n.noteFrequency = songGenerator.PickANote(useLevelTwoNotes);
            n.position = new Vector2(horizontalMax, PlaceSpawnVertically(n.noteFrequency));
            n.gameObject = Instantiate(spawn, n.position, spawn.transform.rotation);
            spawns.Add(n);
            timeOfLastSpawn = Time.timeSinceLevelLoad;
        }
    }

}