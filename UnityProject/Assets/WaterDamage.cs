using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterDamage : MonoBehaviour {
    AudioSource playerAudioSrc;
    AudioSource musicAudioSrc;
    AudioSource ambientAudioSrc;
    public AudioClip Splash;
    public AudioClip Underwater;
    public AudioClip Breath;
    public AudioClip nightTime;
    public AudioClip dayTime;
    public static bool isDrowning;
    public Image UnderWater;
    public Image airFillAmount;
    public Text AirText;
    PlayerStats PlayST;
    public GameObject PlayCTRL;
    public GameObject AirBanner;
    float Timer;
    float air = 100;
    public float drownSpeed = 3;
    public float SetTimer = 0.1f;
    bool Day;
    bool Night;
    bool isJumping;
    AnimatedSword animSword;
    public GameObject swordObj;
    bool isLoading;
    // Use this for initialization
    void Start () {
        AirBanner.SetActive(false);
        playerAudioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
   
        handler();
        if (isDrowning)
        {
            AirBanner.SetActive(true);
            if (air > 0)
            {
                air -= Time.deltaTime * drownSpeed;
            }
            if (air < 0)
            {
                air = 0;
                Timer = SetTimer;
            }
            if (Timer > 0 && air <= 0)
            {
                Timer -= Time.deltaTime;
            }
            if (Timer < 0 && air <= 0)
            {
                PlayST = PlayCTRL.GetComponent<PlayerStats>();
                PlayST.doDamage(1);
                Timer = SetTimer;
            }
        }
        else
        {
            AirBanner.SetActive(false);
        }
	}
    void handler()
    {
        airFillAmount.fillAmount = Map(air, 0, 100, 0, 1);
        SetCountAir();
    }
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    void SetCountAir()
    {
        AirText.text = Mathf.CeilToInt(air).ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        isLoading = SceneLoader.isLoading;
        if (other.gameObject.CompareTag("Water") && !isLoading)
        {
            animSword = swordObj.GetComponent<AnimatedSword>();
            animSword.SelectAnimation(AnimatedSword.PlayerAnimation.Rebind, true);
            playerAudioSrc = GetComponent<AudioSource>();
            musicAudioSrc = GameObject.Find("GameControl/MusicPlayerGame").GetComponent<AudioSource>();
            musicAudioSrc.volume = 0.5f;
            ambientAudioSrc = GameObject.Find("GameControl/MusicPlayerAmbient").GetComponent<AudioSource>();
            ambientAudioSrc.volume = 1f;
            ambientAudioSrc.clip = Underwater;
            ambientAudioSrc.Play();
            isJumping = PlayerController.isJumping;
            if (isJumping)
            {
                playerAudioSrc.PlayOneShot(Splash);
            }
            StartCoroutine(FadeIn(UnderWater, 0.1f));
            isDrowning = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isLoading = SceneLoader.isLoading;
        if (other.gameObject.CompareTag("Water")&& !isLoading)
        {
            animSword = swordObj.GetComponent<AnimatedSword>();
            animSword.SelectAnimation(AnimatedSword.PlayerAnimation.Rebind, true);
            playerAudioSrc = GetComponent<AudioSource>();
            playerAudioSrc.PlayOneShot(Breath);
            musicAudioSrc = GameObject.Find("GameControl/MusicPlayerGame").GetComponent<AudioSource>();
            musicAudioSrc.volume = 1f;
            ambientAudioSrc = GameObject.Find("GameControl/MusicPlayerAmbient").GetComponent<AudioSource>();
            ambientAudioSrc.volume = 0.5f;
            Day = DayNightScript.Day;
            Night = DayNightScript.Night;
            if (Day)
            {
                ambientAudioSrc.clip = dayTime;
                ambientAudioSrc.Play();
            }
            else if (Night)
            {

                ambientAudioSrc.clip = nightTime;
                ambientAudioSrc.Play();
            }
            AirBanner.SetActive(false);
            ResetDrown();
            StartCoroutine(FadeOut(UnderWater, 0.1f));
        }
    }
    public IEnumerator FadeIn(Image image, float speed)
    {
        image.enabled = true;
        float MaxAlpha = 1;
        Color color = image.color;
        for (color.a = 0f; color.a <= MaxAlpha; color.a += speed)
        {
            image.color = new Color(color.r, color.g, color.b, color.a);
            yield return new WaitForSeconds(0.01f);
            if (color.a >= 0.9)
            {
                color.a = MaxAlpha;
                image.color = new Color(color.r, color.g, color.b, 1);
            }
        }
    }
    public IEnumerator FadeOut(Image image, float speed)
    {
        Color color = image.color;
        float MinAlpha = 0;
        for (color.a = 1f; color.a >= MinAlpha; color.a -= speed)
        {
            image.color = new Color(color.r, color.g, color.b, color.a);
            yield return new WaitForSeconds(0.01f);
            if (color.a <= 0.1)
            {
                color.a = MinAlpha;
                image.color = new Color(color.r, color.g, color.b, 0);
                image.enabled = false;
            }
        }
    }
    public void ResetDrown()
    {
        StartCoroutine(FadeOut(UnderWater, 0.1f));
        isDrowning = false;
        Timer = SetTimer;
        air = 100;
    }
}
