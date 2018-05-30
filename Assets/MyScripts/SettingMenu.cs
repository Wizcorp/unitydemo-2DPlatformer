using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour {

    //function to set music volume and gameplay volume
    public AudioMixer audioMixer;
    public void SetVolume(float volume){
        Debug.Log(volume);
        audioMixer.SetFloat("volume",volume);
        AudioListener.volume = 1 + (volume/80);
    }
}
