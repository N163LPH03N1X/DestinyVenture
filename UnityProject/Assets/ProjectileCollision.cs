using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour {

    ImpactReceiver impact;
    AudioSource audioSrc;
    public AudioClip collide;
    // Use this for initialization
    PlayerStats PlayST;
    public int SetDamage;
    public bool Shield;
	void Start () {
        audioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 direction = (this.transform.position - collision.transform.position).normalized;
            impact = collision.gameObject.GetComponent<ImpactReceiver>();
            PlayST = collision.gameObject.GetComponent<PlayerStats>();

            audioSrc.PlayOneShot(collide);
            impact.AddImpact(direction, 100);
            PlayST.doDamage(SetDamage);
            if (!Shield)
            {
                Destroy(gameObject, 1);
            }
           
        }
        else
        {
            audioSrc.PlayOneShot(collide);
            Destroy(gameObject, 1);
        }

    }
}
