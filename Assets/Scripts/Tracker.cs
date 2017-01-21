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

    public float rmsValue;
    public float dbValue;

    public int qSamples = 1024;
    public int binSize = 1024; // you can change this up, I originally used 8192 for better resolution, but I stuck with 1024 because it was slow-performing on the phone
    public float refValue = 0.1f;
    public float threshold = 0.01f;

    float[] samples;
    float[] spectrum;
    int samplerate;

    public Text display; // drag a Text object here to display values
    public bool mute = true;
    public AudioMixer masterMixer; // drag an Audio Mixer here in the inspector

    public PitchTracker pitchTracker;

    void Start()
    {
        samples = new float[qSamples];
        spectrum = new float[binSize];
        samplerate = AudioSettings.outputSampleRate;

        // starts the Microphone and attaches it to the AudioSource
        GetComponent<AudioSource>().clip = Microphone.Start(null, true, 10, samplerate);
        GetComponent<AudioSource>().loop = true; // Set the AudioClip to loop
        while (!(Microphone.GetPosition(null) > 0)) { } // Wait until the recording has started
        GetComponent<AudioSource>().Play();

        // Mutes the mixer. You have to expose the Volume element of your mixer for this to work. I named mine "masterVolume".
        masterMixer.SetFloat("masterVolume", -80f);

        pitchTracker = new PitchTracker();
        pitchTracker.SampleRate = samplerate;
        pitchTracker.PitchDetected += PitchTracker_PitchDetected;
    }

    private void PitchTracker_PitchDetected(PitchTracker sender, PitchTracker.PitchRecord pitchRecord)
    {
        if (display != null) display.text = pitchRecord.Pitch + " - " + pitchRecord.RecordIndex;
    }

    void Update()
    {
        AnalyzeSound();

        pitch = pitchTracker.CurrentPitchRecord.Pitch;
        midiCents = pitchTracker.CurrentPitchRecord.MidiCents;
    }
    

    void AnalyzeSound()
    {
        float[] samples = new float[qSamples];
        GetComponent<AudioSource>().GetOutputData(samples, 0); // fill array with samples
        pitchTracker.ProcessBuffer(samples);
    }
}