using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Breast : MonoBehaviour {

    public bool StartDemo;
    PlayerStats PlayST;
    PowerCounter FrostBreastOn;
    AudioSource audioSrc;
    public AudioClip breast;

    public GameObject gameCtrl;
    PauseMenu ItemPause;
    public GameObject ItemMenuFlameBreast;
    public GameObject ItemMenuPhantomBreast;
    public GameObject ItemMenuFrostBreast;

    public Image PauseFrostBreastUI;
    public Image GameFrostBreastUI;
    public Image FrostSheetUI;
    public Image FrostBarUI;

    float freezeTimer = 0;
    public static bool FrostBreastPickedUp = false;
    public static bool FrostBreast = false;
    bool isPaused;

    public Image PauseFlameBreastUI;
    public Image GameFlameBreastUI;
    public Image FlameSheetUI;
    public Image FlameBarUI;

    float burnTimer = 0;
    public static bool FlameBreastPickedUp = false;
    public static bool FlameBreast = false;

    public Image PausePhantomBreastUI;
    public Image GamePhantomBreastUI;
    public static bool PhantomBreastPickedUp = false;
    public static bool PhantomBreast = false;
    public bool breastSelect;
    bool LeatherBreast;
    public Image GameLeatherBreastUI;
    bool isLoading;
    bool isDisabled;
    public float stamina;
    public static bool FlameBreastActive;
    public static bool PhantBreastActive;
    public static bool FrostBreastActive;
    bool ActivateBreast;

    // Use this for initialization
    void Start() {
        PlayST = GetComponent<PlayerStats>();
        audioSrc = GetComponent<AudioSource>();
        if (StartDemo)
        {
            PauseFlameBreastUI.enabled = true;
            PausePhantomBreastUI.enabled = true;
            PauseFrostBreastUI.enabled = true;
            FrostBreastPickedUp = true;
            FlameBreastPickedUp = true;
            PhantomBreastPickedUp = true;
        }
        else
        {
            PauseFlameBreastUI.enabled = false;
            PausePhantomBreastUI.enabled = false;
            PauseFrostBreastUI.enabled = false;
            FrostBreastPickedUp = false;
            FlameBreastPickedUp = false;
            PhantomBreastPickedUp = false;
           
        }
        FrostSheetUI.enabled = false;
        FrostBarUI.enabled = false;
        FlameSheetUI.enabled = false;
        FlameBarUI.enabled = false;
        EnableBreast(1);
    }

    // Update is called once per frame
    void Update()
    {
        isPaused = PauseMenu.Paused;
        isLoading = SceneLoader.isLoading;
        isDisabled = SceneLoader.isDisabled;
        stamina = PlayerController.stamina;

        if (stamina > 0)
        {
            if (FlameBreast)
            {
                BreastActive(BreastType.Flame, true);
            }
            else if (PhantomBreast)
            {
                BreastActive(BreastType.Phant, true);
            }
            else if (FrostBreast)
            {
                BreastActive(BreastType.Frost, true);
            }
        }
        else if (stamina <= 0)
        {
            if (FlameBreast)
            {
                BreastActive(BreastType.Flame, false);
            }
            else if (PhantomBreast)
            {
                BreastActive(BreastType.Phant, false);
            }
            else if (FrostBreast)
            {
                BreastActive(BreastType.Frost, false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0) && !breastSelect && !isPaused && !isDisabled|| Input.GetAxisRaw("SelectHorizontal") == 1 && !breastSelect && !isPaused && !isDisabled)
        {
           
            if (!isLoading)
            {
                audioSrc.PlayOneShot(breast);
                if (LeatherBreast)
                {
                    RotateBreast(1);
                }
                else if (FlameBreast)
                {
                    RotateBreast(2);
                }
                else if (PhantomBreast)
                {
                    RotateBreast(3);
                }
                else if (FrostBreast)
                {
                    RotateBreast(4);
                }
                breastSelect = true;
            }
           
        }
        else if (Input.GetAxisRaw("SelectHorizontal") == 0 || (Input.GetKeyUp(KeyCode.Alpha0)))
        {
            breastSelect = false;
        }
        if (freezeTimer > 0)
        {
            FrostSheetUI.color = new Color(FrostSheetUI.color.r, FrostSheetUI.color.g, FrostSheetUI.color.b, Mathf.PingPong(Time.time, 0.5f));
            FrostBarUI.color = new Color(FrostBarUI.color.r, FrostBarUI.color.g, FrostBarUI.color.b, Mathf.PingPong(Time.time, 1));
            freezeTimer -= Time.deltaTime;
        }
        if (freezeTimer < 0)
        {
            if (FrostBreast)
            {
                freezeTimer = 30;
                PlayST.doDamage(1);
            }
            else
            {
                freezeTimer = 0.2f;
                PlayST.doDamage(25);
            }


        }
        if (burnTimer > 0)
        {
            FlameSheetUI.color = new Color(FlameSheetUI.color.r, FlameSheetUI.color.g, FlameSheetUI.color.b, Mathf.PingPong(Time.time, 0.5f));
            FlameBarUI.color = new Color(FlameSheetUI.color.r, FlameSheetUI.color.g, FlameSheetUI.color.b, Mathf.PingPong(Time.time, 1));
            burnTimer -= Time.deltaTime;
        }
        if (burnTimer < 0)
        {
            if (FlameBreast)
            {
                burnTimer = 30;
                PlayST.doDamage(1);
            }
            else
            {
                burnTimer = 0.2f;
                PlayST.doDamage(25);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FrostBreast"))
        {
            audioSrc.PlayOneShot(breast);
            FrostBreastPickedUp = true;
            PauseFrostBreastUI.enabled = true;
            EnableBreast(4);
            Destroy(other.gameObject);
            ItemPause = gameCtrl.GetComponent<PauseMenu>();
            ItemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            ItemMenuFrostBreast.SetActive(true);
        }
        if (other.gameObject.CompareTag("FrostSheet"))
        {
            if (FrostBreast)
            {
                freezeTimer = 30;
            }
            else
            {
                freezeTimer = 0.2f;
            }
            FrostSheetUI.enabled = true;
            FrostBarUI.enabled = true;
        }

        if (other.gameObject.CompareTag("FlameBreast"))
        {
            audioSrc.PlayOneShot(breast);
            FlameBreastPickedUp = true;
            PauseFlameBreastUI.enabled = true;
            EnableBreast(2);
            Destroy(other.gameObject);
            ItemPause = gameCtrl.GetComponent<PauseMenu>();
            ItemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            ItemMenuFlameBreast.SetActive(true);
        }
        if (other.gameObject.CompareTag("FlameSheet"))
        {
            if (FlameBreast)
            {
                burnTimer = 30;
            }
            else
            {
                burnTimer = 0.2f;
            }
            FlameSheetUI.enabled = true;
            FlameBarUI.enabled = true;
        }
        if (other.gameObject.CompareTag("PhantomBreast"))
        {
            audioSrc.PlayOneShot(breast);
            PhantomBreastPickedUp = true;
            PausePhantomBreastUI.enabled = true;
            EnableBreast(3);
            Destroy(other.gameObject);
            ItemPause = gameCtrl.GetComponent<PauseMenu>();
            ItemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            ItemMenuPhantomBreast.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FrostSheet"))
        {
            freezeTimer = 0;
            FrostSheetUI.enabled = false;
            FrostBarUI.enabled = false;
        }
        if (other.gameObject.CompareTag("FlameSheet"))
        {
            burnTimer = 0;
            FlameSheetUI.enabled = false;
            FlameBarUI.enabled = false;
        }
    }
    public void EnableBreast(int num)
    {
        if (num == 1)
        {
            LeatherBreast = true;
            FlameBreast = false;
            BreastActive(BreastType.Flame, false);
            PhantomBreast = false;
            BreastActive(BreastType.Phant, false);
            FrostBreast = false;
            BreastActive(BreastType.Frost, false);
            FrostBreastOn = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerCounter>();
            FrostBreastOn.EnableFrostBreast(false);
            GameLeatherBreastUI.enabled = true;
            GamePhantomBreastUI.enabled = false;
            GameFlameBreastUI.enabled = false;
            GameFrostBreastUI.enabled = false;
        }
        if (num == 2)
        {
            if (FlameBreastPickedUp)
            {
                LeatherBreast = false;
                FlameBreast = true;
                BreastActive(BreastType.Frost, false);
                BreastActive(BreastType.Phant, false);
                PhantomBreast = false;
                FrostBreast = false;
                FrostBreastOn = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerCounter>();
                FrostBreastOn.EnableFrostBreast(false);
                GameLeatherBreastUI.enabled = false;
                GamePhantomBreastUI.enabled = false;
                GameFlameBreastUI.enabled = true;
                GameFrostBreastUI.enabled = false;
            }
        }
        if (num == 3)
        {
            if (PhantomBreastPickedUp)
            {
                BreastActive(BreastType.Frost, false);
                BreastActive(BreastType.Flame, false);
                LeatherBreast = false;
                FlameBreast = false;
                PhantomBreast = true;
                FrostBreast = false;
                FrostBreastOn = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerCounter>();
                FrostBreastOn.EnableFrostBreast(false);
                GameLeatherBreastUI.enabled = false;
                GamePhantomBreastUI.enabled = true;
                GameFlameBreastUI.enabled = false;
                GameFrostBreastUI.enabled = false;
            }
        }
        if (num == 4)
        {
            if (FrostBreastPickedUp)
            {
                BreastActive(BreastType.Phant, false);
                BreastActive(BreastType.Flame, false);
                LeatherBreast = false;
                FlameBreast = false;
                PhantomBreast = false;
                FrostBreast = true;
                FrostBreastOn = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerCounter>();
                FrostBreastOn.EnableFrostBreast(true);
                GameLeatherBreastUI.enabled = false;
                GamePhantomBreastUI.enabled = false;
                GameFlameBreastUI.enabled = false;
                GameFrostBreastUI.enabled = true;
            }
        }
    }
    public void RotateBreast(int num)
    {
        if (num == 1)
        {
            //Just Spikeboots
            if (FlameBreastPickedUp && !PhantomBreastPickedUp && !FrostBreastPickedUp)
            {
                EnableBreast(2);
            }
            // Just SpeedBoots
            else if (!FlameBreastPickedUp && PhantomBreastPickedUp && !FrostBreastPickedUp)
            {
                EnableBreast(3);
            }
            //Just HealingBoots
            else if (!FlameBreastPickedUp && !PhantomBreastPickedUp && FrostBreastPickedUp)
            {
                EnableBreast(4);
            }
            //All Three
            else if (FlameBreastPickedUp && PhantomBreastPickedUp && FrostBreastPickedUp)
            {
                EnableBreast(2);
            }
            //Just Spike & Speed
            else if (FlameBreastPickedUp && PhantomBreastPickedUp && !FrostBreastPickedUp)
            {
                EnableBreast(2);
            }
            //Just Speed & Healing
            else if (!FlameBreastPickedUp && PhantomBreastPickedUp && FrostBreastPickedUp)
            {
                EnableBreast(3);
            }
            //Just Spike & Healing
            else if (FlameBreastPickedUp && !PhantomBreastPickedUp && FrostBreastPickedUp)
            {
                EnableBreast(2);
            }
            //Just Speed & Spike
            else if (FlameBreastPickedUp && PhantomBreastPickedUp && !FrostBreastPickedUp)
            {
                EnableBreast(2);
            }
            //Nothing
            else
            {
                EnableBreast(1);
            }
        }
        else if (num == 2)
        {
            //Just Spikeboots
            if (!PhantomBreastPickedUp && !FrostBreastPickedUp)
            {
                EnableBreast(1);
            }
            // Just SpeedBoots
            else if (PhantomBreastPickedUp && !FrostBreastPickedUp)
            {
                EnableBreast(3);
            }
            //Just HealingBoots
            else if (!PhantomBreastPickedUp && FrostBreastPickedUp)
            {
                EnableBreast(4);
            }
            //All Three
            else if (PhantomBreastPickedUp && FrostBreastPickedUp)
            {
                EnableBreast(3);
            }
            //Nothing
            else
            {
                EnableBreast(1);
            }
        }
        else if (num == 3)
        {
            //Just Spikeboots
            if (FlameBreastPickedUp && !FrostBreastPickedUp)
            {
                EnableBreast(1);
            }
            // Just SpeedBoots
            else if (!FlameBreastPickedUp && !FrostBreastPickedUp)
            {
                EnableBreast(1);
            }
            //Just HealingBoots
            else if (!FlameBreastPickedUp && FrostBreastPickedUp)
            {
                EnableBreast(4);
            }
            //All Three
            else if (FlameBreastPickedUp && FrostBreastPickedUp)
            {
                EnableBreast(4);
            }
            //Nothing
            else
            {
                EnableBreast(1);
            }
        }
        else if (num == 4)
        {
            //Just Spikeboots
            if (FlameBreastPickedUp && !PhantomBreastPickedUp)
            {
                EnableBreast(1);
            }
            // Just SpeedBoots
            else if (!FlameBreastPickedUp && PhantomBreastPickedUp)
            {
                EnableBreast(1);
            }
            //Just HealingBoots
            else if (!FlameBreastPickedUp && !PhantomBreastPickedUp)
            {
                EnableBreast(1);
            }
            //All Three
            else if (FlameBreastPickedUp && PhantomBreastPickedUp && FrostBreastPickedUp)
            {
                EnableBreast(1);
            }
            //Nothing
            else
            {
                EnableBreast(1);
            }
        }
    }
    public enum BreastType { Leather,Flame, Phant, Frost}
    public void BreastActive(BreastType type, bool True)
    {
        if (True)
        {
            switch (type)
            {
                case BreastType.Leather:
                    {
                        FlameBreastActive = false;
                        PhantBreastActive = false;
                        FrostBreastActive = false;
                        break;
                    }
                case BreastType.Flame:
                    {
                        FlameBreastActive = true;
                        break;
                    }
                case BreastType.Phant:
                    {
                        PhantBreastActive = true;
                        break;
                    }
                case BreastType.Frost:
                    {
                        FrostBreastActive = true;
                        break;
                    }
            }
        }
        else
        {
            switch (type)
            {
                case BreastType.Leather:
                    {
                        FlameBreastActive = false;
                        PhantBreastActive = false;
                        FrostBreastActive = false;
                        break;
                    }
                case BreastType.Flame:
                    {
                        FlameBreastActive = false;
                        break;
                    }
                case BreastType.Phant:
                    {
                        PhantBreastActive = false;
                        break;
                    }
                case BreastType.Frost:
                    {
                        FrostBreastActive = false;
                        break;
                    }
            }
        }
       
    }
    public void ResetBreastState()
    {
        freezeTimer = 0;
        FrostSheetUI.enabled = false;
        FrostBarUI.enabled = false;
        burnTimer = 0;
        FlameSheetUI.enabled = false;
        FlameBarUI.enabled = false;
    }
}

    
    
