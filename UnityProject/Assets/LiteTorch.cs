using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LiteTorch : MonoBehaviour
{

    public List<Transform> objects;
    float Timer;
    public float SetTimer;
    public bool Lit;
    AudioSource audioSrc;
    public AudioClip ticking;
    public AudioClip tickOver;
    public AudioClip activation;
    public AudioClip torch;
    bool playtick;
    public float tickTimer;
    float setTickTime;
    public bool activateOnce;
    public bool Thunder;
    public bool Fire;
    public bool TorchOff;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        foreach (Transform child in transform)
        {
            objects.Add(child);
        }
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].gameObject.SetActive(false);
        }

    }
    private void Update()
    {
        if (!activateOnce)
        {
            if (Timer > 0)
            {
                Lit = true;

                Timer -= Time.deltaTime;
                if (Timer > 10)
                {
                    setTickTime = 1f;
                }
                if (Timer < 10 && Timer > 5)
                {
                    setTickTime = 0.5f;
                }
                if (Timer < 5 && Timer > 0)
                {
                    setTickTime = 0.25f;
                }

            }
            if (Timer > SetTimer)
            {
                Timer = SetTimer;
            }
            if (Timer < 0)
            {
                objects.Clear();
                foreach (Transform child in transform)
                {
                    objects.Add(child);
                }
                for (int i = 0; i < objects.Count; i++)
                {
                    objects[i].gameObject.SetActive(false);

                }
                TorchOff = false;
                audioSrc.PlayOneShot(tickOver);
                Lit = false;
                Timer = 0;
            }
            if (Lit)
            {
                if (tickTimer > 0)
                {
                    tickTimer -= Time.deltaTime;
                }
                if (tickTimer < 0)
                {
                    audioSrc.PlayOneShot(ticking);
                    tickTimer = setTickTime;
                }

            }
        }
       
    }

    void OnTriggerEnter(Collider col)
    {
        
        if (Thunder && !TorchOff)
        {
            if (col.tag == "Thunder")
            {
                objects.Clear();
                foreach (Transform child in transform)
                {
                    objects.Add(child);
                }
                for (int i = 0; i < objects.Count; i++)
                {
                    objects[i].gameObject.SetActive(true);

                }
                if (!activateOnce)
                {
                    audioSrc.PlayOneShot(torch);
                    tickTimer = 0.5f;
                    Timer = SetTimer;
                }
                else
                {
                    audioSrc.PlayOneShot(activation);
                }
                TorchOff = true;
            }
            else if (col.tag == "ThunderBlade")
            {
                objects.Clear();
                foreach (Transform child in transform)
                {
                    objects.Add(child);
                }
                for (int i = 0; i < objects.Count; i++)
                {
                    objects[i].gameObject.SetActive(true);

                }
                if (!activateOnce)
                {
                    audioSrc.PlayOneShot(torch);
                    tickTimer = 0.5f;
                    Timer = SetTimer;
                }
                else
                {
                    audioSrc.PlayOneShot(activation);
                }
                TorchOff = true;
            }
         

        }
        else if(Fire && !TorchOff)
        {

            if (col.tag == "Fire")
            {
                objects.Clear();
                foreach (Transform child in transform)
                {
                    objects.Add(child);
                }
                for (int i = 0; i < objects.Count; i++)
                {
                    objects[i].gameObject.SetActive(true);

                }
                if (!activateOnce)
                {
                    audioSrc.PlayOneShot(torch);
                    tickTimer = 0.5f;
                    Timer = SetTimer;
                }
                else
                {
                    audioSrc.PlayOneShot(activation);
                }
                TorchOff = true;
            }
            else if (col.tag == "MagicBlade")
            {
                objects.Clear();
                foreach (Transform child in transform)
                {
                    objects.Add(child);
                }
                for (int i = 0; i < objects.Count; i++)
                {
                    objects[i].gameObject.SetActive(true);

                }
                if (!activateOnce)
                {
                    audioSrc.PlayOneShot(torch);
                    tickTimer = 0.5f;
                    Timer = SetTimer;
                }
                else
                {
                    audioSrc.PlayOneShot(activation);
                }
                TorchOff = true;
            }
            
        }

    }
}

