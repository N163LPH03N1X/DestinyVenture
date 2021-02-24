using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterActivateFireObjects : MonoBehaviour
{

    public int hitPoint;
    public static int count;
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
    bool GainCounter;
    private void Start()
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
        hitPoint = 0;
    }
    void Update()
    {
        if (hitPoint >= 1 && !GainCounter)
        {
            count++;
            hitPoint = 1;
            GainCounter = true;
        }
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

                audioSrc.PlayOneShot(tickOver);
                Lit = false;
                count--;
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
        if (col.tag == "Fire")
        {
            hitPoint++;
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
        }
        if (col.tag == "MagicBlade")
        {
            hitPoint++;
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
        }
    }
}
