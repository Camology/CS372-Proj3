using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HoverSound : MonoBehaviour {
	public AudioClip[] audioClip;
	private AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource> ();
	}

	void playSound(int clip) {
		audioSource.clip = audioClip [clip];
		audioSource.Play ();
	}
}
