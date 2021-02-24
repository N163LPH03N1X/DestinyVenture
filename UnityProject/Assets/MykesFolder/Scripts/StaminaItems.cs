using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaItems : MonoBehaviour {
    public bool StartDemo;
    AudioSource audioSrc;
    public AudioClip heal;
    public AudioClip item;

    public Image VitalixerSMUI;
    public Image VitalixerMDUI;
    public Image VitalixerLGUI;

    public Text VitalixerSMQuanity;
    public Text VitalixerMDQuanity;
    public Text VitalixerLGQuanity;

    public Text VitalixerSMName;
    public Text VitalixerMDName;
    public Text VitalixerLGName;

    public static int VitalixerSMAmount;
    public static int VitalixerMDAmount;
    public static int VitalixerLGAmount;

    public static bool VitalixerSMPickedUp = false;
    public static bool VitalixerMDPickedUp = false;
    public static bool VitalixerLGPickedUp = false;

    PlayerController PlayST;
    bool StaminaFull;


    void Start () {

        
        PlayST = GetComponent<PlayerController>();
        audioSrc = GetComponent<AudioSource>();
        if (StartDemo)
        {
            GainVitalixer(1, 9);
            GainVitalixer(2, 9);
            GainVitalixer(3, 9);
        }
        else
        {
            VitalixerSMUI.enabled = false;
            VitalixerSMName.enabled = false;
            VitalixerSMQuanity.enabled = false;
            VitalixerMDUI.enabled = false;
            VitalixerMDName.enabled = false;
            VitalixerMDQuanity.enabled = false;
            VitalixerLGUI.enabled = false;
            VitalixerLGName.enabled = false;
            VitalixerLGQuanity.enabled = false;

            VitalixerSMAmount = 0;
            VitalixerMDAmount = 0;
            VitalixerLGAmount = 0;

        }

        SetCountQuanity();

    }
	
	// Update is called once per frame
	void Update () {
        StaminaFull = PlayerController.StaminaFull;
	}

    public void RemoveVitalixer(int num)
    {
        if (num == 1)
        {
            if (!StaminaFull)
            {
              
                if (VitalixerSMAmount <= 0)
                {
                    VitalixerSMAmount = 0;
                }
                else
                {
                    audioSrc.PlayOneShot(heal);
                    VitalixerSMAmount -= 1;
                    PlayST.GivePlayerStamina(25);
                }
            }
           

        }
        if (num == 2)
        {
            if (!StaminaFull)
            {
               
                if (VitalixerMDAmount <= 0)
                {
                    VitalixerMDAmount = 0;
                }
                else
                {
                    audioSrc.PlayOneShot(heal);
                    VitalixerMDAmount -= 1;
                    PlayST.GivePlayerStamina(50);
                }
            }
        }
        if (num == 3)
        {
            if (!StaminaFull)
            {
              
                if (VitalixerLGAmount <= 0)
                {
                    VitalixerLGAmount = 0;
                }
                else
                {
                    audioSrc.PlayOneShot(heal);
                    VitalixerLGAmount -= 1;
                    PlayST.GivePlayerStamina(100);
                }
            }
           

        }
        SetCountQuanity();
    }
    public void GainVitalixer(int num, int Amount)
    {
        if (num == 1)
        {
            if (VitalixerSMAmount >= 9)
            {
                VitalixerSMAmount = 9;
            }
            else
            {
                VitalixerSMPickedUp = true;
                VitalixerSMUI.enabled = true;
                VitalixerSMName.enabled = true;
                VitalixerSMQuanity.enabled = true;
                VitalixerSMAmount += Amount;
            }
        }
        if (num == 2)
        {

            if (VitalixerMDAmount >= 9)
            {
                VitalixerMDAmount = 9;
            }
            else
            {
                VitalixerMDPickedUp = true;
                VitalixerMDUI.enabled = true;
                VitalixerMDName.enabled = true;
                VitalixerMDQuanity.enabled = true;
                VitalixerMDAmount += Amount;
            }
        }
        if (num == 3)
        {

            if (VitalixerLGAmount >= 9)
            {
                VitalixerLGAmount = 9;
            }
            else
            {
                VitalixerLGPickedUp = true;
                VitalixerLGUI.enabled = true;
                VitalixerLGName.enabled = true;
                VitalixerLGQuanity.enabled = true;
                VitalixerLGAmount += Amount;
            }
        }
        SetCountQuanity();
    }
    public void SetCountQuanity()
    {
        VitalixerSMQuanity.text = VitalixerSMAmount.ToString();
        VitalixerMDQuanity.text = VitalixerMDAmount.ToString();
        VitalixerLGQuanity.text = VitalixerLGAmount.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StaminaSM"))
        {
            if (VitalixerSMAmount != 9)
            {
                audioSrc.PlayOneShot(item);
                GainVitalixer(1, 1);
            }
           
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("StaminaMD"))
        {
            if (VitalixerMDAmount != 9)
            {
                audioSrc.PlayOneShot(item);
                GainVitalixer(2, 1);
            }
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("StaminaLG"))
        {
            if (VitalixerLGAmount != 9)
            {
                audioSrc.PlayOneShot(item);
                VitalixerLGPickedUp = true;
                GainVitalixer(3, 1);
            }
            Destroy(other.gameObject);
        }
    }
}
