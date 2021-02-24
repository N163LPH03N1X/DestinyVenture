using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformForward : MonoBehaviour {

    GameObject Player;
    bool PlayerFound;
    public bool MovingForward;
    public bool MovingBackward;
    public bool MovingLeft;
    public bool MovingRight;
    public bool StartMoving;
    public float MovingSpeed;
    public bool OneWay;
    public float PlatformMaxDistance;
    public float PlatformMinDistance;
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
            if (MovingForward)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * MovingSpeed);

                if (transform.localPosition.z > PlatformMaxDistance)
                {
                    Vector3 newPosition = transform.localPosition;
                    newPosition.z = PlatformMaxDistance;
                    transform.localPosition = newPosition;
                    MovingForward = false;
                    MovingBackward = true;
                    MovingLeft = false;
                    MovingRight = false;
                   
                    if (!AutoWay)
                    {
                        StartMoving = false;
                        inTerritory = false;
                    }
                    if (OneWay)
                    {
                        MovingForward = false;
                        MovingBackward = false;
                        MovingLeft = false;
                        MovingRight = false;
                        StartMoving = false;
                    }
                
                }

            }
            else if (MovingBackward)
            {
                transform.Translate(Vector3.back * Time.deltaTime * MovingSpeed);

                if (transform.localPosition.z < PlatformMinDistance)
                {
                    Vector3 newPosition = transform.localPosition;
                    newPosition.z = PlatformMinDistance;
                    transform.localPosition = newPosition;
                    MovingForward = true;
                    MovingBackward = false;
                    MovingLeft = false;
                    MovingRight = false;
                  
                    if (!AutoWay)
                    {
                        StartMoving = false;
                        inTerritory = false;
                    }
                    if (OneWay)
                    {
                        MovingForward = false;
                        MovingBackward = false;
                        MovingLeft = false;
                        MovingRight = false;
                        StartMoving = false;
                    }
          
                }

            }
            else if (MovingLeft)
            {
                transform.Translate(Vector3.left * Time.deltaTime * MovingSpeed);

                if (transform.localPosition.x < PlatformMaxDistance)
                {
                    Vector3 newPosition = transform.localPosition;
                    newPosition.x = PlatformMaxDistance;
                    transform.localPosition = newPosition;
                    MovingForward = false;
                    MovingBackward = false;
                    MovingLeft = false;
                    MovingRight = true;
                   
                    if (!AutoWay)
                    {
                        StartMoving = false;
                        inTerritory = false;
                    }
                    if (OneWay)
                    {
                        MovingForward = false;
                        MovingBackward = false;
                        MovingLeft = false;
                        MovingRight = false;
                        StartMoving = false;
                    }
                   
                }

            }
            else if (MovingRight)
            {
                transform.Translate(Vector3.right * Time.deltaTime * MovingSpeed);

                if (transform.localPosition.x > PlatformMinDistance)
                {
                    Vector3 newPosition = transform.localPosition;
                    newPosition.x = PlatformMinDistance;
                    transform.localPosition = newPosition;
                    MovingForward = false;
                    MovingBackward = false;
                    MovingLeft = true;
                    MovingRight = false;
                   
                    if (!AutoWay)
                    {
                        StartMoving = false;
                        inTerritory = false;
                    }
                    if (OneWay)
                    {
                        MovingForward = false;
                        MovingBackward = false;
                        MovingLeft = false;
                        MovingRight = false;
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
    void OnTriggerExit(Collider other)
    {

        if (other.gameObject == Player)
        {
            inTerritory = false;

        }
    }

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
    
}
