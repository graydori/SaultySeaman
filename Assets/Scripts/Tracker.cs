using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;
using Pitch;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class Tracker : MonoBehaviour
{
    public float pitch;
    public int midiCents;
    public Text display; // drag a Text object here to display values
    
    public bool mute = true;
    public AudioMixer masterMixer; // drag an Audio Mixer here in the inspector

    string micDeviceName = null; // null = default mic
    int micSampleRate;
    AudioClip micAudio;

    public PitchTracker pitchTracker;

    void Start()
    {
        ForumVersionStart();
    }
    
    // based on https://forum.unity3d.com/threads/detecting-musical-notes-from-vocal-input.316698/
    void ForumVersionStart()
    {
        if (Microphone.devices.Length == 0)
        {
            return;
        }

        int minSampleRate, maxSampleRate;
        Microphone.GetDeviceCaps(micDeviceName, out minSampleRate, out maxSampleRate);
        Debug.Log("Minimum sample rate was: " + minSampleRate + " Hz");
        Debug.Log("Maximum sample rate was: " + maxSampleRate + " Hz");
        micSampleRate = minSampleRate;
        // starts the Microphone and attaches it to the AudioSource
        GetComponent<AudioSource>().clip = Microphone.Start(micDeviceName, true, 10, minSampleRate);
        GetComponent<AudioSource>().loop = true; // Set the AudioClip to loop
        while (!(Microphone.GetPosition(null) > 0)) { } // Wait until the recording has started
        GetComponent<AudioSource>().Play();

        // Mutes the mixer. You have to expose the Volume element of your mixer for this to work. I named mine "masterVolume".
        // NEVER MIND ILL SET IT IN PROPERTY INSPECTOR
        //masterMixer.SetFloat("masterVolume", -80f);

        pitchTracker = new PitchTracker();
        pitchTracker.SampleRate = micSampleRate;
        pitchTracker.PitchDetected += PitchTracker_PitchDetected;
    }
   

    private void PitchTracker_PitchDetected(PitchTracker sender, PitchTracker.PitchRecord pitchRecord)
    {
        if (display != null) display.text = pitchRecord.Pitch + " - " + pitchRecord.RecordIndex;
    }

    void Update()
    {
        if (Microphone.devices.Length == 0)
        {
            return;
        }

        ForumVersionAnalyzeSound();

        pitch = pitchTracker.CurrentPitchRecord.Pitch;
        midiCents = pitchTracker.CurrentPitchRecord.MidiCents;
    }
     
    // Feeds the mic audio through an audio output so we can use GetOutputData.
    // I couldn't get things to work without this bullshit.
    // Therefore we manually made the mixer set audio output level = 0 so people don't hear themselves. 
    void ForumVersionAnalyzeSound()
    {
        float[] samples = new float[2048];
        GetComponent<AudioSource>().GetOutputData(samples, 0); // fill array with samples
        pitchTracker.ProcessBuffer(samples);
    } 
}