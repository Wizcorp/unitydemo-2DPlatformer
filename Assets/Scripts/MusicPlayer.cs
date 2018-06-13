using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
	private AudioSource audioSource;              // Reference to the AudioSource component

	void Awake()
	{
		// Setting up the references.
		audioSource = GetComponent<AudioSource>();
	}

	//Apply music setting volume
	void Update()
	{
		audioSource.volume = SettingsService.GetVolumeMusic();
	}
}
