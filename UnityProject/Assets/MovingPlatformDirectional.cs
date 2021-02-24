using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformDirectional : MonoBehaviour {

    GameObject Player;
    bool PlayerFound;
    public bool MovingForward;
    public bool MovingBackward;
    public bool MovingLeft;
    public bool MovingRight;
    public bool MovingUp;
    public bool MovingDown;
    public bool StartMoving;
    public float MovingSpeed;
    public bool OneWay;
    public float PlatformMaxDistance;
    public float PlatformMinDistance;
    public bool inTerritory;
    public bool AutoWay;
    public bool KeepMoving;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inTerritory)
        {
            MovePlatform();
        }
    }
    public void MovePlatform()
    {
        if (StartMoving)
        {
            if (MovingUp)
            {
                transform.Translate(Vector3.up * Time.deltaTime * MovingSpeed);

                if (transform.localPosition.y > PlatformMaxDistance)
                {
                    Vector3 newPosition = transform.localPosition;
                    newPosition.y = PlatformMaxDistance;
                    transform.localPosition = newPosition;
                    MovingUp = false;
                    MovingDown = true;
                    MovingForward = false;
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
                        MovingUp = false;
                        MovingDown = false;
                        MovingForward = false;
                        MovingBackward = false;
                        MovingLeft = false;
                        MovingRight = false;
                        StartMoving = false;
                    }
                }

            }
            else if (MovingDown)
            {
                transform.Translate(Vector3.down * Time.deltaTime * MovingSpeed);

                if (transform.localPosition.y < PlatformMinDistance)
                {
                    Vector3 newPosition = transform.localPosition;
                    newPosition.y = PlatformMinDistance;
                    transform.localPosition = newPosition;
                    MovingUp = true;
                    MovingDown = false;
                    MovingForward = false;
                    MovingBackward = false;
                    MovingLeft = false;
                    MovingRight = false;
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
                        MovingForward = false;
                        MovingBackward = false;
                        MovingLeft = false;
                        MovingRight = false;
                        StartMoving = false;
                    }
                }
            }
            else if (MovingForward)
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
                    MovingUp = false;
                    MovingDown = false;
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
                        MovingUp = false;
                        MovingDown = false;
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
                    MovingUp = false;
                    MovingDown = false;
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
                        MovingUp = false;
                        MovingDown = false;
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
                    MovingUp = false;
                    MovingDown = false;
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
                        MovingUp = false;
                        MovingDown = false;
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
                    MovingUp = false;
                    MovingDown = false;
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
        if (other.gameObject.CompareTag("Player"))
        {
            inTerritory = true;
            StartMoving = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            if (!KeepMoving)
            {
                inTerritory = false;
            }
        }
    }
}

//Platform 1
//moving: right
//movingspeed: 10
//PlatformMaxDistance: 290
//PlatformMinDistance: 390

//Platform 2
//moving: backward
//movingspeed: 10
//PlatformMaxDistance: -70
//PlatformMinDistance: -130

//Platform 3
//moving: left
//movingspeed: 10
//PlatformMaxDistance: 278.5
//PlatformMinDistance: 380

//Platform 4
//moving: left
//movingspeed: 10
//PlatformMaxDistance: 160
//PlatformMinDistance: 212.5

//Platform 5
//moving: up
//movingspeed: 10
//PlatformMaxDistance: 29
//PlatformMinDistance: -1

//Platform 6
//moving: forward
//movingspeed: 15
//PlatformMaxDistance: -180
//PlatformMinDistance: -260

//Platform 7
//moving: forward
//movingspeed: 15
//PlatformMaxDistance: -50.5
//PlatformMinDistance: -170

//Platform 8
//moving: right
//movingspeed: 10
//PlatformMaxDistance: 180
//PlatformMinDistance: 270

//Platform 9
//moving: down
//movingspeed: 10
//PlatformMaxDistance: 0
//PlatformMinDistance: -46

//Platform 10
//moving: up
//movingspeed: 10
//PlatformMaxDistance: 39
//PlatformMinDistance: 0
