using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementDamage : MonoBehaviour {
    PlayerStats PlayST;
    bool FlameBreastActive;
    bool PhantomBreastActive;
    bool FrostBreastActive;
    bool PhantomBreastEnabled;
    public bool FireWall;
    public bool ShadowWall;
    public bool FrostWall;

    public static bool activateBreast;

    Image shadowOverlay;
    GameObject ShadowScreenOverlay;
    Color colorScreen;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if (FireWall)
            {
                FlameBreastActive = Breast.FlameBreastActive;
                if (!FlameBreastActive)
                {
                    PlayST = other.gameObject.GetComponent<PlayerStats>();
                    PlayST.doDamage(202);
                    PlayST.SendMainMSG(2, "You were burned to Ash.", 3);
                    activateBreast = false;
                }
                else if(FlameBreastActive)
                {
                    activateBreast = true;
                }
            }
            else if (ShadowWall)
            {
                PhantomBreastActive = Breast.PhantBreastActive;
                if (!PhantomBreastActive)
                {
                    ShadowScreenOverlay = GameObject.Find("/GameControl/Player/InGameCanvas/ShadowScreen/");
                    shadowOverlay = ShadowScreenOverlay.GetComponent<Image>();
                    PlayST = other.gameObject.GetComponent<PlayerStats>();
                    PlayST.doDamage(202);
                    PlayST.SendMainMSG(2, "You were consumed by Shadows.", 3);
                    colorScreen = shadowOverlay.color;
                    shadowOverlay.color = new Color(colorScreen.r, colorScreen.g, colorScreen.b, 0);
                    activateBreast = false;
                }
                else if(PhantomBreastActive)
                {
                    activateBreast = true;
                }

            }
            else if (FrostWall)
            {
                FrostBreastActive = Breast.FrostBreastActive;
                if (!FrostBreastActive)
                {
                    PlayST = other.gameObject.GetComponent<PlayerStats>();
                    PlayST.doDamage(202);
                    PlayST.SendMainMSG(2, "You froze to death.", 3);
                    activateBreast = false;
                }
                else if (FrostBreastActive)
                {
                    activateBreast = true;
                }
            }
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            activateBreast = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PhantomBreastEnabled = Breast.PhantomBreast;
            if (!PhantomBreastEnabled)
            {
                ImpactReceiver Impact = collision.gameObject.GetComponent<ImpactReceiver>();
                Vector3 direction = (this.transform.position - collision.transform.position).normalized;
                Impact.AddImpact(direction, 100);
            }
        }

    }
}
