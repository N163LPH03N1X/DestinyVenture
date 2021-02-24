using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {
    GameObject Player;
    AudioSource GameaudioSrc;
    AudioSource BattleaudioSrc;
    AudioClip bossMusic;
    AudioClip GameMusic;

    public AudioClip OverWorldTrack;
    public AudioClip FireTempleTrack;
    public AudioClip PhantomTempleTrack;
    public AudioClip IceTempleTrack;
    public AudioClip FinalTempleTrack;
    public AudioClip BossEncounterTrack;

    GameObject GameMusicPlayer;
    GameObject BattleMusicPlayer;

    //public bool overWorld;
    //public bool fireTemple;
    //public bool phantomTemple;
    //public bool iceTemple;
    //public bool finalTemple;
    //public bool bossEncounter;


    
    IEnumerator FadeIn(AudioSource audio)
    {
        float MaxVol = 1;
        audio.Play();
        for (float f = 0f; f <= MaxVol; f += 0.05f)
        {

            audio.volume = f;
            yield return new WaitForSeconds(.1f);
            if (f >= 0.9)
            {
                audio.volume = MaxVol;
            }
        }
    }
    IEnumerator FadeOut(AudioSource audio)
    {
        float MinVol = 0;
        for (float f = 1f; f >= MinVol; f -= 0.05f)
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
}
