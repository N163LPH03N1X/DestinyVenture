using UnityEngine;
using System.Collections;

public class MovingDoor : MonoBehaviour
{
    [Header("Door Settings")]
    public float DoorOpenSpeed;
    public float DoorheightMax;
    public float DoorHeightMin;
    public float DoorCloseTimer;
    [Space]
    Animator anim;
    AudioSource audioSrc;
    [Header("Sounds")]
    public AudioClip open;
    public AudioClip close;
    public AudioClip Unlocked;
    public AudioClip locked;
    public AudioClip buttonUISFX;
    Interaction PlayerInt;
    public GameObject DoorObject;
    public bool inTerritory;

    public bool OpenDoor;
    public bool CloseDoor;
    GameObject Player;
    bool PlayerFound;
    float Timer = 0f;
    bool setTimer = true;

    bool FireKey = false;
    bool BossFireKey = false;
    bool ThunderKey = false;
    bool BossThunderKey = false;
    bool IceKey = false;
    bool BossIceKey = false;
    bool CrystalKey = false;
    [Space]
    [Header("Keys Used")]
    public bool FireKeyUsed;
    public bool BossFireKeyUsed;
    public bool ThunderKeyUsed;
    public bool BossThunderKeyUsed;
    public bool IceKeyUsed;
    public bool BossIceKeyUsed;
    public bool CrystalKeyUsed;
    [Space]
    [Header("Door Type")]
    public bool NormalDoor;
    public bool FireKeyDoor = false;
    public bool BossFireDoor = false;
    public bool ThunderKeyDoor = false;
    public bool BossThunderDoor = false;
    public bool IceKeyDoor = false;
    public bool BossIceDoor = false;
    public bool CrystalKeyDoor = false;

    TempleKeys UseKey;
    bool isPaused;
    int CrystalKeys;
    bool doorMoving;
    bool PlayLockedSound = false;
    bool LockFDoor;
    bool LockPDoor;
    bool LockIDoor;
    bool DoorSealed = false;

    void Start()
    {
        CloseDoor = true;
        anim = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
        inTerritory = false;
    }

    void Update()
    {
        FireKey = TempleKeys.FireKey;
        BossFireKey = TempleKeys.BossFireKey;
        ThunderKey = TempleKeys.ThunderKey;
        BossThunderKey = TempleKeys.BossThunderKey;
        IceKey = TempleKeys.IceKey;
        BossIceKey = TempleKeys.BossIceKey;
        CrystalKey = TempleKeys.CrystalKey;
        CrystalKeys = TempleKeys.CrystalKeys;
        isPaused = PauseMenu.Paused;
        LockFDoor = LockDoorTrigger.LockFDoor;
        LockPDoor = LockDoorTrigger.LockPDoor;
        LockIDoor = LockDoorTrigger.LockIDoor;

        OpenDoors();
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else if (Timer < 0)
        {
            OpenDoor = false;
            audioSrc.clip = close;
            audioSrc.Play();
            Timer = 0;
        }
        if(LockFDoor || LockPDoor || LockIDoor )
        {
            if (!DoorSealed)
            {
                OpenDoor = false;
                CloseDoor = false;
                audioSrc.clip = close;
                audioSrc.Play();
                Timer = 0;
                DoorSealed = true;
            }
           
        }

        if (inTerritory && !isPaused && !doorMoving)
        {
           
            if (NormalDoor)
            {
                if (Input.GetKeyDown(KeyCode.LeftControl) && !OpenDoor && !CloseDoor|| Input.GetButtonDown("Open") && !OpenDoor || Input.GetKeyDown(KeyCode.RightControl) && !OpenDoor)
                {
                    Open(Doors.Normal);
                }
            }
            else if (FireKeyDoor)
            {
                if (Input.GetButtonDown("Open") && FireKey && !FireKeyUsed)
                {
                    Open(Doors.Fire);
                }
                else if (!OpenDoor && FireKeyUsed)
                {
                    Player = GameObject.FindGameObjectWithTag("Player");
                    PlayerInt = Player.GetComponent<Interaction>();
                    PlayerInt.EnableAUI(true, "Open");
                    if (Input.GetButtonDown("Open"))
                    {
                        PlayerInt.EnableAUI(false, null);
                        Open(Doors.Normal);
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Open") && !FireKeyUsed && !FireKey)
                    {
                        Open(Doors.Locked);
                    }
                }
            }
            else if (BossFireDoor && !LockFDoor)
            {
                
                if (Input.GetButtonDown("Open") && BossFireKey && !BossFireKeyUsed)
                {
                    Open(Doors.FireBoss);
                }
                else if (BossFireKeyUsed && !OpenDoor)
                {
                    Player = GameObject.FindGameObjectWithTag("Player");
                    PlayerInt = Player.GetComponent<Interaction>();
                    PlayerInt.EnableAUI(true, "Open");
                    if (Input.GetButtonDown("Open"))
                    {
                        PlayerInt.EnableAUI(false, null);
                        Open(Doors.Normal);
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Open") && !BossFireKey && !BossFireKeyUsed)
                    {
                        Open(Doors.Locked);
                    }
                }
            }
            else if (ThunderKeyDoor)
            {
                if (Input.GetButtonDown("Open") && ThunderKey && !ThunderKeyUsed)
                {
                    Open(Doors.Phant);
                }
                else if (ThunderKeyUsed && !OpenDoor)
                {
                    Player = GameObject.FindGameObjectWithTag("Player");
                    PlayerInt = Player.GetComponent<Interaction>();
                    PlayerInt.EnableAUI(true, "Open");
                    if (Input.GetButtonDown("Open"))
                    {
                        PlayerInt.EnableAUI(false, null);
                        Open(Doors.Normal);
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Open") && !ThunderKeyUsed && !ThunderKey)
                    {
                        Open(Doors.Locked);
                    }
                }
            }
            else if (BossThunderDoor && !LockPDoor)
            {

                if (Input.GetButtonDown("Open") && BossThunderKey && !BossThunderKeyUsed)
                {
                    Open(Doors.PhantBoss);
                }
                else if (BossThunderKeyUsed && !OpenDoor)
                {
                    Player = GameObject.FindGameObjectWithTag("Player");
                    PlayerInt = Player.GetComponent<Interaction>();
                    PlayerInt.EnableAUI(true, "Open");
                    if (Input.GetButtonDown("Open"))
                    {
                        PlayerInt.EnableAUI(false, null);
                        Open(Doors.Normal);
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Open") && !BossThunderKey && !BossThunderKeyUsed)
                    {
                        Open(Doors.Locked);
                    }
                }
            }
            else if (IceKeyDoor)
            {

                if (Input.GetButtonDown("Open") && IceKey && !IceKeyUsed)
                {
                    Open(Doors.Ice);
                }
                else if (IceKeyUsed && !OpenDoor)
                {
                    Player = GameObject.FindGameObjectWithTag("Player");
                    PlayerInt = Player.GetComponent<Interaction>();
                    PlayerInt.EnableAUI(true, "Open");
                    if (Input.GetButtonDown("Open"))
                    {
                        PlayerInt.EnableAUI(false, null);
                        Open(Doors.Normal);
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Open") && !IceKeyUsed && !IceKey)
                    {
                        Open(Doors.Locked);
                    }
                }
            }
            else if (BossIceDoor && !LockIDoor)
            {

                if (Input.GetButtonDown("Open") && BossIceKey && !BossIceKeyUsed)
                {
                    Open(Doors.IceBoss);
                }
                else if (BossIceKeyUsed && Input.GetButtonDown("Open") && !OpenDoor)
                {
                    Open(Doors.Normal);
                }
                else
                {
                    if (Input.GetButtonDown("Open") && !BossIceKey && !BossIceKeyUsed)
                    {
                        Open(Doors.Locked);
                    }
                }
            }
            else if (CrystalKeyDoor)
            {
                if (Input.GetButtonDown("Open") && CrystalKey && CrystalKeys == 3 && !CrystalKeyUsed)
                {
                    Open(Doors.Final);
                }
                else if (CrystalKeyUsed && !OpenDoor)
                {
                    Player = GameObject.FindGameObjectWithTag("Player");
                    PlayerInt = Player.GetComponent<Interaction>();
                    PlayerInt.EnableAUI(true, "Open");
                    if (Input.GetButtonDown("Open"))
                    {
                        PlayerInt.EnableAUI(false, null);
                        Open(Doors.Normal);
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Open") && !CrystalKeyUsed && !CrystalKey)
                    {
                        Open(Doors.Locked);
                    }
                }
            }
           
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !OpenDoor)
        {
            PlayerInteraction(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInteraction(false);
        }
    }
    public void OpenDoors()
    {

        if (OpenDoor)
        {
            if (CloseDoor)
            {

                doorMoving = true;
                DoorObject.transform.Translate(Vector3.up * Time.deltaTime * DoorOpenSpeed);
                if (DoorObject.transform.localPosition.y > DoorheightMax)
                {
                    Vector3 newPosition = DoorObject.transform.localPosition;
                    newPosition.y = DoorheightMax;
                    DoorObject.transform.localPosition = newPosition;
                    if (newPosition.y == DoorheightMax)
                    {
                        CloseDoor = false;
                        audioSrc.Stop();
                        Timer = DoorCloseTimer;
                       
                    }
                }
            }
          
        }
        else if (!OpenDoor)
        {
            if (!CloseDoor)
            {
                DoorObject.transform.Translate(Vector3.down * Time.deltaTime * DoorOpenSpeed);
                if (DoorObject.transform.localPosition.y < DoorHeightMin)
                {
                    Vector3 newPosition = DoorObject.transform.localPosition;
                    newPosition.y = DoorHeightMin;
                    DoorObject.transform.localPosition = newPosition;
                    if (newPosition.y == DoorHeightMin)
                    {
                        audioSrc.Stop();
                        CloseDoor = true;
                        doorMoving = false;
                    }
                   
                }
            }
           
        }
    }
    public void PlayerInteraction(bool True)
    {
        if (True)
        {
            audioSrc.clip = buttonUISFX;
            audioSrc.Play();
            Player = GameObject.FindGameObjectWithTag("Player");
            PlayerInt = Player.GetComponent<Interaction>();
            if (!FireKeyUsed && FireKeyDoor)
            {
                PlayerInt.EnableAUI(true, "Locked");
            }
            else if (!BossFireKeyUsed && BossFireDoor)
            {
                PlayerInt.EnableAUI(true, "Locked");
            }
            else if (!ThunderKeyUsed && ThunderKeyDoor)
            {
                PlayerInt.EnableAUI(true, "Locked");
            }
            else if (!BossThunderKeyUsed && BossThunderDoor)
            {
                PlayerInt.EnableAUI(true, "Locked");
            }
            else if (!IceKeyUsed && IceKeyDoor)
            {
                PlayerInt.EnableAUI(true, "Locked");
            }
            else if (!BossIceKeyUsed && BossIceDoor)
            {
                PlayerInt.EnableAUI(true, "Locked");
            }
            else if (!CrystalKeyUsed && CrystalKeyDoor)
            {
                PlayerInt.EnableAUI(true, "Locked");
            }
            else
            {
                if (DoorSealed)
                    PlayerInt.EnableAUI(false, null);
                else
                    PlayerInt.EnableAUI(true, "Open");
            }
            inTerritory = true;
        }
        else if(!True)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            PlayerInt = Player.GetComponent<Interaction>();
            PlayerInt.EnableAUI(false, null);
            inTerritory = false;
        }
    }
    public enum Doors { Ice, IceBoss, Phant, PhantBoss, Fire, FireBoss, Final, Normal, Locked}
    public void Open(Doors type)
    {
        switch (type)
        {
            case Doors.Locked:
                {
                    audioSrc.clip = locked;
                    audioSrc.Play();
                    break;
                }
            case Doors.Normal:
                {
                    Player = GameObject.FindGameObjectWithTag("Player");
                    audioSrc.clip = open;
                    audioSrc.Play();
                    PlayerInt = Player.GetComponent<Interaction>();
                    PlayerInt.EnableAUI(false, null);
                    OpenDoor = true;
                    break;
                }
            case Doors.Fire:
                {
                    audioSrc.clip = Unlocked;
                    audioSrc.Play();
                    Player = GameObject.FindGameObjectWithTag("Player");
                    PlayerInt = Player.GetComponent<Interaction>();
                    PlayerInt.EnableAUI(false, null);
                    UseKey = Player.GetComponent<TempleKeys>();
                    anim.SetTrigger("FireDoorUnlocked");
                    UseKey.UseKey(1);
                    FireKeyUsed = true;
                    break;
                }
            case Doors.FireBoss:
                {
                    Player = GameObject.FindGameObjectWithTag("Player");
                    audioSrc.clip = Unlocked;
                    audioSrc.Play();
                    PlayerInt = Player.GetComponent<Interaction>();
                    PlayerInt.EnableAUI(false, null);
                    UseKey = Player.GetComponent<TempleKeys>();
                    anim.SetTrigger("FireBossDoorUnlocked");
                    UseKey.UseKey(2);
                    BossFireKeyUsed = true;
                    break;
                }
            case Doors.Phant:
                {
                    Player = GameObject.FindGameObjectWithTag("Player");
                    audioSrc.clip = Unlocked;
                    audioSrc.Play();
                    PlayerInt = Player.GetComponent<Interaction>();
                    PlayerInt.EnableAUI(false, null);
                    UseKey = Player.GetComponent<TempleKeys>();
                    anim.SetTrigger("PhantDoorUnlocked");
                    UseKey.UseKey(3);
                    ThunderKeyUsed = true;
                    break;
                }
            case Doors.PhantBoss:
                {
                    Player = GameObject.FindGameObjectWithTag("Player");
                    audioSrc.clip = Unlocked;
                    audioSrc.Play();
                    PlayerInt = Player.GetComponent<Interaction>();
                    PlayerInt.EnableAUI(false, null);
                    UseKey = Player.GetComponent<TempleKeys>();
                    anim.SetTrigger("PhantBossDoorUnlocked");
                    UseKey.UseKey(4);
                    BossThunderKeyUsed = true;
                    break;
                }
            case Doors.Ice:
                {
                    Player = GameObject.FindGameObjectWithTag("Player");
                    audioSrc.clip = Unlocked;
                    audioSrc.Play();
                    PlayerInt = Player.GetComponent<Interaction>();
                    PlayerInt.EnableAUI(false, null);
                    UseKey = Player.GetComponent<TempleKeys>();
                    anim.SetTrigger("IceDoorUnlocked");
                    UseKey.UseKey(5);
                    IceKeyUsed = true;
                    break;
                }
            case Doors.IceBoss:
                {
                    Player = GameObject.FindGameObjectWithTag("Player");
                    audioSrc.clip = Unlocked;
                    audioSrc.Play();
                    PlayerInt = Player.GetComponent<Interaction>();
                    PlayerInt.EnableAUI(false, null);
                    UseKey = Player.GetComponent<TempleKeys>();
                    anim.SetTrigger("IceBossDoorUnlocked");
                    UseKey.UseKey(6);
                    BossIceKeyUsed = true;
                    break;
                }
            case Doors.Final:
                {
                    Player = GameObject.FindGameObjectWithTag("Player");
                    audioSrc.clip = Unlocked;
                    audioSrc.Play();
                    PlayerInt = Player.GetComponent<Interaction>();
                    PlayerInt.EnableAUI(false, null);
                    UseKey = Player.GetComponent<TempleKeys>();
                    anim.SetTrigger("FinalDoorUnlocked");
                    UseKey.UseKey(7);
                    CrystalKeyUsed = true;
                    break;
                }
        }
    }
}

