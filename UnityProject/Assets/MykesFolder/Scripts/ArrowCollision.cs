using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollision : MonoBehaviour {

    Rigidbody rb;
    EyeEnemy enemy;
    AudioSource audioSrc;
    public AudioClip ArrowHit;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision other)
    {
        audioSrc.PlayOneShot(ArrowHit);
        if (other.gameObject.CompareTag("Enemy"))
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = true;
            enemy = other.gameObject.GetComponent<EyeEnemy>();
            enemy.DoDamage(1);
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
