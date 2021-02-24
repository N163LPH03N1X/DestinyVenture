using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltWeapon : MonoBehaviour {

    public float MoveAmount = 1;
    public float MoveSpeed = 1;
    public GameObject Gun;
    float MoveOnX;
    float MoveOnY;
    Vector3 defaultPos;
    Vector3 NewGunPos;
    public bool ONOFF;
    bool Pulling;
    bool Pushing;
    bool isDisabled;


    // Use this for initialization
    void Start () {
        //ONOFF = true;
        defaultPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        MoveOnX = Input.GetAxis("Mouse X") * Time.deltaTime * MoveAmount;
        MoveOnY = Input.GetAxis("Mouse Y") * Time.deltaTime * MoveAmount;
        Pulling = PlayerController.isPulling;
        Pushing = PlayerController.isPushing;
        isDisabled = SceneLoader.isDisabled;

        if (ONOFF == true)
        {
            if (!Pushing && !Pulling && !isDisabled)
            {
                NewGunPos = new Vector3(defaultPos.x + MoveOnX, defaultPos.y + MoveOnY, defaultPos.z);
                Gun.transform.localPosition = Vector3.Lerp(Gun.transform.localPosition, NewGunPos, MoveSpeed * Time.deltaTime);
            }
        }
        else
        {
            ONOFF = false;
            Gun.transform.localPosition = Vector3.Lerp(Gun.transform.localPosition, defaultPos, MoveSpeed * Time.deltaTime);
        }
   
    }
}
