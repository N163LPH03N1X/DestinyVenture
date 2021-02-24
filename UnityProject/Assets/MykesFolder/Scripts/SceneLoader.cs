using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public bool isBeginning;
    public bool isDebugging;
    public bool loadScene = false;
    public bool activateScene;
    public Text tipText;
    public bool isOverWorld;
    public bool isFlameTemple;
    public bool isPhantomTemple;
    public bool isFrostTemple;
    public bool isFinalTemple;
    bool FlameTempleFinished;
    bool PhantomTempleFinished;
    bool FrostTempleFinished;
    public static bool Entered;
    public bool SwitchEntered;

    [Header("Level Music")]
    public AudioSource musicAudioSrc;
    public AudioSource bossAudioSrc;
    public AudioSource ambientAudioSrc;
    public AudioClip MainMenuMusic;
    public AudioClip OverWorldMusic;
    public AudioClip FlameTempleMusic;
    public AudioClip PhantomTempleMusic;
    public AudioClip FrostTempleMusic;
    public AudioClip FinalTempleMusic;
    public AudioClip DebugSceneMusic;
    public AudioClip BossMusic;
    public AudioClip CreditsMusic;
    public AudioClip buttonUISFX;
    public AudioClip MiniBossMusic;
    AudioClip CurrentMusic;
    [Space]
    [Header("Current Scene")]
    
    int scene;
    [Space]

    public Image LoadingUI;
    public Text loadingText;
    public Image blackScreen;

    [Header("Level Scenes")]
    public int OverWorld;
    public int FlameTemple;
    public int PhantomTemple;
    public int FrostTemple;
    public int FinalTemple;
    public int LoadAScene;
    public int DebugScene;
    [Space]
    [Header("Level Positions")]
    public Transform OverWorldStartPos;
    public Transform FlameTempleStartPos;
    public Transform FlameTempleOverWorldPos;
    public Transform PhantomTempleStartPos;
    public Transform PhantomTempleOverWorldPos;
    public Transform FrostTempleStartPos;
    public Transform FrostTempleOverWorldPos;
    public Transform FinalTempleStartPos;
    public Transform FinalTempleOverWorldPos;
    public Transform DebugScenePos;
    public GameObject Credits;
    public GameObject mainCam;
    public Image blackScreenMenu;
    public Image CreditScreen;
    public GameObject mainMenuEvent;
    public GameObject playerEvent;
    public GameObject MainMenuCanvas;
    public GameObject InGameCanvas;
    public GameObject Head;
    public GameObject FireWave;
    bool inTerritory;
    public static bool isLoading;
    public static bool isDisabled;
    bool isPaused;
    bool freezeplayer;
    AudioSource audioSrc;
    Interaction PlayerInt;
    Transform newPosition;
    PlayerStats PlayST;
  
    WaterDamage WatDmg;
    public bool enablePlayer;
    public float CreditsTimer;
    bool isDrowning;
    bool underWater;
    //public GameObject UnderWaterUI;
    GameObject LoadBanner;
    private Image loadFillAmount;

    public GameObject snow;
    public GameObject rain;
    public GameObject ash;
    bool CreditStart;
    MapSystem gameMap;
    bool curWeatherFrost;
    bool curWeatherPhant;
    bool curWeatherFlame;

    bool switchInProcess;
    // Updates once per frame
    private void Start()
    {
        if(enablePlayer)
            isDisabled = false;
        else
            isDisabled = true;
        Credits.SetActive(false);
        OriginalPos = transform.position;
        FireWave.SetActive(false);
        CreditScreen.enabled = false;
        //tipText.enabled = false;
    }
    void Update()
    {
        if (isDisabled)
        {
            Vector3 OrgPlayerPos;
            OrgPlayerPos = transform.position;
            transform.position = OrgPlayerPos;

        }
        isDrowning = WaterDamage.isDrowning;
        if (isDrowning)
        {
            FireWave.SetActive(true);
            underWater = true;
        }
        else if (!isDrowning && underWater)
        {
            FireWave.SetActive(false);
            underWater = false;
        }

        if (CreditsTimer > 0)
        {
            CreditsTimer -= Time.deltaTime;
        }
        else if(CreditsTimer < 0)
        {
            StartCoroutine(AudioFadeIn(bossAudioSrc));
            Credits.SetActive(true);
            CreditsTimer = 0;
            isDisabled = true;
        }
        if (SwitchEntered)
        {
            Entered = !Entered;
            SwitchEntered = false;
        }
        if (inTerritory && !loadScene)
        {
            isPaused = PauseMenu.Paused;
            if (Input.GetButtonUp("Open") && !loadScene  && !isPaused)
            {
                isDisabled = true;
                snow.SetActive(false);
                rain.SetActive(false);
                ash.SetActive(false);
                Entered = !Entered;
                tipText.enabled = true;
                isLoading = true;
                StartCoroutine(AudioFadeOut(musicAudioSrc));
                ambientAudioSrc.Stop();
                StartCoroutine(FadeTo(1.0f, 1.0f, blackScreen, true));
                loadScene = true;
            }
        }
        else if (isBeginning && !loadScene)
        {
            scene = LoadAScene;
            StartCoroutine(AudioFadeOut(musicAudioSrc));
            StartCoroutine(FadeTo(1.0f, 1.0f, blackScreenMenu, true));
            isLoading = true;
            loadScene = true;
        }
        else if (isDebugging && !loadScene)
        {
            scene = DebugScene;
            StartCoroutine(AudioFadeOut(musicAudioSrc));
            StartCoroutine(FadeTo(1.0f, 1.0f, blackScreenMenu, true));
            isLoading = true;
            loadScene = true;
        }
        if (loadScene)
        {
            
            LoadingUI.color = new Color(LoadingUI.color.r, LoadingUI.color.g, LoadingUI.color.b, Mathf.PingPong(Time.time, 1));
            if (isOverWorld)
            {
                loadingText.text = "--Fated Island--";
            }
            else if (isFlameTemple)
            {
                loadingText.text = "--Flame Temple--";
            }
            else if (isPhantomTemple)
            {
                loadingText.text = "--Phantom Temple--";
            }
            else if (isFrostTemple)
            {
                loadingText.text = "--Frost Temple--";
            }
            else if (isFinalTemple)
            {
                loadingText.text = "--Final Temple--";
            }
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }
        if (freezeplayer)
        {
            isLoading = true;
            transform.position = OriginalPos;
        }
    }
    public void ActivateLoadBanner(bool True)
    {
        if (True)
        {
            LoadBanner = GameObject.Find("GameControl/Player/InGameCanvas/LoadBanner");
            LoadBanner.SetActive(true);
        }
        else
        {
            LoadBanner = GameObject.Find("GameControl/Player/InGameCanvas/LoadBanner");
            LoadBanner.SetActive(false);
        }
    }
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    IEnumerator LoadNewScene()
    {
        
        yield return new WaitForSeconds(2);
        AsyncOperation async = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        while (!async.isDone)
        {
           
            LoadingUI.enabled = true;
            ActivateLoadBanner(true);
            loadFillAmount = GameObject.Find("GameControl/Player/InGameCanvas/LoadBanner/LoadMask/Load").GetComponent<Image>();
            loadFillAmount.fillAmount = Map(Mathf.RoundToInt(async.progress * 100), 0, 100, 0, 1);
            //loadingText.text = "Loading progress: " + (Mathf.RoundToInt(async.progress * 100) + "%");
            yield return null;
        }
        if (async.isDone)
        {
           
            if (isOverWorld)
            {
                musicAudioSrc.clip = OverWorldMusic;
            }
            else if (isFlameTemple)
            {
                musicAudioSrc.clip = FlameTempleMusic;
            }
            else if (isPhantomTemple)
            {
                musicAudioSrc.clip = PhantomTempleMusic;
            }
            else if (isFrostTemple)
            {
                musicAudioSrc.clip = FrostTempleMusic;
            }
            else if (isFinalTemple)
            {
                musicAudioSrc.clip = FinalTempleMusic;
            }
            else if (isBeginning)
            {
                if (scene == 1)
                {
                    musicAudioSrc.clip = OverWorldMusic;
                }
                if (scene == 2)
                {
                    musicAudioSrc.clip = FlameTempleMusic;
                }
                if (scene == 3)
                {
                    musicAudioSrc.clip = PhantomTempleMusic;
                }
                if (scene == 4)
                {
                    musicAudioSrc.clip = FrostTempleMusic;
                }
                if (scene == 5)
                {
                    musicAudioSrc.clip = DebugSceneMusic;
                }
                if (scene == 6)
                {
                    musicAudioSrc.clip = FinalTempleMusic;
                }
            }
            LoadingUI.enabled = false;
            ActivateLoadBanner(false);
            loadingText.text = "";
            StartGame(true);
        }
    }
    IEnumerator FadeTo(float aValue, float aTime, Image image, bool In)
    {
        bool loaded = false;
        float alpha = image.GetComponent<Image>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(alpha, aValue, t));
            image.GetComponent<Image>().color = newColor;
            if (t >= 0.9)
            {
                if (In)
                {
                    newColor = new Color(1, 1, 1, 1);
                    image.GetComponent<Image>().color = newColor;
                    if (!loaded)
                    {
                        StartCoroutine(LoadNewScene());
                        if (isBeginning || isDebugging)
                        {
                            MainMenuCanvas.SetActive(false);
                            InGameCanvas.SetActive(true);
                            mainCam.SetActive(true);
                            if (scene == 1)
                            {
                                newPosition = OverWorldStartPos.transform;
                            }
                            if (scene == 2)
                            {
                                newPosition = FlameTempleStartPos.transform;
                            }
                            if (scene == 3)
                            {
                                newPosition = PhantomTempleStartPos.transform;
                            }
                            if (scene == 4)
                            {
                                newPosition = FrostTempleStartPos.transform;
                            }
                            if (scene == 5)
                            {
                                newPosition = DebugScenePos.transform;
                            }
                            if (scene == 6)
                            {
                                newPosition = FinalTempleStartPos.transform;
                            }
                        }
                        loaded = true;
                    }
                }
                else if (!In)
                {
                    if (CreditStart)
                    {
                        newColor = new Color(0, 0, 0, 1);
                    }
                    else
                    {
                        newColor = new Color(1, 1, 1, 0);
                    }
                   
                    image.GetComponent<Image>().color = newColor;
                    loaded = true;
                }
                
            }
            else
            {
                image.GetComponent<Image>().enabled = true;
            }
            yield return null;
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
                switchInProcess = false;
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
                switchInProcess = false;
                audio.volume = MaxVol;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("OverWorld"))
        {
       
            PlayST = GetComponent<PlayerStats>();
            PlayST.SendMainMSG(1, "Fated Island", 3);
            PlayerInt = GetComponent<Interaction>();
            audioSrc = GetComponent<AudioSource>();
            audioSrc.PlayOneShot(buttonUISFX);
            PlayerInt.EnableAUI(true, "Enter");

            newPosition = OverWorldStartPos.transform;
            scene = OverWorld;
            isOverWorld = true;
            isFlameTemple = false;
            isPhantomTemple = false;
            isFrostTemple = false;
            isFinalTemple = false;
            inTerritory = true;
        }
        if (other.gameObject.CompareTag("FlameTemple"))
        {
           
            PlayerInt = GetComponent<Interaction>();
            audioSrc = GetComponent<AudioSource>();
            PlayST = GetComponent<PlayerStats>();
            audioSrc.PlayOneShot(buttonUISFX);
            FlameTempleFinished = TempleKeys.FlameTempleFinished;
            if (FlameTempleFinished)
            {
                PlayST.SendMainMSG(1, "Flame Temple Finished", 3);
            }
            else
            {
                if (Entered)
                {
                    curWeatherFlame = true;
                    PlayerInt.EnableAUI(true, "Leave");
                    newPosition = FlameTempleOverWorldPos.transform;
                    PlayST.SendMainMSG(1, "Fated Island", 3);
                    scene = OverWorld;
                    isOverWorld = true;
                    isFlameTemple = false;
                    isPhantomTemple = false;
                    isFinalTemple = false;
                    isFrostTemple = false;
                    inTerritory = true;
                }
                else
                {
                    curWeatherFlame = false;
                    PlayerInt.EnableAUI(true, "Enter");
                    newPosition = FlameTempleStartPos.transform;
                    PlayST.SendMainMSG(1, "Flame Temple", 3);
                    scene = FlameTemple;
                    isFlameTemple = true;
                    isOverWorld = false;
                    isPhantomTemple = false;
                    isFinalTemple = false;
                    isFrostTemple = false;
                    inTerritory = true;
                }
            }
           
            
        }
        if (other.gameObject.CompareTag("PhantomTemple"))
        {
            PlayST = GetComponent<PlayerStats>();
            PlayerInt = GetComponent<Interaction>();
            audioSrc = GetComponent<AudioSource>();
            audioSrc.PlayOneShot(buttonUISFX);
            PhantomTempleFinished = TempleKeys.PhantomTempleFinished;
            if (PhantomTempleFinished)
            {
                PlayST.SendMainMSG(1, "Phantom Temple Finished", 3);
            }
            else
            {
                if (Entered)
                {
                    curWeatherPhant = true;
                    PlayerInt.EnableAUI(true, "Leave");
                    newPosition = PhantomTempleOverWorldPos.transform;
                    PlayST.SendMainMSG(1, "Fated Island", 3);
                    scene = OverWorld;
                    isOverWorld = true;
                    isFlameTemple = false;
                    isPhantomTemple = false;
                    isFinalTemple = false;
                    isFrostTemple = false;
                    inTerritory = true;
                }
                else
                {
                    curWeatherPhant = false;
                    PlayerInt.EnableAUI(true, "Enter");
                    newPosition = PhantomTempleStartPos.transform;
                    PlayST.SendMainMSG(1, "Phantom Temple", 3);
                    scene = PhantomTemple;
                    isPhantomTemple = true;
                    isOverWorld = false;
                    isFlameTemple = false;
                    isFinalTemple = false;
                    isFrostTemple = false;
                    inTerritory = true;
                }
            }
        }
        if (other.gameObject.CompareTag("FrostTemple"))
        {
            PlayST = GetComponent<PlayerStats>();
            PlayerInt = GetComponent<Interaction>();
            audioSrc = GetComponent<AudioSource>();
            audioSrc.PlayOneShot(buttonUISFX);
            FrostTempleFinished = TempleKeys.FrostTempleFinished;
            if (FrostTempleFinished)
            {
                PlayST.SendMainMSG(1, "Frost Temple Finished", 3);
            }
            else
            {
                if (Entered)
                {
                    curWeatherFrost = true;
                    PlayerInt.EnableAUI(true, "Leave");
                    newPosition = FrostTempleOverWorldPos.transform;
                    PlayST.SendMainMSG(1, "Fated Island", 3);
                    scene = OverWorld;
                    isOverWorld = true;
                    isFlameTemple = false;
                    isPhantomTemple = false;
                    isFrostTemple = false;
                    isFinalTemple = false;
                    inTerritory = true;
                }
                else
                {
                    curWeatherFrost = false;
                    PlayerInt.EnableAUI(true, "Enter");
                    newPosition = FrostTempleStartPos.transform;
                    PlayST.SendMainMSG(1, "Frost Temple", 3);
                    scene = FrostTemple;
                    isFrostTemple = true;
                    isOverWorld = false;
                    isFlameTemple = false;
                    isPhantomTemple = false;
                    isFinalTemple = false;
                    inTerritory = true;
                }
            }
               
        }
        if (other.gameObject.CompareTag("FinalTemple"))
        {
            PlayST = GetComponent<PlayerStats>();
            PlayerInt = GetComponent<Interaction>();
            audioSrc = GetComponent<AudioSource>();
            
            if (Entered)
            {
                audioSrc.PlayOneShot(buttonUISFX);
                PlayerInt.EnableAUI(true, "Leave");
                newPosition = FinalTempleOverWorldPos.transform;
                PlayST.SendMainMSG(1, "Fated Island", 3);
                scene = OverWorld;
                isOverWorld = true;
                isFlameTemple = false;
                isPhantomTemple = false;
                isFrostTemple = false;
                isFinalTemple = false;
                inTerritory = true;
            }
            else
            {

                //PlayerInt.EnableAUI(true, "Enter");
                newPosition = FinalTempleStartPos.transform;
                PlayST.SendMainMSG(1, "Final Temple", 3);
                scene = FinalTemple;
                isFrostTemple = false;
                isOverWorld = false;
                isFlameTemple = false;
                isPhantomTemple = false;
                isFinalTemple = true;
                isDisabled = true;
                snow.SetActive(false);
                rain.SetActive(false);
                ash.SetActive(false);
                Entered = !Entered;
                tipText.enabled = true;
                isLoading = true;
                StartCoroutine(AudioFadeOut(musicAudioSrc));
                ambientAudioSrc.Stop();
                StartCoroutine(FadeTo(1.0f, 1.0f, blackScreen, true));
            }

        }
        if (other.gameObject.CompareTag("BossMusic"))
        {
            SwitchMusic(Music.BossMusic);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("MiniBossMusic") && !switchInProcess)
        {
            SwitchMusic(Music.MiniBossMusic);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MiniBossMusic") && !switchInProcess)
        {
            SwitchMusic(Music.Current);
        }
        if (other.gameObject.CompareTag("OverWorld"))
        {
            PlayerInt = GetComponent<Interaction>();
            PlayerInt.EnableAUI(false, null);
            isOverWorld = false;
            isFlameTemple = false;
            isPhantomTemple = false;
            isFrostTemple = false;
            isFinalTemple = false;
            inTerritory = false;
        }
        if (other.gameObject.CompareTag("FlameTemple"))
        {
            PlayerInt = GetComponent<Interaction>();
            PlayerInt.EnableAUI(false, null);
            isOverWorld = false;
            isFlameTemple = false;
            isPhantomTemple = false;
            isFrostTemple = false;
            isFinalTemple = false;
            inTerritory = false;
        }
        if (other.gameObject.CompareTag("PhantomTemple"))
        {
            PlayerInt = GetComponent<Interaction>();
            PlayerInt.EnableAUI(false, null);
            isOverWorld = false;
            isFlameTemple = false;
            isPhantomTemple = false;
            isFrostTemple = false;
            isFinalTemple = false;
            inTerritory = false;
        }
        if (other.gameObject.CompareTag("FrostTemple"))
        {
            PlayerInt = GetComponent<Interaction>();
            PlayerInt.EnableAUI(false, null);
            isOverWorld = false;
            isFlameTemple = false;
            isPhantomTemple = false;
            isFrostTemple = false;
            isFinalTemple = false;
            inTerritory = false;
        }
        if (other.gameObject.CompareTag("FinalTemple"))
        {
            PlayerInt = GetComponent<Interaction>();
            PlayerInt.EnableAUI(false, null);
            isOverWorld = false;
            isFlameTemple = false;
            isPhantomTemple = false;
            isFrostTemple = false;
            isFinalTemple = false;
            inTerritory = false;
        }
    }
    public void StartGame(bool True)
    {
        if (True)
        {
            PlayST = GetComponent<PlayerStats>();
            gameMap = GetComponent<MapSystem>();
            PlayerInt = GetComponent<Interaction>();
            WatDmg = Head.GetComponent<WaterDamage>();
            if (isOverWorld)
            {
                PlayST.SendMainMSG(1, "Fated Island", 3);
                
            }
            else if (isFlameTemple)
            {
                PlayST.SendMainMSG(1, "Flame Temple", 3);
            }
            else if (isPhantomTemple)
            {
                PlayST.SendMainMSG(1, "Phantom Temple", 3);
            }
            else if (isFrostTemple)
            {
                PlayST.SendMainMSG(1, "Frost Temple", 3);
            }
            else if (isFinalTemple)
            {
                PlayST.SendMainMSG(1, "Final Temple", 3);
            }
            else if (isBeginning)
            {
                if (scene == 1)
                {
                    PlayST.SendMainMSG(1, "Fated Island", 3);
                }
                if (scene == 2)
                {
                    PlayST.SendMainMSG(1, "Flame Temple", 3);
                }
                if (scene == 3)
                {
                    PlayST.SendMainMSG(1, "Phantom Temple", 3);
                }
                if (scene == 4)
                {
                    PlayST.SendMainMSG(1, "Frost Temple", 3);
                }
                if (scene == 5)
                {
                    PlayST.SendMainMSG(1, "Debug Scene", 3);
                }
                if (scene == 6)
                {
                    PlayST.SendMainMSG(1, "Final Temple", 3);
                }

            }
            if (curWeatherPhant)
            {
                rain.SetActive(true);
                curWeatherPhant = false;
            }
            else if (curWeatherFrost)
            {
                snow.SetActive(true);
                curWeatherFrost = false;
            }
            else if (curWeatherFlame)
            {
                ash.SetActive(true);
                curWeatherFlame = false;
            }
            tipText.enabled = false;
            WatDmg.ResetDrown();
            PlayerInt.EnableAUI(false, null);
            StartCoroutine(AudioFadeIn(musicAudioSrc));
            musicAudioSrc.Play();
            freezeplayer = false;
            isDisabled = false;
            transform.position = newPosition.transform.position;
            transform.rotation = newPosition.rotation;
            gameMap.FindMap();
            if (isFlameTemple)
            {
                FireWave.SetActive(true);
            }
            else
            {
                FireWave.SetActive(false);
            }
            loadingText.text = "";
            
            if (isBeginning)
            {
               
                StartCoroutine(FadeTo(0.0f, 1.0f, blackScreenMenu, false));
            }
            StartCoroutine(FadeTo(0.0f, 1.0f, blackScreen, false));
            isBeginning = false;
            isDebugging = false;
            PlayST.SetCheckPoint();
            isLoading = false;
            loadScene = false;
            inTerritory = false;
            isOverWorld = false;
            isFlameTemple = false;
            isPhantomTemple = false;
            isFrostTemple = false;
            isFinalTemple = false;
            True = false;
            
        }
    }
    public void BeginGame()
    {
        isBeginning = true;
    }
    public void BeginDebug()
    {
        isDebugging = true;
    }
    public enum Music { Current, OverWorld, FireTemple, PhantTemple, IceTemple, FinalTemple, BossMusic, MiniBossMusic, EndingCredits }
    public void SwitchMusic(Music track)
    {
        switch (track)
        {
            case Music.FireTemple:
                {
                    switchInProcess = true;
                    musicAudioSrc.clip = FlameTempleMusic;
                    StartCoroutine(AudioFadeOut(bossAudioSrc));
                    StartCoroutine(AudioFadeIn(musicAudioSrc));
                    break;
                }
            case Music.PhantTemple:
                {
                    switchInProcess = true;
                    musicAudioSrc.clip = PhantomTempleMusic;
                    StartCoroutine(AudioFadeOut(bossAudioSrc));
                    StartCoroutine(AudioFadeIn(musicAudioSrc));
                    break;
                }
            case Music.IceTemple:
                {
                    switchInProcess = true;
                    musicAudioSrc.clip = FrostTempleMusic;
                    StartCoroutine(AudioFadeOut(bossAudioSrc));
                    StartCoroutine(AudioFadeIn(musicAudioSrc));
                    break;
                }
            case Music.FinalTemple:
                {
                    switchInProcess = true;
                    musicAudioSrc.clip = FinalTempleMusic;
                    StartCoroutine(AudioFadeOut(bossAudioSrc));
                    StartCoroutine(AudioFadeIn(musicAudioSrc));
                    break;
                }
            case Music.OverWorld:
                {
                    switchInProcess = true;
                    musicAudioSrc.clip = OverWorldMusic;
                    StartCoroutine(AudioFadeOut(bossAudioSrc));
                    StartCoroutine(AudioFadeIn(musicAudioSrc));
                    break;
                }
            case Music.BossMusic:
                {
                    switchInProcess = true;
                    bossAudioSrc.clip = BossMusic;
                    StartCoroutine(AudioFadeOut(musicAudioSrc));
                    StartCoroutine(AudioFadeIn(bossAudioSrc));
                    break;
                }
            case Music.MiniBossMusic:
                {
                    switchInProcess = true;
                    switchInProcess = true;
                    bossAudioSrc.clip = MiniBossMusic;
                    StartCoroutine(AudioFadeOut(musicAudioSrc));
                    StartCoroutine(AudioFadeIn(bossAudioSrc));
                    break;
                }
            case Music.Current:
                {
                    switchInProcess = true;
                    CurrentMusic = musicAudioSrc.clip;
                    musicAudioSrc.clip = CurrentMusic;
                    StartCoroutine(AudioFadeOut(bossAudioSrc));
                    StartCoroutine(AudioFadeIn(musicAudioSrc));
                    break;
                }
            case Music.EndingCredits:
                {
                    switchInProcess = true;
                    CreditScreen.enabled = true;
                    CreditStart = true;
                    bossAudioSrc.clip = CreditsMusic;
                    StartCoroutine(AudioFadeOut(musicAudioSrc));
                    StartCoroutine(FadeTo(1.0f, 1.0f, CreditScreen, false));
                    freezeplayer = true;
                    CreditsTimer += 5;
                    break;
                }
        }
    }
    public enum TempleExit { flame, phant, glace}
    public void GrabCrystal(TempleExit exit)
    {
        switch (exit)
        {
            case TempleExit.flame:
                {
                    isOverWorld = true;
                    isFlameTemple = false;
                    isPhantomTemple = false;
                    isFrostTemple = false;
                    isFinalTemple = false;
                    newPosition = FlameTempleOverWorldPos.transform;
                    scene = OverWorld;
                    Entered = !Entered;
                    isLoading = true;
                    inTerritory = true;
                    tipText.enabled = true;
                    StartCoroutine(AudioFadeOut(musicAudioSrc));
                    StartCoroutine(FadeTo(1.0f, 1.0f, blackScreen, true));
                    loadScene = true;
                    break;
                }
            case TempleExit.phant:
                {
                    isOverWorld = true;
                    isFlameTemple = false;
                    isPhantomTemple = false;
                    isFrostTemple = false;
                    isFinalTemple = false;
                    inTerritory = true;
                    newPosition = PhantomTempleOverWorldPos.transform;
                    scene = OverWorld;
                    Entered = !Entered;
                    isLoading = true;
                    tipText.enabled = true;
                    StartCoroutine(AudioFadeOut(musicAudioSrc));
                    StartCoroutine(FadeTo(1.0f, 1.0f, blackScreen, true));
                    loadScene = true;
                    break;
                }
            case TempleExit.glace:
                {
                   
                   
                    isOverWorld = true;
                    isFlameTemple = false;
                    isPhantomTemple = false;
                    isFrostTemple = false;
                    isFinalTemple = false;
                    inTerritory = true;
                    newPosition = FrostTempleOverWorldPos.transform;
                    scene = OverWorld;
                    Entered = !Entered;
                    isLoading = true;
                    tipText.enabled = true;
                    StartCoroutine(AudioFadeOut(musicAudioSrc));
                    StartCoroutine(FadeTo(1.0f, 1.0f, blackScreen, true));
                    loadScene = true;
                    break;
                }
        }
       
    }
}
