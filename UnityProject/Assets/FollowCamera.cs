using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public float RainHeight = 25.0f;

    [Tooltip("How far the rain particle system is ahead of the player")]
    public float RainForwardOffset = 0.0f;
    public ParticleSystem TheParticleSystem;
    [Tooltip("The top y value of the mist particles")]
    public float RainMistHeight = 0.0f;
    bool FollowTheCamera;
    public Camera Camera;
    // Use this for initialization
    void Start () {
        FollowTheCamera = true;

    }
	
	// Update is called once per frame
	void Update () {
        UpdateRain();
    }
    private void UpdateRain()
    {
        // keep rain and mist above the player
        if (TheParticleSystem != null)
        {
            if (FollowTheCamera)
            {
                TheParticleSystem.transform.position = Camera.transform.position;
                TheParticleSystem.transform.Translate(0.0f, RainHeight, RainForwardOffset);
                TheParticleSystem.transform.rotation = Quaternion.Euler(0.0f, Camera.transform.rotation.eulerAngles.y, 0.0f);
            }
        }
    }
}
