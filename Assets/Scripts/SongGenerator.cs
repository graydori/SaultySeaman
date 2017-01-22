using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SongGenerator  
{

    /* 
      
     The song generation algorithm was designed by Michael Fraser from Tree of Audio

    We pick notes that will go with the music. The music is in the key of G.
    We want  both a soprano and a baritone to be able to hit all the notes. 
    Soprano	C4 (262 Hz) to C6 (1,047 Hz).
    Baritone	F2 (87 Hz) to F4 (349 Hz)
    So: C4 262 to F4 349.
    Since we don't need people to sing -well- we use a somewhat wider range that that.


    For the first level:
    Group 1: (B3 = 246.94, D4 = 293.66)
    Group 2: (C4 = 261.63)

    For the second level:
    add G4 = 392 to the Group 1
    add  E4 = 329.63 and FSharp4 = 369.99 to Group 2

    Notes are picked from Group 1 2/3 of the time. It's ok for the same note in Group A to follow itself
    Notes are picked from Group 2 1/3 of the time. It's not ok for the same note in Group B to follow itself
*/

    private const float B3 = 246.9f; 
    private const float D4 = 293.6f; 
    private const float C4 = 261.6f; 
    private const float G4 = 392.0f; 
    private const float E4 = 329.6f; 
    private const float FSharp4 = 370.0f;
    float previousNote = 0.0f;

    //private float[,] levelOneNoteGroups = { { B3, D4 }, { C4 } };
    // private float[,] levelTwoNoteGroups = { { B3, D4, G4 }, { C4, E4, FSharp4 } };

    private float[][] levelOneNoteGroups = new float[][] 
    {
        new float[] { B3, D4 },
        new float[]  { C4 }
    };



    public float PickANote()
    {
        float note;
        bool success;

        System.Random random = new System.Random();

        do
        {
            success = true;
            int r = random.Next(0, 3);
            int groupIndex =0;
            if (r == 0)
            {
                groupIndex = 1;
            } else if (r == 1 || r == 2)
            {
                groupIndex = 0;
            }

            float[] noteGroup = levelOneNoteGroups[groupIndex];
            int noteIndex = random.Next(noteGroup.Length);
            note = noteGroup[noteIndex];

            // Don't repeat notes in second group twice in a row
            if ((groupIndex == 1) && (note == previousNote) && (previousNote != 0))
            {
                //Debug.Log("avoiding repeated note" + note);
                success = false;
            }

            //if (success)
            //{
            //   Debug.Log("Success: groupIndex = " + groupIndex + " noteIndex " + noteIndex);
            //}

        } while (!success);

        previousNote = note;
        
        return note;
    }

    // To test out the notes. Unfinished. And probably needs to be in a MonoBehavior. Idea is that we'd do this when picking notes:
    //         if (playSineWaves){
    //        sineWaveFrequency = note;
    //    }
    //
    // From http://www.develop-online.net/tools-and-tech/procedural-audio-with-unity/0117433
/*    public bool playSineWaves = true;
    private float sineWaveFrequency = 0;
    void OnAudioFilterRead(float[] data, int channels)
    {

        double gain = 0.05;

        double increment;
        double phase = 0;
        double sampling_frequency = AudioSettings.outputSampleRate;

        // update increment in case frequency has changed
        increment = sineWaveFrequency * 2 * Math.PI / sampling_frequency;
        for (var i = 0; i < data.Length; i = i + channels)
        {
            phase = phase + increment;
            // this is where we copy audio data to make them “available” to Unity
            data[i] = (float)(gain * Math.Sin(phase));
            // if we have stereo, we copy the mono data to each channel
            if (channels == 2) data[i + 1] = data[i];
            if (phase > 2 * Math.PI) phase = 0;
        }
    }
 */ 
}
