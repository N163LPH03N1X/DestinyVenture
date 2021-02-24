using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMusic : MonoBehaviour {

    AudioSource audioSrc;
    public AudioClip IceTempleMusic;
    
	// Use this for initialization
	void Start () {
        audioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PlayIceTemple()
    {
        audioSrc.clip = IceTempleMusic;
        audioSrc.Play();
    }
}
