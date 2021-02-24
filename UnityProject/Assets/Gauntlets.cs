using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauntlets : MonoBehaviour {

    public bool StartDemo;
    AudioSource audioSrc;
    public AudioClip gloves;

    public GameObject gameCtrl;
    PauseMenu ItemPause;
    public GameObject ItemMenuStrengthGauntlets;
    public GameObject ItemMenuPowerGauntlets;
    public GameObject ItemMenuGripGauntlets;

    public static bool LeatherGauntlets = true;
    public static bool StrengthGauntlets;
    public static bool PowerGauntlets;
    public static bool GripGauntlets;

    public static bool StrengthGauntletsPickedUp;
    public static bool PowerGauntletsPickedUp;
    public static bool GripGauntletsPickedUp;

    public Image PauseLeatherGauntletsUI;
    public Image PauseStrengthGauntletsUI;
    public Image PausePowerGauntletsUI;
    public Image PauseGripGauntletsUI;

    public Image GameLeatherGauntletsUI;
    public Image GameStrengthGauntletsUI;
    public Image GamePowerGauntletsUI;
    public Image GameGripGauntletsUI;

    [Header("RightHand")]
    public GameObject URightLeatherGloves;
    public GameObject URightStrengthGloves;
    public GameObject URightPowerGloves;
    public GameObject URightGripGloves;

    public GameObject RightLeatherGloves;
    public GameObject RightStrengthGloves;
    public GameObject RightPowerGloves;
    public GameObject RightGripGloves;

    [Space]
    [Header("LeftHand")]
    public GameObject ULeftLeatherGloves;
    public GameObject ULeftStrengthGloves;
    public GameObject ULeftPowerGloves;
    public GameObject ULeftGripGloves;

    public GameObject LeftLeatherGloves;
    public GameObject LeftStrengthGloves;
    public GameObject LeftPowerGloves;
    public GameObject LeftGripGloves;
    bool gauntSelect;
    bool UnarmedEnabled;
    bool isLoading;
    bool isPaused;
    bool isDisabled;
    // Use this for initialization
    void Start () {

        if (StartDemo)
        {
            PauseLeatherGauntletsUI.enabled = true;
            PauseStrengthGauntletsUI.enabled = true;
            PausePowerGauntletsUI.enabled = true;
            PauseGripGauntletsUI.enabled = true;
            GripGauntletsPickedUp = true;
            PowerGauntletsPickedUp = true;
            StrengthGauntletsPickedUp = true;
        }
        else
        {
            PauseLeatherGauntletsUI.enabled = true;
            PauseStrengthGauntletsUI.enabled = false;
            PausePowerGauntletsUI.enabled = false;
            PauseGripGauntletsUI.enabled = false;
            GripGauntletsPickedUp = false;
            PowerGauntletsPickedUp = false;
            StrengthGauntletsPickedUp = false;
        }
   
        EnableGauntlets(1);
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        UnarmedEnabled = WeaponSelection.UnarmedEnabled;
        isPaused = PauseMenu.Paused;
        isDisabled = SceneLoader.isDisabled;
      
        if (Input.GetKeyDown(KeyCode.Alpha8) && !gauntSelect && !isPaused && !isDisabled && !isLoading || Input.GetAxisRaw("SelectHorizontal") == -1 && !gauntSelect && !isPaused && !isDisabled && !isLoading)
        {
            if (!isLoading)
            {
                audioSrc.PlayOneShot(gloves);
                if (LeatherGauntlets)
                {
                    RotateGauntlets(1);
                }
                else if (StrengthGauntlets)
                {
                    RotateGauntlets(2);
                }
                else if (PowerGauntlets)
                {
                    RotateGauntlets(3);
                }
                else if (GripGauntlets)
                {
                    RotateGauntlets(4);
                }
                gauntSelect = true;
            }
        }
        else if (Input.GetAxisRaw("SelectHorizontal") == 0)
        {
            gauntSelect = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("StrengthGauntlets"))
        {
            
            audioSrc.PlayOneShot(gloves);
            StrengthGauntletsPickedUp = true;
            PauseStrengthGauntletsUI.enabled = true;
            EnableGauntlets(2);
            Destroy(other.gameObject);
            ItemPause = gameCtrl.GetComponent<PauseMenu>();
            ItemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            ItemMenuStrengthGauntlets.SetActive(true);
        }
        if (other.gameObject.CompareTag("PowerGauntlets"))
        {
            
            audioSrc.PlayOneShot(gloves);
            PowerGauntletsPickedUp = true;
            PausePowerGauntletsUI.enabled = true;
            EnableGauntlets(3);
            Destroy(other.gameObject);
            ItemPause = gameCtrl.GetComponent<PauseMenu>();
            ItemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            ItemMenuPowerGauntlets.SetActive(true);
        }
        if (other.gameObject.CompareTag("GripGauntlets"))
        {
            audioSrc.PlayOneShot(gloves);
            GripGauntletsPickedUp = true;
            PauseGripGauntletsUI.enabled = true;
            EnableGauntlets(4);
            Destroy(other.gameObject);
            ItemPause = gameCtrl.GetComponent<PauseMenu>();
            ItemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            ItemMenuGripGauntlets.SetActive(true);
        }
    }
    public void EnableGauntlets(int num)
    {
        if (num == 1)
        {
            GripGauntlets = false;
            PowerGauntlets = false;
            StrengthGauntlets = false;
            LeatherGauntlets = true;

            GameLeatherGauntletsUI.enabled = true;
            GameStrengthGauntletsUI.enabled = false;
            GamePowerGauntletsUI.enabled = false;
            GameGripGauntletsUI.enabled = false;

  

            RightLeatherGloves.SetActive(true);
            LeftLeatherGloves.SetActive(true);
            RightStrengthGloves.SetActive(false);
            LeftStrengthGloves.SetActive(false);
            RightPowerGloves.SetActive(false);
            LeftPowerGloves.SetActive(false);
            RightGripGloves.SetActive(false);
            LeftGripGloves.SetActive(false);

            URightLeatherGloves.SetActive(true);
            ULeftLeatherGloves.SetActive(true);
            URightStrengthGloves.SetActive(false);
            ULeftStrengthGloves.SetActive(false);
            URightPowerGloves.SetActive(false);
            ULeftPowerGloves.SetActive(false);
            URightGripGloves.SetActive(false);
            ULeftGripGloves.SetActive(false);
        }
        else if (num == 2)
        {
            if (StrengthGauntletsPickedUp)
            {
                LeatherGauntlets = false;
                StrengthGauntlets = true;
                PowerGauntlets = false;
                GripGauntlets = false;

                GameLeatherGauntletsUI.enabled = false;
                GameStrengthGauntletsUI.enabled = true;
                GamePowerGauntletsUI.enabled = false;
                GameGripGauntletsUI.enabled = false;

                RightLeatherGloves.SetActive(false);
                LeftLeatherGloves.SetActive(false);
                RightStrengthGloves.SetActive(true);
                LeftStrengthGloves.SetActive(true);
                RightPowerGloves.SetActive(false);
                LeftPowerGloves.SetActive(false);
                RightGripGloves.SetActive(false);
                LeftGripGloves.SetActive(false);

                URightLeatherGloves.SetActive(false);
                ULeftLeatherGloves.SetActive(false);
                URightStrengthGloves.SetActive(true);
                ULeftStrengthGloves.SetActive(true);
                URightPowerGloves.SetActive(false);
                ULeftPowerGloves.SetActive(false);
                URightGripGloves.SetActive(false);
                ULeftGripGloves.SetActive(false);
            }
        }
        else if (num == 3)
        {
            if (PowerGauntletsPickedUp)
            {
                LeatherGauntlets = false;
                StrengthGauntlets = false;
                PowerGauntlets = true;
                GripGauntlets = false;

                GameLeatherGauntletsUI.enabled = false;
                GameStrengthGauntletsUI.enabled = false;
                GamePowerGauntletsUI.enabled = true;
                GameGripGauntletsUI.enabled = false;

                RightLeatherGloves.SetActive(false);
                LeftLeatherGloves.SetActive(false);
                RightStrengthGloves.SetActive(false);
                LeftStrengthGloves.SetActive(false);
                RightPowerGloves.SetActive(true);
                LeftPowerGloves.SetActive(true);
                RightGripGloves.SetActive(false);
                LeftGripGloves.SetActive(false);

                URightLeatherGloves.SetActive(false);
                ULeftLeatherGloves.SetActive(false);
                URightStrengthGloves.SetActive(false);
                ULeftStrengthGloves.SetActive(false);
                URightPowerGloves.SetActive(true);
                ULeftPowerGloves.SetActive(true);
                URightGripGloves.SetActive(false);
                ULeftGripGloves.SetActive(false);
            }

        }
        else if (num == 4)
        {
            if (GripGauntletsPickedUp)
            {
                LeatherGauntlets = false;
                StrengthGauntlets = false;
                PowerGauntlets = false;
                GripGauntlets = true;

                GameLeatherGauntletsUI.enabled = false;
                GameStrengthGauntletsUI.enabled = false;
                GamePowerGauntletsUI.enabled = false;
                GameGripGauntletsUI.enabled = true;

                RightLeatherGloves.SetActive(false);
                LeftLeatherGloves.SetActive(false);
                RightStrengthGloves.SetActive(false);
                LeftStrengthGloves.SetActive(false);
                RightPowerGloves.SetActive(false);
                LeftPowerGloves.SetActive(false);
                RightGripGloves.SetActive(true);
                LeftGripGloves.SetActive(true);

                URightLeatherGloves.SetActive(false);
                ULeftLeatherGloves.SetActive(false);
                URightStrengthGloves.SetActive(false);
                ULeftStrengthGloves.SetActive(false);
                URightPowerGloves.SetActive(false);
                ULeftPowerGloves.SetActive(false);
                URightGripGloves.SetActive(true);
                ULeftGripGloves.SetActive(true);
            }
        }
    }
    public void RotateGauntlets(int num)
    {
        if (num == 1)
        {
            //Just Spikeboots
            if (StrengthGauntletsPickedUp && !PowerGauntletsPickedUp && !GripGauntletsPickedUp)
            {
                EnableGauntlets(2);
            }
            // Just SpeedBoots
            else if (!StrengthGauntletsPickedUp && PowerGauntletsPickedUp && !GripGauntletsPickedUp)
            {
                EnableGauntlets(3);
            }
            //Just HealingBoots
            else if (!StrengthGauntletsPickedUp && !PowerGauntletsPickedUp && GripGauntletsPickedUp)
            {
                EnableGauntlets(4);
            }
            //All Three
            else if (StrengthGauntletsPickedUp && PowerGauntletsPickedUp && GripGauntletsPickedUp)
            {
                EnableGauntlets(2);
            }
            //Just Spike & Speed
            else if (StrengthGauntletsPickedUp && PowerGauntletsPickedUp && !GripGauntletsPickedUp)
            {
                EnableGauntlets(2);
            }
            //Just Speed & Healing
            else if (!StrengthGauntletsPickedUp && PowerGauntletsPickedUp && GripGauntletsPickedUp)
            {
                EnableGauntlets(3);
            }
            //Just Spike & Healing
            else if (StrengthGauntletsPickedUp && !PowerGauntletsPickedUp && GripGauntletsPickedUp)
            {
                EnableGauntlets(2);
            }
            //Just Speed & Spike
            else if (StrengthGauntletsPickedUp && PowerGauntletsPickedUp && !GripGauntletsPickedUp)
            {
                EnableGauntlets(2);
            }
            //Nothing
            else
            {
                EnableGauntlets(1);
            }
        }
        else if (num == 2)
        {
            //Just Spikeboots
            if (!PowerGauntletsPickedUp && !GripGauntletsPickedUp)
            {
                EnableGauntlets(1);
            }
            // Just SpeedBoots
            else if (PowerGauntletsPickedUp && !GripGauntletsPickedUp)
            {
                EnableGauntlets(3);
            }
            //Just HealingBoots
            else if (!PowerGauntletsPickedUp && GripGauntletsPickedUp)
            {
                EnableGauntlets(4);
            }
            //All Three
            else if (PowerGauntletsPickedUp && GripGauntletsPickedUp)
            {
                EnableGauntlets(3);
            }
            //Nothing
            else
            {
                EnableGauntlets(1);
            }
        }
        else if (num == 3)
        {
            //Just Spikeboots
            if (StrengthGauntletsPickedUp && !GripGauntletsPickedUp)
            {
                EnableGauntlets(1);
            }
            // Just SpeedBoots
            else if (!StrengthGauntletsPickedUp && !GripGauntletsPickedUp)
            {
                EnableGauntlets(1);
            }
            //Just HealingBoots
            else if (!StrengthGauntletsPickedUp && GripGauntletsPickedUp)
            {
                EnableGauntlets(4);
            }
            //All Three
            else if (StrengthGauntletsPickedUp && GripGauntletsPickedUp)
            {
                EnableGauntlets(4);
            }
            //Nothing
            else
            {
                EnableGauntlets(1);
            }
        }
        else if (num == 4)
        {
            //Just Spikeboots
            if (StrengthGauntletsPickedUp && !PowerGauntletsPickedUp)
            {
                EnableGauntlets(1);
            }
            // Just SpeedBoots
            else if (!StrengthGauntletsPickedUp && PowerGauntletsPickedUp)
            {
                EnableGauntlets(1);
            }
            //Just HealingBoots
            else if (!StrengthGauntletsPickedUp && !PowerGauntletsPickedUp)
            {
                EnableGauntlets(1);
            }
            //All Three
            else if (StrengthGauntletsPickedUp && PowerGauntletsPickedUp && GripGauntletsPickedUp)
            {
                EnableGauntlets(1);
            }
            //Nothing
            else
            {
                EnableGauntlets(1);
            }
        }
    }
}
