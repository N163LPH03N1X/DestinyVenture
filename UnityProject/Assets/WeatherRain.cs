using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherRain : MonoBehaviour {
    AudioSource ambientAudioSrc;
    ParticleSystem PartSysElement;
    ParticleSystem PartSysMist;
    ParticleSystem PartSysExplosion;
    public ParticleSystem Clouds;
    public AudioClip storm;
    public AudioClip nightTime;
    public AudioClip dayTime;
    GameObject Rain;
    bool isDrowning;
    public static bool active;
    bool water;
    public bool Day;
    public bool Night;


    private void Update()
    {
        isDrowning = WaterDamage.isDrowning;
       
        if (isDrowning && !water)
        {
            SwitchWeather(CurWeather.off, true);
            water = true;
        }
        else if (!isDrowning && water)
        {
            if (active)
            {
                SwitchWeather(CurWeather.rain, true);
            }
            water = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            active = true;
            SwitchWeather(CurWeather.rain, true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Day = DayNightScript.Day;
            Night = DayNightScript.Night;
            active = false;
            SwitchWeather(CurWeather.rain, false);
            if (Day)
            {
                ambientAudioSrc = GameObject.Find("GameControl/MusicPlayerAmbient").GetComponent<AudioSource>();
                ambientAudioSrc.volume = 0.5f;
                ambientAudioSrc.clip = dayTime;
                StartCoroutine(AudioFadeIn(ambientAudioSrc));
            }
            else if (Night)
            {
                ambientAudioSrc = GameObject.Find("GameControl/MusicPlayerAmbient").GetComponent<AudioSource>();
                ambientAudioSrc.volume = 0.5f;
                ambientAudioSrc.clip = nightTime;
                StartCoroutine(AudioFadeIn(ambientAudioSrc));
            }
        }
    }
    IEnumerator AudioFadeOut(AudioSource audio)
    {
        float MinVol = 0;
        for (float f = 1f; f > MinVol; f -= 0.05f)
        {
            audio.volume = f;
            yield return new WaitForSeconds(.1f);
            if (f <= 0.1)
            {
                audio.Stop();
                audio.volume = MinVol;
            }
        }
    }
    IEnumerator AudioFadeIn(AudioSource audio)
    {
        float MaxVol = 1;
        audio.Play();
        for (float f = 0f; f < MaxVol; f += 0.05f)
        {
            audio.volume = f;
            yield return new WaitForSeconds(.1f);
            if (f >= 0.9)
            {
                audio.volume = MaxVol;
            }
        }
    }
    public enum CurWeather { rain, off}
    public void SwitchWeather(CurWeather state, bool True)
    {
        if (True)
        {
            switch (state)
            {
                case CurWeather.rain:
                    {
                        var size = Clouds.main;
                        size.startSize = 28000;
                        Clouds.Play();
                        Rain = GameObject.Find("GameControl/Player/PlayerController/Rain");
                        Rain.SetActive(true);
                        PartSysElement = GameObject.Find("GameControl/Player/PlayerController/Rain/RainFallParticleSystem").GetComponent<ParticleSystem>();
                        PartSysElement.Play();
                        PartSysMist = GameObject.Find("GameControl/Player/PlayerController/Rain/RainMistParticleSystem").GetComponent<ParticleSystem>();
                        PartSysMist.Play();
                        PartSysExplosion = GameObject.Find("GameControl/Player/PlayerController/Rain/RainExplosionParticleSystem").GetComponent<ParticleSystem>();
                        PartSysExplosion.Play();
                        ambientAudioSrc = GameObject.Find("GameControl/MusicPlayerAmbient").GetComponent<AudioSource>();
                        ambientAudioSrc.volume = 0.8f;
                        ambientAudioSrc.clip = storm;
                        StartCoroutine(AudioFadeIn(ambientAudioSrc));
                        break;
                    }
                case CurWeather.off:
                    {

                        Rain = GameObject.Find("GameControl/Player/PlayerController/Rain");
                        Rain.SetActive(false);
                        break;
                    }
            }
        }
        else
        {
            switch (state)
            {
                case CurWeather.rain:
                    {
                        var size = Clouds.main;
                        size.startSize = 4000;
                        size.simulationSpeed = 1;
                        Clouds.Stop();
                        Clouds.Play();
                        Rain = GameObject.Find("GameControl/Player/PlayerController/Rain");
                        Rain.SetActive(false);
                        break;
                    }
            }
        }
       
    }
}
