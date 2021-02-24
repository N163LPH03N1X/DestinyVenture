using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFlameTorch : MonoBehaviour {

    public GameObject FireParticle;
    AudioSource audioSrc;
    public AudioClip torch;
    bool gainCount;
    public int hitPoint;
    public static int count;


    // Use this for initialization
    void Start () {
        audioSrc = GetComponent<AudioSource>();
        FireParticle.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (!gainCount && hitPoint > 0)
        {
            FireParticle.SetActive(true);
            audioSrc.PlayOneShot(torch);
            count++;
            gainCount = true;
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            hitPoint++;
        }
        if (other.gameObject.CompareTag("MagicBlade"))
        {
            hitPoint++;
        }
    }
}
