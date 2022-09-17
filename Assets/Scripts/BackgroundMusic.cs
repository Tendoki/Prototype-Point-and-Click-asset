using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class BackgroundMusic : MonoBehaviour
{
	public AudioClip BackgroundMusicLevel;
	public AudioClip currentMusic;

	private void Start()
	{
		this.GetComponent<AudioSource>().clip = BackgroundMusicLevel;
		this.GetComponent<AudioSource>().Play();
	}
}
