using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallCollision : MonoBehaviour {

    AudioSource audioSrc;
    public AudioClip clang;
    public bool playSound;
    Vector3 startVec;
    PlayerStats playST;
    ImpactReceiver Impact;

    // Use this for initialization
    void Start () {
        startVec = gameObject.transform.localPosition;
        audioSrc = GetComponent<AudioSource>();
        
	}
    private void Update()
    {
        if (gameObject.transform.localPosition.y >= startVec.y)
        {
            playSound = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") && !playSound)
        {
            audioSrc.PlayOneShot(clang);
            playSound = true;
        }
        if (other.gameObject.CompareTag("Death"))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Impact = collision.gameObject.GetComponent<ImpactReceiver>();
            Vector3 direction = (collision.transform.position - this.transform.position).normalized;
            Impact.AddImpact(direction, 20);
            playST = collision.gameObject.GetComponent<PlayerStats>();
            playST.doDamage(10);
        }
    }
}
