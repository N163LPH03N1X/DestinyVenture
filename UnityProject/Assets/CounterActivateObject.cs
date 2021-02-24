using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterActivateObject : MonoBehaviour {

    public int Count;
    public int ObjectCount;
    public bool FireTorch;
    public bool ThunderTorch;


    void Update()
    {
        if (ThunderTorch)
        {
            Count = CounterActivateObjects.count;

            if (Count >= ObjectCount)
            {
                gameObject.SetActive(false);
            }
        }
        else if (FireTorch)
        {
            Count = CounterActivateFireObjects.count;

            if (Count >= ObjectCount)
            {
                gameObject.SetActive(false);
            }
        }
        

    }
}
