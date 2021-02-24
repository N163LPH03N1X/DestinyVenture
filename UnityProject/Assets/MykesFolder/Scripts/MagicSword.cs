using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSword : MonoBehaviour
{
    
    public AudioClip fire;
    public Camera CameraPosition;
    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    public float Bullet_Forward_Force;

    PowerCounter powCount;
    public ParticleSystem ParticleSys;
    public GameObject BladeParticle;
    public float PartEmis;
    public GameObject Player;
    public int Count;
    Animator anim;
    AudioSource audioSrc;
    public AudioClip swordSwipe;
    public MeshCollider cc;
    bool PlaySound;
    bool Paused;
    bool WeakAttack;
    bool StrongAttack;
    public static bool Attacked;
    bool isGrounded;
    public bool isJumping;
  


    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        powCount = Player.GetComponent<PowerCounter>();
        ParticleSys = BladeParticle.GetComponent<ParticleSystem>();
        cc.enabled = false;
    }
    void Update()
    {
        Paused = PauseMenu.Paused;
        Count = PowerCounter.magicCount;
        isGrounded = PlayerController.isGrounded;
        isJumping = PlayerController.isJumping;

        var emission = ParticleSys.emission;
        emission.rateOverTime = PartEmis;

        if (Count == 0)
        {
            PartEmis = 0f;
        }
        else
        {
            if (Count == 1)
            {
                PartEmis = 50f;
            }
            if (Count == 2)
            {
                PartEmis = 100f;
            }
            if (Count == 3)
            {
                PartEmis = 200f;
            }
        }
       
        if (Input.GetMouseButtonDown(0) && !Paused && !Attacked && !isJumping || Input.GetAxisRaw("Attack") == -1 && !Paused && !Attacked && !isJumping)
        {
            Attacked = true;
            StrongAttack = false;
            WeakAttack = true;
            anim.SetTrigger("Attack");
            audioSrc.PlayOneShot(swordSwipe);
        }
        if (Input.GetMouseButtonDown(0) && !Paused && !Attacked && !isJumping || Input.GetAxisRaw("Attack") == 1 && !Paused && !Attacked && !isJumping)
        {
            WeakAttack = false;
            StrongAttack = true;
  
            if (Count == 1)
            {
                StartCoroutine(PowerAttack());
            }
            else if (Count == 2)
            {
                StartCoroutine(PowerAttack());
            }
            else if (Count == 3)
            {
                StartCoroutine(PowerAttack());
            }

        }
    }
    public void ActivateCollider(int active)
    {
        if (active == 0)
        {
            PlaySound = false;
            cc.enabled = false;
        }
        else
        {
            cc.enabled = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ISwordHittable>() != null)

        {
            cc.enabled = false;
            if (WeakAttack)
            {
                other.gameObject.SendMessage("OnGetHitBySword", 2);
            }
            else if (StrongAttack)
            {
                other.gameObject.SendMessage("OnGetHitBySword", 3);
            }
            

        }
    }
    IEnumerator PowerAttack()
    {
        Attacked = true;
        anim.SetTrigger("StrongAttack");
        audioSrc.PlayOneShot(fire);
        powCount.RemoveMagicCounter(1);
        Rigidbody Temporary_RigidBody;
        GameObject Temporary_Bullet_Handler;
        Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;
        Temporary_Bullet_Handler.transform.Rotate(Vector3.left);
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
        Temporary_RigidBody.AddForce(CameraPosition.transform.forward * Bullet_Forward_Force);
        yield return new WaitForSeconds(3);
       
    }
    public void ResetAttacked()
    {
        Attacked = false;
    }
    public void AlertObservers(string message)
    {
        if (message.Equals("AttackAnimationStarted"))
        {
            Attacked = true;

        }
        if (message.Equals("AttackAnimationEnded"))
        {
            Attacked = false;

        }
    }
    public void JumpAnimation(bool True)
    {
        if (True)
        {
            anim.SetTrigger("Jump");
            True = false;
        }
    }
}