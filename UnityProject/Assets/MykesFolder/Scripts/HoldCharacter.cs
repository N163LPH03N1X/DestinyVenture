using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldCharacter : MonoBehaviour {

    GameObject gameCtrl;


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.transform.parent = gameObject.transform;
        }
        
    }
    void OnTriggerExit(Collider col)
    {
        gameCtrl = GameObject.Find("/GameControl/Player/");
        col.gameObject.transform.parent = gameCtrl.transform;
    }
  
}
