using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowScreenUI : MonoBehaviour {

    GameObject ShadowScreenOverlay;
    Image shadowOverlay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShadowScreenOverlay = GameObject.Find("/GameControl/Player/InGameCanvas/ShadowScreen/");
            shadowOverlay = ShadowScreenOverlay.GetComponent<Image>();
            shadowOverlay.enabled = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShadowScreenOverlay = GameObject.Find("/GameControl/Player/InGameCanvas/ShadowScreen/");
            shadowOverlay = ShadowScreenOverlay.GetComponent<Image>();
            shadowOverlay.enabled = false;
        }
    }
}
