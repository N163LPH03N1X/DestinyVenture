using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSnow : MonoBehaviour {
    AudioSource ambientAudioSrc;
    ParticleSystem PartSysElement;
    ParticleSystem PartSysMist;
    ParticleSystem PartSysExplosion;
    public ParticleSystem Clouds;

    public AudioClip wind;
    public AudioClip nightTime;
    public AudioClip dayTime;
    GameObject Snow;
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
                SwitchWeather(CurWeather.snow, true);
            }
            water = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            active = true;
            SwitchWeather(CurWeather.snow, true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Day = DayNightScript.Day;
            Night = DayNightScript.Night;
            active = false;
            SwitchWeather(CurWeather.snow, false);
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
    public enum CurWeather {snow, off}
    public void SwitchWeather(CurWeather state, bool True)
    {
        if (True)
        {
            switch (state)
            {
               
                case CurWeather.snow:
                    {
                        var size = Clouds.main;
                        size.startSize = 22000;
                        size.simulationSpeed = 5;
                        Clouds.Play();
                        Snow = GameObject.Find("GameControl/Player/PlayerController/Snow");
                        Snow.SetActive(true);
                        PartSysElement = GameObject.Find("GameControl/Player/PlayerController/Snow/RainFallParticleSystem").GetComponent<ParticleSystem>();
                        PartSysElement.Play();
                        PartSysMist = GameObject.Find("GameControl/Player/PlayerController/Snow/RainMistParticleSystem").GetComponent<ParticleSystem>();
                        PartSysMist.Play();
                        PartSysExplosion = GameObject.Find("GameControl/Player/PlayerController/Snow/RainExplosionParticleSystem").GetComponent<ParticleSystem>();
                        PartSysExplosion.Play();
                        ambientAudioSrc = GameObject.Find("GameControl/MusicPlayerAmbient").GetComponent<AudioSource>();
                        ambientAudioSrc.volume = 0.8f;
                        ambientAudioSrc.clip = wind;
                        StartCoroutine(AudioFadeIn(ambientAudioSrc));
                        break;
                    }
                case CurWeather.off:
                    {
                        Snow = GameObject.Find("GameControl/Player/PlayerController/Snow");
                        Snow.SetActive(false);
                        break;
                    }


            }
        }
        else
        {
            switch (state)
            {
                case CurWeather.snow:
                    {
                        var size = Clouds.main;
                        size.startSize = 4000;
                        size.simulationSpeed = 1;
                        Clouds.Stop();
                        Clouds.Play();
                        Snow = GameObject.Find("GameControl/Player/PlayerController/Snow");
                        Snow.SetActive(false);
                        break;
                    }
              
            }
        }
       
    }
}
