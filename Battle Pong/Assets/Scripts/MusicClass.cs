using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicClass : MonoBehaviour {

	private AudioSource audioSource;
	private void Start()
	{
		DontDestroyOnLoad (transform.gameObject);
		audioSource = GetComponent<AudioSource> ();

	}

	public void playMusic()
	{
		if (audioSource.isPlaying)
			return;
		//audioSource.Play ();
	}

	public void stopMusic()
	{
		audioSource.Stop ();
	}
}
