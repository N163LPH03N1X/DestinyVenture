using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Switch : MonoBehaviour {

    AudioSource audioSrc;
    public AudioSource audioSrc2;
    public AudioClip pressedSwitch;
    public AudioClip activateSfx;
    public AudioClip wrongSfx;

    MeshRenderer meshRend;
    Color meshRendOrg;
    public GameObject[] switchOffObjects;
    public GameObject[] switchOnObjects;
    bool changeColor;
    PlayerStats PlayST;
    GameObject Player;
    bool ShakeScreen;
    float timer;
    bool Pressed;
    public bool fake;
    public bool Momentary;
    public bool Quake;
    bool startQuake;
 
    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        meshRend = GetComponent<MeshRenderer>();
        meshRendOrg = meshRend.material.GetColor("_EmissionColor");
        for (int i = 0; i < switchOnObjects.Length; i++)
        {
            switchOnObjects[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < switchOffObjects.Length; i++)
        {
            switchOffObjects[i].gameObject.SetActive(true);
        }

    }
    private void Update()
    {
        if (changeColor)
        {
           
            meshRend.material.SetColor("_EmissionColor", Color.red);
        }
        else
        {
            meshRend.material.SetColor("_EmissionColor", meshRendOrg);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") )
        {
            if (!Pressed)
            {
                if (!fake)
                {
                    audioSrc2.PlayOneShot(activateSfx);
                }
                else if (fake)
                {
                    audioSrc2.PlayOneShot(wrongSfx);
                }
                if (Quake)
                {
                    PlayST = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
                    PlayST.QuakeState();
                }
                changeColor = true;
                audioSrc.PlayOneShot(pressedSwitch);
                for (int i = 0; i < switchOffObjects.Length; i++)
                {
                    switchOffObjects[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < switchOnObjects.Length; i++)
                {
                    switchOnObjects[i].gameObject.SetActive(true);
                }
                Pressed = true;
            }
        
        }
        if (col.gameObject.CompareTag("PowerBlockSwitch"))
        {
            if (!Pressed)
            {
                if (!fake)
                {
                    audioSrc2.PlayOneShot(activateSfx);
                }
                else if (fake)
                {
                    audioSrc2.PlayOneShot(wrongSfx);
                }
                if (Quake)
                {
                    PlayST = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
                    PlayST.QuakeState();
                }
                changeColor = true;
                audioSrc.PlayOneShot(pressedSwitch);
                for (int i = 0; i < switchOffObjects.Length; i++)
                {
                    switchOffObjects[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < switchOnObjects.Length; i++)
                {
                    switchOnObjects[i].gameObject.SetActive(true);
                }
                Pressed = true;
            }
        }
        if (col.gameObject.CompareTag("StrengthBlockSwitch"))
        {
            if (!Pressed)
            {
                if (!fake)
                {
                    audioSrc2.PlayOneShot(activateSfx);
                }
                else if (fake)
                {
                    audioSrc2.PlayOneShot(wrongSfx);
                }
                if (Quake)
                {
                    PlayST = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
                    PlayST.QuakeState();
                }
                changeColor = true;
                audioSrc.PlayOneShot(pressedSwitch);
                for (int i = 0; i < switchOffObjects.Length; i++)
                {
                    switchOffObjects[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < switchOnObjects.Length; i++)
                {
                    switchOnObjects[i].gameObject.SetActive(true);
                }
                Pressed = true;
            }
        }
        if (col.gameObject.CompareTag("GripBlockSwitch"))
        {
            if (!Pressed)
            {
                if (!fake)
                {
                    audioSrc2.PlayOneShot(activateSfx);
                }
                else if (fake)
                {
                    audioSrc2.PlayOneShot(wrongSfx);
                }
                if (Quake)
                {
                    PlayST = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
                    PlayST.QuakeState();
                }
                changeColor = true;
                audioSrc.PlayOneShot(pressedSwitch);
                for (int i = 0; i < switchOffObjects.Length; i++)
                {
                    switchOffObjects[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < switchOnObjects.Length; i++)
                {
                    switchOnObjects[i].gameObject.SetActive(true);
                }
                Pressed = true;
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (Momentary)
        {
            if (col.gameObject.CompareTag("PowerBlockSwitch"))
            {
                changeColor = false;
                audioSrc.PlayOneShot(pressedSwitch);
                for (int i = 0; i < switchOffObjects.Length; i++)
                {
                    switchOffObjects[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < switchOnObjects.Length; i++)
                {
                    switchOnObjects[i].gameObject.SetActive(false);
                }
                Pressed = false;
            }
            if (col.gameObject.CompareTag("Player"))
            {
                changeColor = false;
                audioSrc.PlayOneShot(pressedSwitch);
                for (int i = 0; i < switchOffObjects.Length; i++)
                {
                    switchOffObjects[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < switchOnObjects.Length; i++)
                {
                    switchOnObjects[i].gameObject.SetActive(false);
                }
                Pressed = false;
            }
            if (col.gameObject.CompareTag("StrengthBlockSwitch"))
            {
                if (!Pressed)
                {
                
                    changeColor = true;
                    audioSrc.PlayOneShot(pressedSwitch);
                    for (int i = 0; i < switchOffObjects.Length; i++)
                    {
                        switchOffObjects[i].gameObject.SetActive(false);
                    }
                    for (int i = 0; i < switchOnObjects.Length; i++)
                    {
                        switchOnObjects[i].gameObject.SetActive(true);
                    }
                    Pressed = true;
                }
            }
            if (col.gameObject.CompareTag("GripBlockSwitch"))
            {
                if (!Pressed)
                {
  
                    changeColor = true;
                    audioSrc.PlayOneShot(pressedSwitch);
                    for (int i = 0; i < switchOffObjects.Length; i++)
                    {
                        switchOffObjects[i].gameObject.SetActive(false);
                    }
                    for (int i = 0; i < switchOnObjects.Length; i++)
                    {
                        switchOnObjects[i].gameObject.SetActive(true);
                    }
                    Pressed = true;
                }
            }
            
        }
       
    }
}
