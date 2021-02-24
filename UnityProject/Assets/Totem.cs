using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour {

    Interaction PlayInt;
    AudioSource audioSrc;
    public AudioClip insert;
    public static int count;
    int crystalKeys;
    bool insertKey;
    bool inTerritory;
    public GameObject CrystalObject;
    public GameObject Symbol;
    MeshRenderer symbolOrgColor;

    public bool FlameCrystal;
    public bool PhantomCrystal;
    public bool FrostCrystal;

    // Use this for initialization
    void Start () {
        audioSrc = GetComponent<AudioSource>();
        symbolOrgColor = Symbol.GetComponent<MeshRenderer>();
        symbolOrgColor.material.SetColor("_EmissionColor", Color.black);
        CrystalObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        crystalKeys = TempleKeys.CrystalKeys;
        if (crystalKeys == 3 && inTerritory)
        {
            if (Input.GetButtonDown("Open") && !insertKey)
            {
                PlayInt = GameObject.FindGameObjectWithTag("Player").GetComponent<Interaction>();
                PlayInt.EnableAUI(false, null);
                symbolOrgColor.material.SetColor("_EmissionColor", Color.white);
                CrystalObject.SetActive(true);
                audioSrc.PlayOneShot(insert);
                count++;
                insertKey = true;
            }
        }
	}
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !insertKey)
        {
            PlayInt = GameObject.FindGameObjectWithTag("Player").GetComponent<Interaction>();
            if (FlameCrystal)
            {
                PlayInt.EnableAUI(true, "Insert Flame Crystal");
            }
            else if (PhantomCrystal)
            {
                PlayInt.EnableAUI(true, "Insert Phantom Crystal");
            }
            else if (FrostCrystal)
            {
                PlayInt.EnableAUI(true, "Insert Frost Crystal");
            }
            inTerritory = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayInt = GameObject.FindGameObjectWithTag("Player").GetComponent<Interaction>();
            PlayInt.EnableAUI(false, null);
            inTerritory = false;
        }
    }
}
