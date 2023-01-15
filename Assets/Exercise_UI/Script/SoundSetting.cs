using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoundSetting : MonoBehaviour
{
   public static float musicVolume { get; private set; }
   public static float soundVolume { get; private set; }

    [SerializeField] private TextMeshProUGUI musicSlider;
    [SerializeField] private TextMeshProUGUI soundSlider;

    
    public void OnMusicSlider(float value)
    {
        musicVolume = value;
        musicSlider.text =((int)(value * 100)).ToString() + "%";
    }

    public void OnSoundSlider(float value)
    {
        soundVolume = value;
        soundSlider.text = ((int)(value * 100)).ToString() + "%";
    }

}
