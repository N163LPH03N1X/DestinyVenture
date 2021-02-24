using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoPlay : MonoBehaviour {
    public MovieTexture movTexture;
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.mainTexture = movTexture;
        movTexture.Play();
        movTexture.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
       
	}
}
