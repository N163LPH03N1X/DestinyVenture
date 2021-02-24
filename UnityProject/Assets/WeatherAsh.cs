using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherAsh : MonoBehaviour {
    ParticleSystem AshSystem;

    GameObject Ash;
    bool isDrowning;
    public static bool active;
    bool water;



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
                SwitchWeather(CurWeather.ash, true);
            }
            water = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            active = true;
            SwitchWeather(CurWeather.ash, true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            active = false;
            SwitchWeather(CurWeather.ash, false);
        }
    }
    public enum CurWeather {ash, off}
    public void SwitchWeather(CurWeather state, bool True)
    {
        if (True)
        {
            switch (state)
            {
               
                case CurWeather.ash:
                    {
                        Ash = GameObject.Find("GameControl/Player/PlayerController/Ash");
                        Ash.SetActive(true);
                        AshSystem = GameObject.Find("GameControl/Player/PlayerController/Ash").GetComponent<ParticleSystem>();
                        AshSystem.Play();
                        break;
                    }
                case CurWeather.off:
                    {
                        Ash = GameObject.Find("GameControl/Player/PlayerController/Ash");
                        Ash.SetActive(false);
                        break;
                    }


            }
        }
        else
        {
            switch (state)
            {
                case CurWeather.ash:
                    {
                        Ash = GameObject.Find("GameControl/Player/PlayerController/Ash");
                        Ash.SetActive(false);
                        break;
                    }
              
            }
        }
       
    }
}
