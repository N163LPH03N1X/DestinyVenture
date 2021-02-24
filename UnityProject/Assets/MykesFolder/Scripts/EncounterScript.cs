using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterScript : MonoBehaviour {
    GameObject AudioSrcGame;
    GameObject AudioSrcEnemy;
    GameObject Player;
    AudioSource mainAudioSrc;
    AudioSource battleAudioSrc;

    [HideInInspector]
    Vector3 PlayerPosition;
    [HideInInspector]
    Vector3 normalPosition;
    public float attackRange;
    float MaxVol;
    float MinVol;
    public bool MainMusic;
    public bool BattleMusic;
    public bool StopBattleMusic;
    public bool inRange;
    bool BattleMusicPlaying;

    public bool foundMainMusic;
    public bool foundPlayer;
    public bool foundBattleMusic;
    bool EnemyDead;
    public bool otherEnemies = false;
    bool triggered = false;
    Collider other;

    // Use this for initialization
    void Start () {
        battleAudioSrc = GetComponent<AudioSource>();
        normalPosition = gameObject.transform.position;
        StopBattleMusic = false;
        MainMusic = true;
        MaxVol = 1;
        MinVol = 0;
        foundPlayer = false;
        foundMainMusic = false;
        foundBattleMusic = false;
        EnemyDead = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (EnemyDead && !otherEnemies)
        {
            battleAudioSrc.volume = 0;
            mainAudioSrc.volume = 1;
        }
        if (triggered && !other)
        {
            otherEnemies = false;
          
        }

        if (!foundMainMusic)
        {
            StartCoroutine(FindMainMusicPlayer());
        }
        if (!foundBattleMusic)
        {
            StartCoroutine(FindBattleMusicPlayer());
        }
        if (!foundPlayer)
        {
            StartCoroutine(FindPlayer());
        }
        if (foundPlayer)
        {
            PlayerPosition = Player.gameObject.transform.position;
        }
        if (Vector3.Magnitude(gameObject.transform.position - PlayerPosition) < attackRange && battleAudioSrc.volume <= 0f)
        {
            if (MainMusic)
            {
                PlayMusic(1, true);
                BattleMusic = true;
                MainMusic = false;
            }
           
                

           
           
        }
       

        if (Vector3.Magnitude(gameObject.transform.position - PlayerPosition) > attackRange && mainAudioSrc.volume <= 0f && !EnemyDead)
        {
            if (BattleMusic)
            {
                PlayMusic(2, true);
                BattleMusic = false;
            }
            MainMusic = true;
            
        }
      
    }
    IEnumerator FadeOut(AudioSource audio)
    {
        for (float f = 1f; f >= MinVol; f -= 0.1f)
        {
            audio.volume = f;
            yield return new WaitForSeconds(.1f);
            if (f <= 0.1)
            {
                
                audio.volume = MinVol;
            }
        }
    }
    IEnumerator FadeIn(AudioSource audio)
    {
        
        for (float f = 0f; f <= MaxVol; f += 0.1f)
        {
            audio.volume = f;
            yield return new WaitForSeconds(.1f);
            if (f >= 0.9)
            {
                audio.volume = MaxVol;
            }
        }
    }
    IEnumerator FindMainMusicPlayer()
    {
        AudioSrcGame = GameObject.Find("/GameControl/MusicPlayerGame/");
        if (AudioSrcGame == null)
        {
            yield return null;
        }
        else
        {
            AudioSrcGame = GameObject.Find("/GameControl/MusicPlayerGame/");
            mainAudioSrc = AudioSrcGame.GetComponent<AudioSource>();
            foundMainMusic = true;
        }
    }
    IEnumerator FindBattleMusicPlayer()
    {
        AudioSrcEnemy = GameObject.Find("/GameControl/MusicPlayerEnemy/");
        if (AudioSrcEnemy == null)
        {
            yield return null;
        }
        else
        {

            AudioSrcEnemy = GameObject.Find("/GameControl/MusicPlayerEnemy/");
            battleAudioSrc = AudioSrcEnemy.GetComponent<AudioSource>();
            foundBattleMusic = true;
        }

    }
    IEnumerator FindPlayer()
    {
        Player = GameObject.Find("/GameControl/Player/PlayerController/");
        if (Player == null)
        {
            yield return null;
        }
        else
        {
            Player = GameObject.Find("/GameControl/Player/PlayerController/");
            foundPlayer = true;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public void EnemyKilled()
    {
        EnemyDead = true;
    }
    void PlayMusic(int num, bool True)
    {
        if (num == 1 && True)
        {
            StartCoroutine(FadeOut(mainAudioSrc));
            StartCoroutine(FadeIn(battleAudioSrc));
            if (EnemyDead)
            {
                StartCoroutine(FadeOut(battleAudioSrc));
                StartCoroutine(FadeIn(mainAudioSrc));
            }
            True = false;
        }
        if (num == 2 && True)
        {
            StartCoroutine(FadeOut(battleAudioSrc));
            StartCoroutine(FadeIn(mainAudioSrc));
           
            True = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            triggered = true;
            this.other = other;
            otherEnemies = true;
        }
  
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
        
        
            otherEnemies = false;
        }

    }


}
