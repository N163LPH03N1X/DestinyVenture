using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerCounter : MonoBehaviour
{
    public bool StartDemo;
    public static PowerCounter PowCount;

    AudioSource audioSrc;
    public AudioClip powGemSL;
    public AudioClip powGemGD;
    public AudioClip BroadPowerup;
    public AudioClip MagicPowerup;
    public AudioClip ThunderPowerup;
    public AudioClip IcePowerup;
    public AudioClip SwordPowerup;


    [SerializeField]
    private Text UpGradeText;

    
    public Text PowerGemSLName;
    public Image PowerGemSLUI;
    public Text PowerGemSLQuanity;
    public static int PowerGemSLAmount;
    public static int PowerGemGDAmount;
    public Text PowerGemGDName;
    public Image PowerGemGDUI;
    public Text PowerGemGDQuanity;

    public static bool PowerGemSLPickedUp = false;
    public static bool PowerGemGDPickedUp = false;

    public float regenSpeed;
    [Header("BroadSword")]
    public static int broadCount = 0;
    public int upgradeBroad;
    public float broadRegenTime = 0.0f;
    public bool powerBroadUp;
    public static bool BroadSword;
    public Image broadpowerIco1;
    public Image broadpowerIco2;
    public Image broadpowerIco3;
    public Image broadpowerIco4;
    public Image broadpowerIcoOff1;
    public Image broadpowerIcoOff2;
    public Image broadpowerIcoOff3;
    public Image broadpowerIcoOff4;
    public Image broadweapFillAmount;
    public Image weaponIconBroad;
    [Space]
    [Header("MagicSword")]
    public static int magicCount = 0;
    public int upgradeMagic;
    float magicRegenTime = 0.0f;
    public bool powerMagicUp;
    public static bool MagicSword;
    bool MagicSwordPickedUp;
    public Image magicpowerIco1;
    public Image magicpowerIco2;
    public Image magicpowerIco3;
    public Image magicpowerIco4;
    public Image magicpowerIcoOff1;
    public Image magicpowerIcoOff2;
    public Image magicpowerIcoOff3;
    public Image magicpowerIcoOff4;
    public Image magicweapFillAmount;
    public Image weaponIconMagic;
    [Space]
    [Header("ThunderSword")]
    public static int thunderCount = 0;
    public int upgradeThunder;
    float thunderRegenTime = 0.0f;
    public static bool ThunderSword;
    public bool powerThunderUp;
    bool ThunderSwordPickedUp;
    public Image thunderpowerIco1;
    public Image thunderpowerIco2;
    public Image thunderpowerIco3;
    public Image thunderpowerIco4;
    public Image thunderpowerIcoOff1;
    public Image thunderpowerIcoOff2;
    public Image thunderpowerIcoOff3;
    public Image thunderpowerIcoOff4;
    public Image thunderweapFillAmount;
    public Image weaponIconThunder;
    [Space]
    [Header("IceSword")]
    public static int iceCount = 0;
    public int upgradeIce;
    float iceRegenTime = 0.0f;
    public bool powerIceUp;
    public static bool IceSword;
    bool IceSwordPickedUp;
    public Image icepowerIco1;
    public Image icepowerIco2;
    public Image icepowerIco3;
    public Image icepowerIco4;
    public Image icepowerIcoOff1;
    public Image icepowerIcoOff2;
    public Image icepowerIcoOff3;
    public Image icepowerIcoOff4;
    public Image iceweapFillAmount;
    public Image weaponIconIce;
    [Space]
    [Header("PauseMenu")]

    public int upgradePoint;
    public Text upgradePointText;

    public Image broadpauseIco1;
    public Image broadpauseIco2;
    public Image broadpauseIco3;
    public Image broadpauseIco4;
    public Image broadpauseIcoOff1;
    public Image broadpauseIcoOff2;
    public Image broadpauseIcoOff3;
    public Image broadpauseIcoOff4;

    public Image magicpauseIco1;
    public Image magicpauseIco2;
    public Image magicpauseIco3;
    public Image magicpauseIco4;
    public Image magicpauseIcoOff1;
    public Image magicpauseIcoOff2;
    public Image magicpauseIcoOff3;
    public Image magicpauseIcoOff4;

    public Image thunderpauseIco1;
    public Image thunderpauseIco2;
    public Image thunderpauseIco3;
    public Image thunderpauseIco4;
    public Image thunderpauseIcoOff1;
    public Image thunderpauseIcoOff2;
    public Image thunderpauseIcoOff3;
    public Image thunderpauseIcoOff4;

    public Image icepauseIco1;
    public Image icepauseIco2;
    public Image icepauseIco3;
    public Image icepauseIco4;
    public Image icepauseIcoOff1;
    public Image icepauseIcoOff2;
    public Image icepauseIcoOff3;
    public Image icepauseIcoOff4;

    public Button MagicSwordButton;
    public Button ThunderSwordButton;
    public Button IceSwordButton;

    public Text MagicSwordButtonText;
    public Text ThunderSwordButtonText;
    public Text IceSwordButtonText;

    public GameObject PauseUpgradeFlameImage;
    public GameObject PauseUpgradeElectroImage;
    public GameObject PauseUpgradeFrostImage;

    bool GripGauntletsEnabled;
    bool FrostBreastEnabled;

    bool addFrostUpgrade = false;
    public int bcnt;
    public int mcnt;
    public int tcnt;
    public int icnt;

    bool isPaused;

    void Start()
    {
        if (StartDemo)
        {
            AddPowerGem(1, 3);
            AddPowerGem(2, 3);
            PowerGemSLUI.enabled = true;
            PowerGemSLName.enabled = true;
            PowerGemSLQuanity.enabled = true;
            PowerGemGDUI.enabled = true;
            PowerGemGDName.enabled = true;
            PowerGemGDQuanity.enabled = true;
        }
        else
        {

            PowerGemSLAmount = 0;
            PowerGemGDAmount = 0;
            PowerGemSLUI.enabled = false;
            PowerGemSLName.enabled = false;
            PowerGemSLQuanity.enabled = false;
            PowerGemGDUI.enabled = false;
            PowerGemGDName.enabled = false;
            PowerGemGDQuanity.enabled = false;

        }
        UpGradeText.text = "";
        upgradePointText.text = "0";
       
        MagicSwordButton.enabled = false;
        MagicSwordButtonText.enabled = false;
        ThunderSwordButton.enabled = false;
        ThunderSwordButtonText.enabled = false;
        IceSwordButton.enabled = false;
        IceSwordButtonText.enabled = false;
        PauseUpgradeFlameImage.SetActive(false);
        PauseUpgradeElectroImage.SetActive(false);
        PauseUpgradeFrostImage.SetActive(false);
        SetCountQuanity();
        EnableWeapon(1);
        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
       
        handler();
        MagicSwordPickedUp = WeaponSelection.MagicSwordPickedUp;
        ThunderSwordPickedUp = WeaponSelection.ThunderSwordPickedUp;
        IceSwordPickedUp = WeaponSelection.IceSwordPickedUp;
        GripGauntletsEnabled = Gauntlets.GripGauntlets;
        FrostBreastEnabled = Breast.FrostBreast;
        isPaused = PauseMenu.Paused;

        bcnt = broadCount;
        mcnt = magicCount;
        tcnt = thunderCount;
        icnt = iceCount;

        if (upgradePoint > 0)
        {
            if (upgradePoint == 1)
            {
                UpGradeText.text = upgradePoint.ToString() + " Power Up Available!";
            }
            else
            {
                UpGradeText.text = upgradePoint.ToString() + " Power Ups Available!";
            }
            if (!isPaused)
            {
                UpGradeText.color = new Color(UpGradeText.color.r, UpGradeText.color.g, UpGradeText.color.b, Mathf.PingPong(Time.time, 1));
            }
            else
            {
                UpGradeText.color = new Color(UpGradeText.color.r, UpGradeText.color.g, UpGradeText.color.b, 1);
            }
           
        }
        else
        {
            UpGradeText.text = "";
        }


        //=======================================BroadRegenTime=================================//
        if (broadRegenTime >= 99)
        {
            if (broadCount == 1)
            {
                broadweapFillAmount.color = new Color(broadweapFillAmount.color.r, broadweapFillAmount.color.g, broadweapFillAmount.color.b, Mathf.PingPong(Time.time, 0.5f));
            }
            if (broadCount == 2)
            {
                broadweapFillAmount.color = new Color(broadweapFillAmount.color.r, broadweapFillAmount.color.g, broadweapFillAmount.color.b, Mathf.PingPong(Time.time, 0.5f));
            }
            if (broadCount == 3)
            {
                broadweapFillAmount.color = new Color(broadweapFillAmount.color.r, broadweapFillAmount.color.g, broadweapFillAmount.color.b, Mathf.PingPong(Time.time, 0.5f));
            }
            if (FrostBreastEnabled)
            {
                if (broadCount == 4)
                {
                    broadweapFillAmount.color = new Color(broadweapFillAmount.color.r, broadweapFillAmount.color.g, broadweapFillAmount.color.b, Mathf.PingPong(Time.time, 0.5f));
                }
            }
        }
        else
        {
            broadweapFillAmount.color = new Color(broadweapFillAmount.color.r, broadweapFillAmount.color.g, broadweapFillAmount.color.b, 1f);
        }

        //=======================================MagicRegenTime=================================//
        if (magicRegenTime >= 99)
        {
            if (magicCount == 1)
            {
                magicweapFillAmount.color = new Color(magicweapFillAmount.color.r, broadweapFillAmount.color.g, magicweapFillAmount.color.b, Mathf.PingPong(Time.time, 0.5f));
            }
            if (magicCount == 2)
            {
                magicweapFillAmount.color = new Color(magicweapFillAmount.color.r, broadweapFillAmount.color.g, magicweapFillAmount.color.b, Mathf.PingPong(Time.time, 0.5f));
            }
            if (magicCount == 3)
            {
                magicweapFillAmount.color = new Color(magicweapFillAmount.color.r, broadweapFillAmount.color.g, magicweapFillAmount.color.b, Mathf.PingPong(Time.time, 0.5f));
            }
            if (FrostBreastEnabled)
            {
                if (magicCount == 4)
                {
                    magicweapFillAmount.color = new Color(magicweapFillAmount.color.r, broadweapFillAmount.color.g, magicweapFillAmount.color.b, Mathf.PingPong(Time.time, 0.5f));
                }
            }
        }
        else
        {
            magicweapFillAmount.color = new Color(magicweapFillAmount.color.r, magicweapFillAmount.color.g, magicweapFillAmount.color.b, 1f);
        }

        //=======================================ThunderRegenTime=================================//
        if (thunderRegenTime >= 99)
        {
            if (thunderCount == 1)
            {
                thunderweapFillAmount.color = new Color(thunderweapFillAmount.color.r, thunderweapFillAmount.color.g, thunderweapFillAmount.color.b, Mathf.PingPong(Time.time, 0.5f));
            }
            if (thunderCount == 2)
            {
                thunderweapFillAmount.color = new Color(thunderweapFillAmount.color.r, thunderweapFillAmount.color.g, thunderweapFillAmount.color.b, Mathf.PingPong(Time.time, 0.5f));
            }
            if (thunderCount == 3)
            {
                thunderweapFillAmount.color = new Color(thunderweapFillAmount.color.r, thunderweapFillAmount.color.g, thunderweapFillAmount.color.b, Mathf.PingPong(Time.time, 0.5f));
            }
            if (FrostBreastEnabled)
            {
                if (thunderCount == 4)
                {
                    thunderweapFillAmount.color = new Color(thunderweapFillAmount.color.r, thunderweapFillAmount.color.g, thunderweapFillAmount.color.b, Mathf.PingPong(Time.time, 0.5f));
                }
            }
        }
        else
        {
            thunderweapFillAmount.color = new Color(thunderweapFillAmount.color.r, thunderweapFillAmount.color.g, thunderweapFillAmount.color.b, 1f);
        }

        //=======================================IceRegenTime=================================//
        if (iceRegenTime >= 99)
        {
            if (iceCount == 1)
            {
                iceweapFillAmount.color = new Color(iceweapFillAmount.color.r, iceweapFillAmount.color.g, iceweapFillAmount.color.b, Mathf.PingPong(Time.time, 0.5f));
            }
            if (iceCount == 2)
            {
                iceweapFillAmount.color = new Color(iceweapFillAmount.color.r, iceweapFillAmount.color.g, iceweapFillAmount.color.b, Mathf.PingPong(Time.time, 0.5f));
            }
            if (iceCount == 3)
            {
                iceweapFillAmount.color = new Color(iceweapFillAmount.color.r, iceweapFillAmount.color.g, iceweapFillAmount.color.b, Mathf.PingPong(Time.time, 0.5f));
            }
            if (FrostBreastEnabled)
            {
                if (iceCount == 4)
                {
                    iceweapFillAmount.color = new Color(iceweapFillAmount.color.r, iceweapFillAmount.color.g, iceweapFillAmount.color.b, Mathf.PingPong(Time.time, 0.5f));
                }
            }

        }
        else
        {
            iceweapFillAmount.color = new Color(iceweapFillAmount.color.r, iceweapFillAmount.color.g, iceweapFillAmount.color.b, 1f);
        }

     
        if (MagicSwordPickedUp)
        {
            MagicSwordButton.enabled = true;
            MagicSwordButtonText.enabled = true;
            PauseUpgradeFlameImage.SetActive(true);
        }
        if (ThunderSwordPickedUp)
        {
            ThunderSwordButton.enabled = true;
            ThunderSwordButtonText.enabled = true;
            PauseUpgradeElectroImage.SetActive(true);
        }
        if (IceSwordPickedUp)
        {
            IceSwordButton.enabled = true;
            IceSwordButtonText.enabled = true;
            PauseUpgradeFrostImage.SetActive(true);
        }

        if (upgradeBroad == 0)
        {
            broadpauseIco1.enabled = false;
            broadpauseIco2.enabled = false;
            broadpauseIco3.enabled = false;
            broadpauseIcoOff1.enabled = true;
            broadpauseIcoOff2.enabled = true;
            broadpauseIcoOff3.enabled = true;
           
            if (FrostBreastEnabled)
            {
                broadpauseIco4.enabled = false;
                broadpauseIcoOff4.enabled = true;
            }
            else
            {
                broadpauseIco4.enabled = false;
                broadpauseIcoOff4.enabled = false;
            }
        }
        if (upgradeMagic == 0)
        {
            magicpauseIco1.enabled = false;
            magicpauseIco2.enabled = false;
            magicpauseIco3.enabled = false;
            magicpauseIcoOff1.enabled = true;
            magicpauseIcoOff2.enabled = true;
            magicpauseIcoOff3.enabled = true;
            if (FrostBreastEnabled)
            {
                magicpauseIco4.enabled = false;
                magicpauseIcoOff4.enabled = true;
            }
            else
            {
                magicpauseIco4.enabled = false;
                magicpauseIcoOff4.enabled = false;
            }
        }
        if (upgradeThunder == 0)
        {
            thunderpauseIco1.enabled = false;
            thunderpauseIco2.enabled = false;
            thunderpauseIco3.enabled = false;
            thunderpauseIcoOff1.enabled = true;
            thunderpauseIcoOff2.enabled = true;
            thunderpauseIcoOff3.enabled = true;
            if (FrostBreastEnabled)
            {
                thunderpauseIco4.enabled = false;
                thunderpauseIcoOff4.enabled = true;
            }
            else
            {
                thunderpauseIco4.enabled = false;
                thunderpauseIcoOff4.enabled = false;
            }
        }
        if (upgradeIce == 0)
        {
            icepauseIco1.enabled = false;
            icepauseIco2.enabled = false;
            icepauseIco3.enabled = false;
            icepauseIcoOff1.enabled = true;
            icepauseIcoOff2.enabled = true;
            icepauseIcoOff3.enabled = true;
            if (FrostBreastEnabled)
            {
                icepauseIco4.enabled = false;
                icepauseIcoOff4.enabled = true;
            }
            else
            {
                icepauseIco4.enabled = false;
                icepauseIcoOff4.enabled = false;
            }
        }

        //=====================================BROADSWORDCOUNTERS=======================================//
        if (BroadSword)
        {
            magicpowerIco1.enabled = false;
            magicpowerIco2.enabled = false;
            magicpowerIco3.enabled = false;
            magicpowerIco4.enabled = false;
            magicpowerIcoOff1.enabled = false;
            magicpowerIcoOff2.enabled = false;
            magicpowerIcoOff3.enabled = false;
            magicpowerIcoOff4.enabled = false;
            thunderpowerIco1.enabled = false;
            thunderpowerIco2.enabled = false;
            thunderpowerIco3.enabled = false;
            thunderpowerIco4.enabled = false;
            thunderpowerIcoOff1.enabled = false;
            thunderpowerIcoOff2.enabled = false;
            thunderpowerIcoOff3.enabled = false;
            thunderpowerIcoOff4.enabled = false;
            icepowerIco1.enabled = false;
            icepowerIco2.enabled = false;
            icepowerIco3.enabled = false;
            icepowerIco4.enabled = false;
            icepowerIcoOff1.enabled = false;
            icepowerIcoOff2.enabled = false;
            icepowerIcoOff3.enabled = false;
            icepowerIcoOff4.enabled = false;
            weaponIconBroad.enabled = true;
            weaponIconMagic.enabled = false;
            weaponIconThunder.enabled = false;
            weaponIconIce.enabled = false;
            if (broadRegenTime < 100 && powerBroadUp)
            {
                if (upgradeBroad > 0)
                {
                    if (GripGauntletsEnabled)
                    {
                        broadRegenTime += Time.smoothDeltaTime * regenSpeed * 2;
                    }
                    else
                    {
                        broadRegenTime += Time.smoothDeltaTime * regenSpeed;
                    }
                    if (broadRegenTime > 100)
                    {
                        broadCount++;
                        audioSrc.PlayOneShot(SwordPowerup);
                        if (upgradeBroad == 1)
                        {
                            broadRegenTime = 100;
                        }
                        
                        if (upgradeBroad == 2 && broadCount < 2)
                        {
                            broadRegenTime = 0;
                          
                        }
                        else if (upgradeBroad == 2 && broadCount == 2)
                        {
                            broadRegenTime = 100;
                        }

                        if (upgradeBroad == 3 && broadCount < 3)
                        {
                            broadRegenTime = 0;
                            
                        }
                        else if (upgradeBroad == 3 && broadCount == 3)
                        {
                            broadRegenTime = 100;
                        }
                        if (FrostBreastEnabled)
                        {
                            if (upgradeBroad == 4 && broadCount < 4)
                            {
                                broadRegenTime = 0;

                            }
                            else if (upgradeBroad == 4 && broadCount == 4)
                            {
                                broadRegenTime = 100;
                            }
                        }
                    }
                }
            }
            if (broadCount == 0)
            {
                broadpowerIco1.enabled = false;
                broadpowerIco2.enabled = false;
                broadpowerIco3.enabled = false;
                broadpowerIcoOff1.enabled = true;
                broadpowerIcoOff2.enabled = true;
                broadpowerIcoOff3.enabled = true;
                if (FrostBreastEnabled)
                {
                    broadpowerIco4.enabled = false;
                    broadpowerIcoOff4.enabled = true;
                }
                else
                {
                    broadpowerIco4.enabled = false;
                    broadpowerIcoOff4.enabled = false;
                }
            }
            else if (broadCount == 1)
            {
                broadpowerIco1.enabled = true;
                broadpowerIco2.enabled = false;
                broadpowerIco3.enabled = false;
                broadpowerIcoOff1.enabled = false;
                broadpowerIcoOff2.enabled = true;
                broadpowerIcoOff3.enabled = true;
                if (FrostBreastEnabled)
                {
                    broadpowerIco4.enabled = false;
                    broadpowerIcoOff4.enabled = true;
                }
                else
                {
                    broadpowerIco4.enabled = false;
                    broadpowerIcoOff4.enabled = false;
                }

                if (upgradeBroad == 1 && broadCount == 1)
                {
                    powerBroadUp = false;
                }
            }
            else if (broadCount == 2)
            {
                broadpowerIco1.enabled = true;
                broadpowerIco2.enabled = true;
                broadpowerIco3.enabled = false;
                broadpowerIcoOff1.enabled = false;
                broadpowerIcoOff2.enabled = false;
                broadpowerIcoOff3.enabled = true;
                if (FrostBreastEnabled)
                {
                    broadpowerIco4.enabled = false;
                    broadpowerIcoOff4.enabled = true;
                }
                else
                {
                    broadpowerIco4.enabled = false;
                    broadpowerIcoOff4.enabled = false;
                }
                if (upgradeBroad == 2 && broadCount == 2)
                {
                    powerBroadUp = false;
                }
                else
                {
                    powerBroadUp = true;
                }
            }
            else if (broadCount == 3)
            {
                broadpowerIco1.enabled = true;
                broadpowerIco2.enabled = true;
                broadpowerIco3.enabled = true;
                broadpowerIcoOff1.enabled = false;
                broadpowerIcoOff2.enabled = false;
                broadpowerIcoOff3.enabled = false;
                if (FrostBreastEnabled)
                {
                    broadpowerIco4.enabled = false;
                    broadpowerIcoOff4.enabled = true;
                }
                else
                {
                    broadpowerIco4.enabled = false;
                    broadpowerIcoOff4.enabled = false;
                }
                if (upgradeBroad == 3 && broadCount == 3)
                {
                    powerBroadUp = false;
                }
                else
                {
                    powerBroadUp = true;
                }
            }
            else if (FrostBreastEnabled)
            {
                if (broadCount == 4)
                {
                    broadpowerIco1.enabled = true;
                    broadpowerIco2.enabled = true;
                    broadpowerIco3.enabled = true;
                    broadpowerIcoOff1.enabled = false;
                    broadpowerIcoOff2.enabled = false;
                    broadpowerIcoOff3.enabled = false;
                    broadpowerIco4.enabled = true;
                    broadpowerIcoOff4.enabled = false;
                    if (upgradeBroad == 4 && broadCount == 4)
                    {
                        powerBroadUp = false;
                    }
                    else
                    {
                        powerBroadUp = true;
                    }
                }
            }
            
        }
        if (MagicSword)
        {
            //=====================================MAGICSWORDCOUNTERS=======================================//
            broadpowerIco1.enabled = false;
            broadpowerIco2.enabled = false;
            broadpowerIco3.enabled = false;
            broadpowerIco4.enabled = false;
            broadpowerIcoOff1.enabled = false;
            broadpowerIcoOff2.enabled = false;
            broadpowerIcoOff3.enabled = false;
            broadpowerIcoOff4.enabled = false;
            thunderpowerIco1.enabled = false;
            thunderpowerIco2.enabled = false;
            thunderpowerIco3.enabled = false;
            thunderpowerIco4.enabled = false;
            thunderpowerIcoOff1.enabled = false;
            thunderpowerIcoOff2.enabled = false;
            thunderpowerIcoOff3.enabled = false;
            thunderpowerIcoOff4.enabled = false;
            icepowerIco1.enabled = false;
            icepowerIco2.enabled = false;
            icepowerIco3.enabled = false;
            icepowerIco4.enabled = false;
            icepowerIcoOff1.enabled = false;
            icepowerIcoOff2.enabled = false;
            icepowerIcoOff3.enabled = false;
            icepowerIcoOff4.enabled = false;
            weaponIconBroad.enabled = false;
            weaponIconMagic.enabled = true;
            weaponIconThunder.enabled = false;
            weaponIconIce.enabled = false;

            if (magicRegenTime < 100 && powerMagicUp)
            {
                if (upgradeMagic > 0)
                {
                    if (GripGauntletsEnabled)
                    {
                        magicRegenTime += Time.smoothDeltaTime * regenSpeed * 2;
                    }
                    else
                    {
                        magicRegenTime += Time.smoothDeltaTime * regenSpeed;
                    }
                   
                    if (magicRegenTime > 100)
                    {
                        magicCount++;
                        audioSrc.PlayOneShot(SwordPowerup);
                        if (upgradeMagic == 1)
                        {
                            magicRegenTime = 100;
                        }

                        if (upgradeMagic == 2 && magicCount < 2)
                        {
                            magicRegenTime = 0;

                        }
                        else if (upgradeMagic == 2 && magicCount == 2)
                        {
                            magicRegenTime = 100;
                        }

                        if (upgradeMagic == 3 && magicCount < 3)
                        {
                            magicRegenTime = 0;

                        }
                        else if (upgradeMagic == 3 && magicCount == 3)
                        {
                            magicRegenTime = 100;
                        }
                        if (FrostBreastEnabled)
                        {
                            if (upgradeMagic == 4 && magicCount < 4)
                            {
                                magicRegenTime = 0;

                            }
                            else if (upgradeMagic == 4 && magicCount == 4)
                            {
                                magicRegenTime = 100;
                            }
                        }
                    }
                }
            }
            if (magicCount == 0)
            {
                magicpowerIco1.enabled = false;
                magicpowerIco2.enabled = false;
                magicpowerIco3.enabled = false;
                magicpowerIcoOff1.enabled = true;
                magicpowerIcoOff2.enabled = true;
                magicpowerIcoOff3.enabled = true;
                if (FrostBreastEnabled)
                {
                    magicpowerIco4.enabled = false;
                    magicpowerIcoOff4.enabled = true;
                }
                else
                {
                    magicpowerIco4.enabled = false;
                    magicpowerIcoOff4.enabled = false;
                }
            }
            else if (magicCount == 1)
            {
                magicpowerIco1.enabled = true;
                magicpowerIco2.enabled = false;
                magicpowerIco3.enabled = false;
                magicpowerIcoOff1.enabled = false;
                magicpowerIcoOff2.enabled = true;
                magicpowerIcoOff3.enabled = true;
                if (FrostBreastEnabled)
                {
                    magicpowerIco4.enabled = false;
                    magicpowerIcoOff4.enabled = true;
                }
                else
                {
                    magicpowerIco4.enabled = false;
                    magicpowerIcoOff4.enabled = false;
                }
                if (upgradeMagic == 1 && magicCount == 1)
                {
                    powerMagicUp = false;
                }
            }
            else if (magicCount == 2)
            {
                magicpowerIco1.enabled = true;
                magicpowerIco2.enabled = true;
                magicpowerIco3.enabled = false;
                magicpowerIcoOff1.enabled = false;
                magicpowerIcoOff2.enabled = false;
                magicpowerIcoOff3.enabled = true;
                if (FrostBreastEnabled)
                {
                    magicpowerIco4.enabled = false;
                    magicpowerIcoOff4.enabled = true;
                }
                else
                {
                    magicpowerIco4.enabled = false;
                    magicpowerIcoOff4.enabled = false;
                }
                if (upgradeMagic == 2 && magicCount == 2)
                {
                    powerMagicUp = false;
                }
                else
                {
                    powerMagicUp = true;
                }
            }
            else if (magicCount == 3)
            {
                magicpowerIco1.enabled = true;
                magicpowerIco2.enabled = true;
                magicpowerIco3.enabled = true;
                magicpowerIcoOff1.enabled = false;
                magicpowerIcoOff2.enabled = false;
                magicpowerIcoOff3.enabled = false;
                if (FrostBreastEnabled)
                {
                    magicpowerIco4.enabled = false;
                    magicpowerIcoOff4.enabled = true;
                }
                else
                {
                    magicpowerIco4.enabled = false;
                    magicpowerIcoOff4.enabled = false;
                }
                if (upgradeMagic == 3 && magicCount == 3)
                {
                    powerMagicUp = false;
                }
                else
                {
                    powerMagicUp = true;
                }
            }
            else if (FrostBreastEnabled)
            {
                if (magicCount == 4)
                {
                    magicpowerIco1.enabled = true;
                    magicpowerIco2.enabled = true;
                    magicpowerIco3.enabled = true;
                    magicpowerIco4.enabled = true;
                    magicpowerIcoOff1.enabled = false;
                    magicpowerIcoOff2.enabled = false;
                    magicpowerIcoOff3.enabled = false;
                    magicpowerIcoOff4.enabled = false;
                    if (upgradeMagic == 4 && magicCount == 4)
                    {
                        powerMagicUp = false;
                    }
                    else
                    {
                        powerMagicUp = true;
                    }
                }
            }
        }
        if (ThunderSword)
        {
            //=====================================THUNDERSWORDCOUNTERS=======================================//
            broadpowerIco1.enabled = false;
            broadpowerIco2.enabled = false;
            broadpowerIco3.enabled = false;
            broadpowerIco4.enabled = false;
            broadpowerIcoOff1.enabled = false;
            broadpowerIcoOff2.enabled = false;
            broadpowerIcoOff3.enabled = false;
            broadpowerIcoOff4.enabled = false;
            magicpowerIco1.enabled = false;
            magicpowerIco2.enabled = false;
            magicpowerIco3.enabled = false;
            magicpowerIco4.enabled = false;
            magicpowerIcoOff1.enabled = false;
            magicpowerIcoOff2.enabled = false;
            magicpowerIcoOff3.enabled = false;
            magicpowerIcoOff4.enabled = false;
            icepowerIco1.enabled = false;
            icepowerIco2.enabled = false;
            icepowerIco3.enabled = false;
            icepowerIco4.enabled = false;
            icepowerIcoOff1.enabled = false;
            icepowerIcoOff2.enabled = false;
            icepowerIcoOff3.enabled = false;
            icepowerIcoOff4.enabled = false;
            weaponIconBroad.enabled = false;
            weaponIconMagic.enabled = false;
            weaponIconThunder.enabled = true;
            weaponIconIce.enabled = false;

            if (thunderRegenTime < 100 && powerThunderUp)
            {
                if (upgradeThunder > 0)
                {
                    if (GripGauntletsEnabled)
                    {
                        thunderRegenTime += Time.smoothDeltaTime * regenSpeed * 2;
                    }
                    else
                    {
                        thunderRegenTime += Time.smoothDeltaTime * regenSpeed;
                    }
                   
                    if (thunderRegenTime > 100)
                    {
                        thunderCount++;
                        audioSrc.PlayOneShot(SwordPowerup);
                        if (upgradeThunder == 1)
                        {
                            thunderRegenTime = 100;
                        }

                        if (upgradeThunder == 2 && thunderCount < 2)
                        {
                            thunderRegenTime = 0;

                        }
                        else if (upgradeThunder == 2 && thunderCount == 2)
                        {
                            thunderRegenTime = 100;
                        }

                        if (upgradeThunder == 3 && thunderCount < 3)
                        {
                            thunderRegenTime = 0;

                        }
                        else if (upgradeThunder == 3 && thunderCount == 3)
                        {
                            thunderRegenTime = 100;
                        }
                        if (FrostBreastEnabled)
                        {
                            if (upgradeThunder == 4 && thunderCount < 4)
                            {
                                thunderRegenTime = 0;

                            }
                            else if (upgradeThunder == 4 && thunderCount == 4)
                            {
                                thunderRegenTime = 100;
                            }
                        }
                    }
                }
            }
            if (thunderCount == 0)
            {
                thunderpowerIco1.enabled = false;
                thunderpowerIco2.enabled = false;
                thunderpowerIco3.enabled = false;
                thunderpowerIcoOff1.enabled = true;
                thunderpowerIcoOff2.enabled = true;
                thunderpowerIcoOff3.enabled = true;
                if (FrostBreastEnabled)
                {
                    thunderpowerIco4.enabled = false;
                    thunderpowerIcoOff4.enabled = true;
                }
                else
                {
                    thunderpowerIco4.enabled = false;
                    thunderpowerIcoOff4.enabled = false;
                }
            }
            else if (thunderCount == 1)
            {
                thunderpowerIco1.enabled = true;
                thunderpowerIco2.enabled = false;
                thunderpowerIco3.enabled = false;
                thunderpowerIcoOff1.enabled = false;
                thunderpowerIcoOff2.enabled = true;
                thunderpowerIcoOff3.enabled = true;
                if (FrostBreastEnabled)
                {
                    thunderpowerIco4.enabled = false;
                    thunderpowerIcoOff4.enabled = true;
                }
                else
                {
                    thunderpowerIco4.enabled = false;
                    thunderpowerIcoOff4.enabled = false;
                }
                if (upgradeThunder == 1 && thunderCount == 1)
                {
                    powerThunderUp = false;
                }
            }
            else if (thunderCount == 2)
            {
                thunderpowerIco1.enabled = true;
                thunderpowerIco2.enabled = true;
                thunderpowerIco3.enabled = false;
                thunderpowerIcoOff1.enabled = false;
                thunderpowerIcoOff2.enabled = false;
                thunderpowerIcoOff3.enabled = true;
                if (FrostBreastEnabled)
                {
                    thunderpowerIco4.enabled = false;
                    thunderpowerIcoOff4.enabled = true;
                }
                else
                {
                    thunderpowerIco4.enabled = false;
                    thunderpowerIcoOff4.enabled = false;
                }
                if (upgradeThunder == 2 && thunderCount == 2)
                {
                    powerThunderUp = false;
                }
                else
                {
                    powerThunderUp = true;
                }
            }
            else if (thunderCount == 3)
            {
                thunderpowerIco1.enabled = true;
                thunderpowerIco2.enabled = true;
                thunderpowerIco3.enabled = true;
                thunderpowerIcoOff1.enabled = false;
                thunderpowerIcoOff2.enabled = false;
                thunderpowerIcoOff3.enabled = false;
                if (FrostBreastEnabled)
                {
                    thunderpowerIco4.enabled = false;
                    thunderpowerIcoOff4.enabled = true;
                }
                else
                {
                    thunderpowerIco4.enabled = false;
                    thunderpowerIcoOff4.enabled = false;
                }
                if (upgradeThunder == 3 && thunderCount == 3)
                {
                    powerThunderUp = false;
                }
                else
                {
                    powerThunderUp = true;
                }
            }
            if (FrostBreastEnabled)
            {
                if (thunderCount == 4)
                {
                    thunderpowerIco1.enabled = true;
                    thunderpowerIco2.enabled = true;
                    thunderpowerIco3.enabled = true;
                    thunderpowerIco4.enabled = true;
                    thunderpowerIcoOff1.enabled = false;
                    thunderpowerIcoOff2.enabled = false;
                    thunderpowerIcoOff3.enabled = false;
                    thunderpowerIcoOff4.enabled = false;
                    if (upgradeThunder == 4 && thunderCount == 4)
                    {
                        powerThunderUp = false;
                    }
                    else
                    {
                        powerThunderUp = true;
                    }
                }
            }
        }
        if (IceSword)
        {
            //=====================================ICESWORDCOUNTERS=======================================//
            broadpowerIco1.enabled = false;
            broadpowerIco2.enabled = false;
            broadpowerIco3.enabled = false;
            broadpowerIco4.enabled = false;
            broadpowerIcoOff1.enabled = false;
            broadpowerIcoOff2.enabled = false;
            broadpowerIcoOff3.enabled = false;
            broadpowerIcoOff4.enabled = false;
            magicpowerIco1.enabled = false;
            magicpowerIco2.enabled = false;
            magicpowerIco3.enabled = false;
            magicpowerIco4.enabled = false;
            magicpowerIcoOff1.enabled = false;
            magicpowerIcoOff2.enabled = false;
            magicpowerIcoOff3.enabled = false;
            magicpowerIcoOff4.enabled = false;
            thunderpowerIco1.enabled = false;
            thunderpowerIco2.enabled = false;
            thunderpowerIco3.enabled = false;
            thunderpowerIco4.enabled = false;
            thunderpowerIcoOff1.enabled = false;
            thunderpowerIcoOff2.enabled = false;
            thunderpowerIcoOff3.enabled = false;
            thunderpowerIcoOff4.enabled = false;
            weaponIconBroad.enabled = false;
            weaponIconMagic.enabled = false;
            weaponIconThunder.enabled = false;
            weaponIconIce.enabled = true;
            if (iceRegenTime < 100 && powerIceUp)
            {
                if (upgradeIce > 0)
                {
                    if (GripGauntletsEnabled)
                    {
                        iceRegenTime += Time.smoothDeltaTime * regenSpeed * 2;
                    }
                    else
                    {
                        iceRegenTime += Time.smoothDeltaTime * regenSpeed;
                    }
                   
                    if (iceRegenTime > 100)
                    {
                        iceCount++;
                        audioSrc.PlayOneShot(SwordPowerup);
                        if (upgradeIce == 1)
                        {
                            iceRegenTime = 100;
                        }

                        if (upgradeIce == 2 && iceCount < 2)
                        {
                            iceRegenTime = 0;

                        }
                        else if (upgradeIce == 2 && iceCount == 2)
                        {
                            iceRegenTime = 100;
                        }

                        if (upgradeIce == 3 && iceCount < 3)
                        {
                            iceRegenTime = 0;

                        }
                        else if (upgradeIce == 3 && iceCount == 3)
                        {
                            iceRegenTime = 100;
                        }
                        if (FrostBreastEnabled)
                        {
                            if (upgradeIce == 4 && iceCount < 4)
                            {
                                iceRegenTime = 0;

                            }
                            else if (upgradeIce == 4 && iceCount == 4)
                            {
                                iceRegenTime = 100;
                            }
                        }
                    }
                }
            }
            if (iceCount == 0)
            {
                icepowerIco1.enabled = false;
                icepowerIco2.enabled = false;
                icepowerIco3.enabled = false;
                icepowerIcoOff1.enabled = true;
                icepowerIcoOff2.enabled = true;
                icepowerIcoOff3.enabled = true;
                if (FrostBreastEnabled)
                {
                    icepowerIco4.enabled = false;
                    icepowerIcoOff4.enabled = true;
                }
                else
                {
                    icepowerIco4.enabled = false;
                    icepowerIcoOff4.enabled = false;
                }
            }
            else if (iceCount == 1)
            {
                icepowerIco1.enabled = true;
                icepowerIco2.enabled = false;
                icepowerIco3.enabled = false;
                icepowerIcoOff1.enabled = false;
                icepowerIcoOff2.enabled = true;
                icepowerIcoOff3.enabled = true;
                if (FrostBreastEnabled)
                {
                    icepowerIco4.enabled = false;
                    icepowerIcoOff4.enabled = true;
                }
                else
                {
                    icepowerIco4.enabled = false;
                    icepowerIcoOff4.enabled = false;
                }
                if (upgradeIce == 1 && iceCount == 1)
                {
                    powerIceUp = false;
                }
            }
            else if (iceCount == 2)
            {
                icepowerIco1.enabled = true;
                icepowerIco2.enabled = true;
                icepowerIco3.enabled = false;
                icepowerIcoOff1.enabled = false;
                icepowerIcoOff2.enabled = false;
                icepowerIcoOff3.enabled = true;
                if (FrostBreastEnabled)
                {
                    icepowerIco4.enabled = false;
                    icepowerIcoOff4.enabled = true;
                }
                else
                {
                    icepowerIco4.enabled = false;
                    icepowerIcoOff4.enabled = false;
                }
                if (upgradeIce == 2 && iceCount == 2)
                {
                    powerIceUp = false;
                }
                else
                {
                    powerIceUp = true;
                }
            }
            else if (iceCount == 3)
            {
                icepowerIco1.enabled = true;
                icepowerIco2.enabled = true;
                icepowerIco3.enabled = true;
                icepowerIcoOff1.enabled = false;
                icepowerIcoOff2.enabled = false;
                icepowerIcoOff3.enabled = false;
                if (FrostBreastEnabled)
                {
                    icepowerIco4.enabled = false;
                    icepowerIcoOff4.enabled = true;
                }
                else
                {
                    icepowerIco4.enabled = false;
                    icepowerIcoOff4.enabled = false;
                }
                if (upgradeIce == 3 && iceCount == 3)
                {
                    powerIceUp = false;
                }
                else
                {
                    powerIceUp = true;
                }
            }
            if (FrostBreastEnabled)
            {
                if (iceCount == 4)
                {
                    icepowerIco1.enabled = true;
                    icepowerIco2.enabled = true;
                    icepowerIco3.enabled = true;
                    icepowerIcoOff1.enabled = false;
                    icepowerIcoOff2.enabled = false;
                    icepowerIcoOff3.enabled = false;
                    icepowerIco4.enabled = true;
                    icepowerIcoOff4.enabled = false;
                    if (upgradeIce == 4 && iceCount == 4)
                    {
                        powerIceUp = false;
                    }
                    else
                    {
                        powerIceUp = true;
                    }
                }
            }
        }
    }
    public void RemoveBroadCounter(int amount)
    {
        broadCount -= amount;
        powerBroadUp = true;
        if (broadCount <= 0)
        {
            broadCount = 0;
        }
        if (broadRegenTime == 100)
        {
            broadRegenTime = 0;
        }
      
    }
    public void RemoveMagicCounter(int amount)
    {
        magicCount -= amount;
        powerMagicUp = true;
        if (magicCount <= 0)
        {
            magicCount = 0;
        }
        if (magicRegenTime == 100)
        {
            magicRegenTime = 0;
        }
    }
    public void RemoveThunderCounter(int amount)
    {
        thunderCount -= amount;
        powerThunderUp = true;
        if (thunderCount <= 0)
        {
            thunderCount = 0;
        }
        if (thunderRegenTime == 100)
        {
            thunderRegenTime = 0;
        }
        
    }
    public void RemoveIceCounter(int amount)
    {
        iceCount -= amount;
        powerIceUp = true;
        if (iceCount <= 0)
        {
            iceCount = 0;
        }
        if (iceRegenTime == 100)
        {
            iceRegenTime = 0;
        }
       
    }
    void handler()
    {
        if (BroadSword)
        {
            broadweapFillAmount.enabled = true;
            magicweapFillAmount.enabled = false;
            thunderweapFillAmount.enabled = false;
            iceweapFillAmount.enabled = false;
            broadweapFillAmount.fillAmount = Map(broadRegenTime, 0, 100, 0, 1);
        }
        if (MagicSword)
        {
            broadweapFillAmount.enabled = false;
            magicweapFillAmount.enabled = true;
            thunderweapFillAmount.enabled = false;
            iceweapFillAmount.enabled = false;
            magicweapFillAmount.fillAmount = Map(magicRegenTime, 0, 100, 0, 1);
        }
        if (ThunderSword)
        {
            broadweapFillAmount.enabled = false;
            magicweapFillAmount.enabled = false;
            thunderweapFillAmount.enabled = true;
            iceweapFillAmount.enabled = false;
            thunderweapFillAmount.fillAmount = Map(thunderRegenTime, 0, 100, 0, 1);
        }
        if (IceSword)
        {
            broadweapFillAmount.enabled = false;
            magicweapFillAmount.enabled = false;
            thunderweapFillAmount.enabled = false;
            iceweapFillAmount.enabled = true;
            iceweapFillAmount.fillAmount = Map(iceRegenTime, 0, 100, 0, 1);
        }
    }
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    public void EnableWeapon(int num)
    {
        if (num == 1)
        {
            BroadSword = true;
            MagicSword = false;
            ThunderSword = false;
            IceSword = false;

        }
        if (num == 2)
        {

            BroadSword = false;
            MagicSword = true;
            ThunderSword = false;
            IceSword = false;

        }
        if (num == 3)
        {
            BroadSword = false;
            MagicSword = false;
            ThunderSword = true;
            IceSword = false;

        }
        if (num == 4)
        {
            BroadSword = false;
            MagicSword = false;
            ThunderSword = false;
            IceSword = true;

        }
    }
    public void AddUpgradePoint(int amount)
    {
        upgradePoint += amount;
        if (upgradePoint >= 12)
        {
            upgradePoint = 12;
        }
        SetCountUpgradePoint();
    }
    public void AddWeaponUpgrade(int weapon)
    {
        if (weapon == 1)
        {
            if (FrostBreastEnabled)
            {
                if (upgradePoint > 0 && upgradeBroad < 4)
                {

                    audioSrc.PlayOneShot(BroadPowerup);
                    upgradePoint -= 1;
                    upgradeBroad += 1;
                    powerBroadUp = true;
                }
            }
            else
            {
                if (upgradePoint > 0 && upgradeBroad < 3)
                {

                    audioSrc.PlayOneShot(BroadPowerup);
                    upgradePoint -= 1;
                    upgradeBroad += 1;
                    powerBroadUp = true;
                }
            }
            if (upgradePoint < 0)
            {
                upgradePoint = 0;
            }
            if (upgradeBroad == 1)
            {
                broadpauseIco1.enabled = true;
                broadpauseIco2.enabled = false;
                broadpauseIco3.enabled = false;
                broadpauseIcoOff1.enabled = false;
                broadpauseIcoOff2.enabled = true;
                broadpauseIcoOff3.enabled = true;
                if (FrostBreastEnabled)
                {
                    broadpauseIco4.enabled = false;
                    broadpauseIcoOff4.enabled = true;
                }
                else
                {
                    broadpauseIco4.enabled = false;
                    broadpauseIcoOff4.enabled = false;
                }
            }
            else if (upgradeBroad == 2)
            {
                broadpauseIco1.enabled = true;
                broadpauseIco2.enabled = true;
                broadpauseIco3.enabled = false;
                broadpauseIcoOff1.enabled = false;
                broadpauseIcoOff2.enabled = false;
                broadpauseIcoOff3.enabled = true;
                if (FrostBreastEnabled)
                {
                    broadpauseIco4.enabled = false;
                    broadpauseIcoOff4.enabled = true;
                }
                else
                {
                    broadpauseIco4.enabled = false;
                    broadpauseIcoOff4.enabled = false;
                }
            }
            else if (upgradeBroad == 3)
            {
                broadpauseIco1.enabled = true;
                broadpauseIco2.enabled = true;
                broadpauseIco3.enabled = true;
                broadpauseIcoOff1.enabled = false;
                broadpauseIcoOff2.enabled = false;
                broadpauseIcoOff3.enabled = false;
                if (FrostBreastEnabled)
                {
                    broadpauseIco4.enabled = false;
                    broadpauseIcoOff4.enabled = true;
                }
                else
                {
                    broadpauseIco4.enabled = false;
                    broadpauseIcoOff4.enabled = false;
                }
            }
            else if (upgradeBroad == 4)
            {
                broadpauseIco1.enabled = true;
                broadpauseIco2.enabled = true;
                broadpauseIco3.enabled = true;
                broadpauseIcoOff1.enabled = false;
                broadpauseIcoOff2.enabled = false;
                broadpauseIcoOff3.enabled = false;
                broadpauseIco4.enabled = true;
                broadpauseIcoOff4.enabled = false;
            }
            if (broadRegenTime == 100)
            {
                broadRegenTime = 0;
                powerBroadUp = true;
            }
        }
        if (weapon == 2)
        {
            if (MagicSwordPickedUp)
            {
                if (FrostBreastEnabled)
                {
                    if (upgradePoint > 0 && upgradeMagic < 4)
                    {
                        audioSrc.PlayOneShot(MagicPowerup);
                        upgradePoint -= 1;
                        upgradeMagic += 1;
                        powerMagicUp = true; 
                    }
                }
                else
                {
                    if (upgradePoint > 0 && upgradeMagic < 3)
                    {

                        audioSrc.PlayOneShot(MagicPowerup);
                        upgradePoint -= 1;
                        upgradeMagic += 1;
                        powerMagicUp = true;
                    }
                }
                if (upgradePoint < 0)
                {
                    upgradePoint = 0;
                }
              
                if (upgradeMagic == 1)
                {
                    magicpauseIco1.enabled = true;
                    magicpauseIco2.enabled = false;
                    magicpauseIco3.enabled = false;
                    magicpauseIcoOff1.enabled = false;
                    magicpauseIcoOff2.enabled = true;
                    magicpauseIcoOff3.enabled = true;
                    if (FrostBreastEnabled)
                    {
                        magicpauseIco4.enabled = false;
                        magicpauseIcoOff4.enabled = true;
                    }
                    else
                    {
                        magicpauseIco4.enabled = false;
                        magicpauseIcoOff4.enabled = false;
                    }
                }
                else if (upgradeMagic == 2)
                {
                    magicpauseIco1.enabled = true;
                    magicpauseIco2.enabled = true;
                    magicpauseIco3.enabled = false;
                    magicpauseIcoOff1.enabled = false;
                    magicpauseIcoOff2.enabled = false;
                    magicpauseIcoOff3.enabled = true;
                    if (FrostBreastEnabled)
                    {
                        magicpauseIco4.enabled = false;
                        magicpauseIcoOff4.enabled = true;
                    }
                    else
                    {
                        magicpauseIco4.enabled = false;
                        magicpauseIcoOff4.enabled = false;
                    }
                }
                else if (upgradeMagic == 3)
                {
                    magicpauseIco1.enabled = true;
                    magicpauseIco2.enabled = true;
                    magicpauseIco3.enabled = true;
                    magicpauseIcoOff1.enabled = false;
                    magicpauseIcoOff2.enabled = false;
                    magicpauseIcoOff3.enabled = false;
                    if (FrostBreastEnabled)
                    {
                        magicpauseIco4.enabled = false;
                        magicpauseIcoOff4.enabled = true;
                    }
                    else
                    {
                        magicpauseIco4.enabled = false;
                        magicpauseIcoOff4.enabled = false;
                    }
                }
                else if (upgradeMagic == 4)
                {
                    magicpauseIco1.enabled = true;
                    magicpauseIco2.enabled = true;
                    magicpauseIco3.enabled = true;
                    magicpauseIcoOff1.enabled = false;
                    magicpauseIcoOff2.enabled = false;
                    magicpauseIcoOff3.enabled = false;
                    magicpauseIco4.enabled = true;
                    magicpauseIcoOff4.enabled = false;
                }
            }
            if (magicRegenTime == 100)
            {
                magicRegenTime = 0;
                powerMagicUp = true;
            }
        }
        if (weapon == 3)
        {
            if (ThunderSwordPickedUp)
            {
                if (FrostBreastEnabled)
                {
                    if (upgradePoint > 0 && upgradeThunder < 4)
                    {

                        audioSrc.PlayOneShot(ThunderPowerup);
                        upgradePoint -= 1;
                        upgradeThunder += 1;
                        powerThunderUp = true;
                    }
                }
                else
                {
                    if (upgradePoint > 0 && upgradeThunder < 3)
                    {

                        audioSrc.PlayOneShot(ThunderPowerup);
                        upgradePoint -= 1;
                        upgradeThunder += 1;
                        powerThunderUp = true;
                    }
                }
                if (upgradePoint < 0)
                {
                    upgradePoint = 0;
                }
              
                if (upgradeThunder == 1)
                {
                    thunderpauseIco1.enabled = true;
                    thunderpauseIco2.enabled = false;
                    thunderpauseIco3.enabled = false;
                    thunderpauseIcoOff1.enabled = false;
                    thunderpauseIcoOff2.enabled = true;
                    thunderpauseIcoOff3.enabled = true;
                    if (FrostBreastEnabled)
                    {
                        thunderpauseIco4.enabled = false;
                        thunderpauseIcoOff4.enabled = true;
                    }
                    else
                    {
                        thunderpauseIco4.enabled = false;
                        thunderpauseIcoOff4.enabled = false;
                    }
                }
                else if (upgradeThunder == 2)
                {
                    thunderpauseIco1.enabled = true;
                    thunderpauseIco2.enabled = true;
                    thunderpauseIco3.enabled = false;
                    thunderpauseIcoOff1.enabled = false;
                    thunderpauseIcoOff2.enabled = false;
                    thunderpauseIcoOff3.enabled = true;
                    if (FrostBreastEnabled)
                    {
                        thunderpauseIco4.enabled = false;
                        thunderpauseIcoOff4.enabled = true;
                    }
                    else
                    {
                        thunderpauseIco4.enabled = false;
                        thunderpauseIcoOff4.enabled = false;
                    }
                }
                else if (upgradeThunder == 3)
                {
                    thunderpauseIco1.enabled = true;
                    thunderpauseIco2.enabled = true;
                    thunderpauseIco3.enabled = true;
                    thunderpauseIcoOff1.enabled = false;
                    thunderpauseIcoOff2.enabled = false;
                    thunderpauseIcoOff3.enabled = false;
                    if (FrostBreastEnabled)
                    {
                        thunderpauseIco4.enabled = false;
                        thunderpauseIcoOff4.enabled = true;
                    }
                    else
                    {
                        thunderpauseIco4.enabled = false;
                        thunderpauseIcoOff4.enabled = false;
                    }
                }
                else if (upgradeThunder == 4)
                {
                    thunderpauseIco1.enabled = true;
                    thunderpauseIco2.enabled = true;
                    thunderpauseIco3.enabled = true;
                    thunderpauseIcoOff1.enabled = false;
                    thunderpauseIcoOff2.enabled = false;
                    thunderpauseIcoOff3.enabled = false;
                    thunderpauseIco4.enabled = true;
                    thunderpauseIcoOff4.enabled = false;
                }
            }
            if (thunderRegenTime == 100)
            {
                thunderRegenTime = 0;
                powerThunderUp = true;
            }
        }
        if (weapon == 4)
        {
            if (IceSwordPickedUp)
            {
                if (FrostBreastEnabled)
                {
                    if (upgradePoint > 0 && upgradeIce < 4)
                    {

                        audioSrc.PlayOneShot(IcePowerup);
                        upgradePoint -= 1;
                        upgradeIce += 1;
                        powerIceUp = true;
                    }
                }
                else
                {
                    if (upgradePoint > 0 && upgradeIce < 3)
                    {

                        audioSrc.PlayOneShot(IcePowerup);
                        upgradePoint -= 1;
                        upgradeIce += 1;
                        powerIceUp = true;
                    }
                }
                if (upgradePoint < 0)
                {
                    upgradePoint = 0;
                }
              
                if (upgradeIce == 1)
                {
                    icepauseIco1.enabled = true;
                    icepauseIco2.enabled = false;
                    icepauseIco3.enabled = false;
                    icepauseIcoOff1.enabled = false;
                    icepauseIcoOff2.enabled = true;
                    icepauseIcoOff3.enabled = true;
                    if (FrostBreastEnabled)
                    {
                        icepauseIco4.enabled = false;
                        icepauseIcoOff4.enabled = true;
                    }
                    else
                    {
                        icepauseIco4.enabled = false;
                        icepauseIcoOff4.enabled = false;
                    }
                }
                else if (upgradeIce == 2)
                {
                    icepauseIco1.enabled = true;
                    icepauseIco2.enabled = true;
                    icepauseIco3.enabled = false;
                    icepauseIcoOff1.enabled = false;
                    icepauseIcoOff2.enabled = false;
                    icepauseIcoOff3.enabled = true;
                    if (FrostBreastEnabled)
                    {
                        icepauseIco4.enabled = false;
                        icepauseIcoOff4.enabled = true;
                    }
                    else
                    {
                        icepauseIco4.enabled = false;
                        icepauseIcoOff4.enabled = false;
                    }
                }
                else if (upgradeIce == 3)
                {
                    icepauseIco1.enabled = true;
                    icepauseIco2.enabled = true;
                    icepauseIco3.enabled = true;
                    icepauseIcoOff1.enabled = false;
                    icepauseIcoOff2.enabled = false;
                    icepauseIcoOff3.enabled = false;
                    if (FrostBreastEnabled)
                    {
                        icepauseIco4.enabled = false;
                        icepauseIcoOff4.enabled = true;
                    }
                    else
                    {
                        icepauseIco4.enabled = false;
                        icepauseIcoOff4.enabled = false;
                    }
                }
                else if (upgradeIce == 4)
                {
                    icepauseIco1.enabled = true;
                    icepauseIco2.enabled = true;
                    icepauseIco3.enabled = true;
                    icepauseIcoOff1.enabled = false;
                    icepauseIcoOff2.enabled = false;
                    icepauseIcoOff3.enabled = false;
                    icepauseIco4.enabled = true;
                    icepauseIcoOff4.enabled = false;   
                }
                    
            }
            if (iceRegenTime == 100)
            {
                iceRegenTime = 0;
                powerIceUp = true;
            }
        }
        SetCountUpgradePoint();
    }
    public void SetCountUpgradePoint()
    {
        upgradePointText.text = upgradePoint.ToString();
    }
    public void AddPowerGem(int num,int amount)
    {
        if (num == 1)
        {
            
            if (PowerGemSLAmount >= 3)
            {
                PowerGemSLAmount = 3;
            }
            else
            {
                PowerGemSLPickedUp = true;
                PowerGemSLUI.enabled = true;
                PowerGemSLName.enabled = true;
                PowerGemSLQuanity.enabled = true;
                PowerGemSLAmount += amount;
            }
        }
        if (num == 2)
        {
            
            if (PowerGemGDAmount >= 3)
            {
                PowerGemGDAmount = 3;
            }
            else
            {
                PowerGemGDPickedUp = true;
                PowerGemGDUI.enabled = true;
                PowerGemGDName.enabled = true;
                PowerGemGDQuanity.enabled = true;
                PowerGemGDAmount += amount;
            }
        }
        SetCountQuanity();


    }
    public void RemovePowerGem(int num)
    {
        FrostBreastEnabled = Breast.FrostBreast;
        if (num == 1)
        {
            if (BroadSword)
            {
             
                if (broadCount < 4 && upgradeBroad == 4)
                {
                    if (PowerGemSLAmount <= 0)
                    {
                        PowerGemSLAmount = 0;
                    }
                    else
                    {
                        PowerGemSLAmount -= 1;
                        AddBroadCounter(1);
                    }
                }
                if (broadCount < 3 && upgradeBroad == 3)
                {
                    if (PowerGemSLAmount <= 0)
                    {
                        PowerGemSLAmount = 0;
                    }
                    else
                    {
                        PowerGemSLAmount -= 1;
                        AddBroadCounter(1);
                    }
                }
                if (broadCount < 2 && upgradeBroad == 2)
                {
                    if (PowerGemSLAmount <= 0)
                    {
                        PowerGemSLAmount = 0;
                    }
                    else
                    {
                        PowerGemSLAmount -= 1;
                        AddBroadCounter(1);
                    }
                }
                if (broadCount < 1 && upgradeBroad == 1)
                {
                    if (PowerGemSLAmount <=0 )
                    {
                        PowerGemSLAmount = 0;
                    }
                    else
                    {
                        PowerGemSLAmount -= 1;
                        AddBroadCounter(1);
                    }
                }
            }
            if (MagicSword)
            {
                if (magicCount < 1 && upgradeMagic == 1)
                {
                    if (PowerGemSLAmount <= 0)
                    {
                        PowerGemSLAmount = 0;
                    }
                    else
                    {
                        PowerGemSLAmount -= 1;
                        AddMagicCounter(1);
                    }
                }
                if (magicCount < 2 && upgradeMagic == 2)
                {
                    if (PowerGemSLAmount <= 0)
                    {
                        PowerGemSLAmount = 0;
                    }
                    else
                    {
                        PowerGemSLAmount -= 1;
                        AddMagicCounter(1);
                    }
                }
                if (magicCount < 3 && upgradeMagic == 3)
                {
                    if (PowerGemSLAmount <= 0)
                    {
                        PowerGemSLAmount = 0;
                    }
                    else
                    {
                        PowerGemSLAmount -= 1;
                        AddMagicCounter(1);
                    }
                }
                if (magicCount < 4 && upgradeMagic == 4)
                {
                    if (PowerGemSLAmount <= 0)
                    {
                        PowerGemSLAmount = 0;
                    }
                    else
                    {
                        PowerGemSLAmount -= 1;
                        AddMagicCounter(1);
                    }
                }
            }
            if (ThunderSword)
            {
                if (thunderCount < 1 && upgradeThunder == 1)
                {
                    if (PowerGemSLAmount <= 0)
                    {
                        PowerGemSLAmount = 0;
                    }
                    else
                    {
                        PowerGemSLAmount -= 1;
                        AddThunderCounter(1);
                    }
                }
                if (thunderCount < 2 && upgradeThunder == 2)
                {
                    if (PowerGemSLAmount <= 0)
                    {
                        PowerGemSLAmount = 0;
                    }
                    else
                    {
                        PowerGemSLAmount -= 1;
                        AddThunderCounter(1);
                    }
                }
                if (thunderCount < 3 && upgradeThunder == 3)
                {
                    if (PowerGemSLAmount <= 0)
                    {
                        PowerGemSLAmount = 0;
                    }
                    else
                    {
                        PowerGemSLAmount -= 1;
                        AddThunderCounter(1);
                    }
                }
                if (thunderCount < 4 && upgradeThunder == 4)
                {
                    if (PowerGemSLAmount <= 0)
                    {
                        PowerGemSLAmount = 0;
                    }
                    else
                    {
                        PowerGemSLAmount -= 1;
                        AddThunderCounter(1);
                    }
                }
            }
            if (IceSword)
            {
                if (iceCount < 1 && upgradeIce == 1)
                {
                    if (PowerGemSLAmount <= 0)
                    {
                        PowerGemSLAmount = 0;
                    }
                    else
                    {
                        PowerGemSLAmount -= 1;
                        AddIceCounter(1);
                    }
                }
                if (iceCount < 2 && upgradeIce == 2)
                {
                    if (PowerGemSLAmount <= 0)
                    {
                        PowerGemSLAmount = 0;
                    }
                    else
                    {
                        PowerGemSLAmount -= 1;
                        AddIceCounter(1);
                    }
                }
                if (iceCount < 3 && upgradeIce == 3)
                {
                    if (PowerGemSLAmount <= 0)
                    {
                        PowerGemSLAmount = 0;
                    }
                    else
                    {
                        PowerGemSLAmount -= 1;
                        AddIceCounter(1);
                    }
                }
                if (iceCount < 4 && upgradeIce == 4)
                {
                    if (PowerGemSLAmount <= 0)
                    {
                        PowerGemSLAmount = 0;
                    }
                    else
                    {
                        PowerGemSLAmount -= 1;
                        AddIceCounter(1);
                    }
                }
            }
        }
        if (num == 2)
        {
            if (BroadSword)
            {
                if (broadCount < 4 && upgradeBroad == 4)
                {
                    if (PowerGemGDAmount <= 0)
                    {
                        PowerGemGDAmount = 0;
                    }
                    else
                    {
                        if (broadCount >= 4)
                        {
                            broadCount = 4;
                        }
                        else
                        {
                            PowerGemGDAmount -= 1;
                            AddBroadCounter(4);
                        }
                    }
                }
                if (broadCount < 3 && upgradeBroad == 3)
                {
                    if (PowerGemGDAmount <= 0)
                    {
                        PowerGemGDAmount = 0;
                    }
                    else
                    {
                        if (broadCount >= 3)
                        {
                            broadCount = 3;
                        }
                        else
                        {
                            PowerGemGDAmount -= 1;
                            AddBroadCounter(3);
                        }
                    }
                }
                if (broadCount < 2 && upgradeBroad == 2)
                {
                    if (PowerGemGDAmount <= 0)
                    {
                        PowerGemGDAmount = 0;
                    }
                    else
                    {
                        PowerGemGDAmount -= 1;
                        AddBroadCounter(2);
                        if (broadCount >= 2)
                        {
                            broadCount = 2;
                            if (broadRegenTime <= 100)
                            {
                                broadRegenTime = 100;
                            }
                        }

                    }
                }
                if (broadCount < 1 && upgradeBroad == 1)
                {
                    if (PowerGemGDAmount <= 0)
                    {
                        PowerGemGDAmount = 0;
                    }
                    else
                    {
                        PowerGemGDAmount -= 1;
                        AddBroadCounter(1);
                    }
                }
            }
            if (MagicSword)
            {
                if (magicCount < 1 && upgradeMagic == 1)
                {
                    if (PowerGemGDAmount <= 0)
                    {
                        PowerGemGDAmount = 0;
                    }
                    else
                    {
                        PowerGemGDAmount -= 1;
                        AddMagicCounter(1);
                    }
                }
                if (magicCount < 2 && upgradeMagic == 2)
                {
                    if (PowerGemGDAmount <= 0)
                    {
                        PowerGemGDAmount = 0;
                    }
                    else
                    {
                        PowerGemGDAmount -= 1;
                        AddMagicCounter(2);
                        if (magicCount >= 2)
                        {
                            magicCount = 2;
                            if (magicRegenTime <= 100)
                            {
                                magicRegenTime = 100;
                            }
                        }
                    }
                }
                if (magicCount < 3 && upgradeMagic == 3)
                {
                    if (PowerGemGDAmount <= 0)
                    {
                        PowerGemGDAmount = 0;
                    }
                    else
                    {
                        if (magicCount >= 3)
                        {
                            magicCount = 3;
                        }
                        else
                        {
                            PowerGemGDAmount -= 1;
                            AddMagicCounter(3);
                        }
                    }
                }
                if (magicCount < 4 && upgradeMagic == 4)
                {
                    if (PowerGemGDAmount <= 0)
                    {
                        PowerGemGDAmount = 0;
                    }
                    else
                    {
                        if (magicCount >= 4)
                        {
                            magicCount = 4;
                        }
                        else
                        {
                            PowerGemGDAmount -= 1;
                            AddMagicCounter(4);
                        }
                    }
                }
            }
            if (ThunderSword)
            {
                if (thunderCount < 1 && upgradeThunder == 1)
                {
                    if (PowerGemGDAmount <= 0)
                    {
                        PowerGemGDAmount = 0;
                    }
                    else
                    {
                        PowerGemGDAmount -= 1;
                        AddThunderCounter(1);

                    }
                }
                if (thunderCount < 2 && upgradeThunder == 2)
                {
                    if (PowerGemGDAmount <= 0)
                    {
                        PowerGemGDAmount = 0;
                    }
                    else
                    {
                   
                        PowerGemGDAmount -= 1;
                        AddThunderCounter(2);
                        if (thunderCount >= 2)
                        {
                            thunderCount = 2;
                            if (thunderRegenTime <= 100)
                            {
                                thunderRegenTime = 100;
                            }
                        }
                    }
                }
                if (thunderCount < 3 && upgradeThunder == 3)
                {
                    if (PowerGemGDAmount <= 0)
                    {
                        PowerGemGDAmount = 0;
                    }
                    else
                    {
                        if (thunderCount >= 3)
                        {
                            thunderCount = 3;
                        }
                        else
                        {
                            PowerGemGDAmount -= 1;
                            AddThunderCounter(3);
                        }
                    }
                }
                if (thunderCount < 4 && upgradeThunder == 4)
                {
                    if (PowerGemGDAmount <= 0)
                    {
                        PowerGemGDAmount = 0;
                    }
                    else
                    {
                        if (thunderCount >= 4)
                        {
                            thunderCount = 4;
                        }
                        else
                        {
                            PowerGemGDAmount -= 1;
                            AddThunderCounter(4);
                        }
                    }
                }
            }
            if (IceSword)
            {
                if (iceCount < 1 && upgradeIce == 1)
                {
                    if (PowerGemGDAmount <= 0)
                    {
                        PowerGemGDAmount = 0;
                    }
                    else
                    {
                        PowerGemGDAmount -= 1;
                        AddIceCounter(1);
                    }
                }
                if (iceCount < 2 && upgradeIce == 2)
                {
                    if (PowerGemGDAmount <= 0)
                    {
                        PowerGemGDAmount = 0;
                    }
                    else
                    {
                        PowerGemGDAmount -= 1;
                        AddIceCounter(2);
                        if (iceCount >= 2)
                        {
                            iceCount = 2;
                            if (iceRegenTime <= 100)
                            {
                                iceRegenTime = 100;
                            }
                        }
                    }
                }
                if (iceCount < 3 && upgradeIce == 3)
                {
                    if (PowerGemGDAmount <= 0)
                    {
                        PowerGemGDAmount = 0;
                    }
                    else
                    {
                        if (iceCount >= 3)
                        {
                            iceCount = 3;
                        }
                        else
                        {
                            PowerGemGDAmount -= 1;
                            AddIceCounter(3);
                        }
                    }
                }
                if (iceCount < 4 && upgradeIce == 4)
                {
                    if (PowerGemGDAmount <= 0)
                    {
                        PowerGemGDAmount = 0;
                    }
                    else
                    {
                        if (iceCount >= 4)
                        {
                            iceCount = 4;
                        }
                        else
                        {
                            PowerGemGDAmount -= 1;
                            AddIceCounter(4);
                        }
                    }
                }
            }
        }
        SetCountQuanity();
       
    }
    public void AddBroadCounter(int amount)
    {
        if (upgradeBroad > 0)
        {
            broadCount += amount;
            if (broadCount > 4 && FrostBreastEnabled)
            {
                broadCount = 4;
            }
            else if (broadCount > 3 && !FrostBreastEnabled)
            {
                broadCount = 3;
            }
            if (upgradeBroad == 1)
            {
                if (broadRegenTime < 100)
                {
                    broadRegenTime = 100;
                }
            }
            else if (upgradeBroad == 2)
            {
                if (broadRegenTime < 100 && broadCount == 2)
                {
                    broadRegenTime = 100;
                }

            }
            else if (upgradeBroad == 3)
            {
                if (broadRegenTime < 100 && broadCount == 3)
                {
                    broadRegenTime = 100;
                }
            }
            else if (upgradeBroad == 4)
            {
                if (broadRegenTime < 100 && broadCount == 4)
                {
                    broadRegenTime = 100;
                }
            }
        }
    }
    public void AddMagicCounter(int amount)
    {
        if (upgradeMagic > 0)
        {
            magicCount += amount;
            if (magicCount > 4 && FrostBreastEnabled)
            {
                magicCount = 4;
            }
            else if (magicCount > 3 && !FrostBreastEnabled)
            {
                magicCount = 3;
            }
            if (magicCount > 4)
            {
                magicCount = 4;
            }
            if (upgradeMagic == 1)
            {
                if (magicRegenTime < 100)
                {
                    magicRegenTime = 100;
                }
            }
            else if (upgradeMagic == 2)
            {
                if (magicRegenTime < 100 && magicCount == 2)
                {
                    magicRegenTime = 100;
                }

            }
            else if (upgradeMagic == 3)
            {
                if (magicRegenTime < 100 && magicCount == 3)
                {
                    magicRegenTime = 100;
                }
            }
            else if (upgradeMagic == 4)
            {
                if (magicRegenTime < 100 && magicCount == 4)
                {
                    magicRegenTime = 100;
                }
            }
        }
    }
    public void AddThunderCounter(int amount)
    {
        if (upgradeThunder > 0)
        {
            thunderCount += amount;
            if (thunderCount > 4 && FrostBreastEnabled)
            {
                thunderCount = 4;
            }
            else if (thunderCount > 3 && !FrostBreastEnabled)
            {
                thunderCount = 3;
            }
            if (thunderCount > 4)
            {
                thunderCount = 4;
            }
            if (upgradeThunder == 1)
            {
                if (thunderRegenTime < 100)
                {
                    thunderRegenTime = 100;
                }
            }
            else if (upgradeThunder == 2)
            {
                if (thunderRegenTime < 100 && thunderCount == 2)
                {
                    thunderRegenTime = 100;
                }

            }
            else if (upgradeThunder == 3)
            {
                if (thunderRegenTime < 100 && thunderCount == 3)
                {
                    thunderRegenTime = 100;
                }
            }
            else if (upgradeThunder == 4)
            {
                if (thunderRegenTime < 100 && thunderCount == 4)
                {
                    thunderRegenTime = 100;
                }
            }
        }
    }
    public void AddIceCounter(int amount)
    {
        if (upgradeIce > 0)
        {
            iceCount += amount;
            if (iceCount > 4 && FrostBreastEnabled)
            {
                iceCount = 4;
            }
            else if (iceCount > 3 && !FrostBreastEnabled)
            {
                iceCount = 3;
            }
            if (upgradeIce == 1)
            {
                if (iceRegenTime < 100)
                {
                    iceRegenTime = 100;
                }
            }
            else if (upgradeIce == 2)
            {
                if (iceRegenTime < 100 && iceCount == 2)
                {
                    iceRegenTime = 100;
                }

            }
            else if (upgradeIce == 3)
            {
                if (iceRegenTime < 100 && iceCount == 3)
                {
                    iceRegenTime = 100;
                }
            }
            else if (upgradeIce == 4)
            {
                if (iceRegenTime < 100 && iceCount == 4)
                {
                    iceRegenTime = 100;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerGemSL"))
        {
            if (PowerGemSLAmount != 3)
            {
                audioSrc.PlayOneShot(powGemSL);
                AddPowerGem(1, 1);
            }
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("PowerGemGD"))
        {
            if (PowerGemGDAmount != 3)
            {
                audioSrc.PlayOneShot(powGemGD);
                AddPowerGem(2, 1);
            }
            Destroy(other.gameObject);
        }

    }
    public void SetCountQuanity()
    {
       PowerGemSLQuanity.text = PowerGemSLAmount.ToString();
       PowerGemGDQuanity.text = PowerGemGDAmount.ToString();
    }
    public void EnableFrostBreast(bool On)
    {
        
        if (On && !addFrostUpgrade)
        {
            //===================================================BroadSword======================================//
            upgradeBroad += 1;
            upgradeMagic += 1;
            upgradeThunder += 1;
            upgradeIce += 1;
            if (upgradeBroad == 1)
            {
                broadpauseIco1.enabled = true;
                broadpauseIco2.enabled = false;
                broadpauseIco3.enabled = false;
                broadpauseIco4.enabled = false;
                broadpauseIcoOff1.enabled = false;
                broadpauseIcoOff2.enabled = true;
                broadpauseIcoOff3.enabled = true;
                broadpauseIcoOff4.enabled = true;
                if (broadRegenTime < 100)
                {
                    broadRegenTime = 0;
                    powerBroadUp = true;
                }
            }
            else if (upgradeBroad == 2)
            {
                broadpauseIco1.enabled = true;
                broadpauseIco2.enabled = true;
                broadpauseIco3.enabled = false;
                broadpauseIco4.enabled = false;
                broadpauseIcoOff1.enabled = false;
                broadpauseIcoOff2.enabled = false;
                broadpauseIcoOff3.enabled = true;
                broadpauseIcoOff4.enabled = true;
            }
            else if (upgradeBroad == 3)
            {
                broadpauseIco1.enabled = true;
                broadpauseIco2.enabled = true;
                broadpauseIco3.enabled = true;
                broadpauseIco4.enabled = false;
                broadpauseIcoOff1.enabled = false;
                broadpauseIcoOff2.enabled = false;
                broadpauseIcoOff3.enabled = false;
                broadpauseIcoOff4.enabled = true;
            }
            else if (upgradeBroad == 4)
            {
                broadpauseIco1.enabled = true;
                broadpauseIco2.enabled = true;
                broadpauseIco3.enabled = true;
                broadpauseIco4.enabled = true;
                broadpauseIcoOff1.enabled = false;
                broadpauseIcoOff2.enabled = false;
                broadpauseIcoOff3.enabled = false;
                broadpauseIcoOff4.enabled = false;
            }
            if (broadRegenTime == 100)
            {
                broadRegenTime = 0;
                powerBroadUp = true;
            }
            //===================================================MagicSword======================================//
  
            if (upgradeMagic == 1)
            {
                magicpauseIco1.enabled = true;
                magicpauseIco2.enabled = false;
                magicpauseIco3.enabled = false;
                magicpauseIco4.enabled = false;
                magicpauseIcoOff1.enabled = false;
                magicpauseIcoOff2.enabled = true;
                magicpauseIcoOff3.enabled = true;
                magicpauseIcoOff4.enabled = true;
                if (magicRegenTime < 100)
                {
                    magicRegenTime = 0;
                    powerMagicUp = true;
                }
            }
            else if (upgradeMagic == 2)
            {
                magicpauseIco1.enabled = true;
                magicpauseIco2.enabled = true;
                magicpauseIco3.enabled = false;
                magicpauseIco4.enabled = false;
                magicpauseIcoOff1.enabled = false;
                magicpauseIcoOff2.enabled = false;
                magicpauseIcoOff3.enabled = true;
                magicpauseIcoOff4.enabled = true;
            }
            else if (upgradeMagic == 3)
            {
                magicpauseIco1.enabled = true;
                magicpauseIco2.enabled = true;
                magicpauseIco3.enabled = true;
                magicpauseIco4.enabled = false;
                magicpauseIcoOff1.enabled = false;
                magicpauseIcoOff2.enabled = false;
                magicpauseIcoOff3.enabled = false;
                magicpauseIcoOff4.enabled = true;
            }
            else if (upgradeMagic == 4)
            {
                magicpauseIco1.enabled = true;
                magicpauseIco2.enabled = true;
                magicpauseIco3.enabled = true;
                magicpauseIco4.enabled = true;
                magicpauseIcoOff1.enabled = false;
                magicpauseIcoOff2.enabled = false;
                magicpauseIcoOff3.enabled = false;
                magicpauseIcoOff4.enabled = false;
            }
            
            if (magicRegenTime == 100)
            {
                magicRegenTime = 0;
                powerMagicUp = true;
            }
            //===================================================ThunderSword======================================//

            if (upgradeThunder == 1)
            {
                thunderpauseIco1.enabled = true;
                thunderpauseIco2.enabled = false;
                thunderpauseIco3.enabled = false;
                thunderpauseIco4.enabled = false;
                thunderpauseIcoOff1.enabled = false;
                thunderpauseIcoOff2.enabled = true;
                thunderpauseIcoOff3.enabled = true;
                thunderpauseIcoOff4.enabled = true;
                if (thunderRegenTime < 100)
                {
                    thunderRegenTime = 0;
                    powerThunderUp = true;
                }
            }
            else if (upgradeThunder == 2)
            {
                thunderpauseIco1.enabled = true;
                thunderpauseIco2.enabled = true;
                thunderpauseIco3.enabled = false;
                thunderpauseIco4.enabled = false;
                thunderpauseIcoOff1.enabled = false;
                thunderpauseIcoOff2.enabled = false;
                thunderpauseIcoOff3.enabled = true;
                thunderpauseIcoOff4.enabled = true;
            }
            else if (upgradeThunder == 3)
            {
                thunderpauseIco1.enabled = true;
                thunderpauseIco2.enabled = true;
                thunderpauseIco3.enabled = true;
                thunderpauseIco4.enabled = false;
                thunderpauseIcoOff1.enabled = false;
                thunderpauseIcoOff2.enabled = false;
                thunderpauseIcoOff3.enabled = false;
                thunderpauseIcoOff4.enabled = true;
            }
            else if (upgradeThunder == 4)
            {
                thunderpauseIco1.enabled = true;
                thunderpauseIco2.enabled = true;
                thunderpauseIco3.enabled = true;
                thunderpauseIco4.enabled = true;
                thunderpauseIcoOff1.enabled = false;
                thunderpauseIcoOff2.enabled = false;
                thunderpauseIcoOff3.enabled = false;
                thunderpauseIcoOff4.enabled = false;
            }
            if (thunderRegenTime == 100)
            {
                thunderRegenTime = 0;
                powerThunderUp = true;
            }
            //===================================================IceSword======================================//

            if (upgradeIce == 1)
            {
                icepauseIco1.enabled = true;
                icepauseIco2.enabled = false;
                icepauseIco3.enabled = false;
                icepauseIco4.enabled = false;
                icepauseIcoOff1.enabled = false;
                icepauseIcoOff2.enabled = true;
                icepauseIcoOff3.enabled = true;
                icepauseIcoOff4.enabled = true;
                if (iceRegenTime < 100)
                {
                    iceRegenTime = 0;
                    powerIceUp = true;
                }
            }
            else if (upgradeIce == 2)
            {
                icepauseIco1.enabled = true;
                icepauseIco2.enabled = true;
                icepauseIco3.enabled = false;
                icepauseIco4.enabled = false;
                icepauseIcoOff1.enabled = false;
                icepauseIcoOff2.enabled = false;
                icepauseIcoOff3.enabled = true;
                icepauseIcoOff4.enabled = true;
            }
            else if (upgradeIce == 3)
            {
                icepauseIco1.enabled = true;
                icepauseIco2.enabled = true;
                icepauseIco3.enabled = true;
                icepauseIco4.enabled = false;
                icepauseIcoOff1.enabled = false;
                icepauseIcoOff2.enabled = false;
                icepauseIcoOff3.enabled = false;
                icepauseIcoOff4.enabled = true;
            }
            else if (upgradeIce == 4)
            {
                icepauseIco1.enabled = true;
                icepauseIco2.enabled = true;
                icepauseIco3.enabled = true;
                icepauseIco4.enabled = true;
                icepauseIcoOff1.enabled = false;
                icepauseIcoOff2.enabled = false;
                icepauseIcoOff3.enabled = false;
                icepauseIcoOff4.enabled = false;
            }
        
            if (iceRegenTime == 100)
            {
                iceRegenTime = 0;
                powerIceUp = true;
            }
            addFrostUpgrade = true;
        }
        else if(!On && addFrostUpgrade)
        {
            upgradeBroad -= 1;
            upgradeMagic -= 1;
            upgradeThunder -= 1;
            upgradeIce -= 1;
            if (upgradeBroad == 0)
            {
                broadpauseIco1.enabled = false;
                broadpauseIco2.enabled = false;
                broadpauseIco3.enabled = false;
                broadpauseIco4.enabled = false;
                broadpauseIcoOff1.enabled = true;
                broadpauseIcoOff2.enabled = true;
                broadpauseIcoOff3.enabled = true;
                broadpauseIcoOff4.enabled = false;
                if (broadRegenTime == 100)
                {
                    if (broadCount == 1)
                    {
                        broadCount -= 1;
                        broadRegenTime = 0;
                        powerBroadUp = false;
                    }
                    
                }
                else if (broadRegenTime < 100)
                {
                    broadRegenTime = 0;
                    powerBroadUp = false;
                }
            }

            else if (upgradeBroad == 1)
            {
                broadpauseIco1.enabled = true;
                broadpauseIco2.enabled = false;
                broadpauseIco3.enabled = false;
                broadpauseIco4.enabled = false;
                broadpauseIcoOff1.enabled = false;
                broadpauseIcoOff2.enabled = true;
                broadpauseIcoOff3.enabled = true;
                broadpauseIcoOff4.enabled = false;
                if (broadRegenTime == 100)
                {
                    if (broadCount == 1)
                    {
                        broadCount -= 1;
                        broadRegenTime = 0;
                        powerBroadUp = false;
                    }
                    else if (broadCount == 2)
                    {
                        broadCount -= 1;
                        broadRegenTime = 100;
                        powerBroadUp = false;
                    }

                }
                else if (broadRegenTime < 100 && broadCount == 1)
                {
                    broadRegenTime = 100;
                    powerBroadUp = false;
                }
            }
            else if (upgradeBroad == 2)
            {
                broadpauseIco1.enabled = true;
                broadpauseIco2.enabled = true;
                broadpauseIco3.enabled = false;
                broadpauseIco4.enabled = false;
                broadpauseIcoOff1.enabled = false;
                broadpauseIcoOff2.enabled = false;
                broadpauseIcoOff3.enabled = true;
                broadpauseIcoOff4.enabled = false;
                if (broadRegenTime == 100)
                {
                    if (broadCount == 1)
                    {
                        broadCount -= 1;
                        broadRegenTime = 0;
                        powerBroadUp = false;
                    }
                    else if (broadCount == 2)
                    {
                        broadCount -= 1;
                        broadRegenTime = 100;
                        powerBroadUp = false;
                    }
                    else if (broadCount == 3)
                    {
                        broadCount -= 1;
                        broadRegenTime = 100;
                        powerBroadUp = false;
                    }

                }
                else if (broadRegenTime < 100 && broadCount == 2)
                {
                    broadRegenTime = 100;
                    powerBroadUp = false;
                }
            }
            else if (upgradeBroad == 3)
            {
                broadpauseIco1.enabled = true;
                broadpauseIco2.enabled = true;
                broadpauseIco3.enabled = true;
                broadpauseIco4.enabled = false;
                broadpauseIcoOff1.enabled = false;
                broadpauseIcoOff2.enabled = false;
                broadpauseIcoOff3.enabled = false;
                broadpauseIcoOff4.enabled = false;
                if (broadRegenTime == 100)
                {
                    if (broadCount == 1)
                    {
                        broadCount -= 1;
                        broadRegenTime = 0;
                        powerBroadUp = false;
                    }
                    else if (broadCount == 2)
                    {
                        broadCount -= 1;
                        broadRegenTime = 100;
                        powerBroadUp = false;
                    }
                    else if (broadCount == 3)
                    {
                        broadCount -= 1;
                        broadRegenTime = 100;
                        powerBroadUp = false;
                    }
                    else if (broadCount == 4)
                    {
                        broadCount -= 1;
                        broadRegenTime = 100;
                        powerBroadUp = false;
                    }

                }
                else if (broadRegenTime < 100 && broadCount == 3)
                {
                    broadRegenTime = 100;
                    powerBroadUp = false;
                }
            }


            if (upgradeMagic == 0)
            {
                magicpauseIco1.enabled = false;
                magicpauseIco2.enabled = false;
                magicpauseIco3.enabled = false;
                magicpauseIco4.enabled = false;
                magicpauseIcoOff1.enabled = true;
                magicpauseIcoOff2.enabled = true;
                magicpauseIcoOff3.enabled = true;
                magicpauseIcoOff4.enabled = false;
                if (magicRegenTime == 100)
                {
                    if (magicCount == 1)
                    {
                        magicCount -= 1;
                        magicRegenTime = 0;
                        powerMagicUp = false;
                    }

                }
                else if (magicRegenTime < 100)
                {
                    magicRegenTime = 0;
                    powerMagicUp = false;
                }
            }
            if (upgradeMagic == 1)
            {
                magicpauseIco1.enabled = true;
                magicpauseIco2.enabled = false;
                magicpauseIco3.enabled = false;
                magicpauseIco4.enabled = false;
                magicpauseIcoOff1.enabled = false;
                magicpauseIcoOff2.enabled = true;
                magicpauseIcoOff3.enabled = true;
                magicpauseIcoOff4.enabled = false;
                if (magicRegenTime == 100)
                {
                    if (magicCount == 1)
                    {
                        magicCount -= 1;
                        magicRegenTime = 0;
                        powerMagicUp = false;
                    }
                    else if (magicCount == 2)
                    {
                        magicCount -= 1;
                        magicRegenTime = 100;
                        powerMagicUp = false;
                    }

                }
                else if (magicRegenTime < 100 && magicCount == 1)
                {
                    magicRegenTime = 100;
                    powerMagicUp = false;
                }
            }
            else if (upgradeMagic == 2)
            {
                magicpauseIco1.enabled = true;
                magicpauseIco2.enabled = true;
                magicpauseIco3.enabled = false;
                magicpauseIco4.enabled = false;
                magicpauseIcoOff1.enabled = false;
                magicpauseIcoOff2.enabled = false;
                magicpauseIcoOff3.enabled = true;
                magicpauseIcoOff4.enabled = false;
                if (magicRegenTime == 100)
                {
                    if (magicCount == 1)
                    {
                        magicCount -= 1;
                        magicRegenTime = 0;
                        powerMagicUp = false;
                    }
                    else if (magicCount == 2)
                    {
                        magicCount -= 1;
                        magicRegenTime = 100;
                        powerMagicUp = false;
                    }
                    else if (magicCount == 3)
                    {
                        magicCount -= 1;
                        magicRegenTime = 100;
                        powerMagicUp = false;
                    }

                }
                else if (magicRegenTime < 100 && magicCount == 2)
                {
                    magicRegenTime = 100;
                    powerMagicUp = false;
                }
            }
            else if (upgradeMagic >= 3)
            {
                magicpauseIco1.enabled = true;
                magicpauseIco2.enabled = true;
                magicpauseIco3.enabled = true;
                magicpauseIco4.enabled = false;
                magicpauseIcoOff1.enabled = false;
                magicpauseIcoOff2.enabled = false;
                magicpauseIcoOff3.enabled = false;
                magicpauseIcoOff4.enabled = false;
                if (magicRegenTime == 100)
                {
                    if (magicCount == 1)
                    {
                        magicCount -= 1;
                        magicRegenTime = 0;
                        powerMagicUp = false;
                    }
                    else if (magicCount == 2)
                    {
                        magicCount -= 1;
                        magicRegenTime = 100;
                        powerMagicUp = false;
                    }
                    else if (magicCount == 3)
                    {
                        magicCount -= 1;
                        magicRegenTime = 100;
                        powerMagicUp = false;
                    }
                    else if (magicCount == 4)
                    {
                        magicCount -= 1;
                        magicRegenTime = 100;
                        powerMagicUp = false;
                    }

                }
                else if (magicRegenTime < 100 && magicCount == 3)
                {
                    magicRegenTime = 100;
                    powerMagicUp = false;
                }
            }

            if (upgradeThunder == 0)
            {
                thunderpauseIco1.enabled = false;
                thunderpauseIco2.enabled = false;
                thunderpauseIco3.enabled = false;
                thunderpauseIco4.enabled = false;
                thunderpauseIcoOff1.enabled = true;
                thunderpauseIcoOff2.enabled = true;
                thunderpauseIcoOff3.enabled = true;
                thunderpauseIcoOff4.enabled = false;
                if (thunderRegenTime == 100)
                {
                    if (thunderCount == 1)
                    {
                        thunderCount -= 1;
                        thunderRegenTime = 0;
                        powerThunderUp = false;
                    }

                }
                else if (thunderRegenTime < 100)
                {
                    thunderRegenTime = 0;
                    powerThunderUp = false;
                }
            }
            if (upgradeThunder == 1)
            {
                thunderpauseIco1.enabled = true;
                thunderpauseIco2.enabled = false;
                thunderpauseIco3.enabled = false;
                thunderpauseIco4.enabled = false;
                thunderpauseIcoOff1.enabled = false;
                thunderpauseIcoOff2.enabled = true;
                thunderpauseIcoOff3.enabled = true;
                thunderpauseIcoOff4.enabled = false;
                if (thunderRegenTime == 100)
                {
                    if (thunderCount == 1)
                    {
                        thunderCount -= 1;
                        thunderRegenTime = 0;
                        powerThunderUp = false;
                    }
                    else if (thunderCount == 2)
                    {
                        thunderCount -= 1;
                        thunderRegenTime = 100;
                        powerThunderUp = false;
                    }

                }
                else if (thunderRegenTime < 100 && thunderCount == 1)
                {
                    thunderRegenTime = 100;
                    powerThunderUp = false;
                }
            }
            else if (upgradeThunder == 2)
            {
                thunderpauseIco1.enabled = true;
                thunderpauseIco2.enabled = true;
                thunderpauseIco3.enabled = false;
                thunderpauseIco4.enabled = false;
                thunderpauseIcoOff1.enabled = false;
                thunderpauseIcoOff2.enabled = false;
                thunderpauseIcoOff3.enabled = true;
                thunderpauseIcoOff4.enabled = false;
                if (thunderRegenTime == 100)
                {
                    if (thunderCount == 1)
                    {
                        thunderCount -= 1;
                        thunderRegenTime = 0;
                        powerThunderUp = false;
                    }
                    else if (thunderCount == 2)
                    {
                        thunderCount -= 1;
                        thunderRegenTime = 100;
                        powerThunderUp = false;
                    }
                    else if (thunderCount == 3)
                    {
                        thunderCount -= 1;
                        thunderRegenTime = 100;
                        powerThunderUp = false;
                    }

                }
                else if (thunderRegenTime < 100 && thunderCount == 2)
                {
                    thunderRegenTime = 100;
                    powerThunderUp = false;
                }
            }
            else if (upgradeThunder >= 3)
            {
                thunderpauseIco1.enabled = true;
                thunderpauseIco2.enabled = true;
                thunderpauseIco3.enabled = true;
                thunderpauseIco4.enabled = false;
                thunderpauseIcoOff1.enabled = false;
                thunderpauseIcoOff2.enabled = false;
                thunderpauseIcoOff3.enabled = false;
                thunderpauseIcoOff4.enabled = false;
                if (thunderRegenTime == 100)
                {
                    if (thunderCount == 1)
                    {
                        thunderCount -= 1;
                        thunderRegenTime = 0;
                        powerThunderUp = false;
                    }
                    else if (thunderCount == 2)
                    {
                        thunderCount -= 1;
                        thunderRegenTime = 100;
                        powerThunderUp = false;
                    }
                    else if (thunderCount == 3)
                    {
                        thunderCount -= 1;
                        thunderRegenTime = 100;
                        powerThunderUp = false;
                    }
                    else if (thunderCount == 4)
                    {
                        thunderCount -= 1;
                        thunderRegenTime = 100;
                        powerThunderUp = false;
                    }

                }
                else if (thunderRegenTime < 100 && thunderCount == 3)
                {
                    thunderRegenTime = 100;
                    powerThunderUp = false;
                }
            }

            if (upgradeIce == 0)
            {
                icepauseIco1.enabled = false;
                icepauseIco2.enabled = false;
                icepauseIco3.enabled = false;
                icepauseIco4.enabled = false;
                icepauseIcoOff1.enabled = true;
                icepauseIcoOff2.enabled = true;
                icepauseIcoOff3.enabled = true;
                icepauseIcoOff4.enabled = false;
                if (iceRegenTime == 100)
                {
                    if (iceCount == 1)
                    {
                        iceCount -= 1;
                        iceRegenTime = 0;
                        powerIceUp = false;
                    }

                }
                else if (iceRegenTime < 100)
                {
                    iceRegenTime = 0;
                    powerIceUp = false;
                }
            }
            if (upgradeIce == 1)
            {
                icepauseIco1.enabled = true;
                icepauseIco2.enabled = false;
                icepauseIco3.enabled = false;
                icepauseIco4.enabled = false;
                icepauseIcoOff1.enabled = false;
                icepauseIcoOff2.enabled = true;
                icepauseIcoOff3.enabled = true;
                icepauseIcoOff4.enabled = false;
                if (iceRegenTime == 100)
                {
                    if (iceCount == 1)
                    {
                        iceCount -= 1;
                        iceRegenTime = 0;
                        powerIceUp = false;
                    }
                    else if (iceCount == 2)
                    {
                        iceCount -= 1;
                        iceRegenTime = 100;
                        powerIceUp = false;
                    }

                }
                else if (iceRegenTime < 100 && iceCount == 1)
                {
                    iceRegenTime = 100;
                    powerIceUp = false;
                }
            }
            else if (upgradeIce == 2)
            {
                icepauseIco1.enabled = true;
                icepauseIco2.enabled = true;
                icepauseIco3.enabled = false;
                icepauseIco4.enabled = false;
                icepauseIcoOff1.enabled = false;
                icepauseIcoOff2.enabled = false;
                icepauseIcoOff3.enabled = true;
                icepauseIcoOff4.enabled = false;
                if (iceRegenTime == 100)
                {
                    if (iceCount == 1)
                    {
                        iceCount -= 1;
                        iceRegenTime = 0;
                        powerIceUp = false;
                    }
                    else if (iceCount == 2)
                    {
                        iceCount -= 1;
                        iceRegenTime = 100;
                        powerIceUp = false;
                    }
                    else if (iceCount == 3)
                    {
                        iceCount -= 1;
                        iceRegenTime = 100;
                        powerIceUp = false;
                    }

                }
                else if (iceRegenTime < 100 && iceCount == 2)
                {
                    iceRegenTime = 100;
                    powerIceUp = false;
                }
            }
            else if (upgradeIce >= 3)
            {
                icepauseIco1.enabled = true;
                icepauseIco2.enabled = true;
                icepauseIco3.enabled = true;
                icepauseIco4.enabled = false;
                icepauseIcoOff1.enabled = false;
                icepauseIcoOff2.enabled = false;
                icepauseIcoOff3.enabled = false;
                icepauseIcoOff4.enabled = false;
                if (iceRegenTime == 100)
                {
                    if (iceCount == 1)
                    {
                        iceCount -= 1;
                        iceRegenTime = 0;
                        powerIceUp = false;
                    }
                    else if (iceCount == 2)
                    {
                        iceCount -= 1;
                        iceRegenTime = 100;
                        powerIceUp = false;
                    }
                    else if (iceCount == 3)
                    {
                        iceCount -= 1;
                        iceRegenTime = 100;
                        powerIceUp = false;
                    }
                    else if (iceCount == 4)
                    {
                        iceCount -= 1;
                        iceRegenTime = 100;
                        powerIceUp = false;
                    }

                }
                else if (iceRegenTime < 100 && iceCount == 3)
                {
                    iceRegenTime = 100;
                    powerIceUp = false;
                }
            }
            addFrostUpgrade = false;
        }
    }
}
