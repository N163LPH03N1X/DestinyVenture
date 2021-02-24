using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalPortal : MonoBehaviour {

    AudioSource audioSrc;
    public AudioClip telePort;
    Interaction PlayInt;
    PlayerStats PlayST;
    FadeScreen fadeScreen;
    GameObject Player;
    Image BlackScreen;
    public Transform bossArenaPos;
    bool inTerritory;
    bool TelePort;
    float Timer;
	// Use this for initialization
	void Start () {
        audioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Timer > 0)
        {
           
            Timer -= Time.deltaTime;
        }
        else if (Timer < 0)
        {
            StartCoroutine(FadeTo(0.0f, 1.0f, BlackScreen, false));
            Player = GameObject.FindGameObjectWithTag("Player");
            Player.transform.position = bossArenaPos.transform.position;
            Timer = 0;
        }

        if (inTerritory)
        {
          
            if (Input.GetButtonDown("Open") && !TelePort)
            {
                BlackScreen = GameObject.FindGameObjectWithTag("Black").GetComponent<Image>();
                StartCoroutine(FadeTo(1.0f, 1.0f, BlackScreen, true));
                PlayInt = GameObject.FindGameObjectWithTag("Player").GetComponent<Interaction>();
                PlayInt.EnableAUI(false, null);
                audioSrc.PlayOneShot(telePort);
                Timer = 5;
                TelePort = true;
            }
        }
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
            PlayInt = GameObject.FindGameObjectWithTag("Player").GetComponent<Interaction>();
            PlayST = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
            PlayInt.EnableAUI(true, "Enter");
            PlayST.SendMainMSG(1, "???", 3);
            inTerritory = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayInt = GameObject.FindGameObjectWithTag("Player").GetComponent<Interaction>();
            PlayInt.EnableAUI(false, null);
            inTerritory = false;
        }
    }
    IEnumerator FadeTo(float aValue, float aTime, Image image, bool In)
    {
        bool loaded = false;
        float alpha = image.GetComponent<Image>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
            image.GetComponent<Image>().color = newColor;
            if (t >= 0.9 && !loaded)
            {
                if (In)
                {
                    newColor.a = 1;
                    image.GetComponent<Image>().color = newColor;
                }
                else if (!In)
                {
                    newColor.a = 0;
                    image.GetComponent<Image>().color = newColor;
                }
            }
            else
            {
                image.GetComponent<Image>().enabled = true;
            }
            yield return null;
        }

    }
}
