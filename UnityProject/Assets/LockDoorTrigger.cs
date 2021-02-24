using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoorTrigger : MonoBehaviour {

    public static bool LockFDoor;
    public static bool LockPDoor;
    public static bool LockIDoor;

    public bool FlameDoor;
    public bool PhantDoor;
    public bool FrostDoor;


    // Use this for initialization
    void Start () {
        LockFDoor = false;
        LockPDoor = false;
        LockIDoor = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (FlameDoor)
            {
                LockFDoor = true;
                LockPDoor = false;
                LockIDoor = false;
            }
            else if (PhantDoor)
            {
                LockFDoor = false;
                LockPDoor = true;
                LockIDoor = false;
            }
            else if (FrostDoor)
            {
                LockFDoor = false;
                LockPDoor = false;
                LockIDoor = true;
            }
        }
    }

}
