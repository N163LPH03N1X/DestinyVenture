using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerItems : MonoBehaviour {
    public Image PowerGemSLUI;
    public Image PowerGemGDUI;

    public Text PowerGemSLQuanity;
    public Text PowerGemGDQuanity;

    public Text PowerGemSLName;
    public Text PowerGemGDName;

    public int PowerGemSLAmount;
    public int PowerGemGDAmount;

    public static bool PowerGemSLPickedUp = false;
    public static bool PowerGemGDPickedUp = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
