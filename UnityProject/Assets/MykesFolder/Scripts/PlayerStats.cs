using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    public bool StartDemo;
    AudioSource audioSrc;
    public AudioSource audioSrc2;
    public AudioClip levelUpSFX;
    public AudioClip moneyUpSFX;
    public AudioClip death;
    public AudioClip hurt;
    public AudioClip quake;
    public AudioClip cash;
    public AudioClip error;
    public GameObject Head;
    WaterDamage WatDMG;
    private PlayerController MovementSet;
    CrossBow CrossBowWeap;
    public GameObject CrossbowObj;
    bool startQuake = false;
    public static PlayerStats playST;
    public Text Message;
     Vector3 MSGOrgPos;
    public Text GDMessage;
    Vector3 GoldOrgPos;
    public Text HPMessage;
   Vector3 HPOrgPos;
    public Text XPMessage;
    Vector3 XPOrgPos;
    public Text lvlMessage;
    Vector3 lvlOrgPos;
    public Text healthUI;
    public Text TotalXPUI;
    public Text LevelUI;
    public Text GoldUI;
    public Image healthFillAmount;
    public Image staminaFillAmount;
    public Image expFillAmount;
  
    PowerCounter powCount;
    public int Interval = 4;
    public static bool HealthFull;
    public float health = 100.0f;
    public float stamina = 100.0f;
    public int experience = 0;
    public int totalExperience = 0;
    public int xpLimit = 1000;
    public int gold = 0;
    public int levelUpCounter = 1;

    public Image itemScreen;
    public Image hurtScreen;
    public Image drainScreen;
    public Image goldScreen;
    public Image SavingUI;
    public GameObject ScreenFade;
    FadeScreen fadeScr;
    Image ScreenFadeAlpha;
    Color ScreenFadeColor;
    Image flashScreen;

    public bool ScreenFlashEnabled = false;
    public float ScreenFlashTimer = 0.1f;
    private float ScreenFlashTimerStart;
    
 
    public float hurtTimer;
    float resetHurtTimer;
    bool hurtPlayer = false;
    bool DrainPlayer;
    public bool DrownPlayer;

    bool FireKeyUsed;
    bool ThunderKeyUsed;
    bool IceKeyUsed;
    bool FireBossKeyUsed;
    bool ThunderBossKeyUsed;
    bool IceBossKeyUsed;
    bool CrystalKeysUsed;

    bool StrengthGauntletsPickedUp;
    bool PowerGauntletsPickedUp;
    bool GripGauntletsPickedUp;

    bool FlameBreastEnabled;
    bool PhantomBreastEnabled;
    public Transform playerTransform;
    public Vector3 lastPosition;
    public Quaternion lastRotation;
    bool Enemy;
    bool IceSpike;
    bool BigSpikeBall;
    bool SmallSpikeBall;
    bool BossIceField;
    Rigidbody rb;
    Breast breastState;

    public Transform camTransform;
    public Vector3 originalPos;

    ImpactReceiver Impact;

    bool isSaving;
    float SaveTimer;

    HealthItems healSM;
    StaminaItems vitaSM;
    HealthItems healMD;
    StaminaItems vitaMD;
    PowerCounter PowerSL;
    HealthItems healLG;
    StaminaItems vitaLG;
    PowerCounter powerGD;
    Boots speedBoots;
    Boots spikeBoots;

    int HealixerSMAmount;
    int HealixerMDAmount;
    int HealixerLGAmount;
    int VitalixerSMAmount;
    int VitalixerMDAmount;
    int VitalixerLGAmount;
    int PowerGemSLAmount;
    int PowerGemGDAmount;
    bool SpikeBootsPickedUp;
    bool SpeedBootsPickedUp;
    bool fallDamage;

    public bool InitQuake;
    float quakeTimer = 5.0f;

    public PlayerController PlayCtrl;

    void Start () {

        Message.text = "";
        HPMessage.text = "";
        XPMessage.text = "";
        GDMessage.text = "";
        lvlMessage.text = "";
        GoldUI.text = "0";
        resetHurtTimer = hurtTimer;
        MovementSet = GetComponent<PlayerController>();
        audioSrc = GetComponent<AudioSource>();
        powCount = GetComponent<PowerCounter>();
        playerTransform = transform;
        lastPosition = playerTransform.position;
        lastRotation = playerTransform.rotation;
        rb = GetComponent<Rigidbody>();
        ScreenFlashTimerStart = ScreenFlashTimer;
        Impact = GetComponent<ImpactReceiver>();
        XPOrgPos = XPMessage.GetComponent<RectTransform>().anchoredPosition;
        lvlOrgPos = lvlMessage.GetComponent<RectTransform>().anchoredPosition;
        HPOrgPos = HPMessage.GetComponent<RectTransform>().anchoredPosition;
        MSGOrgPos = Message.GetComponent<RectTransform>().anchoredPosition;
        GoldOrgPos = GDMessage.GetComponent<RectTransform>().anchoredPosition;
        GDMessage.GetComponent<Text>().enabled = false;
        HPMessage.GetComponent<Text>().enabled = false;
        Message.GetComponent<Text>().enabled = false;
        XPMessage.GetComponent<Text>().enabled = false;
        camTransform = Camera.main.transform;
        originalPos = camTransform.localPosition;
    }
    void Update () {

        stamina = PlayerController.stamina;
        DrainPlayer = PlayerController.isDraining;
        fallDamage = PlayerController.fallDamage;

        handler();
        setUIText();
        if (InitQuake)
        {
            if(quakeTimer > 0)
            {
                ShakeScreen(0.5f, true);
                quakeTimer -= Time.deltaTime;
            }
            else if (quakeTimer < 0)
            {
                quakeTimer = 5;
                ShakeScreen(0.5f, false);
                InitQuake = false;
            }
           
        }

        if (hurtTimer > 0 && hurtPlayer)
        {
            hurtTimer -= Time.deltaTime;
        }
        if (hurtTimer < 0)
        {
            if (Enemy)
            {
                doDamage(Random.Range(1, 5));
            }
            if (IceSpike)
            {
                doDamage(10);
            }
            if (BigSpikeBall)
            {
                doDamage(20);
            }
            if (SmallSpikeBall)
            {
                doDamage(5);
            }
            if (BossIceField)
            {
                doDamage(2);
            }
            
            hurtTimer = resetHurtTimer;
            BossIceField = false;
            Enemy = false;
            IceSpike = false;
            BigSpikeBall = false;
            SmallSpikeBall = false;
            hurtPlayer = false;


        }
        if (StartDemo)
        {
            gold = 9999;
            levelUpCounter = 50;
            powCount.AddUpgradePoint(12);
            StartDemo = false;
        }
       
        if (SaveTimer > 0)
        {
            SavingUI.enabled = true;
            SavingUI.color = new Color(SavingUI.color.r, SavingUI.color.g, SavingUI.color.b, Mathf.PingPong(Time.time, 1));
            SaveTimer -= Time.deltaTime;
        }
        else if (SaveTimer < 0)
        {
            SavingUI.enabled = false;
            SaveTimer = 0; 
        }


        if (gold <= 0)
        {
            gold = 0;
        }

        if (health <= 25)
        {
            hurtScreen.enabled = true;
            if (health <= 10)
            {
                hurtScreen.color = new Color(hurtScreen.color.r, hurtScreen.color.g, hurtScreen.color.b, Mathf.PingPong(Time.time * 10, 1));
                healthUI.color = new Color(Mathf.Sin(Time.time * 30), 0f, 0f, 1.0f);
            }
            else
            {
                hurtScreen.color = new Color(hurtScreen.color.r, hurtScreen.color.g, hurtScreen.color.b, Mathf.PingPong(Time.time, 1));
                healthUI.color = new Color(Mathf.Sin(Time.time * 10), 0f, 0f, 1.0f);
            }
        }
        else if (health >= 26)
        {
            healthUI.color = new Color(1f, 1f, 1f, 1f);
            hurtScreen.enabled = false; 
        }

        if (ScreenFlashEnabled == true)
        {
            flashScreen.enabled = true;
            ScreenFlashTimer -= Time.deltaTime;
        }

        if (ScreenFlashTimer < 0)
        {
            hurtScreen.enabled = false;
            itemScreen.enabled = false;
            goldScreen.enabled = false;
            ScreenFlashEnabled = false;
            ScreenFlashTimer = ScreenFlashTimerStart;
        }

        if (health < 100)
        {
            HealthFull = false;
        }
        else
        {
            HealthFull = true;
        }
        if (DrainPlayer)
        {
            drainScreen.enabled = true;
        }
        else
        {
            drainScreen.enabled = false;
        }
    }
    void handler()
    {
        healthFillAmount.fillAmount = Map(health,0,100,0,1);
        staminaFillAmount.fillAmount = Map(stamina, 0, 100, 0, 1);
        expFillAmount.fillAmount = Map(experience, 0, xpLimit, 0, 1);
    }
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    void setUIText()
    {
        LevelUI.text = (levelUpCounter).ToString();
        healthUI.text = Mathf.CeilToInt(health).ToString();
        GoldUI.text = (gold).ToString();
        TotalXPUI.text = "Total Exp: " + (totalExperience).ToString();
    }
    public void doDamage(float amount)
    {
        DrainPlayer = PlayerController.isDraining;
        DrownPlayer = WaterDamage.isDrowning;

        rb.isKinematic = true;
        rb.useGravity = false;

        if (!DrainPlayer)
        {
            if (!DrownPlayer || fallDamage)
            {
                audioSrc2.volume = Random.Range(0.8f, 1.0f);
                audioSrc2.pitch = Random.Range(0.8f, 1.0f);

                audioSrc2.PlayOneShot(hurt);
                
                ScreenFlashEnabled = true;
            }
           
        }
        flashScreen = hurtScreen;
        HPMessage.GetComponent<Text>().enabled = true;
        HPMessage.GetComponent<RectTransform>().anchoredPosition = HPOrgPos;
        FlameBreastEnabled = Breast.FlameBreast;
        if (FlameBreastEnabled && stamina > 1)
        {
            PlayCtrl.RemovePlayerStamina(amount);
        }
        else
        {
            health -= amount;
        }
       
        HPMessage.text = "-" + Mathf.CeilToInt(amount);
        if (health <= 0)
        {
            breastState = GetComponent<Breast>();
            breastState.ResetBreastState();
            WatDMG = Head.GetComponent<WaterDamage>();
            WatDMG.ResetDrown();
            hurtScreen.color = new Color(hurtScreen.color.r, hurtScreen.color.g, hurtScreen.color.b, 1);
            fadeScr = ScreenFade.GetComponent<FadeScreen>();
            fadeScr.SetAlpha(1);
            fadeScr.FadeOutScreen();
            audioSrc2.volume = Random.Range(0.8f, 1.0f);
            audioSrc2.pitch = Random.Range(0.8f, 1.0f);
            audioSrc2.PlayOneShot(death);
            Message.GetComponent<Text>().enabled = true;
            playerTransform.position = lastPosition;
            playerTransform.rotation = lastRotation;
            Message.text = "You Died...";
            Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
            StartCoroutine(FadeTextToZeroAlpha(3f, Message));
            flashScreen.enabled = false;
            MovementSet.ResetStam();
            
            health = 100;
            setUIText();
        }
        StartCoroutine(FadeTextToZeroAlpha(3f, HPMessage));
    }
    public void gainExp(int amount)
    {
        audioSrc.volume = 1.0f;
        audioSrc.pitch = 1.0f;
        if (levelUpCounter < 50)
        {
            XPMessage.GetComponent<Text>().enabled = true;
            XPMessage.GetComponent<RectTransform>().anchoredPosition = XPOrgPos;
            flashScreen = itemScreen;
            ScreenFlashEnabled = true;
            experience += amount;
            totalExperience = totalExperience + amount;
            XPMessage.text = "+" + amount;
            if (experience >= xpLimit)
            {
                audioSrc.volume = 1.0f;
                audioSrc.pitch = 1.0f;
                audioSrc.PlayOneShot(levelUpSFX);
                experience = experience - xpLimit;
                xpLimit += 100;
                levelUpCounter += 1;
                if (levelUpCounter >= Interval)
                {
                    Interval += 4;
                    powCount.AddUpgradePoint(1);
                }
                lvlMessage.GetComponent<Text>().enabled = true;
                lvlMessage.text = "Level " + levelUpCounter;
                lvlMessage.GetComponent<RectTransform>().anchoredPosition = lvlOrgPos;
                StartCoroutine(FadeTextToFullAlpha(3f, lvlMessage));
            }
            StartCoroutine(FadeTextToZeroAlpha(3f, XPMessage));
        }
    }
    public void gainGold(int amount)
    {
        audioSrc.volume = 1.0f;
        audioSrc.pitch = 1.0f;
        Message.GetComponent<Text>().enabled = true;
        Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
        flashScreen = goldScreen;
        ScreenFlashEnabled = true;
        gold += amount;
        if (gold >= 9999)
        {
            GDMessage.GetComponent<Text>().enabled = true;
            Message.text = "Your wallet is full!";
            gold = 9999;
            GDMessage.text = "+0";
            GDMessage.GetComponent<RectTransform>().anchoredPosition = GoldOrgPos;
            StartCoroutine(FadeTextToZeroAlpha(3f, GDMessage));
        }
        else
        {
            GDMessage.GetComponent<Text>().enabled = true;
            Message.text = "You got " + amount + " gold!";
            GDMessage.text = "+" + amount;
            GDMessage.GetComponent<RectTransform>().anchoredPosition = GoldOrgPos;
            StartCoroutine(FadeTextToFullAlpha(3f, GDMessage));
        }
        StartCoroutine(FadeTextToZeroAlpha(3f, Message));
    }
    public void PurchaseItem(int num)
    {
        if (num == 1)
        {
            audioSrc.volume = 1.0f;
            audioSrc.pitch = 1.0f;
            Message.GetComponent<Text>().enabled = true;
            Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
            HealixerSMAmount = HealthItems.HealixerSMAmount;
            int orgGold = gold;
            gold -= 250;
            if (gold < 0)
            {
                audioSrc.PlayOneShot(error);
                gold = orgGold;
                Message.text = "You don't have enough money!";
            }
            else if (HealixerSMAmount >= 9)
            {
                audioSrc.PlayOneShot(error);
                Message.text = "Item is maxed out!";
                gold = orgGold;
            }
            else if(HealixerSMAmount < 9)
            {
                audioSrc.PlayOneShot(cash);
                BoughtItem(Item.healSM);
                GDMessage.GetComponent<Text>().enabled = true;
                Message.text = "Purchased Healixer Small!";
                GDMessage.text = "-$" + 250;
                GDMessage.GetComponent<RectTransform>().anchoredPosition = GoldOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, GDMessage));
            }
        }
        else if (num == 2)
        {
            audioSrc.volume = 1.0f;
            audioSrc.pitch = 1.0f;
            Message.GetComponent<Text>().enabled = true;
            Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
            VitalixerSMAmount = StaminaItems.VitalixerSMAmount;
            int orgGold = gold;
            gold -= 200;
            if (gold < 0)
            {
                audioSrc.PlayOneShot(error);
                gold = orgGold;
                Message.text = "You don't have enough money!";
            }
            else if (VitalixerSMAmount >= 9)
            {
                audioSrc.PlayOneShot(error);
                Message.text = "Item is maxed out!";
                gold = orgGold;
            }
            else if (VitalixerSMAmount < 9)
            {
                audioSrc.PlayOneShot(cash);
                BoughtItem(Item.vitaSM);
                GDMessage.GetComponent<Text>().enabled = true;
                Message.text = "Purchased Vitalixer Small!";
                GDMessage.text = "-$" + 200;
                GDMessage.GetComponent<RectTransform>().anchoredPosition = GoldOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, GDMessage));
            }
        }
        else if (num == 3)
        {
            audioSrc.volume = 1.0f;
            audioSrc.pitch = 1.0f;
            Message.GetComponent<Text>().enabled = true;
            Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
            HealixerMDAmount = HealthItems.HealixerMDAmount;
            int orgGold = gold;
            gold -= 500;
            if (gold < 0)
            {
                audioSrc.PlayOneShot(error);
                gold = orgGold;
                Message.text = "You don't have enough money!";
            }
            else if (HealixerMDAmount >= 9)
            {
                audioSrc.PlayOneShot(error);
                gold = orgGold;
                Message.text = "Item is maxed out!";
            }
            else if (HealixerMDAmount < 9)
            {
                audioSrc.PlayOneShot(cash);
                BoughtItem(Item.healMD);
                GDMessage.GetComponent<Text>().enabled = true;
                Message.text = "Purchased Healixer Medium!";
                GDMessage.text = "-$" + 500;
                GDMessage.GetComponent<RectTransform>().anchoredPosition = GoldOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, GDMessage));
            }
        }
        else if (num == 4)
        {
            audioSrc.volume = 1.0f;
            audioSrc.pitch = 1.0f;
            Message.GetComponent<Text>().enabled = true;
            Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
            VitalixerMDAmount = StaminaItems.VitalixerMDAmount;
            int orgGold = gold;
            gold -= 400;
            if (gold < 0)
            {
                audioSrc.PlayOneShot(error);
                gold = orgGold;
                Message.text = "You don't have enough money!";
            }
            else if (VitalixerMDAmount >= 9)
            {
                audioSrc.PlayOneShot(error);
                gold = orgGold;
                Message.text = "Item is maxed out!";
            }
            else if (VitalixerMDAmount < 9)
            {
                audioSrc.PlayOneShot(cash);
                BoughtItem(Item.vitaMD);
                GDMessage.GetComponent<Text>().enabled = true;
                Message.text = "Purchased Vitalixer Medium!";
                GDMessage.text = "-$" + 400;
                GDMessage.GetComponent<RectTransform>().anchoredPosition = GoldOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, GDMessage));
            }

        }
        else if (num == 5)
        {
            audioSrc.volume = 1.0f;
            audioSrc.pitch = 1.0f;
            Message.GetComponent<Text>().enabled = true;
            Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
            PowerGemSLAmount = PowerCounter.PowerGemSLAmount;
            int orgGold = gold;
            gold -= 450;
            if (gold < 0)
            {
                audioSrc.PlayOneShot(error);
                gold = orgGold;
                Message.text = "You don't have enough money!";
            }
            else if (PowerGemSLAmount >= 9)
            {
                audioSrc.PlayOneShot(error);
                gold = orgGold;
                Message.text = "Item is maxed out!";
            }
            else if (PowerGemSLAmount < 9)
            {
                audioSrc.PlayOneShot(cash);
                BoughtItem(Item.powerGemSL);
                GDMessage.GetComponent<Text>().enabled = true;
                Message.text = "Purchased Power Gem Silver!";
                GDMessage.text = "-$" + 450;
                GDMessage.GetComponent<RectTransform>().anchoredPosition = GoldOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, GDMessage));
            }
        }
        else if (num == 6)
        {
            audioSrc.volume = 1.0f;
            audioSrc.pitch = 1.0f;
            Message.GetComponent<Text>().enabled = true;
            Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
            HealixerLGAmount = HealthItems.HealixerLGAmount;
            int orgGold = gold;
            gold -= 900;
            if (gold < 0)
            {
                audioSrc.PlayOneShot(error);
                gold = orgGold;
                Message.text = "You don't have enough money!";
            }
            else if (HealixerLGAmount >= 9)
            {
                audioSrc.PlayOneShot(error);
                gold = orgGold;
                Message.text = "Item is maxed out!";
            }
            else if (HealixerLGAmount < 9)
            {
                audioSrc.PlayOneShot(cash);
                BoughtItem(Item.healLG);
                GDMessage.GetComponent<Text>().enabled = true;
                Message.text = "Purchased Healixer Large!";
                GDMessage.text = "-$" + 900;
                GDMessage.GetComponent<RectTransform>().anchoredPosition = GoldOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, GDMessage));
            }
        }
        else if (num == 7)
        {
            audioSrc.volume = 1.0f;
            audioSrc.pitch = 1.0f;
            Message.GetComponent<Text>().enabled = true;
            Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
            VitalixerLGAmount = StaminaItems.VitalixerLGAmount;
            int orgGold = gold;
            gold -= 800;
            if (gold < 0)
            {
                audioSrc.PlayOneShot(error);
                gold = orgGold;
                Message.text = "You don't have enough money!";
            }
            else if (VitalixerLGAmount >= 9)
            {
                audioSrc.PlayOneShot(error);
                gold = orgGold;
                Message.text = "Item is maxed out!";
            }
            else if (VitalixerLGAmount < 9)
            {
                audioSrc.PlayOneShot(cash);
                BoughtItem(Item.vitaLG);
                GDMessage.GetComponent<Text>().enabled = true;
                Message.text = "Purchased Vitalixer Large!";
                GDMessage.text = "-$" + 800;
                GDMessage.GetComponent<RectTransform>().anchoredPosition = GoldOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, GDMessage));
            }

        }
        else if (num == 8)
        {
            audioSrc.volume = 1.0f;
            audioSrc.pitch = 1.0f;
            Message.GetComponent<Text>().enabled = true;
            Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
            PowerGemGDAmount = PowerCounter.PowerGemGDAmount;
            int orgGold = gold;
            gold -= 1000;
            if (gold < 0)
            {
                audioSrc.PlayOneShot(error);
                gold = orgGold;
                Message.text = "You don't have enough money!";
            }
            else if (PowerGemGDAmount >= 9)
            {
                audioSrc.PlayOneShot(error);
                gold = orgGold;
                Message.text = "Item is maxed out!";
            }
            else if (PowerGemGDAmount < 9)
            {
                audioSrc.PlayOneShot(cash);
                BoughtItem(Item.powerGemGD);
                GDMessage.GetComponent<Text>().enabled = true;
                Message.text = "Purchased Power Gem Gold!";
                GDMessage.text = "-$" + 1000;
                GDMessage.GetComponent<RectTransform>().anchoredPosition = GoldOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, GDMessage));
            }
        }
        else if (num == 9)
        {
            audioSrc.volume = 1.0f;
            audioSrc.pitch = 1.0f;
            Message.GetComponent<Text>().enabled = true;
            Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
            SpikeBootsPickedUp = Boots.SpikeBootsPickedUp;
            int orgGold = gold;
            gold -= 3000;
            if (gold < 0)
            {
                audioSrc.PlayOneShot(error);
                gold = orgGold;
                Message.text = "You don't have enough money!";
            }
            else if (SpikeBootsPickedUp)
            {
                audioSrc.PlayOneShot(error);
                gold = orgGold;
                Message.text = "You already have spike boots!";
            }
            else if (!SpikeBootsPickedUp)
            {
                audioSrc.PlayOneShot(cash);
                BoughtItem(Item.spikeBoots);
                GDMessage.GetComponent<Text>().enabled = true;
                Message.text = "Purchased Spike Boots!";
                GDMessage.text = "-$" + 3000;
                GDMessage.GetComponent<RectTransform>().anchoredPosition = GoldOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, GDMessage));
            }
        }
        else if (num == 10)
        {
            audioSrc.volume = 1.0f;
            audioSrc.pitch = 1.0f;
            Message.GetComponent<Text>().enabled = true;
            Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
            SpeedBootsPickedUp = Boots.SpeedBootsPickedUp;
            int orgGold = gold;
            gold -= 5000;
            if (gold < 0)
            {
                audioSrc.PlayOneShot(error);
                gold = orgGold;
                Message.text = "You don't have enough money!";
            }
            else if (SpeedBootsPickedUp)
            {
                audioSrc.PlayOneShot(error);
                gold = orgGold;
                Message.text = "You already have speed boots!";
            }
            else if (!SpeedBootsPickedUp)
            {
                audioSrc.PlayOneShot(cash);
                BoughtItem(Item.speedBoots);
                GDMessage.GetComponent<Text>().enabled = true;
                Message.text = "Purchased Speed Boots!";
                GDMessage.text = "-$" + 5000;
                GDMessage.GetComponent<RectTransform>().anchoredPosition = GoldOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, GDMessage));
            }
        }
        StartCoroutine(FadeTextToZeroAlpha(3f, Message));
    }
    public enum Item{ healSM, vitaSM, healMD, vitaMD, healLG, vitaLG, powerGemSL, powerGemGD, spikeBoots, speedBoots}
    public void BoughtItem(Item addItem)
    {
        switch (addItem)
        {
            case Item.healSM:
                {
                    healSM = GetComponent<HealthItems>();
                    healSM.GainHealixer(1, 1);
                    break;
                }
            case Item.vitaSM:
                {
                    vitaSM = GetComponent<StaminaItems>();
                    vitaSM.GainVitalixer(1, 1);
                    break;
                }
            case Item.healMD:
                {
                    healMD = GetComponent<HealthItems>();
                    healMD.GainHealixer(2, 1);
                    break;
                }
            case Item.vitaMD:
                {
                    vitaMD = GetComponent<StaminaItems>();
                    vitaMD.GainVitalixer(2, 1);
                    break;
                }
            case Item.powerGemSL:
                {
                    PowerSL = GetComponent<PowerCounter>();
                    PowerSL.AddPowerGem(1, 1);
                    break;
                }
            case Item.healLG:
                {
                    healLG = GetComponent<HealthItems>();
                    healLG.GainHealixer(3, 1);
                    break;
                }
            case Item.vitaLG:
                {
                    vitaLG = GetComponent<StaminaItems>();
                    vitaLG.GainVitalixer(3, 1);
                    break;
                }
            case Item.powerGemGD:
                {
                    powerGD = GetComponent<PowerCounter>();
                    powerGD.AddPowerGem(2, 1);
                    break;
                }
            case Item.spikeBoots:
                {
                    spikeBoots = GetComponent<Boots>();
                    spikeBoots.BuyBoots(1);
                    break;
                }
            case Item.speedBoots:
                {
                    speedBoots = GetComponent<Boots>();
                    speedBoots.BuyBoots(2);
                    break;
                }
        }
    }
    //public void GainArrows()
    //{
    //    flashScreen = itemScreen;
    //    ScreenFlashEnabled = true;
    //    Message.GetComponent<Text>().enabled = true;
    //    Message.GetComponent<RectTransform>().transform.position = MSGOrgPos;
    //    Message.text = "Picked Up Arrow(s)";
    //    StartCoroutine(FadeTextToZeroAlpha(3f, Message));
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyPhantom"))
        {
            StartCoroutine(FadeIn(drainScreen));
        }
        if (other.gameObject.CompareTag("CheckPoint") && SaveTimer == 0)
        {
            lastPosition = playerTransform.position;
            lastRotation = playerTransform.rotation;
            SaveTimer = 4;
        }
    
        if (other.gameObject.CompareTag("GainXP"))
        {
            audioSrc.volume = 1.0f;
            audioSrc.pitch = 1.0f;
            gainExp(Random.Range(5000, 100000));
        }

        if (other.gameObject.CompareTag("GainGold"))
        {
            audioSrc.volume = 1.0f;
            audioSrc.pitch = 1.0f;
            audioSrc.PlayOneShot(moneyUpSFX);
            gainGold(Random.Range(100, 200));
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("GainGoldEnemy"))
        {
            audioSrc.volume = 1.0f;
            audioSrc.pitch = 1.0f;
            audioSrc.PlayOneShot(moneyUpSFX);
            gainGold(Random.Range(10, 50));
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("FireDoor"))
        {
            FireKeyUsed = other.gameObject.GetComponent<MovingDoor>().FireKeyUsed;
            if (!FireKeyUsed)
            {
                Message.GetComponent<Text>().enabled = true;
                Message.text = "Door Locked";
                Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, Message));
            }
        }
        if (other.gameObject.CompareTag("ThunderDoor"))
        {
            ThunderKeyUsed = other.gameObject.GetComponent<MovingDoor>().ThunderKeyUsed;
            if (!ThunderKeyUsed)
            {
                Message.GetComponent<Text>().enabled = true;
                Message.text = "Door Locked";
                Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, Message));
            }
        }
        if (other.gameObject.CompareTag("IceDoor"))
        {

            IceKeyUsed = other.gameObject.GetComponent<MovingDoor>().IceKeyUsed;
            if (!IceKeyUsed)
            {
                Message.GetComponent<Text>().enabled = true;
                Message.text = "Door Locked";
                Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, Message));
            }
        }
        if (other.gameObject.CompareTag("BossFireDoor"))
        {
            FireBossKeyUsed = other.gameObject.GetComponent<MovingDoor>().BossFireKeyUsed;
            if (!FireBossKeyUsed)
            {
                Message.GetComponent<Text>().enabled = true;
                Message.text = "Door Locked";
                Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, Message));
            }
            else
            {
                Message.GetComponent<Text>().enabled = true;
                Message.text = "";
            }
        }
        if (other.gameObject.CompareTag("BossThunderDoor"))
        {
            ThunderBossKeyUsed = other.gameObject.GetComponent<MovingDoor>().BossThunderKeyUsed;
            if (!ThunderBossKeyUsed)
            {
                Message.GetComponent<Text>().enabled = true;
                Message.text = "Door Locked";
                Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, Message));
            }
        }
        if (other.gameObject.CompareTag("BossIceDoor"))
        {

            IceBossKeyUsed = other.gameObject.GetComponent<MovingDoor>().BossIceKeyUsed;
            if (!IceBossKeyUsed)
            {
                Message.GetComponent<Text>().enabled = true;
                Message.text = "Door Locked";
                Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, Message));
            }
        }
        if (other.gameObject.CompareTag("StrengthBlock"))
        {
            StrengthGauntletsPickedUp = Gauntlets.StrengthGauntletsPickedUp;
            if (!StrengthGauntletsPickedUp)
            {
                Message.GetComponent<Text>().enabled = true;
                Message.text = "Strength Gauntlets Required!";
                Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, Message));
            }
        }
        if (other.gameObject.CompareTag("GripBlock"))
        {
            GripGauntletsPickedUp = Gauntlets.GripGauntletsPickedUp;
            if (!GripGauntletsPickedUp)
            {
                Message.GetComponent<Text>().enabled = true;
                Message.text = "Grip Gauntlets Required!";
                Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, Message));
            }
        }
        if (other.gameObject.CompareTag("PowerBlock"))
        {
            PowerGauntletsPickedUp = Gauntlets.PowerGauntletsPickedUp;
            if (!PowerGauntletsPickedUp)
            {
                Message.GetComponent<Text>().enabled = true;
                Message.text = "Power Gauntlets Required!";
                Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
                StartCoroutine(FadeTextToZeroAlpha(3f, Message));
            }
        }
        if (other.gameObject.CompareTag("Death"))
        {
            if (FlameBreastEnabled)
            {
                doDamage(202);
            }
            else
            {
                doDamage(101);
            }

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyPhantom"))
        {
            StartCoroutine(FadeOut(drainScreen));
        }
        if (other.gameObject.CompareTag("IceDoor"))
        {
            Message.text = "";
        }
        if (other.gameObject.CompareTag("StrengthBlock"))
        {
            Message.text = "";
        }
        if (other.gameObject.CompareTag("PowerBlock"))
        {
            Message.text = "";
        }
        if (other.gameObject.CompareTag("GripBlock"))
        {
            Message.text = "";
        }
    }
    public IEnumerator FadeTextToFullAlpha(float t, Text i)

    {
        var turnOff =  i.GetComponent<Text>();
        var rise = i.GetComponent<RectTransform>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.enabled = true;
            rise.transform.Translate(Vector3.up * Time.fixedDeltaTime * 10);
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.fixedDeltaTime / t));
            yield return null;
        }
        if (i.color.a <= 0.0f)
        {
            turnOff.enabled = false;
        }
    }
    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        var turnOff = i.GetComponent<Text>();
        var fall = i.GetComponent<RectTransform>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.enabled = true;
            fall.transform.Translate(Vector3.up * -Time.fixedDeltaTime * 10);
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.fixedDeltaTime / t));
            yield return null;
        }
        if (i.color.a <= 0.0f)
        {
            turnOff.enabled = false;
        }
    }
    public void GivePlayerHealth(int amount)
    {
        health += amount;
        if (health >= 100)
        {
            HealthFull = true;
            health = 100;
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            Vector3 direction = (this.transform.position - hit.transform.position).normalized;
            Impact.AddImpact(direction, 50);
            doDamage(Random.Range(1, 5));
        }
        if (hit.gameObject.CompareTag("IceSpike"))
        {
            Vector3 direction = (this.transform.position - hit.transform.position).normalized;
            Impact.AddImpact(direction, 50);
            IceSpike = true;
            hurtPlayer = true;
            Destroy(hit.gameObject);
        }
        if (hit.collider.tag == "BigSpikeBall")
        {
            Vector3 direction = (this.transform.position - hit.transform.position).normalized;
            Impact.AddImpact(direction, 200);
            BigSpikeBall = true;
            hurtPlayer = true;
        }
        if (hit.collider.tag == "SmallSpikeBall")
        {
            Vector3 direction = (this.transform.position - hit.transform.position).normalized;
            Impact.AddImpact(direction, 100);
            SmallSpikeBall = true;
            hurtPlayer = true;
        }
        if (hit.gameObject.CompareTag("BossIceField"))
        {
            Vector3 direction = (this.transform.position - hit.transform.position).normalized;
            Impact.AddImpact(direction, 300);
            BossIceField = true;
            hurtPlayer = true;
        }
    }
    public void SendMainMSG(int num, string mainMsg, float speed)
    {

        if (num == 1)
        {
            lvlMessage.GetComponent<Text>().enabled = true;
            lvlMessage.text = mainMsg;
            lvlMessage.GetComponent<RectTransform>().transform.position.Set(0, 0, 0);
            StartCoroutine(FadeTextToFullAlpha(speed, lvlMessage));
        }
        else if (num == 2)
        {
            Message.GetComponent<Text>().enabled = true;
            Message.text = mainMsg;
            Message.GetComponent<RectTransform>().anchoredPosition = MSGOrgPos;
            StartCoroutine(FadeTextToZeroAlpha(3f, Message));
        }
       
       
    }
    public void ShakeScreen(float Strength, bool True)
    {
        if (True)
        {
            if (!startQuake)
            {
                audioSrc2.PlayOneShot(quake);
                startQuake = true;
            }
            camTransform.localPosition = originalPos + Random.insideUnitSphere * Strength;
        }
        else
        {
            camTransform.localPosition = originalPos;
            startQuake = false;
            InitQuake = false;
        }
    }
    public void QuakeState()
    {
        InitQuake = true;
    }
    public IEnumerator FadeIn(Image image)
    {
        float MaxAlpha = 1;
        flashScreen = image;
        Color color = flashScreen.color;
 
        for (color.a = 0f; color.a <= MaxAlpha; color.a += 0.01f)
        {
            flashScreen.color = new Color(color.r, color.g, color.b, color.a);
            yield return new WaitForSeconds(0.01f);
            if (color.a >= 0.9)
            {
                color.a = MaxAlpha;
                flashScreen.color = new Color(color.r, color.g, color.b, 1);
                break;
            }
        }
    }
    public IEnumerator FadeOut(Image image)
    {
        flashScreen = image;
        Color color = flashScreen.color;
        
        float MinAlpha = 0;
        for (color.a = 1f; color.a >= MinAlpha; color.a -= 0.01f)
        {
            flashScreen.color = new Color(color.r, color.g, color.b, color.a);
            yield return new WaitForSeconds(0.01f);
            if (color.a <= 0.1)
            {
                color.a = MinAlpha;
                flashScreen.color = new Color(color.r, color.g, color.b, 0);
                break;
            }
            else if (!DrainPlayer)
            {
                color.a = MinAlpha;
                flashScreen.color = new Color(color.r, color.g, color.b, 0);
                break;
            }
        }
    }
    public void ShutOffDrainScreen()
    {
        Color Drain = drainScreen.color;
        drainScreen.color = new Color (Drain.r, Drain.g, Drain.b, 0);
    }
    public void SetCheckPoint()
    {
        playerTransform = transform;
        lastPosition = playerTransform.position;
        lastRotation = playerTransform.rotation;
    }

}
