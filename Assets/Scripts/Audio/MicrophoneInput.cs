﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour {
    public float sensitivity = 100;
    public float loudness = 0;
    public string device = "Logitech USB Microphone";

    void Start() {
        if (device == null) device = Microphone.devices[0];
        audio.clip = Microphone.Start(null, true, 10, 44100);
        audio.loop = true; // Set the AudioClip to loop
        audio.mute = true; // Mute the sound, we don't want the player to hear it
        //while (!(Microphone.GetPosition("Logitech USB Microphone") > 0)){} // Wait until the recording has started
        if (Microphone.devices.Length <= 0) { return; }
        while (!(Microphone.GetPosition(device) > 0)){} // Wait until the recording has started
        audio.Play(); // Play the audio source!
    }

    void Update(){
        loudness = GetAveragedVolume() * sensitivity;
    }

float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        audio.GetOutputData(data,0);
        foreach(float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a/256;
    }
}
