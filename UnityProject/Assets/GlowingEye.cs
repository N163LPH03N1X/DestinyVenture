using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingEye : MonoBehaviour {
    // Use this for initialization


    Color meshColor;

	void Start () {
        meshColor = GetComponent<MeshRenderer>().material.color;
    }
	
    // Update is called once per frame
    void Update () {
        meshColor = new Color(Mathf.PingPong(Time.time, 1), Mathf.PingPong(Time.time, 1), Mathf.PingPong(Time.time, 1), 1);
        GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", meshColor);
     

    }

}
