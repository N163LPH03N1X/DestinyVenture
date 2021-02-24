using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ShadowFade : MonoBehaviour {

    public bool Boss;
    AudioSource audioSrc;
    public AudioClip FadeSound;
    public bool Startsound = false;
    GameObject ShadowWave;
    GameObject ShadowScreenOverlay;
    Image shadowOverlay;
    public GameObject shadowCol;
    BoxCollider boxCol;
    SphereCollider sphereCol;
    Color shadowScreen;
    PlayerStats PlayST;
    bool PhantomBreastEnabled;

    void Start()
    {
        if (Boss)
            sphereCol = shadowCol.GetComponent<SphereCollider>();
        else
            boxCol = shadowCol.GetComponent<BoxCollider>();

        audioSrc = GetComponent<AudioSource>();
    }
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayST = other.gameObject.GetComponent<PlayerStats>();
            PhantomBreastEnabled = Breast.PhantomBreast;
            if (PhantomBreastEnabled)
            {
                ShadowWave = GameObject.Find("/GameControl/Player/PlayerController/MainCamera/FireWave/");
                ShadowWave.SetActive(true);
                if (Boss)
                    sphereCol.isTrigger = true;
                else
                    boxCol.isTrigger = true;

                audioSrc.PlayOneShot(FadeSound);
                StartCoroutine(FadeIn());
            }
            else
            {
                if (Boss)
                    sphereCol.isTrigger = false;
                else
                    boxCol.isTrigger = false;

                PlayST.SendMainMSG(2, "it's too Dark, You will be consumed by Shadows!", 3);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (PhantomBreastEnabled)
            {
                audioSrc.PlayOneShot(FadeSound);
            }
            StartCoroutine(FadeOut());
            ShadowWave = GameObject.Find("/GameControl/Player/PlayerController/MainCamera/FireWave/");
            ShadowWave.SetActive(false);
        }
    }
  
    public IEnumerator FadeIn()
    {
        ShadowScreenOverlay = GameObject.Find("/GameControl/Player/InGameCanvas/ShadowScreen/");
       
        shadowOverlay = ShadowScreenOverlay.GetComponent<Image>();
        shadowScreen = shadowOverlay.color;
        float MaxAlpha = 1;
        for (shadowScreen.a = 0f; shadowScreen.a <= MaxAlpha; shadowScreen.a += 0.15f)
        {
            shadowOverlay.color = new Color(shadowScreen.r, shadowScreen.g, shadowScreen.b, shadowScreen.a);
            yield return new WaitForSeconds(.1f);
            if (shadowScreen.a >= 0.9)
            {
                shadowScreen.a = MaxAlpha;
                shadowOverlay.color = new Color(shadowScreen.r, shadowScreen.g, shadowScreen.b, 1);
            }
        }
    }
    public IEnumerator FadeOut()
    {
        ShadowScreenOverlay = GameObject.Find("/GameControl/Player/InGameCanvas/ShadowScreen/");
        shadowOverlay = ShadowScreenOverlay.GetComponent<Image>();
        shadowScreen = shadowOverlay.color;
        float MinAlpha = 0;
        for (shadowScreen.a = 1f; shadowScreen.a >= MinAlpha; shadowScreen.a -= 0.15f)
        {
            shadowOverlay.color = new Color(shadowScreen.r, shadowScreen.g, shadowScreen.b, shadowScreen.a);
            yield return new WaitForSeconds(.1f);
            if (shadowScreen.a <= 0.1)
            {
                shadowScreen.a = MinAlpha;
                shadowOverlay.color = new Color(shadowScreen.r, shadowScreen.g, shadowScreen.b, 0);
            }
        }
    }


}
