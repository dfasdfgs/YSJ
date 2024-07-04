using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundValue : MonoBehaviour
{
    public AudioSource AudioSource;

    public void SetMusicVolume(float Volume)
    {
        AudioSource.volume = Volume;
    }
}
