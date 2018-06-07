using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    //Apply music setting volume
    void Update()
    {
        this.GetComponent<AudioSource>().volume = SettingsService.GetVolumeMusic();
    }
}
