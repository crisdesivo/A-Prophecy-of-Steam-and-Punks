using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    // Controls global volume
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    void Start()
    {
        GetComponent<UnityEngine.UI.Slider>().value = AudioListener.volume;
    }
}
