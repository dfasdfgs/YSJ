using UnityEngine;

public class SoundValue : MonoBehaviour
{
    public AudioSource AudioSource;

    public void SetMusicVolume(float Volume)
    {
        AudioSource.volume = Volume;
    }
}
