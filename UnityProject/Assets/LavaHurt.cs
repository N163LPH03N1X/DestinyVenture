using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaHurt : MonoBehaviour {
    Vector3 Originalpos;
    Vector3 MaxPos;
    public bool FreezePosition;
    public GameObject FlowingLava;
	// Use this for initialization
	void Start () {

        Originalpos = transform.position;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.localPosition.y >= 0 && !FreezePosition)
        {
            Destroy(FlowingLava);
            Originalpos = transform.position;
            FreezePosition = true;
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position = Originalpos;
        }
    }
}
