using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        var a = this.GetComponent<AudioSource>();
        var b = SoundService.GetVolumeMusic();
        a.Stop();
        a.volume = b;
        a.Play();
        a.loop = true;

    }
}
