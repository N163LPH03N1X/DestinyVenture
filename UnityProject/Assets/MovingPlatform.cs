using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    GameObject Player;
    bool PlayerFound;
    public bool MovingUp;
    public bool MovingDown;
    public bool StartMoving;
    public float MovingSpeed;
    public bool OneWay;
    public float PlatformMaxHeight;
    public float PlatformMinHeight;
    public bool inTerritory;
    public bool AutoWay;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (!PlayerFound)
        {
            StartCoroutine(FindPlayer());
        }
        if (PlayerFound)
        {
            if (inTerritory)
            { 
                MovePlatform();
            }
        }
    }
    public void MovePlatform()
    {
        if (StartMoving)
        {
            if (MovingUp)
            {
                transform.Translate(Vector3.up * Time.deltaTime * MovingSpeed);

                if (transform.localPosition.y > PlatformMaxHeight)
                {
                    Vector3 newPosition = transform.localPosition;
                    newPosition.y = PlatformMaxHeight;
                    transform.localPosition = newPosition;
                    MovingUp = false;
                    MovingDown = true;
                    if (!AutoWay)
                    {
                        StartMoving = false;
                        inTerritory = false;
                    }
                    if (OneWay)
                    {
                        MovingUp = false;
                        MovingDown = false;
                        StartMoving = false;
                    }
                }

            }
            else if (MovingDown)
            {
                transform.Translate(Vector3.down * Time.deltaTime * MovingSpeed);

                if (transform.localPosition.y < PlatformMinHeight)
                {
                    Vector3 newPosition = transform.localPosition;
                    newPosition.y = PlatformMinHeight;
                    transform.localPosition = newPosition;
                    MovingUp = true;
                    MovingDown = false;
                    if (!AutoWay)
                    {
                        StartMoving = false;
                        inTerritory = false;
                    }
                    else
                    {
                        StartMoving = true;
                        inTerritory = true;
                    }
                    if (OneWay)
                    {
                        MovingUp = false;
                        MovingDown = false;
                        StartMoving = false;
                    }
                }
            }
        }
       
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player )
        {
            inTerritory = true;
            StartMoving = true;
        }
    }
    //void OnTriggerExit(Collider other)
    //{

    //    if (other.gameObject == Player)
    //    {
    //        inTerritory = false;
          
    //    }
    //}

    IEnumerator FindPlayer()
    {
        Player = GameObject.Find("GameControl/Player/PlayerController/");
        if (Player == null)
        {
            yield return null;
        }
        else
        {
            Player = GameObject.Find("GameControl/Player/PlayerController/");
            PlayerFound = true;
        }
    }
    public void PlatformMove()
    {
        inTerritory = true;
        StartMoving = true;
    }
}
