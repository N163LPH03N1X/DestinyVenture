using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightScript : MonoBehaviour {

    public Light Sun;
    public Light Moon;
    AudioSource ambientAudioSrc;
    public AudioClip nightTime;
    public AudioClip dayTime;
    public static bool Day;
    public static bool Night;
    public static bool darkEnemies;
    public Material[] Skyboxes;
    public float SecondsInDay = 60f;

    public float CurrentTimeOfDay = 0.25f;
    [Range(0,1)]
    public float TimeMultiplier = 1.0f;

    public float SunInitialIntensity;
    public float MoonInitialIntensity;

    public float SunCurrentIntensity;
    public float MoonCurrentIntensity;
    bool switchSky;
    bool weatherRain;
    bool weatherSnow;

    bool storming;
    // Use this for initialization
    void Start () {
        SunInitialIntensity = Sun.intensity;
        MoonInitialIntensity = Moon.intensity;
        ambientAudioSrc = GameObject.Find("GameControl/MusicPlayerAmbient").GetComponent<AudioSource>();
        ambientAudioSrc.volume = 0.5f;
        ambientAudioSrc.clip = nightTime;
        ambientAudioSrc.Play();
        Night = false;
        Day = true;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateSun();
        weatherRain = WeatherRain.active;
        weatherSnow = WeatherSnow.active;
        CurrentTimeOfDay += (Time.deltaTime / SecondsInDay) * TimeMultiplier;

        if(CurrentTimeOfDay >= 1)
        {
            CurrentTimeOfDay = 0;
        }
        if (!weatherRain && !weatherSnow)
        {
            storming = false;
            //====================Morning=======================//
            if (CurrentTimeOfDay >= 0 && CurrentTimeOfDay < 0.25f && !switchSky)
            {
              
                RenderSettings.skybox = Skyboxes[0];
                DynamicGI.UpdateEnvironment();
                switchSky = true;
            }
            //====================Noon=======================//
            else if (CurrentTimeOfDay >= 0.25f && CurrentTimeOfDay < 0.5f && switchSky)
            {
                if (!Day)
                {
                    ambientAudioSrc = GameObject.Find("GameControl/MusicPlayerAmbient").GetComponent<AudioSource>();
                    ambientAudioSrc.volume = 0.5f;
                    ambientAudioSrc.clip = dayTime;
                    ambientAudioSrc.Play();
                    Night = false;
                    Day = true;
                }
                RenderSettings.skybox = Skyboxes[1];
                DynamicGI.UpdateEnvironment();
                switchSky = false;
            }
            //====================Evening=======================//
            else if (CurrentTimeOfDay >= 0.5f && CurrentTimeOfDay < 0.75f && !switchSky)
            {
               
                RenderSettings.skybox = Skyboxes[2];
                DynamicGI.UpdateEnvironment();
                switchSky = true;
            }
            //====================Night=======================//
            else if (CurrentTimeOfDay >= 0.75f && CurrentTimeOfDay < 1f && switchSky)
            {
                if (!Night)
                {
                    ambientAudioSrc = GameObject.Find("GameControl/MusicPlayerAmbient").GetComponent<AudioSource>();
                    ambientAudioSrc.volume = 0.5f;
                    ambientAudioSrc.clip = nightTime;
                    ambientAudioSrc.Play();
                    Day = false;
                    Night = true;
                }
                RenderSettings.skybox = Skyboxes[3];
                DynamicGI.UpdateEnvironment();
                switchSky = false;
            }
        }
        else if (weatherRain && !storming)
        {
            RenderSettings.skybox = Skyboxes[4];
            DynamicGI.UpdateEnvironment();
            storming = true;
        }
        else if (weatherSnow && !storming)
        {
            RenderSettings.skybox = Skyboxes[5];
            DynamicGI.UpdateEnvironment();
            storming = true;
        }

    }

    void UpdateSun()
    {
        Sun.transform.localRotation = Quaternion.Euler((CurrentTimeOfDay * 360f) - 90, 170, 0);
        //Moon.transform.localRotation = Quaternion.Euler((CurrentTimeOfDay * 360f) - 180, 170, 0);

        float SunIntensityMultiplier = 1;
        //float MoonIntensityMultiplier = 1;
        if (CurrentTimeOfDay <= 0.23f || CurrentTimeOfDay >= 0.75f)
        {
            SunIntensityMultiplier = 0;
            //MoonIntensityMultiplier = 1;
        }
        else if (CurrentTimeOfDay <= 0.25f)
        {
            SunIntensityMultiplier = Mathf.Clamp01((CurrentTimeOfDay - 0.23f) * (1 / 0.02f));
            //MoonIntensityMultiplier = Mathf.Clamp01((CurrentTimeOfDay + 0.23f) * (1 / 0.02f));
        }
        else if (CurrentTimeOfDay >= 0.73f)
        {
            SunIntensityMultiplier = Mathf.Clamp01(1 - ((CurrentTimeOfDay - 0.73f) * (1 / 0.02f)));
            //MoonIntensityMultiplier = Mathf.Clamp01(1 - ((CurrentTimeOfDay + 0.73f) * (1 / 0.02f)));
        }

        Sun.intensity = SunInitialIntensity * SunIntensityMultiplier;
        Moon.intensity = MoonInitialIntensity - (Sun.intensity * MoonInitialIntensity);

        SunCurrentIntensity = Sun.intensity;
        MoonCurrentIntensity = Moon.intensity;
    }
}
