using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TempleKeys : MonoBehaviour {

    // Use this for initialization
    public bool StartDemo = false;
    SceneLoader ExitTemple;
    AudioSource audioSrc;
    public AudioClip key;

    public GameObject gameCtrl;
    PauseMenu ItemPause;
    public GameObject ItemMenuFlameKey;
    public GameObject ItemMenuShadowKey;
    public GameObject ItemMenuFrostKey;
    public GameObject ItemMenuFlareCrystal;
    public GameObject ItemMenuElectroCrystal;
    public GameObject ItemMenuGlaceCrystal;

    public int FireKeys;
    public int BossFireKeys;
    public int ThunderKeys;
    public int BossThunderKeys;
    public int IceKeys;
    public int BossIceKeys;
   
    public static int CrystalKeys;

    public static bool FireKey = false;
    public static bool BossFireKey = false;
    public static bool ThunderKey = false;
    public static bool BossThunderKey = false;
    public static bool IceKey = false;
    public static bool BossIceKey = false;
    public static bool FlameTempleFinished = false;
    public static bool PhantomTempleFinished = false;
    public static bool FrostTempleFinished = false;

    public static bool CrystalKey = false;

    public Image FireKeyUI;
    public Text FireKeyQuanity;
    public Text FireKeyName;

    public Image BossFireKeyUI;
    public Text BossFireKeyName;

    public Image ThunderKeyUI;
    public Text ThunderKeyQuanity;
    public Text ThunderKeyName;

    public Image BossThunderKeyUI;
    public Text BossThunderKeyName;

    public Image IceKeyUI;
    public Text IceKeyQuanity;
    public Text IceKeyName;

    public Image BossIceKeyUI;
    public Text BossIceKeyName;

    public Image Crystal1KeyUI;
 
    public Text Crystal1KeyName;

    public Image Crystal2KeyUI;
   
    public Text Crystal2KeyName;

    public Image Crystal3KeyUI;
   
    public Text Crystal3KeyName;
    bool BossFireKeyUsed;
    bool FireKeyUsed;
    bool BossThunderKeyUsed;
    bool ThunderKeyUsed;
    bool BossIceKeyUsed;
    bool IceKeyUsed;
    bool CrystalKeysUsed;

    void Start ()
    {
        audioSrc = GetComponent<AudioSource>();
        if (StartDemo)
        {
            GainKey(1, 3);
            GainKey(2, 1);
            GainKey(3, 3);
            GainKey(4, 1);
            GainKey(5, 3);
            GainKey(6, 1);
            GainKey(7, 3);
        }
        else
        {
            FireKeys = 0;
            BossFireKeys = 0;
            ThunderKeys = 0;
            BossThunderKeys = 0;
            IceKeys = 0;
            BossIceKeys = 0;
            CrystalKeys = 0;
            ShutoffKeyUI();
        }
        SetCountQuanity();
    }

    private void Update()
    {
        if (CrystalKeys == 0)
        {
            Crystal3KeyUI.enabled = false;
            Crystal3KeyName.enabled = false;
            Crystal2KeyUI.enabled = false;
            Crystal2KeyName.enabled = false;
            Crystal1KeyUI.enabled = false;
            Crystal1KeyName.enabled = false;
        }
        if (CrystalKeys == 1)
        {
            Crystal3KeyUI.enabled = false;
            Crystal3KeyName.enabled = false;
            Crystal2KeyUI.enabled = false;
            Crystal2KeyName.enabled = false;
            Crystal1KeyUI.enabled = true;
            Crystal1KeyName.enabled = true;
        }
        else if (CrystalKeys == 2)
        {
            Crystal3KeyUI.enabled = false;
            Crystal3KeyName.enabled = false;
            Crystal2KeyUI.enabled = true;
            Crystal2KeyName.enabled = true;
            Crystal1KeyUI.enabled = true;
            Crystal1KeyName.enabled = true;
        }
        else if (CrystalKeys == 3)
        {
            Crystal3KeyUI.enabled = true;
            Crystal3KeyName.enabled = true;
            Crystal2KeyUI.enabled = true;
            Crystal2KeyName.enabled = true;
            Crystal1KeyUI.enabled = true;
            Crystal1KeyName.enabled = true;
            CrystalKey = true;
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FireKey"))
        {
            audioSrc.PlayOneShot(key);
            GainKey(1, 1);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("BossFireKey"))
        {
            audioSrc.PlayOneShot(key);
            GainKey(2, 1);
            Destroy(other.gameObject);
            ItemPause = gameCtrl.GetComponent<PauseMenu>();
            ItemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            ItemMenuFlameKey.SetActive(true);
        }
        if (other.gameObject.CompareTag("ThunderKey"))
        {
            audioSrc.PlayOneShot(key);
            GainKey(3, 1);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("BossThunderKey"))
        {
            audioSrc.PlayOneShot(key);
            GainKey(4, 1);
            Destroy(other.gameObject);
            ItemPause = gameCtrl.GetComponent<PauseMenu>();
            ItemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            ItemMenuShadowKey.SetActive(true);
        }
        if (other.gameObject.CompareTag("IceKey"))
        {
            audioSrc.PlayOneShot(key);
            GainKey(5, 1);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("BossIceKey"))
        {
            audioSrc.PlayOneShot(key);
            GainKey(6, 1);
            Destroy(other.gameObject);
            ItemPause = gameCtrl.GetComponent<PauseMenu>();
            ItemPause.ItemPauseGame(PauseMenu.PauseState.ItemPause);
            ItemMenuFrostKey.SetActive(true);
        }
        if (other.gameObject.CompareTag("CrystalKeyFlame"))
        {
            audioSrc.PlayOneShot(key);
            GainKey(7, 1);
            Destroy(other.gameObject);
            ItemPause = gameCtrl.GetComponent<PauseMenu>();
            ItemPause.ItemPauseGame(PauseMenu.PauseState.CrystalPause);
            ItemMenuFlareCrystal.SetActive(true);
        }
        if (other.gameObject.CompareTag("CrystalKeyPhantom"))
        {
            audioSrc.PlayOneShot(key);
            GainKey(7, 1);
            Destroy(other.gameObject);
            ItemPause = gameCtrl.GetComponent<PauseMenu>();
            ItemPause.ItemPauseGame(PauseMenu.PauseState.CrystalPause);
            ItemMenuElectroCrystal.SetActive(true);
        }
        if (other.gameObject.CompareTag("CrystalKeyFrost"))
        {
            audioSrc.PlayOneShot(key);
            GainKey(7, 1);
            Destroy(other.gameObject);
            ItemPause = gameCtrl.GetComponent<PauseMenu>();
            ItemPause.ItemPauseGame(PauseMenu.PauseState.CrystalPause);
            ItemMenuGlaceCrystal.SetActive(true);
        }
    }
    public void GainKey(int num, int Amount)
    {
        if (num == 1)
        {
            FireKeyUsed = false;
            FireKeys += Amount;
            FireKeyUI.enabled = true;
            FireKeyQuanity.enabled = true;
            FireKeyName.enabled = true;
            FireKey = true;
            
        }
        if (num == 2)
        {
            BossFireKeyUsed = false;
            BossFireKeys += Amount;
            BossFireKeyUI.enabled = true;
            BossFireKeyName.enabled = true;
            BossFireKey = true;
        }
        if (num == 3)
        {
            ThunderKeyUsed = false;
            ThunderKeys += Amount;
            ThunderKeyUI.enabled = true;
            ThunderKeyQuanity.enabled = true;
            ThunderKeyName.enabled = true;
            ThunderKey = true;

        }
        if (num == 4)
        {
            BossThunderKeyUsed = false;
            BossThunderKeys += Amount;
            BossThunderKeyUI.enabled = true;
            BossThunderKeyName.enabled = true;
            BossThunderKey = true;
        }
        if (num == 5)
        {
            IceKeyUsed = false;
            IceKeys += Amount;
            IceKeyUI.enabled = true;
            IceKeyQuanity.enabled = true;
            IceKeyName.enabled = true;
            IceKey = true;

        }
        if (num == 6)
        {
            BossIceKeyUsed = false;
            BossIceKeys += Amount;
            BossIceKeyUI.enabled = true;
            BossIceKeyName.enabled = true;
            BossIceKey = true;
        }
        if (num == 7)
        {
            CrystalKeysUsed = false;
            CrystalKeys += Amount;
            if (CrystalKeys > 3)
            {
                CrystalKeys = 3;
            }
        }
        SetCountQuanity();
    }
    public void UseKey(int key)
    {
        if (key == 1)
        {
            FireKeys -= 1;
            FireKeyUsed = true;
            if (FireKeys <= 0)
            {
                FireKeys = 0;
                FireKey = false;
            }
        }
        if (key == 2)
        {
            BossFireKeys -= 1;
            BossFireKeyUsed = true;
            if (BossFireKeys <= 0)
            {
                BossFireKeys = 0;
                BossFireKey = false;
            }
        }
        if (key == 3)
        {
            ThunderKeys -= 1;
            ThunderKeyUsed = true;
            if (ThunderKeys <= 0)
            {
                ThunderKeys = 0;
                ThunderKey = false;
            }
        }
        if (key == 4)
        {
            BossThunderKeys -= 1;
            BossThunderKeyUsed = true;
            if (BossThunderKeys <= 0)
            {
                BossThunderKeys = 0;
                BossThunderKey = false;
            }
        }
        if (key == 5)
        {
            IceKeys -= 1;
            IceKeyUsed = true;
            if (IceKeys <= 0)
            {
                IceKeys = 0;
                IceKey = false;
            }
        }
        if (key == 6)
        {
            BossIceKeys -= 1;
            BossIceKeyUsed = true;
            if (BossIceKeys <= 0)
            {
                BossIceKeys = 0;
                BossIceKey = false;
            }
        }
        if (key == 7)
        {
            if (CrystalKeys == 3)
            {
                CrystalKeys -= 3;
                CrystalKeysUsed = true;
                if (CrystalKeys <= 0)
                {
                    CrystalKeys = 0;
                    CrystalKey = false;
                }
            }

        }
        SetCountQuanity();
    }
    public void SetCountQuanity()
    {
        FireKeyQuanity.text = FireKeys.ToString();
        ThunderKeyQuanity.text = ThunderKeys.ToString();
        IceKeyQuanity.text = IceKeys.ToString();
    }
    public void ShutoffKeyUI()
    {
        FireKeyUI.enabled = false;
        FireKeyQuanity.enabled = false;
        FireKeyName.enabled = false;

        BossFireKeyUI.enabled = false;
        BossFireKeyName.enabled = false;

        ThunderKeyUI.enabled = false;
        ThunderKeyQuanity.enabled = false;
        ThunderKeyName.enabled = false;

        BossThunderKeyUI.enabled = false;
        BossThunderKeyName.enabled = false;

        IceKeyUI.enabled = false;
        IceKeyQuanity.enabled = false;
        IceKeyName.enabled = false;

        BossIceKeyUI.enabled = false;
        BossIceKeyName.enabled = false;

        Crystal1KeyUI.enabled = false;
        Crystal1KeyName.enabled = false;

        Crystal2KeyUI.enabled = false;
        Crystal2KeyName.enabled = false;

        Crystal3KeyUI.enabled = false;
        Crystal3KeyName.enabled = false;
    }
    public void BeatTemple()
    {
        if (CrystalKeys == 1)
        {
            FlameTempleFinished = true;
            ExitTemple = GetComponent<SceneLoader>();
            ExitTemple.GrabCrystal(SceneLoader.TempleExit.flame);
        }
        else if (CrystalKeys == 2)
        {
            PhantomTempleFinished = true;
            ExitTemple = GetComponent<SceneLoader>();
            ExitTemple.GrabCrystal(SceneLoader.TempleExit.phant);
        }
        else if (CrystalKeys == 3)
        {
            FrostTempleFinished = true;
            ExitTemple = GetComponent<SceneLoader>();
            ExitTemple.GrabCrystal(SceneLoader.TempleExit.glace);
            
        }
    }
}
