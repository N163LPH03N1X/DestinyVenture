using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCoryChest : MonoBehaviour {
  
    Animator anim;
    Interaction playInt;
    [Header("Gauntlets")]
    public bool StrengthGauntlets;
    public bool PowerGauntlets;
    public bool GripGauntlets;
    [Space]
    [Header("Boots")]
    public bool SpikeBoots;
    public bool SpeedBoots;
    public bool HealingBoots;
    [Space]
    [Header("Breasts")]
    public bool FlameBreast;
    public bool PhantomBreast;
    public bool FrostBreast;
    [Space]
    [Header("BossKeys")]
    public bool FlameBossKey;
    public bool PhantomBossKey;
    public bool FrostBossKey;
    [Space]
    [Header("Prefabs")]
    public GameObject StrGauntPrefab;
    public GameObject PowGauntPrefab;
    public GameObject GriGauntPrefab;
    [Space]
    public GameObject FrostBreastPrefab;
    public GameObject FlameBreastPrefab;
    public GameObject PhantBreastPrefab;
    [Space]
    public GameObject SpikeBootPrefab;
    public GameObject SpeedBootPrefab;
    public GameObject HealBootPrefab;
    [Space]
    public GameObject BossFlameKeyPrefab;
    public GameObject BossPhantKeyPrefab;
    public GameObject BossGlaceKeyPrefab;
    [Space]
    [Header("Emitter")]
    public GameObject EmitPosition;
    [Space]
    [Header("Sound FX")]
    AudioSource audioSrc;
    public AudioClip open;
    public AudioClip lockSfx;
    public AudioClip interaction;
    public GameObject Map;
    public bool OpenChest;
    public  bool ChestOpened;

    void Start() {
        anim = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
        //ChestOpened = false;
    }
    void Update() {

        
        if (OpenChest)
        {
            if (Input.GetButtonDown("Open"))
            {
                Map.SetActive(false);
                audioSrc.PlayOneShot(lockSfx);
                anim.SetTrigger("Open");
                OpenChest = false;
                playInt = GameObject.FindGameObjectWithTag("Player").GetComponent<Interaction>();
                playInt.EnableAUI(false, "");
                ChestOpened = true;

            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !ChestOpened)
        {
            playInt = other.gameObject.GetComponent<Interaction>();
            playInt.EnableAUI(true, "Open Chest");
            audioSrc.PlayOneShot(interaction);
            OpenChest = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !ChestOpened)
        {
            playInt = other.gameObject.GetComponent<Interaction>();
            playInt.EnableAUI(false, "");
            OpenChest = false;
        }
    }
    public enum Equipment { Strength, Power, Grip, Spike, Speed, Heal, Flame, Phantom, Frost, FlameKey, PhantKey, GlaceKey, }
    public void SwitchEquipment(Equipment equip)
    {
        switch (equip)
        {
            case Equipment.Strength:
                {
                    Instantiate(StrGauntPrefab, EmitPosition.transform.position, Quaternion.identity, transform);
                    break;
                }
            case Equipment.Power:
                {
                    Instantiate(PowGauntPrefab, EmitPosition.transform.position, Quaternion.identity, transform);
                    break;
                }
            case Equipment.Grip:
                {
                    Instantiate(GriGauntPrefab, EmitPosition.transform.position, Quaternion.identity, transform);
                    break;
                }
            case Equipment.Spike:
                {
                    Instantiate(SpikeBootPrefab, EmitPosition.transform.position, Quaternion.identity, transform);
                    break;
                }
            case Equipment.Speed:
                {
                    Instantiate(SpeedBootPrefab, EmitPosition.transform.position, Quaternion.identity, transform);
                    break;
                }
            case Equipment.Heal:
                {
                    Instantiate(HealBootPrefab, EmitPosition.transform.position, Quaternion.identity, transform);
                    break;
                }
            case Equipment.Flame:
                {
                    Instantiate(FlameBreastPrefab, EmitPosition.transform.position, Quaternion.identity, transform);
                    break;
                }
            case Equipment.Phantom:
                {
                    Instantiate(PhantBreastPrefab, EmitPosition.transform.position, Quaternion.identity, transform);
                    break;
                }
            case Equipment.Frost:
                {
                    Instantiate(FrostBreastPrefab, EmitPosition.transform.position, Quaternion.identity, transform);
                    break;
                }
            case Equipment.FlameKey:
                {
                    Instantiate(BossFlameKeyPrefab, EmitPosition.transform.position, Quaternion.identity, transform);
                    break;
                }
            case Equipment.PhantKey:
                {
                    Instantiate(BossPhantKeyPrefab, EmitPosition.transform.position, Quaternion.identity, transform);
                    break;
                }
            case Equipment.GlaceKey:
                {
                    Instantiate(BossGlaceKeyPrefab, EmitPosition.transform.position, Quaternion.identity, transform);
                    break;
                }
        }
    }
    public void InstantiateItem()
    {
        if (StrengthGauntlets)
        {
            SwitchEquipment(Equipment.Strength);
        }
        else if(PowerGauntlets)
        {
            SwitchEquipment(Equipment.Power);
        }
        else if (GripGauntlets)
        {
            SwitchEquipment(Equipment.Grip);
        }
        else if (SpikeBoots)
        {
            SwitchEquipment(Equipment.Spike);
        }
        else if (SpeedBoots)
        {
            SwitchEquipment(Equipment.Speed);
        }
        else if (HealingBoots)
        {
            SwitchEquipment(Equipment.Heal);
        }
        else if (FlameBreast)
        {
            SwitchEquipment(Equipment.Flame);
        }
        else if (PhantomBreast)
        {
            SwitchEquipment(Equipment.Phantom);
        }
        else if (FrostBreast)
        {
            SwitchEquipment(Equipment.Frost);
        }
        else if (FlameBossKey)
        {
            SwitchEquipment(Equipment.FlameKey);
        }
        else if (PhantomBossKey)
        {
            SwitchEquipment(Equipment.PhantKey);
        }
        else if (FrostBossKey)
        {
            SwitchEquipment(Equipment.GlaceKey);
        }
    }
    public void PlayOpenSound()
    {
        audioSrc.PlayOneShot(open);
    }
}

