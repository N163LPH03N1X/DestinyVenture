using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : MonoBehaviour {

    AudioSource audioSrc;
    public AudioClip BlockHit;
    public bool Startsound = false;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        audioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Wall") &&Startsound))
        {
            audioSrc.PlayOneShot(BlockHit);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Startsound = true;

        }
        
    }
}
