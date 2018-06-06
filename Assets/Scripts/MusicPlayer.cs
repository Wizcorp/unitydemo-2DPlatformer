using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	// Use this for initialization
	void Awake () {

    }

    void Update()
    {
        this.GetComponent<AudioSource>().volume = SettingsService.GetVolumeMusic();
    }
}
