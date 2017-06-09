using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {

	[Header("Properties:")]
	public AudioSource audioSrc;
	public AudioClip clipBlast;

	// Use this for initialization
	void Start () {
		if(audioSrc == null)
			audioSrc = GetComponent<AudioSource> ();
	}

	public void Blast () {
		if(audioSrc != null && clipBlase != null) {
			audioSrc.clip = clipBlast;
			audioSrc.Play ();
		}
	}
}
