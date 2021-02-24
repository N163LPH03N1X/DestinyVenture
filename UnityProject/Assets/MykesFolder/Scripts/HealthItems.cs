using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthItems : MonoBehaviour {
    public bool StartDemo;
    AudioSource audioSrc;
    public AudioClip heal;
    public AudioClip item;

    public Image HealixerSMUI;
    public Image HealixerMDUI;
    public Image HealixerLGUI;

    public Text HealixerSMQuanity;
    public Text HealixerMDQuanity;
    public Text HealixerLGQuanity;

    public Text HealixerSMName;
    public Text HealixerMDName;
    public Text HealixerLGName;

    public static int HealixerSMAmount;
    public static int HealixerMDAmount;
    public static int HealixerLGAmount;

    public static bool HealixerSMPickedUp = false;
    public static bool HealixerMDPickedUp = false;
    public static bool HealixerLGPickedUp = false;

    PlayerStats PlayST;
    bool HealthFull;

   

    // Use this for initialization

    void Start () {

        
        PlayST = GetComponent<PlayerStats>();
        audioSrc = GetComponent<AudioSource>();
        if (StartDemo)
        {
            GainHealixer(1, 9);
            GainHealixer(2, 9);
            GainHealixer(3, 9);
        }
        else
        {
            HealixerSMUI.enabled = false;
            HealixerSMName.enabled = false;
            HealixerSMQuanity.enabled = false;
            HealixerMDUI.enabled = false;
            HealixerMDName.enabled = false;
            HealixerMDQuanity.enabled = false;
            HealixerLGUI.enabled = false;
            HealixerLGName.enabled = false;
            HealixerLGQuanity.enabled = false;

            HealixerSMAmount = 0;
            HealixerMDAmount = 0;
            HealixerLGAmount = 0;
        }
        SetCountQuanity();

    }
	
	// Update is called once per frame
	void Update () {
        HealthFull = PlayerStats.HealthFull;
	}

    public void RemoveHealixer(int num)
    {
        if (num == 1)
        {
            if (!HealthFull)
            {
             
                if (HealixerSMAmount <= 0)
                {
                    HealixerSMAmount = 0;
                    //ElixerSMUI.enabled = false;
                    //ElixerSMName.enabled = false;
                    //ElixerSMQuanity.enabled = false;
                }
                else
                {
                    audioSrc.PlayOneShot(heal);
                    HealixerSMAmount -= 1;
                    PlayST.GivePlayerHealth(25);
                }
            }
           

        }
        if (num == 2)
        {
            if (!HealthFull)
            {
               
                if (HealixerMDAmount <= 0)
                {
                    HealixerMDAmount = 0;
                    //ElixerMDUI.enabled = false;
                    //ElixerMDName.enabled = false;
                    //ElixerMDQuanity.enabled = false;
                }
                else
                {
                    audioSrc.PlayOneShot(heal);
                    HealixerMDAmount -= 1;
                    PlayST.GivePlayerHealth(50);
                }
            }
        }
        if (num == 3)
        {
            if (!HealthFull)
            {
               
                if (HealixerLGAmount <= 0)
                {
                    HealixerLGAmount = 0;
                    //ElixerLGUI.enabled = false;
                    //ElixerLGName.enabled = false;
                    //ElixerLGQuanity.enabled = false;
                }
                else
                {
                    audioSrc.PlayOneShot(heal);
                    HealixerLGAmount -= 1;
                    PlayST.GivePlayerHealth(100);
                }
            }
           

        }
        SetCountQuanity();
    }
    public void GainHealixer(int num, int Amount)
    {
        if (num == 1)
        {
            if (HealixerSMAmount >= 9)
            {
                HealixerSMAmount = 9;
            }
            else
            {
                HealixerSMPickedUp = true;
                HealixerSMUI.enabled = true;
                HealixerSMName.enabled = true;
                HealixerSMQuanity.enabled = true;
                HealixerSMAmount += Amount;
            }
        }
        if (num == 2)
        {

            if (HealixerMDAmount >= 9)
            {
                HealixerMDAmount = 9;
            }
            else
            {
                HealixerMDPickedUp = true;
                HealixerMDUI.enabled = true;
                HealixerMDName.enabled = true;
                HealixerMDQuanity.enabled = true;
                HealixerMDAmount += Amount;
            }
        }
        if (num == 3)
        {

            if (HealixerLGAmount >= 9)
            {
                HealixerLGAmount = 9;
            }
            else
            {
                HealixerLGPickedUp = true;
                HealixerLGUI.enabled = true;
                HealixerLGName.enabled = true;
                HealixerLGQuanity.enabled = true;
                HealixerLGAmount += Amount;
            }
        }
        SetCountQuanity();
    }
    public void SetCountQuanity()
    {
        HealixerSMQuanity.text = HealixerSMAmount.ToString();
        HealixerMDQuanity.text = HealixerMDAmount.ToString();
        HealixerLGQuanity.text = HealixerLGAmount.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HealthSM"))
        {
            if (HealixerSMAmount != 9)
            {
                audioSrc.PlayOneShot(item);
                GainHealixer(1, 1);
            }
           
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("HealthMD"))
        {
            if (HealixerMDAmount != 9)
            {
                audioSrc.PlayOneShot(item);
                GainHealixer(2, 1);
            }
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("HealthLG"))
        {
            if (HealixerLGAmount != 9)
            {
                audioSrc.PlayOneShot(item);
                GainHealixer(3, 1);
            }
            Destroy(other.gameObject);
        }
    }
}
