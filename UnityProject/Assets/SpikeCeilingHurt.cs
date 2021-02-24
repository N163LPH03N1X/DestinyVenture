using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCeilingHurt : MonoBehaviour {
    public Vector3 Originalpos;
    PlayerStats PlayST;
	// Use this for initialization
	void Start () {

        Originalpos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.localPosition.y <= -22)
        {
            transform.position = Originalpos;
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayST = other.gameObject.GetComponent<PlayerStats>();
            PlayST.doDamage(202);
            transform.position = Originalpos;
        }
    }
}
