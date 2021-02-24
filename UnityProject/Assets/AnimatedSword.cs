using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSword : MonoBehaviour {

    Animator anim;

    PowerCounter BroadpowCount;
    PowerCounter MagicpowCount;
    PowerCounter ThunderpowCount;
    PowerCounter IcepowCount;

    AudioSource audioSrc;
    public static bool isAnimating;
    float MagicPartEmis;
    float ThunderPartEmis;
    float IcePartEmis;

    public GameObject Ice_Emitter;
    public GameObject Magic_Thunder_Emitter;

    public GameObject Blade;
    public GameObject Flame;
    public GameObject ElectroBall;
    public GameObject Shield;

    public float Flame_Forward_Force;
    public float Electroball_Forward_Force;

    public int BroadCount;
    public int MagicCount;
    public int ThunderCount;
    public int IceCount;

    public AudioClip BroadSwipe;
    public AudioClip MagicSwipe;
    public AudioClip ThunderSwipe;
    public AudioClip IceSwipe;

    public AudioClip MagicFlame;
    public AudioClip ThunderBall;
    public AudioClip IceShield;

    ParticleSystem MagicParticleSys;
    ParticleSystem ThunderParticleSys;
    ParticleSystem IceParticleSys;

    public GameObject MagicBladeParticle;
    public GameObject ThunderBladeParticle;
    public GameObject IceBladeParticle;

    public MeshCollider BroadBladeColider;
    public MeshCollider MagicBladeColider;
    public MeshCollider ThunderBladeColider;
    public MeshCollider IceBladeColider;

    public static bool BroadSwordEnabled;
    public static bool MagicSwordEnabled;
    public static bool ThunderSwordEnabled;
    public static bool IceSwordEnabled;

    public GameObject Player;
    public Camera CameraPosition;

    public static bool isAttacking;
    public static bool WeakAttack;
    public static bool StrongAttack;
    public bool WeakAttacked;
    public bool StrongAttacked;

    bool StrengthGauntletsEnabled;
    PlayerController PlayCtrl;
    public bool isGrounded;
    public bool isJumping;
    public bool isRunning;
    public bool isPaused;
    public bool isMoving;
    bool isDisabled;
    bool isDrowning;
    public bool hasAttacked;
    void Start ()
    {
        audioSrc = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        BroadpowCount = Player.GetComponent<PowerCounter>();
        MagicpowCount = Player.GetComponent<PowerCounter>();
        ThunderpowCount = Player.GetComponent<PowerCounter>();
        IcepowCount = Player.GetComponent<PowerCounter>();
        MagicParticleSys = MagicBladeParticle.GetComponent<ParticleSystem>();
        ThunderParticleSys = ThunderBladeParticle.GetComponent<ParticleSystem>();
        IceParticleSys = IceBladeParticle.GetComponent<ParticleSystem>();
        BroadBladeColider.enabled = false;
        MagicBladeColider.enabled = false;
        ThunderBladeColider.enabled = false;
        IceBladeColider.enabled = false;
        SwordEquip(EquipSword.BroadSword);
    }
    void Update()
    {
        //==================================BoolCollection=========================//
        isPaused = PauseMenu.Paused;
        BroadCount = PowerCounter.broadCount;
        MagicCount = PowerCounter.magicCount;
        ThunderCount = PowerCounter.thunderCount;
        IceCount = PowerCounter.iceCount;
        isGrounded = PlayerController.isGrounded;
        isJumping = PlayerController.isJumping;
        isRunning = PlayerController.isRunning;
        isDisabled = SceneLoader.isDisabled;
        hasAttacked = isAttacking;
        //=========================Particle Emissions=============================//
        if (MagicSwordEnabled)
        {
            var emission = MagicParticleSys.emission;
            emission.rateOverTime = MagicPartEmis;

            if (MagicCount == 0)
            {
                MagicPartEmis = 0f;
            }
            else
            {
                if (MagicCount == 1)
                {
                    MagicPartEmis = 500f;
                }
                if (MagicCount == 2)
                {
                    MagicPartEmis = 1000f;
                }
                if (MagicCount == 3)
                {
                    MagicPartEmis = 1500f;
                }
                if (MagicCount == 4)
                {
                    MagicPartEmis = 2000f;
                }
            }
        }
        if (ThunderSwordEnabled)
        {
            var emission = ThunderParticleSys.emission;
            emission.rateOverTime = ThunderPartEmis;

            if (ThunderCount == 0)
            {
                ThunderPartEmis = 0f;
            }
            else
            {
                if (ThunderCount == 1)
                {
                    ThunderPartEmis = 50f;
                }
                if (ThunderCount == 2)
                {
                    ThunderPartEmis = 100f;
                }
                if (ThunderCount == 3)
                {
                    ThunderPartEmis = 200f;
                }
                if (ThunderCount == 4)
                {
                    ThunderPartEmis = 400f;
                }
            }
        }
        if (IceSwordEnabled)
        {
            var emission = IceParticleSys.emission;
            emission.rateOverTime = IcePartEmis;

            if (IceCount == 0)
            {
                IcePartEmis = 0f;
            }
            else
            {
                if (IceCount == 1)
                {
                    IcePartEmis = 2000f;
                }
                if (IceCount == 2)
                {
                    IcePartEmis = 3000f;
                }
                if (IceCount == 3)
                {
                    IcePartEmis = 4000f;
                }
                if (IceCount == 4)
                {
                    IcePartEmis = 5000f;
                }
            }
        }
        if (WeakAttacked)
        {
            SelectAnimation(PlayerAnimation.WeakAttack, true);
        }
        else
        {
            SelectAnimation(PlayerAnimation.WeakAttack, false);
        }
        if (StrongAttacked)
        {
            SelectAnimation(PlayerAnimation.StrongAttack, true);
        }
        else
        {
            SelectAnimation(PlayerAnimation.StrongAttack, false);
        }

        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            isAttacking = false;
        }
        //=========================WeakAttack=====================================//
        if (Input.GetMouseButtonDown(0) && !isPaused && !isJumping && !WeakAttacked && !isAttacking || Input.GetAxisRaw("Attack") == -1 && !isPaused && !isJumping && !WeakAttacked && !isAttacking && !isDisabled)
        {
            isAttacking = true;
            WeakAttack = true;
            WeakAttacked = true;
            StrongAttacked = false;
        }
        else
        {
            WeakAttacked = false;
        }

        if (Input.GetMouseButtonDown(1) && !isPaused && !isJumping && !StrongAttacked && !isAttacking || Input.GetAxisRaw("Attack") == 1 && !isPaused && !isJumping && !StrongAttacked && !isAttacking && !isDisabled)
        {
            StrongAttack = true;
            isAttacking = true;
            StrongAttacked = true;
            WeakAttacked = false;
        }
        else
        {
            StrongAttacked = false;

        }
    }
    public void ActivateCollider(int active)
    {
        if (active == 0)
        {
            if (BroadSwordEnabled)
            {
                BroadBladeColider.enabled = false;
            }
            if (MagicSwordEnabled)
            {
                MagicBladeColider.enabled = false;
            }
            if (ThunderSwordEnabled)
            {
                ThunderBladeColider.enabled = false;
            }
            if (IceSwordEnabled)
            {
                IceBladeColider.enabled = false;
            }

        }
        else
        {
            if (BroadSwordEnabled)
            {
                BroadBladeColider.enabled = true;
            }
            if (MagicSwordEnabled)
            {
                MagicBladeColider.enabled = true;
            }
            if (ThunderSwordEnabled)
            {
                ThunderBladeColider.enabled = true;
            }
            if (IceSwordEnabled)
            {
                IceBladeColider.enabled = true;
            }

        }
    }
    public void AlertObservers(string message)
    {
        if (message.Equals("AttackAnimationStarted"))
        {
            isAttacking = true;

        }
        else if (message.Equals("AttackAnimationEnded"))
        {
            isAttacking = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ISwordHittable>() != null)

        {
            if (BroadSwordEnabled)
            {
                
                if (WeakAttack)
                {
                    StrengthGauntletsEnabled = Gauntlets.StrengthGauntlets;
                    if (StrengthGauntletsEnabled)
                    {
                        other.gameObject.SendMessage("OnGetHitBySword", 2);
                    }
                    else
                    {
                        other.gameObject.SendMessage("OnGetHitBySword", 1);
                    }
                }
                else if (StrongAttack)
                {
                    StrengthGauntletsEnabled = Gauntlets.StrengthGauntlets;
                    if (StrengthGauntletsEnabled)
                    {
                        other.gameObject.SendMessage("OnGetHitBySword", 3);
                    }
                    else
                    {
                        other.gameObject.SendMessage("OnGetHitBySword", 2);
                    }
                }
            }
            if (MagicSwordEnabled)
            {
                if (WeakAttack)
                {
                    StrengthGauntletsEnabled = Gauntlets.StrengthGauntlets;
                    if (StrengthGauntletsEnabled)
                    {
                        other.gameObject.SendMessage("OnGetHitBySword", 3);
                    }
                    else
                    {
                        other.gameObject.SendMessage("OnGetHitBySword", 2);
                    }
                }
                else if (StrongAttack)
                {
                    StrengthGauntletsEnabled = Gauntlets.StrengthGauntlets;
                    if (StrengthGauntletsEnabled)
                    {
                        other.gameObject.SendMessage("OnGetHitBySword", 4);
                    }
                    else
                    {
                        other.gameObject.SendMessage("OnGetHitBySword", 3);
                    }
                }
            }
            if (ThunderSwordEnabled)
            {
                if (WeakAttack)
                {
                    StrengthGauntletsEnabled = Gauntlets.StrengthGauntlets;
                    if (StrengthGauntletsEnabled)
                    {
                        other.gameObject.SendMessage("OnGetHitBySword", 4);
                    }
                    else
                    {
                        other.gameObject.SendMessage("OnGetHitBySword", 3);
                    }
                }
                else if (StrongAttack)
                {
                    StrengthGauntletsEnabled = Gauntlets.StrengthGauntlets;
                    if (StrengthGauntletsEnabled)
                    {
                        other.gameObject.SendMessage("OnGetHitBySword", 5);
                    }
                    else
                    {
                        other.gameObject.SendMessage("OnGetHitBySword", 4);
                    }
                }
            }
            if (IceSwordEnabled)
            {
                if (WeakAttack)
                {
                    StrengthGauntletsEnabled = Gauntlets.StrengthGauntlets;
                    if (StrengthGauntletsEnabled)
                    {
                        other.gameObject.SendMessage("OnGetHitBySword", 5);
                    }
                    else
                    {
                        other.gameObject.SendMessage("OnGetHitBySword", 4);
                    }
                }
                else if (StrongAttack)
                {
                    StrengthGauntletsEnabled = Gauntlets.StrengthGauntlets;
                    if (StrengthGauntletsEnabled)
                    {
                        other.gameObject.SendMessage("OnGetHitBySword", 6);
                    }
                    else
                    {
                        other.gameObject.SendMessage("OnGetHitBySword", 5);
                    }
                }
            }

        }
    }
    public enum SwordType { Broad, Flame, Thunder, Glace }
    public enum EquipSword { BroadSword, FlameSword, ThunderSword, GlacialSword}
    public enum PlayerAnimation { WeakAttack, StrongAttack, Jump, Rebind, Fall}
    public void SwordPowers(SwordType type)
    {
        switch (type)
        {
            case SwordType.Broad:
                {
                    if (BroadSwordEnabled && BroadCount > 0)
                    {
                        audioSrc.PlayOneShot(BroadSwipe);
                        BroadpowCount.RemoveBroadCounter(1);
                        Rigidbody Temporary_RigidBody;
                        GameObject Temporary_Bullet_Handler;
                        Temporary_Bullet_Handler = Instantiate(Blade, Magic_Thunder_Emitter.transform.position, Magic_Thunder_Emitter.transform.rotation) as GameObject;
                        Temporary_Bullet_Handler.transform.Rotate(30, 180, 0);
                        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
                        Temporary_RigidBody.AddForce(CameraPosition.transform.forward * Flame_Forward_Force * 2);
                        Destroy(Temporary_Bullet_Handler, 0.2f);
                    }
                    break;
                }
            case SwordType.Flame:
                {
                    if (MagicSwordEnabled && MagicCount > 0)
                    {
                        audioSrc.PlayOneShot(MagicFlame);
                        MagicpowCount.RemoveMagicCounter(1);
                        Rigidbody Temporary_RigidBody;
                        GameObject Temporary_Bullet_Handler;
                        Temporary_Bullet_Handler = Instantiate(Flame, Magic_Thunder_Emitter.transform.position, Magic_Thunder_Emitter.transform.rotation) as GameObject;
                        Temporary_Bullet_Handler.transform.Rotate(0, 180, 0);
                        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
                        Temporary_RigidBody.AddForce(CameraPosition.transform.forward * Flame_Forward_Force);
                    }
                    break;
                }
            case SwordType.Thunder:
                {
                    if (ThunderSwordEnabled && ThunderCount > 0)
                    {
                        audioSrc.PlayOneShot(ThunderBall);
                        ThunderpowCount.RemoveThunderCounter(1);
                        Rigidbody Temporary_RigidBody;
                        GameObject Temporary_Bullet_Handler;
                        Temporary_Bullet_Handler = Instantiate(ElectroBall, Magic_Thunder_Emitter.transform.position, Magic_Thunder_Emitter.transform.rotation) as GameObject;
                        Temporary_Bullet_Handler.transform.Rotate(Vector3.left);
                        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
                        Temporary_RigidBody.AddForce(CameraPosition.transform.forward * Electroball_Forward_Force);
                    }
                    break;
                }
            case SwordType.Glace:
                {
                    if (IceSwordEnabled && IceCount > 0)
                    {
                        audioSrc.volume = 1.0f;
                        audioSrc.pitch = Random.Range(0.8f, 1.0f);
                        audioSrc.PlayOneShot(IceShield);
                        IcepowCount.RemoveIceCounter(1);
                        Rigidbody Temporary_RigidBody;
                        GameObject Temporary_Bullet_Handler;
                        Temporary_Bullet_Handler = Instantiate(Shield, Ice_Emitter.transform.position, Ice_Emitter.transform.rotation) as GameObject;
                        Temporary_Bullet_Handler.transform.Rotate(Vector3.left);
                        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
                        Temporary_RigidBody.AddForce(CameraPosition.transform.forward * Flame_Forward_Force);
                    }
                    break;
                }
        }
    }
    public void SwordEquip(EquipSword equip)
    {
        switch (equip)
        {
            case EquipSword.BroadSword:
                {
                    BroadSwordEnabled = true;
                    MagicSwordEnabled = false;
                    ThunderSwordEnabled = false;
                    IceSwordEnabled = false;
                    break;
                }
            case EquipSword.FlameSword:
                {
                    BroadSwordEnabled = false;
                    MagicSwordEnabled = true;
                    ThunderSwordEnabled = false;
                    IceSwordEnabled = false;
                    break;
                }
            case EquipSword.ThunderSword:
                {
                    BroadSwordEnabled = false;
                    MagicSwordEnabled = false;
                    ThunderSwordEnabled = true;
                    IceSwordEnabled = false;
                    break;
                }
            case EquipSword.GlacialSword:
                {
                    BroadSwordEnabled = false;
                    MagicSwordEnabled = false;
                    ThunderSwordEnabled = false;
                    IceSwordEnabled = true;
                    break;
                }
        }
    }
    public void SelectAnimation(PlayerAnimation animation, bool True)
    {
        if (True)
        {
            switch (animation)
            {
                case PlayerAnimation.WeakAttack:
                    {
                        anim.SetBool("WeakAttack", true);
                        break;
                    }
                case PlayerAnimation.StrongAttack:
                    {
                        anim.SetBool("StrongAttack", true);
                        break;
                    }
                case PlayerAnimation.Jump:
                    {
                        isDrowning = WaterDamage.isDrowning;
                        if (!isDrowning)
                        {
                            anim.SetBool("Jump", true);
                        }
                        
                        break;
                    }
                case PlayerAnimation.Fall:
                    { 
                        anim.SetBool("Falling", true);
                        break;
                    }
                case PlayerAnimation.Rebind:
                    {
                        anim.Rebind();
                        break;
                    }
            }
        }
        else
        {
            switch (animation)
            {
                case PlayerAnimation.WeakAttack:
                    {
                        anim.SetBool("WeakAttack", false);
                        break;
                    }
                case PlayerAnimation.StrongAttack:
                    {
                        anim.SetBool("StrongAttack", false);
                        break;
                    }
                case PlayerAnimation.Jump:
                    {
                        anim.SetBool("Jump", false);
                        break;
                    }
                case PlayerAnimation.Fall:
                    {
                        anim.SetBool("Falling", false);
                        break;
                    }

            }
        }
        
    }
    public void PlaySound()
    {
        audioSrc.volume = 1f;
        audioSrc.pitch = Random.Range(0.8f, 1.2f);
        if (BroadSwordEnabled)
        {
            audioSrc.PlayOneShot(BroadSwipe);
        }
        if (MagicSwordEnabled)
        {
            audioSrc.PlayOneShot(MagicSwipe);
        }
        if (ThunderSwordEnabled)
        {
            audioSrc.PlayOneShot(ThunderSwipe);
        }
        if (IceSwordEnabled)
        {
            audioSrc.PlayOneShot(IceSwipe);
        }
    }
    public void ResetAttacked()
    {
        isAttacking = false;
    }

    public void Animating(int num)
    {
        if (num == 1)
            isAnimating = true;
        else
            isAnimating = false;
    }
}
