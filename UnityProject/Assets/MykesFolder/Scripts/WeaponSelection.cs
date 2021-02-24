using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelection : MonoBehaviour {

    public bool StartDemo;
    AudioSource audioSrc;
    public AudioClip selection;
    public AudioClip pickupSword;
    public static bool MagicSwordPickedUp;
    public static bool ThunderSwordPickedUp;
    public static bool IceSwordPickedUp;
    public int SwordCount;
    SwordOfIce IceAttacked;
    MagicSword FireAttacked;
    ThunderSword ThundAttacked;
    AnimatedSword SwordAttacked;
    AnimatedSword EnableSword;

    PowerCounter powcount;

    public GameObject gameCtrl;
    PauseMenu ItemPause;
    public GameObject ItemMenuFlameSword;
    public GameObject ItemMenuElectroSword;
    public GameObject ItemMenuFrostSword;

    public GameObject crossBow;
    public GameObject broadSword;
    public GameObject magicSword;
    public GameObject thunderSword;
    public GameObject swordOfIce;
    public GameObject Armed;
    public GameObject Unarmed;

    public GameObject SwordModel;
    public GameObject UnarmedModel;

    public Text weaponSelected;
    public Text arrows;
    public Image arrowUI;

    public Image crossbowImg;
    public Image broadSwordImg;
    public Image magicSwordImg;
    public Image thunderSwordImg;
    public Image swordOfIceImg;

    public Image MagicSwordIcon;
    public Image ThunderSwordIcon;
    public Image IceSwordIcon;
    bool swordSelect;
    public static bool UnarmedEnabled;

    bool BroadSword;
    bool MagicSword;
    bool ThunderSword;
    bool IceSword;
    bool isLoading;
    bool isPaused;
    public bool isDrowning;
    public bool isSubmerged;
    bool isDisabled;
    bool started;
    bool isJumping;
    bool isAttacking;
    // Use this for initialization
    void Start () {
        if (StartDemo)
        {
            MagicSwordPickedUp = true;
            ThunderSwordPickedUp = true;
            IceSwordPickedUp = true;
            MagicSwordIcon.enabled = true;
            ThunderSwordIcon.enabled = true;
            IceSwordIcon.enabled = true;
        }
        else
        {
            MagicSwordPickedUp = false;
            ThunderSwordPickedUp = false;
            IceSwordPickedUp = false;
            MagicSwordIcon.enabled = false;
            ThunderSwordIcon.enabled = false;
            IceSwordIcon.enabled = false;
        }
       
        EnableWeapon(1);

        audioSrc = GetComponent<AudioSource>();
      
    }
	
	// Update is called once per frame
	void Update () {
        isPaused = PauseMenu.Paused;
        isDisabled = SceneLoader.isDisabled;
        isLoading = SceneLoader.isLoading;
        isDrowning = WaterDamage.isDrowning;
        isAttacking = AnimatedSword.isAttacking;
        isJumping = PlayerController.isJumping;

        if (!started)
        {
            EnableWeapon(1);
            started = true;

        }
        if (isDrowning && !isJumping && !isAttacking)
        {
            EnableWeapon(5);
            isSubmerged = true;
        }
        if (!isDrowning && isSubmerged)
        {
            if (BroadSword)
            {
                EnableWeapon(1);
            }
            else if (MagicSword)
            {
                EnableWeapon(2);
            }
            else if (ThunderSword)
            {
                EnableWeapon(3);
            }
            else if (IceSword)
            {
                EnableWeapon(4);
            }
            isSubmerged = false;
        }

        if (MagicSwordPickedUp)
        {
            MagicSwordIcon.enabled = true;
        }
        if (ThunderSwordPickedUp)
        {
            ThunderSwordIcon.enabled = true;
        }
        if (IceSwordPickedUp)
        {
            IceSwordIcon.enabled = true;
        }
       
        if (Input.GetKeyDown(KeyCode.Alpha7) && !swordSelect && !isPaused && !isDisabled && !isLoading && !isJumping && !isAttacking && !isSubmerged || Input.GetAxisRaw("SelectVertical") == 1 && !swordSelect && !isPaused && !isDisabled && !isLoading && !isDrowning && !isJumping && !isAttacking && !isSubmerged)
        {
      
            if (!isLoading)
            {
                BroadSword = AnimatedSword.BroadSwordEnabled;
                MagicSword = AnimatedSword.MagicSwordEnabled;
                ThunderSword = AnimatedSword.ThunderSwordEnabled;
                IceSword = AnimatedSword.IceSwordEnabled;

                if (BroadSword)
                {
                    audioSrc.volume = 1.0f;
                    audioSrc.pitch = 1.0f;
                    audioSrc.PlayOneShot(pickupSword);
                    RotateSwords(1);
                }
                else if (MagicSword)
                {
                    audioSrc.volume = 1.0f;
                    audioSrc.pitch = 0.8f;
                    audioSrc.PlayOneShot(pickupSword);
                    RotateSwords(2);
                }
                else if (ThunderSword)
                {
                    audioSrc.volume = 1.0f;
                    audioSrc.pitch = 1.2f;
                    audioSrc.PlayOneShot(pickupSword);
                    RotateSwords(3);
                }
                else if (IceSword)
                {
                    audioSrc.volume = 1.0f;
                    audioSrc.pitch = 1.5f;
                    audioSrc.PlayOneShot(pickupSword);
                    RotateSwords(4);
                }
                swordSelect = true;
            }
        }
        else if (Input.GetAxisRaw("SelectVertical") == 0)
        {
            audioSrc.volume = 1.0f;
            audioSrc.pitch = 1.0f;
            swordSelect = false;
        }
        
    }
    public void EnableWeapon(int sword)
    {
        //audioSrc.PlayOneShot(selection);
        EnableSword = SwordModel.GetComponent<AnimatedSword>();
        //if (sword == 0)
        //{
        //    Armed.SetActive(true);
        //    arrows.enabled = true;
        //    arrowUI.enabled = true;
        //    weaponSelected.text = "Crossbow";
        //    broadSword.SetActive(false);
        //    magicSword.SetActive(false);
        //    thunderSword.SetActive(false);
        //    crossBow.SetActive(true);
        //    swordOfIce.SetActive(false);
        //    Unarmed.SetActive(false);
        //    crossbowImg.enabled = true;
        //    broadSwordImg.enabled = false;
        //    magicSwordImg.enabled = false;
        //    thunderSwordImg.enabled = false;
        //    swordOfIceImg.enabled = false;
        //    UnarmedEnabled = false;

        //}
        if (sword == 1)
        {
            powcount = GetComponent<PowerCounter>();
            Armed.SetActive(true);
            arrows.enabled = false;
            arrowUI.enabled = false;
            weaponSelected.text = "Broad Sword";
            broadSword.SetActive(true);
            magicSword.SetActive(false);
            thunderSword.SetActive(false);
            crossBow.SetActive(false);
            swordOfIce.SetActive(false);
            Unarmed.SetActive(false);
            crossbowImg.enabled = false;
            broadSwordImg.enabled = true;
            magicSwordImg.enabled = false;
            thunderSwordImg.enabled = false;
            swordOfIceImg.enabled = false;
            UnarmedEnabled = false;
            powcount.EnableWeapon(1);
            EnableSword.SwordEquip(AnimatedSword.EquipSword.BroadSword);
        }
        if (sword == 2)
        {
            powcount = GetComponent<PowerCounter>();
            Armed.SetActive(true);
            arrows.enabled = false;
            arrowUI.enabled = false;
            weaponSelected.text = "Flame Sword";
            broadSword.SetActive(false);
            magicSword.SetActive(true);
            thunderSword.SetActive(false);
            crossBow.SetActive(false);
            swordOfIce.SetActive(false);
            Unarmed.SetActive(false);
            crossbowImg.enabled = false;
            broadSwordImg.enabled = false;
            magicSwordImg.enabled = true;
            thunderSwordImg.enabled = false;
            swordOfIceImg.enabled = false;
            UnarmedEnabled = false;
            powcount.EnableWeapon(2);
            EnableSword.SwordEquip(AnimatedSword.EquipSword.FlameSword);
        }
        if (sword == 3)
        {
            powcount = GetComponent<PowerCounter>();
            Armed.SetActive(true);
            arrows.enabled = false;
            arrowUI.enabled = false;
            weaponSelected.text = "Electro Sword";
            broadSword.SetActive(false);
            magicSword.SetActive(false);
            thunderSword.SetActive(true);
            crossBow.SetActive(false);
            swordOfIce.SetActive(false);
            Unarmed.SetActive(false);
            crossbowImg.enabled = false;
            broadSwordImg.enabled = false;
            magicSwordImg.enabled = false;
            thunderSwordImg.enabled = true;
            swordOfIceImg.enabled = false;
            UnarmedEnabled = false;
            powcount.EnableWeapon(3);
            EnableSword.SwordEquip(AnimatedSword.EquipSword.ThunderSword);
        }
        if (sword == 4)
        {
            powcount = GetComponent<PowerCounter>();
            Armed.SetActive(true);
            arrows.enabled = false;
            arrowUI.enabled = false;
            weaponSelected.text = "Glacial Sword";
            broadSword.SetActive(false);
            magicSword.SetActive(false);
            thunderSword.SetActive(false);
            crossBow.SetActive(false);
            swordOfIce.SetActive(true);
            Unarmed.SetActive(false);
            crossbowImg.enabled = false;
            broadSwordImg.enabled = false;
            magicSwordImg.enabled = false;
            thunderSwordImg.enabled = false;
            swordOfIceImg.enabled = true;
            UnarmedEnabled = false;

            EnableSword.SwordEquip(AnimatedSword.EquipSword.GlacialSword);
            powcount.EnableWeapon(4);
        }
        if (sword == 5)
        {
            arrows.enabled = false;
            arrowUI.enabled = false;
            weaponSelected.text = "Unarmed";
            Armed.SetActive(false);
            Unarmed.SetActive(true);
            crossbowImg.enabled = false;
            broadSwordImg.enabled = false;
            magicSwordImg.enabled = false;
            thunderSwordImg.enabled = false;
            swordOfIceImg.enabled = false;
            UnarmedEnabled = true;

        }
        UnarmedEnabled = false;
        SwordAttacked = SwordModel.GetComponent<AnimatedSword>();
        SwordAttacked.ResetAttacked();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MagicSword"))
        {
            audioSrc.volume = 1.0f;
            audioSrc.pitch = 0.8f;
            audioSrc.PlayOneShot(pickupSword);
            MagicSwordPickedUp = true;
            EnableWeapon(2);
            powcount.EnableWeapon(2);
            Destroy(other.gameObject);
            ItemPause = gameCtrl.GetComponent<PauseMenu>();
            ItemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            ItemMenuFlameSword.SetActive(true);
        }
        if (other.gameObject.CompareTag("ThunderSword"))
        {
            audioSrc.volume = 1.0f;
            audioSrc.pitch = 1.2f;
            audioSrc.PlayOneShot(pickupSword);
            ThunderSwordPickedUp = true;
            EnableWeapon(3);
            powcount.EnableWeapon(3);
            Destroy(other.gameObject);
            ItemPause = gameCtrl.GetComponent<PauseMenu>();
            ItemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            ItemMenuElectroSword.SetActive(true);
        }
        if (other.gameObject.CompareTag("IceSword"))
        {
            audioSrc.volume = 1.0f;
            audioSrc.pitch = 1.5f;
            audioSrc.PlayOneShot(pickupSword);
            IceSwordPickedUp = true;
            EnableWeapon(4);
            powcount.EnableWeapon(4);
            Destroy(other.gameObject);
            ItemPause = gameCtrl.GetComponent<PauseMenu>();
            ItemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            ItemMenuFrostSword.SetActive(true);
        }
      
    }
    public void RotateSwords(int num)
    {
        if (num == 1)
        {
            //Just Spikeboots
            if (MagicSwordPickedUp && !ThunderSwordPickedUp && !IceSwordPickedUp)
            {
                EnableWeapon(2);
            }
            // Just SpeedBoots
            else if (!MagicSwordPickedUp && ThunderSwordPickedUp && !IceSwordPickedUp)
            {
                EnableWeapon(3);
            }
            //Just HealingBoots
            else if (!MagicSwordPickedUp && !ThunderSwordPickedUp && IceSwordPickedUp)
            {
                EnableWeapon(4);
            }
            //All Three
            else if (MagicSwordPickedUp && ThunderSwordPickedUp && IceSwordPickedUp)
            {
                EnableWeapon(2);
            }
            //Just Spike & Speed
            else if (MagicSwordPickedUp && ThunderSwordPickedUp && !IceSwordPickedUp)
            {
                EnableWeapon(2);
            }
            //Just Speed & Healing
            else if (!MagicSwordPickedUp && ThunderSwordPickedUp && IceSwordPickedUp)
            {
                EnableWeapon(3);
            }
            //Just Spike & Healing
            else if (MagicSwordPickedUp && !ThunderSwordPickedUp && IceSwordPickedUp)
            {
                EnableWeapon(2);
            }
            //Just Speed & Spike
            else if (MagicSwordPickedUp && ThunderSwordPickedUp && !IceSwordPickedUp)
            {
                EnableWeapon(2);
            }
            //Nothing
            else
            {
                EnableWeapon(1);
            }
        }
        else if (num == 2)
        {
            //Just Spikeboots
            if (!ThunderSwordPickedUp && !IceSwordPickedUp)
            {
                EnableWeapon(1);
            }
            // Just SpeedBoots
            else if (ThunderSwordPickedUp && !IceSwordPickedUp)
            {
                EnableWeapon(3);
            }
            //Just HealingBoots
            else if (!ThunderSwordPickedUp && IceSwordPickedUp)
            {
                EnableWeapon(4);
            }
            //All Three
            else if (ThunderSwordPickedUp && IceSwordPickedUp)
            {
                EnableWeapon(3);
            }
            //Nothing
            else
            {
                EnableWeapon(1);
            }
        }
        else if (num == 3)
        {
            //Just Spikeboots
            if (MagicSwordPickedUp && !IceSwordPickedUp)
            {
                EnableWeapon(1);
            }
            // Just SpeedBoots
            else if (!MagicSwordPickedUp && !IceSwordPickedUp)
            {
                EnableWeapon(1);
            }
            //Just HealingBoots
            else if (!MagicSwordPickedUp && IceSwordPickedUp)
            {
                EnableWeapon(4);
            }
            //All Three
            else if (MagicSwordPickedUp && IceSwordPickedUp)
            {
                EnableWeapon(4);
            }
            //Nothing
            else
            {
                EnableWeapon(1);
            }
        }
        else if (num == 4)
        {
            //Just Spikeboots
            if (MagicSwordPickedUp && !ThunderSwordPickedUp)
            {
                EnableWeapon(1);
            }
            // Just SpeedBoots
            else if (!MagicSwordPickedUp && ThunderSwordPickedUp)
            {
                EnableWeapon(1);
            }
            //Just HealingBoots
            else if (!MagicSwordPickedUp && !ThunderSwordPickedUp)
            {
                EnableWeapon(1);
            }
            //All Three
            else if (MagicSwordPickedUp && ThunderSwordPickedUp && IceSwordPickedUp)
            {
                EnableWeapon(1);
            }
            //Nothing
            else
            {
                EnableWeapon(1);
            }
        }
    }
}
