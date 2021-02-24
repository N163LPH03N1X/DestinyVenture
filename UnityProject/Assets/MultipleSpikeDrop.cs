using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleSpikeDrop : MonoBehaviour {

    [Header("Spike Settings")]
    public bool random;
    public bool allAtOnce;
    public bool linear;
    [Space]
    [Header("Emitters")]
    public Transform PositionOne;
    public Transform PositionTwo;
    public Transform PositionThree;
    public Transform PositionFour;
    public float SetTimer;
    float Timer;
    public GameObject SpikePrefab;
    int count;
    public bool inTerritory = false;
    // Use this for initialization
    void Start() {
        Timer = 0.1f;
    }

    // Update is called once per frame
    void Update() {

        if (inTerritory)
        {
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
            }
            if (Timer < 0)
            {
                if (random)
                {
                    DropSpikes(1);
                }
                else if (allAtOnce)
                {
                    DropSpikes(2);
                }
                else if (linear)
                {
                    count++;
                    DropSpikes(3);

                }
                Timer = SetTimer;
            }
        }
    }
    void DropSpikes(int state)
    {
        if (state == 1)
        {
           //=========================Randomize=====================//
           for(int i = 0; i < 1; i++)
            {
                float randomNumber = Random.Range(1, 5);
                if (randomNumber == 1)
                {
                    Instantiate(SpikePrefab, PositionOne.transform.position, PositionOne.transform.rotation);
                }
                if (randomNumber == 2)
                {
                    Instantiate(SpikePrefab, PositionTwo.transform.position, PositionTwo.transform.rotation);
                }
                if (randomNumber == 3)
                {
                    Instantiate(SpikePrefab, PositionThree.transform.position, PositionThree.transform.rotation);
                }
                if (randomNumber == 4)
                {
                    Instantiate(SpikePrefab, PositionFour.transform.position, PositionFour.transform.rotation);
                }
            }
        }
        else if (state == 2)
        {
            //========================= AllAtOnce =====================//
            Instantiate(SpikePrefab, PositionOne.transform.position, PositionOne.transform.rotation);
            Instantiate(SpikePrefab, PositionTwo.transform.position, PositionTwo.transform.rotation);
            Instantiate(SpikePrefab, PositionThree.transform.position, PositionThree.transform.rotation);
            Instantiate(SpikePrefab, PositionFour.transform.position, PositionFour.transform.rotation);
        }
        else if (state == 3)
        {
            //=========================Linear=====================//

            if (count == 1)
            {
                Instantiate(SpikePrefab, PositionOne.transform.position, PositionOne.transform.rotation);
            }
            else if (count == 2)
            {
                Instantiate(SpikePrefab, PositionTwo.transform.position, PositionTwo.transform.rotation);
                  
            }
            else if (count == 3)
            {
                Instantiate(SpikePrefab, PositionThree.transform.position, PositionThree.transform.rotation);
                 
            }
            else if (count == 4)
            {
                Instantiate(SpikePrefab, PositionFour.transform.position, PositionFour.transform.rotation);
                count = 0;
            }
                

            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inTerritory = true;
        }
        
    }
}

