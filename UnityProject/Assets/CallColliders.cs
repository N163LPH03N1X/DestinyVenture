using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallColliders : MonoBehaviour {

    public GameObject GhostParentObject;
    GhostBoss ghost;
    public SphereCollider rightHandCollider;

    public void ActivateRightHandCollider(int active)
    {
        if (active == 0)
        {
            rightHandCollider.enabled = false;
        }
        else
        {
            rightHandCollider.enabled = true;
        }
    }
    public void ActivateShot(int active)
    {
        if (active == 0)
        {
            ghost = GhostParentObject.GetComponent<GhostBoss>();
            ghost.PowerMove();
        }
        else
        {
            ghost = GhostParentObject.GetComponent<GhostBoss>();
            ghost.Shoot();
        }
    }
}
