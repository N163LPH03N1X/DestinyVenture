using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour {

    Animator anim;
    public GameObject VitaSMPickupPrefab;
    public GameObject VitaMDPickupPrefab;
    public GameObject VitaLGPickupPrefab;
    public GameObject HealSMPickupPrefab;
    public GameObject HealMDPickupPrefab;
    public GameObject HealLGPickupPrefab;
    public GameObject PowGemSLPickupPrefab;
    public GameObject PowGemGDPickupPrefab;
    public GameObject GoldPrefab;
    public GameObject StrGauntPrefab;
    public GameObject PowGauntPrefab;
    public GameObject GriGauntPrefab;
    public GameObject FrostBreastPrefab;
    public GameObject flameBreastPrefab;
    public GameObject PhantBreastPrefab;
    public GameObject SpikeBootPrefab;
    public GameObject SpeedBootPrefab;
    public GameObject HealBootPrefab;
    public GameObject KeyPrefab;
    public GameObject BossKeyPrefab;
    public GameObject FlameMapPrefab;
    public GameObject PhantMapPrefab;
    public GameObject FrostMapPrefab;
    public GameObject FinalMapPrefab;
    public GameObject OverMapPrefab;
    public GameObject Emitter;
    int RandomNumber;
    GameObject Player;
    PlayerStats PlayST;
    bool PlayerFound = false;
    AudioSource audioSrc;
    public AudioClip open;
    public AudioClip buttonUISFX;
    Interaction PlayerInt;
    bool ChestOpen;
    public bool inTerritory;
    public GameObject Map;
    public bool BigHealth;
    public bool BigStam;
    public bool FullSword;
    public bool Item;
    public bool Gold;
    public bool Key;
    public bool BossKey;
    public bool FlameArmor;
    public bool StrengthGauntlet;
    public bool SpikeBoots;

    public bool PhantomArmor;
    public bool PowerGauntlet;
    public bool SpeedBoots;

    public bool FrostArmor;
    public bool GripGauntlet;
    public bool HealBoots;

    public bool FlameMap;
    public bool PhantMap;
    public bool FrostMap;
    public bool FinalMap;
    public bool OverMap;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
	}
    private void Update()
    {
        if (inTerritory)
        {
            // move door up
            if (Input.GetKeyDown(KeyCode.LeftControl) && !ChestOpen || Input.GetButtonDown("Open") && !ChestOpen)
            {
                audioSrc.clip = open;
                audioSrc.Play();
                OpenThisChest();
                Map.SetActive(false);
                Player = GameObject.FindGameObjectWithTag("Player");
                PlayerInt = Player.GetComponent<Interaction>();
                PlayerInt.EnableAUI(false, null);
                ChestOpen = true;
            }
        }
    }

    void OpenThisChest()
    {
        anim.SetTrigger("ChestOpen");
    }
    public void AlertObservers(string message)
    {
        if (message.Equals("ChestAnimationEnded"))
        {
            if (BigHealth)
            {
                Instantiate(HealLGPickupPrefab, Emitter.transform.position, Quaternion.identity, transform);
            }
            else if (FlameMap)
            {
                Instantiate(FlameMapPrefab, Emitter.transform.position, Quaternion.identity, transform);
            }
            else if (PhantMap)
            {
                Instantiate(PhantMapPrefab, Emitter.transform.position, Quaternion.identity, transform);
            }
            else if (FrostMap)
            {
                Instantiate(FrostMapPrefab, Emitter.transform.position, Quaternion.identity, transform);
            }
            else if (FinalMap)
            {
                Instantiate(FinalMapPrefab, Emitter.transform.position, Quaternion.identity, transform);
            }
            else if (OverMap)
            {
                Instantiate(OverMapPrefab, Emitter.transform.position, Quaternion.identity, transform);
            }
            else if (FullSword)
            {
                Instantiate(PowGemGDPickupPrefab, Emitter.transform.position, Quaternion.identity, transform);
            }
            else if (BigStam)
            {
                Instantiate(VitaLGPickupPrefab, Emitter.transform.position, Quaternion.identity, transform);
            }

            else if (Item)
            {
                InstantiatePickup();
            }
            else if (Gold)
            {
                Instantiate(GoldPrefab, Emitter.transform.position, Quaternion.identity, transform);
            }
            else if (Key)
            {
                InstantiateKey(1);
            }
            else if (BossKey)
            {
                InstantiateKey(2);
            }
            else if (FlameArmor)
            {
                InstantiateArmor(1);
            }
            else if (PhantomArmor)
            {
                InstantiateArmor(2);
            }
            else if (FrostArmor)
            {
                InstantiateArmor(3);
            }

            else if (StrengthGauntlet)
            {
                InstantiateGaunt(1);
            }
            else if (PowerGauntlet)
            {
                InstantiateGaunt(2);
            }
            else if (GripGauntlet)
            {
                 InstantiateGaunt(3);
            }

            else if (SpikeBoots)
            {
                InstantiateBoot(1);
            }
            else if (SpeedBoots)
            {
                InstantiateBoot(2);
            }
            else if (HealBoots)
            {
                InstantiateBoot(3);
            }
        }
    }
    public void InstantiatePickup()
    {
        int[] VitaSMnum = { 1, 2, 3 };
        int[] VitaMDnum = { 4, 5 };
        int[] VitaLGnum = { 6 };
        int[] HealSMnum = { 7, 8, 9 };
        int[] HealMDnum = { 10, 11 };
        int[] HealLGnum = { 12 };
        int[] PowGemSLnum = { 13, 14 };
        int[] PowGemGDnum = { 15 };
        //int[] Goldnum = { 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 };

        RandomNumber = Random.Range(1, 15);

        for (int i = 0; i < 1; i++)
        {
            for (int vsm = 0; vsm < VitaSMnum.Length; vsm++)
            {
                if (VitaSMnum[vsm] == RandomNumber)
                {
                    Instantiate(VitaSMPickupPrefab, Emitter.transform.position, Quaternion.identity, transform);
                }
            }
            for (int vmd = 0; vmd < VitaMDnum.Length; vmd++)
            {
                if (VitaMDnum[vmd] == RandomNumber)
                {
                    Instantiate(VitaMDPickupPrefab, Emitter.transform.position, Quaternion.identity, transform);
                }
            }
            for (int vlg = 0; vlg < VitaLGnum.Length; vlg++)
            {
                if (VitaLGnum[vlg] == RandomNumber)
                {
                    Instantiate(VitaLGPickupPrefab, Emitter.transform.position, Quaternion.identity, transform);
                }
            }
            for (int hsm = 0; hsm < HealSMnum.Length; hsm++)
            {
                if (HealSMnum[hsm] == RandomNumber)
                {
                    Instantiate(HealSMPickupPrefab, Emitter.transform.position, Quaternion.identity, transform);
                }
            }
            for (int hmd = 0; hmd < HealMDnum.Length; hmd++)
            {
                if (HealMDnum[hmd] == RandomNumber)
                {
                    Instantiate(HealMDPickupPrefab, Emitter.transform.position, Quaternion.identity, transform);
                }
            }
            for (int hlg = 0; hlg < HealLGnum.Length; hlg++)
            {
                if (HealLGnum[hlg] == RandomNumber)
                {
                    Instantiate(HealLGPickupPrefab, Emitter.transform.position, Quaternion.identity, transform);
                }
            }
            for (int pgsl = 0; pgsl < PowGemSLnum.Length; pgsl++)
            {
                if (PowGemSLnum[pgsl] == RandomNumber)
                {
                    Instantiate(PowGemSLPickupPrefab, Emitter.transform.position, Quaternion.identity, transform);
                }
            }
            for (int pggd = 0; pggd < PowGemGDnum.Length; pggd++)
            {
                if (PowGemGDnum[pggd] == RandomNumber)
                {
                    Instantiate(PowGemGDPickupPrefab, Emitter.transform.position, Quaternion.identity, transform);
                }
            }
            //for (int gold = 0; gold < Goldnum.Length; gold++)
            //{
            //    if (Goldnum[gold] == RandomNumber)
            //    {
            //        Instantiate(GoldPrefab, Emitter.transform.position, Quaternion.identity);
            //    }
                
            //}
        }
    }
    public void InstantiateKey(int num)
    {
        if (num == 1)
        {
            Instantiate(KeyPrefab, Emitter.transform.position, Quaternion.identity, transform);
        }
        if (num == 2)
        {
            Instantiate(BossKeyPrefab, Emitter.transform.position, Quaternion.identity, transform);
        }
     
    }
    public void InstantiateArmor(int num)
    {
        if (num == 1)
        {
            Instantiate(flameBreastPrefab, Emitter.transform.position, Quaternion.identity, transform);
        }
        if (num == 2)
        {
            Instantiate(PhantBreastPrefab, Emitter.transform.position, Quaternion.identity, transform);
        }
        if (num == 3)
        {
            Instantiate(FrostBreastPrefab, Emitter.transform.position, Quaternion.identity, transform);
        }

    }
    public void InstantiateBoot(int num)
    {
        if (num == 1)
        {
            Instantiate(SpikeBootPrefab, Emitter.transform.position, Quaternion.identity, transform);
        }
        if (num == 2)
        {
            Instantiate(SpeedBootPrefab, Emitter.transform.position, Quaternion.identity, transform);
        }
        if (num == 3)
        {
            Instantiate(HealBootPrefab, Emitter.transform.position, Quaternion.identity, transform);
        }
    }
    public void InstantiateGaunt(int num)
    {
        if (num == 1)
        {
            Instantiate(StrGauntPrefab, Emitter.transform.position, Quaternion.identity, transform);
        }
        if (num == 2)
        {
            Instantiate(PowGauntPrefab, Emitter.transform.position, Quaternion.identity, transform);
        }
        if (num == 3)
        {
            Instantiate(GriGauntPrefab, Emitter.transform.position, Quaternion.identity, transform);
        }
    }
  

   
    void OnTriggerEnter(Collider other)
    {

        
        if (other.gameObject.CompareTag("Player") && !ChestOpen)
        {
            PlayerInt = other.gameObject.GetComponent<Interaction>();
            audioSrc.clip = buttonUISFX;
            audioSrc.Play();
            PlayerInt.EnableAUI(true, "Open");
            inTerritory = true;
        }

    }
    void OnTriggerExit(Collider other)
    {
      
        if (other.gameObject.CompareTag("Player") && !ChestOpen)
        {
            PlayerInt = other.gameObject.GetComponent<Interaction>();
            PlayerInt.EnableAUI(false, "");
            inTerritory = false;
        }

    }
}


