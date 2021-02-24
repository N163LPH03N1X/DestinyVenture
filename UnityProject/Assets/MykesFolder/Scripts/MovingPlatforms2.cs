﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms2 : MonoBehaviour {

    public Transform movingPlatform;
    public Transform position1;
    public Transform position2;
    public Transform position3;
    public Transform position4;
    public Transform position5;
    public Transform position6;
    public Vector3 newPosition;
    public string currentState;
    public float smooth;
    public float resetTime;

    // Use this for initialization
    void Start () {
        ChangeTarget();
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        movingPlatform.position = Vector3.Lerp(movingPlatform.position, newPosition, smooth * Time.deltaTime);
		
	}
    void ChangeTarget()
    {
        if (currentState == "Moving To Position 1")
        {
            currentState = "Moving To Position 2";
            newPosition = position2.position;
        }
        else if(currentState == "Moving To Position 2")
        {
            currentState = "Moving To Position 3";
            newPosition = position3.position;
        }
        else if (currentState == "Moving To Position 3")
        {
            currentState = "Moving To Position 4";
            newPosition = position4.position;
        }
        else if (currentState == "Moving To Position 4")
        {
            currentState = "Moving To Position 5";
            newPosition = position5.position;
        }
        else if (currentState == "Moving To Position 5")
        {
            currentState = "Moving To Position 6";
            newPosition = position6.position;
        }
        else if (currentState == "Moving To Position 6")
        {
            currentState = "Moving To Position 1";
            newPosition = position1.position;
        }
       
        else if (currentState == "")
        {
            currentState = "Moving To Position 1";
            newPosition = position1.position;
        }
        Invoke("ChangeTarget", resetTime);
    }
}
