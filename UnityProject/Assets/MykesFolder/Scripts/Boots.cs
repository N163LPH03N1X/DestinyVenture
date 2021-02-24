using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boots : MonoBehaviour {


    public bool StartDemo;
    AudioSource audioSrc;
    public AudioClip boots;

    public Image PauseSpikeBootsUI;
    public Image GameSpikeBootsUI;
    public static bool SpikeBootsPickedUp;
    public static bool SpikeBoots = false;
    public float hurtTimer;
    float resetHurtTimer;
    bool hurtPlayer = false;

    public Image PauseHealingBootsUI;
    public Image GameHealingBootsUI;

    public static bool HealingBootsPickedUp;
    public static bool HealingBoots = false;
    public float healtimer;
    float resetTimer;

    public Image PauseLeatherBootsUI;
    public Image GameLeatherBootsUI;
    bool LeatherBoots;

    public Image PauseSpeedBootsUI;
    public Image GameSpeedBootsUI;
    public static bool SpeedBootsPickedUp;
    public static bool SpeedBoots = false;

    PlayerStats PlayST;
    bool isMoving;
    bool isRunning;
    bool Spike;
    bool isLoading;
    public bool bootSelect;
    bool isPaused;
    bool isDisabled;

    public bool leatherenable;
    public bool spikeenable;
    public bool speedenable;
    public bool healenable;

    public GameObject gameCtrl;
    PauseMenu ItemPause;
    public GameObject ItemMenuSpikeBoots;
    public GameObject ItemMenuSpeedBoots;
    public GameObject ItemMenuHealingBoots;

    // Use this for initialization
    void Start () {

        if (StartDemo)
        {
            PauseLeatherBootsUI.enabled = true;
            SpikeBootsPickedUp = true;
            PauseSpikeBootsUI.enabled = true;
            SpeedBootsPickedUp = true;
            PauseSpeedBootsUI.enabled = true;
            HealingBootsPickedUp = true;
            PauseHealingBootsUI.enabled = true;
        }
        else
        {
            PauseLeatherBootsUI.enabled = true;
            SpikeBootsPickedUp = false;
            PauseSpikeBootsUI.enabled = false;
            SpeedBootsPickedUp = false;
            PauseSpeedBootsUI.enabled = false;
            HealingBootsPickedUp = false;
            PauseHealingBootsUI.enabled = false;
        }


        PlayST = GetComponent<PlayerStats>();
        audioSrc = GetComponent<AudioSource>();
        resetTimer = 5f;
        resetHurtTimer = hurtTimer;

       
        EnableBoots(1);
    }
	
	// Update is called once per frame
	void Update () {

        isMoving = PlayerController.isMoving;
        isRunning = PlayerController.isRunning;
        isPaused = PauseMenu.Paused;
        isDisabled = SceneLoader.isDisabled;
        leatherenable = LeatherBoots;
        speedenable = SpeedBoots;
        spikeenable = SpikeBoots;
        healenable = HealingBoots;

        if (Input.GetKeyDown(KeyCode.Alpha9) && !bootSelect && !isPaused && !isDisabled || Input.GetAxisRaw("SelectVertical") == -1 && !bootSelect && !isPaused && !isDisabled)
        {
            isLoading = SceneLoader.isLoading;
            if (!isLoading)
            {
                audioSrc.PlayOneShot(boots);
                if (LeatherBoots)
                {
                    RotateBoots(1);
                }
                else if (SpikeBoots)
                {
                    RotateBoots(2);
                }
                else if (SpeedBoots)
                {
                    RotateBoots(3);
                }
                else if (HealingBoots)
                {
                    RotateBoots(4);
                }
                bootSelect = true;
            }
        }
        else if(Input.GetAxisRaw("SelectVertical") == 0)
        {
            bootSelect = false;
        }

        if (SpikeBoots)
        {
            hurtPlayer = false;
        }
        else
        {
            if (hurtTimer > 0 && hurtPlayer)
            {
                hurtTimer -= Time.deltaTime;
            }
            if (hurtTimer < 0)
            {
                if (Spike && !SpikeBoots)
                {
                    PlayST.doDamage(5);
                }

                hurtTimer = resetHurtTimer;
                Spike = false;
                hurtPlayer = false;
            }
        }
        if (HealingBoots)
        {
            if (isMoving)
            {
                if (healtimer > 0 && !isRunning)
                {
                    healtimer -= Time.deltaTime;
                }
                else if (healtimer > 0 && isRunning)
                {
                    healtimer -= Time.deltaTime * 2;
                }
                if (healtimer < 0)
                {
                    PlayST.GivePlayerHealth(1);
                    healtimer = resetTimer;
                }
            }
        }
        else
        {
            healtimer = 0;
        }
	}
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("SpikeBoots"))
        {
            audioSrc.PlayOneShot(boots);
            SpikeBootsPickedUp = true;
            PauseSpikeBootsUI.enabled = true;
            EnableBoots(2);
            Destroy(other.gameObject);
            ItemPause = gameCtrl.GetComponent<PauseMenu>();
            ItemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            ItemMenuSpikeBoots.SetActive(true);
        }
        if (other.gameObject.CompareTag("SpeedBoots"))
        {
            audioSrc.PlayOneShot(boots);
            SpeedBootsPickedUp = true;
            PauseSpeedBootsUI.enabled = true;
            EnableBoots(3);
            Destroy(other.gameObject);
            ItemPause = gameCtrl.GetComponent<PauseMenu>();
            ItemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            ItemMenuSpeedBoots.SetActive(true);
        }
        if (other.gameObject.CompareTag("HealingBoots"))
        {
            audioSrc.PlayOneShot(boots);
            HealingBootsPickedUp = true;
            PauseHealingBootsUI.enabled = true;
            EnableBoots(4);
            Destroy(other.gameObject);
            ItemPause = gameCtrl.GetComponent<PauseMenu>();
            ItemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            ItemMenuHealingBoots.SetActive(true);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Spike") && !SpikeBoots)
        {
            Spike = true;
            hurtPlayer = true;
        }
       
    }

    public void EnableBoots(int num)
    {
        if (num == 1)
        {
            LeatherBoots = true;
            SpikeBoots = false;
            SpeedBoots = false;
            HealingBoots = false;

            GameLeatherBootsUI.enabled = true;
            GameSpikeBootsUI.enabled = false;
            GameSpeedBootsUI.enabled = false;
            GameHealingBootsUI.enabled = false;

        }
        if (num == 2)
        {
            if (SpikeBootsPickedUp)
            {
                LeatherBoots = false;
                SpikeBoots = true;
                SpeedBoots = false;
                HealingBoots = false;

                GameLeatherBootsUI.enabled = false;
                GameSpikeBootsUI.enabled = true;
                GameSpeedBootsUI.enabled = false;
                GameHealingBootsUI.enabled = false;
            }
        }
        if (num == 3)
        {
            if (SpeedBootsPickedUp)
            {
                LeatherBoots = false;
                SpikeBoots = false;
                SpeedBoots = true;
                HealingBoots = false;
                GameLeatherBootsUI.enabled = false;
                GameSpikeBootsUI.enabled = false;
                GameSpeedBootsUI.enabled = true;
                GameHealingBootsUI.enabled = false;
            }
        }
        if (num == 4)
        {
            if (HealingBootsPickedUp)
            {
                LeatherBoots = false;
                SpikeBoots = false;
                SpeedBoots = false;
                HealingBoots = true;
                GameLeatherBootsUI.enabled = false;
                GameSpikeBootsUI.enabled = false;
                GameSpeedBootsUI.enabled = false;
                GameHealingBootsUI.enabled = true;
                healtimer += resetTimer;
                if (healtimer > resetTimer)
                {
                    healtimer = resetTimer;
                }
            }
        }
    }
    public void RotateBoots(int num)
    {
        if (num == 1)
        {
            //Just Spikeboots
            if (SpikeBootsPickedUp && !SpeedBootsPickedUp && !HealingBootsPickedUp)
            {
                EnableBoots(2);
            }
            // Just SpeedBoots
            else if (!SpikeBootsPickedUp && SpeedBootsPickedUp && !HealingBootsPickedUp)
            {
                EnableBoots(3);
            }
            //Just HealingBoots
            else if (!SpikeBootsPickedUp && !SpeedBootsPickedUp && HealingBootsPickedUp)
            {
                EnableBoots(4);
            }
            //All Three
            else if (SpikeBootsPickedUp && SpeedBootsPickedUp && HealingBootsPickedUp)
            {
                EnableBoots(2);
            }
            //Just Spike & Speed
            else if (SpikeBootsPickedUp && SpeedBootsPickedUp && !HealingBootsPickedUp)
            {
                EnableBoots(2);
            }
            //Just Speed & Healing
            else if (!SpikeBootsPickedUp && SpeedBootsPickedUp && HealingBootsPickedUp)
            {
                EnableBoots(3);
            }
            //Just Spike & Healing
            else if (SpikeBootsPickedUp && !SpeedBootsPickedUp && HealingBootsPickedUp)
            {
                EnableBoots(2);
            }
            //Just Speed & Spike
            else if (SpikeBootsPickedUp && SpeedBootsPickedUp && !HealingBootsPickedUp)
            {
                EnableBoots(2);
            }
            //Nothing
            else
            {
                EnableBoots(1);
            }
        }
        else if (num == 2)
        {
            //Just Spikeboots
            if (!SpeedBootsPickedUp && !HealingBootsPickedUp)
            {
                EnableBoots(1);
            }
            // Just SpeedBoots
            else if (SpeedBootsPickedUp && !HealingBootsPickedUp)
            {
                EnableBoots(3);
            }
            //Just HealingBoots
            else if (!SpeedBootsPickedUp && HealingBootsPickedUp)
            {
                EnableBoots(4);
            }
            //All Three
            else if (SpeedBootsPickedUp && HealingBootsPickedUp)
            {
                EnableBoots(3);
            }
            //Nothing
            else
            {
                EnableBoots(1);
            }
        }
        else if (num == 3)
        {
            //Just Spikeboots
            if (SpikeBootsPickedUp && !HealingBootsPickedUp)
            {
                EnableBoots(1);
            }
            // Just SpeedBoots
            else if (!SpikeBootsPickedUp && !HealingBootsPickedUp)
            {
                EnableBoots(1);
            }
            //Just HealingBoots
            else if (!SpikeBootsPickedUp && HealingBootsPickedUp)
            {
                EnableBoots(4);
            }
            //All Three
            else if (SpikeBootsPickedUp && HealingBootsPickedUp)
            {
                EnableBoots(4);
            }
            //Nothing
            else
            {
                EnableBoots(1);
            }
        }
        else if (num == 4)
        {
            //Just Spikeboots
            if (SpikeBootsPickedUp && !SpeedBootsPickedUp)
            {
                EnableBoots(1);
            }
            // Just SpeedBoots
            else if (!SpikeBootsPickedUp && SpeedBootsPickedUp)
            {
                EnableBoots(1);
            }
            //Just HealingBoots
            else if (!SpikeBootsPickedUp && !SpeedBootsPickedUp)
            {
                EnableBoots(1);
            }
            //All Three
            else if (SpikeBootsPickedUp && SpeedBootsPickedUp && HealingBootsPickedUp)
            {
                EnableBoots(1);
            }
            //Nothing
            else
            {
                EnableBoots(1);
            }
        }
    }
    public void BuyBoots(int num)
    {
        if (num == 1)
        {
            PauseSpikeBootsUI.enabled = true;
            SpikeBootsPickedUp = true;
            EnableBoots(2);
        }
        else if (num == 2)
        {
            PauseSpeedBootsUI.enabled = true;
            SpeedBootsPickedUp = true;
            EnableBoots(3);
        }
        else if (num == 3)
        {
            PauseHealingBootsUI.enabled = true;
            HealingBootsPickedUp = true;
            EnableBoots(4);
        }
    }
}


