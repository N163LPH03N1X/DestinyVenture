using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour {

    AudioSource audioSrc;

    public AudioClip selection;
    public AudioClip select;
    public AudioClip back;

    // Use this for initialization
    void Start () {
        audioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnMouseEnter()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.PlayOneShot(selection);
    }
    public void OnSelect()
    {
        audioSrc.PlayOneShot(select);
    }
    public void OnBack()
    {
        audioSrc.PlayOneShot(back);
    }
}
