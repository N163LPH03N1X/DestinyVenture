using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDropTrigger : MonoBehaviour {

    public GameObject SpikeBallDrop;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            Destroy(SpikeBallDrop);
        }
    }
}
