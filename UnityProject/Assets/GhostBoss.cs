using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GhostBoss : MonoBehaviour, ISwordHittable
{
    GameObject BossBanner;
    private Image healthFillAmount;
    private Image shieldFillAmount;
    Image firePic;
    Image phantPic;
    Image icePic;

    public bool Fire;
    public bool Phant;
    public bool Ice;
    public bool Dummy;

    public int Randomize;
    NavMeshAgent Nav;
    Rigidbody rb;
    public float AwarenessRange;
    public float MeleeRange;
    public float ProjectileRange;

    SceneLoader music;

    public Animator anim;
    AudioSource audioSrc;
    public AudioClip damage;
    public AudioClip death;
    public AudioClip shoot;
    public AudioClip powerShoot;
    public AudioClip charge;
    public AudioClip teleport;
    public AudioClip frozen;

    GameObject Projectile;
    public GameObject FireCrystal;
    public GameObject FireBall;
    public GameObject PhantCrystal;
    public GameObject PhantBall;
    public GameObject IceCrystal;
    public GameObject IceBall;
    public GameObject Emitter;
    public GameObject EmitterCenter;
    public GameObject LeftCrystal;
    public GameObject RightCrystal;
    public GameObject TeleportParticle;
    float nextFire;
    public float fireRate;
    public float projectileForce;

    PlayerStats playST;
    BoxCollider Collider;
    int healthMax;
    int hitMax;
    public SkinnedMeshRenderer[] meshRends;
    SkinnedMeshRenderer meshRend;
    MeshRenderer maskRend;
    Color originalColor;
    Color maskOrgColor;
    public GameObject Body;
    public GameObject EnemyBody;
    public GameObject GhostDummy;
    public GameObject Mask;
    GameObject Player;
    Vector3 PlayerPosition;
    Vector3 OtherBossPosition;
    bool flash;
    float flashTime = 0.1f;
    float flashTimer;
    public bool Dying;
    float DeathTimer;
    public int health;
    public GameObject CrystalPrefab;
    public GameObject itemDrop;
    public int setHitCounter;
    int hitCounter;
    bool ShootLeftCrystal;
    bool ShootRightCrystal;

    public Transform[] Position;


    bool Frozen;
    public float unFreezeTime;
    bool returnColor;

   
    public GameObject IceShield;
    public bool ShieldUp;
    public int shieldCounter;
    float shieldTimer;

    bool WeakAttack;
    bool StrongAttack;
    bool MagicSwordEnabled;

    bool damageDummy;
    bool Down;
    bool PhantomBreastEnabled;

    public void Start()
    {
       
        if (Ice)
        {
            IceShield.SetActive(true);
            ShieldUp = true;
        }
        if (Dummy)
        {
            Mask.SetActive(true);
            IceShield.SetActive(false);
        }
    
        else if (!Ice && !Dummy)
        {

            shieldCounter = 0;
            Mask.SetActive(false);
            IceShield.SetActive(false);
            hitCounter = setHitCounter;
        }
       
        maskRend = Mask.GetComponent<MeshRenderer>();
       
        rb = GetComponent<Rigidbody>();
        Nav = GetComponent<NavMeshAgent>();
        audioSrc = GetComponent<AudioSource>();
        Collider = GetComponent<BoxCollider>();
        meshRend = Body.GetComponent<SkinnedMeshRenderer>();
        maskOrgColor = maskRend.material.GetColor("_EmissionColor");
        originalColor = meshRend.material.color;
        flashTimer = flashTime;
        anim.SetBool("Idle", true);
        healthMax = health;
        hitMax = shieldCounter;
    }
    public void Update()
    {
        FindClosestTarget("GhostBoss");
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerPosition = Player.transform.position;
        if (!Dummy)
        {
            handler();
        }
      
        if (Dying && !Dummy)
        {
            ActivateBossBanner(false);
        }
        //========================================================MovingRange========================================//
        if (ShieldUp && Ice)
        {
            Down = false;
            rb.constraints = RigidbodyConstraints.None;
            transform.LookAt(Camera.main.transform.position);
            IceShield.SetActive(true);
            firePic = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/FaceMask/GhostBossFlame").GetComponent<Image>();
            phantPic = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/FaceMask/GhostBossPhant").GetComponent<Image>();
            icePic = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/FaceMask/GhostBossFrost").GetComponent<Image>();
            firePic.enabled = false;
            phantPic.enabled = false;
            icePic.enabled = true;
            if (Vector3.Magnitude(transform.position - PlayerPosition) < AwarenessRange && Vector3.Magnitude(transform.position - PlayerPosition) >= ProjectileRange &&  !Dying && !Frozen && !Down)
            {
                ActivateBossBanner(true);
                SwitchState(EnemyStates.InRange);
            }

            if (Vector3.Magnitude(transform.position - PlayerPosition) >= AwarenessRange && !Dying && !Frozen && !Down)
            {
                SwitchState(EnemyStates.OutRange);
            }
            if (Vector3.Magnitude(transform.position - PlayerPosition) < MeleeRange && !Dying && !Frozen && !Down)
            {
                SwitchState(EnemyStates.CloseRange);
            }
            if (Vector3.Magnitude(transform.position - PlayerPosition) >= MeleeRange && Vector3.Magnitude(transform.position - PlayerPosition) < ProjectileRange && !Dying && !Frozen && !Down)
            {
                SwitchState(EnemyStates.LongRange);
            }

        }
        else if (!ShieldUp && Ice)
        {
            IceShield.SetActive(false);
            SwitchState(EnemyStates.Freeze);
        }
        else if (Phant || Dummy)
        {
          
            rb.constraints = RigidbodyConstraints.None;
            transform.LookAt(Camera.main.transform.position);
            
            if (Vector3.Magnitude(transform.position - PlayerPosition) < AwarenessRange && Vector3.Magnitude(transform.position - PlayerPosition) >= ProjectileRange && !Dying && !Frozen)
            {
                if (Phant && !Dummy)
                {
                    ActivateBossBanner(true);
                    firePic = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/FaceMask/GhostBossFlame").GetComponent<Image>();
                    phantPic = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/FaceMask/GhostBossPhant").GetComponent<Image>();
                    icePic = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/FaceMask/GhostBossFrost").GetComponent<Image>();
                    firePic.enabled = false;
                    phantPic.enabled = true;
                    icePic.enabled = false;
                }
                SwitchState(EnemyStates.InRange);
            }

            if (Vector3.Magnitude(transform.position - PlayerPosition) >= AwarenessRange && !Dying && !Frozen)
            {
                SwitchState(EnemyStates.OutRange);
            }
          
            if (Vector3.Magnitude(transform.position - PlayerPosition) < MeleeRange && !Dying && !Frozen)
            {
                SwitchState(EnemyStates.CloseRange);
            }
            if (Vector3.Magnitude(transform.position - PlayerPosition) >= MeleeRange && Vector3.Magnitude(transform.position - PlayerPosition) < ProjectileRange && !Dying && !Frozen)
            {
                SwitchState(EnemyStates.LongRange);
            }
        }
        else
        {
            
            rb.constraints = RigidbodyConstraints.None;
            transform.LookAt(Camera.main.transform.position);
          
            if (Vector3.Magnitude(transform.position - PlayerPosition) < AwarenessRange && Vector3.Magnitude(transform.position - PlayerPosition) >= ProjectileRange && !Dying && !Frozen && !Down)
            {
                if (Fire)
                {
                    ActivateBossBanner(true);
                    firePic = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/FaceMask/GhostBossFlame").GetComponent<Image>();
                    phantPic = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/FaceMask/GhostBossPhant").GetComponent<Image>();
                    icePic = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/FaceMask/GhostBossFrost").GetComponent<Image>();
                    firePic.enabled = true;
                    phantPic.enabled = false;
                    icePic.enabled = false;
                }
               
                SwitchState(EnemyStates.InRange);
            }

            if (Vector3.Magnitude(transform.position - PlayerPosition) >= AwarenessRange && !Dying && !Frozen && !Down)
            {
                SwitchState(EnemyStates.OutRange);
            }
            if (Vector3.Magnitude(transform.position - PlayerPosition) < MeleeRange && !Dying && !Frozen && !Down)
            {
                SwitchState(EnemyStates.CloseRange);
            }
            if (Vector3.Magnitude(transform.position - PlayerPosition) >= MeleeRange && Vector3.Magnitude(transform.position - PlayerPosition) < ProjectileRange && !Dying && !Frozen && !Down)
            {
                SwitchState(EnemyStates.LongRange);
            }
        }
        //=================================================FreezeBossFrozen==================================//
        if (unFreezeTime > 0)
        {
            Frozen = true;
            SwitchState(EnemyStates.Freeze);
            unFreezeTime -= Time.deltaTime;
        }
        if (unFreezeTime < 0)
        {
            unFreezeTime = 0;
            SwitchState(EnemyStates.InRange);
            returnColor = true;
            Frozen = false;
        }
        if (Frozen && !flash)
        {
            if (Ice)
            {
                meshRend.material.color = Color.white;
            }
            else
            {
                meshRend.material.color = Color.blue;
            }
        }
        else if (Frozen && flash)
        {
            meshRend.material.color = Color.red;
        }
        if (returnColor)
        {
            meshRend.material.color = originalColor;
            returnColor = false;
        }
        //=========================================================FadeDeath=================================//
        if (DeathTimer > 0)
        {
            DeathTimer -= Time.deltaTime;
        }
        if (DeathTimer < 0)
        {
            StartCoroutine(Fading(true));
            music = Player.GetComponent<SceneLoader>();
            if (Fire)
            {
                music.SwitchMusic(SceneLoader.Music.FireTemple);
            }
            else if (Phant)
            {
                music.SwitchMusic(SceneLoader.Music.PhantTemple);
            }
            else if (Ice)
            {
                music.SwitchMusic(SceneLoader.Music.IceTemple);
            }
           
            DeathTimer = 0;
        }
        //======================================================DamagingBoss==================================//
        if (flash)
        {
            flashTimer -= Time.deltaTime;
            if (hitCounter > 0 && Dummy)
            {
                maskRend.material.SetColor("_EmissionColor", Color.red);
            }
            else
            {
                meshRend.material.color = Color.red;
            }
        }
        if (flashTimer < 0)
        {
            meshRend.material.color = originalColor;
            maskRend.material.SetColor("_EmissionColor", maskOrgColor);
            flash = false;
            flashTimer = flashTime;
        }
        //=============================================ICEBossShield==============================================//
        if (Ice)
        {
            if (shieldTimer > 0 && !Dying)
            {
                ShieldUp = false;
                shieldTimer -= Time.deltaTime;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            if (shieldTimer < 0)
            {
                anim.Rebind();
                shieldCounter = hitMax;
                ShieldUp = true;
                SwitchState(EnemyStates.Teleport);
                shieldTimer = 0;
            }
        }
    }
    public void ActivateBossBanner(bool True)
    {
        if (True)
        {
            BossBanner = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner");
            BossBanner.SetActive(true);
            Text text = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/BossName").GetComponent<Text>();
            if (Ice)
            {
                text.text = "Cryo, The Elemental Frost Ghost";
            }
            else if (Phant)
            {
                text.text = "Necros , The Elemental Phantom Ghost";
            }
            else
            {
                text.text = "Pyroc, The Elemental Flame Ghost";
            }
           
        }
        else
        {
            BossBanner = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner");
            BossBanner.SetActive(false);
        }
    }
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    void handler()
    {
        healthFillAmount = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/HealthMask/Health").GetComponent<Image>();
        shieldFillAmount = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/ShieldMask/Shield").GetComponent<Image>();
        healthFillAmount.fillAmount = Map(health, 0, healthMax, 0, 1);
        shieldFillAmount.fillAmount = Map(shieldCounter, 0, hitMax, 0, 1);

    }
    GameObject FindClosestTarget(string trgt)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(trgt);

        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;
    }
    public void OnGetHitBySword(int hitValue)
    {
        MagicSwordEnabled = PowerCounter.MagicSword;
        if (Ice)
        {
            if (!ShieldUp)
            {
                WeakAttack = AnimatedSword.WeakAttack;
                StrongAttack = AnimatedSword.StrongAttack;
                if (MagicSwordEnabled)
                {
                    if (WeakAttack)
                    {
                        DoDamage(20);
                    }
                    else if (StrongAttack)
                    {
                        DoDamage(30);
                    }
                }
                else
                {
                    DoDamage(hitValue);
                }
            }
        }
        else if (Dummy)
        {
            if (damageDummy)
            {
                DoDamage(hitValue);
            }
        }
        else
        {
            DoDamage(hitValue);
        }
    }
    public void HitShieldCounter(int amount)
    {
        shieldCounter -= amount;
        if (shieldCounter <= 0)
        {
            shieldCounter = 0;
            ShieldUp = false;
            shieldTimer = 5;
        }
    }
    public void DoDamage(int amount)
    {
        audioSrc.PlayOneShot(damage);
        health -= amount;
        flash = true;
        if (health <= 0)
        {
            if (!Dummy)
            {
                playST = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
                ActivateBossBanner(false);
                PhantomBreastEnabled = Breast.PhantomBreast;
                if (PhantomBreastEnabled)
                {
                    playST.gainExp(2500 * 2);
                }
                else
                {
                    playST.gainExp(2500);
                }
            }
          
            anim.SetTrigger("Death");
            Dying = true;
            DeathTimer = 1f;
            audioSrc.volume = Random.Range(0.8f, 1.0f);
            audioSrc.pitch = Random.Range(0.8f, 1.0f);
            audioSrc.PlayOneShot(death);
            health = 0;
            Collider.enabled = false;
        }
    }
    public void HitDamage(int amount)
    {
        audioSrc.PlayOneShot(damage);
        hitCounter -= amount;
        flash = true;
        if (hitCounter <= 0)
        {
            if (Dummy)
            {
                Mask.SetActive(false);
                damageDummy = true;
            }
            else
            {
                SwitchState(EnemyStates.Teleport);
            }
            hitCounter = setHitCounter;
           
        }
    }
    IEnumerator Fading(bool On)
    {
        for (float i = 0.20f; i > 0; i -= 0.01f)
        {
            if (On)
            {
                MeshRendTurnOn(true);
                yield return new WaitForSeconds(i);
                On = false;
            }
            else
            {
                MeshRendTurnOn(false);
                yield return new WaitForSeconds(i);
                On = true;
            }
        }
        if (Dummy)
        {
            Destroy(gameObject);
        }
        else
        {
            DropCrystal();
            Destroy(gameObject);
        }
       
    }
    void MeshRendTurnOn(bool On)
    {
        if (On)
        {
            for (int i = 0; i < meshRends.Length; i++)
            {
                meshRends[i].enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < meshRends.Length; i++)
            {
                meshRends[i].enabled = false;
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AwarenessRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MeleeRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ProjectileRange);
   
    }
    public void DropCrystal()
    {
        Instantiate(CrystalPrefab, itemDrop.transform.position, Quaternion.identity);
    }
    public enum EnemyStates{ InRange, OutRange, LongRange, CloseRange, PowerMove, Shoot, Teleport, Freeze}
    public void SwitchState(EnemyStates state)
    {
            //============================If All In Range=================//
        switch (state)
        {

            case EnemyStates.InRange:
                {
                    //=========MoveToPlayer=========//
                    anim.SetBool("Idle", true);
                    Nav.isStopped = false;
                    Nav.speed = 10;
                    rb.velocity = Vector3.zero;
                    Nav.destination = PlayerPosition;
                    break;
                }
            case EnemyStates.OutRange:
                {
                    //=====StopMoving======//
                    anim.SetBool("Idle", false);
                    anim.SetBool("Attack", false);
                    anim.SetBool("Attack3", false);
                    anim.SetBool("Attack2", false);
                    anim.SetBool("Death", false);
                    Nav.isStopped = true;
                    Nav.speed = 0;
                    Nav.destination = transform.position;
                    break;
                }
            case EnemyStates.LongRange:
                {
                    if (!Dummy)
                    {
                        Nav.isStopped = false;
                        Nav.speed = 10;
                        rb.velocity = Vector3.zero;
                        Nav.destination = PlayerPosition;
                    }
                   
                    //==============ShootPlayer===========//
                    for (int a = 0; a < 1; a++)
                    {
                        Randomize = Random.Range(1, 6);
                        if (Randomize == 1 && !Frozen)
                        {
                            SwitchState(EnemyStates.Shoot);
                        }
                        else if (Randomize == 2 && !Frozen)
                        {
                            SwitchState(EnemyStates.Shoot);
                        }
                        else if (Randomize == 3 && !Frozen)
                        {
                            SwitchState(EnemyStates.Shoot);
                        }
                        else if (Randomize == 4 && !Frozen)
                        {
                            SwitchState(EnemyStates.Shoot);
                        }
                        else if (Randomize == 5 && !Frozen)
                        {
                            SwitchState(EnemyStates.PowerMove);
                        }
                    }
                    break;
                }
            case EnemyStates.CloseRange:
                {
                    //==================MeleeAttack===========//
                    Nav.isStopped = true;
                    Nav.speed = 0;
                    Nav.destination = transform.position;
                    anim.SetBool("Attack", false);
                    anim.SetBool("Attack3", true);
                    anim.SetBool("Attack2", false);
                    anim.SetBool("Death", false);

                    break;
                }
            case EnemyStates.Freeze:
                {
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    rb.Sleep();
                    rb.velocity = Vector3.zero;
                    Nav.velocity = Vector3.zero;
                    Down = true;
                    Nav.isStopped = true;
                    Nav.speed = 0;
                    Nav.destination = transform.position;
                    anim.SetBool("Death", true);
                    anim.SetBool("Idle", false);
                    anim.SetBool("Attack", false);
                    anim.SetBool("Attack3", false);
                    anim.SetBool("Attack2", false);
                    break;
                }
            case EnemyStates.Shoot:
                {
                    anim.SetBool("Attack", true);
                    anim.SetBool("Attack3", false);
                    anim.SetBool("Attack2", false);
                    anim.SetBool("Death", false);
                    break;
                }

            case EnemyStates.PowerMove:
                {
                    anim.SetBool("Attack2", true);
                    anim.SetBool("Attack", false);
                    anim.SetBool("Attack3", false);
                    anim.SetBool("Death", false);
                    break;
                }
            case EnemyStates.Teleport:
                {
                    int RandomPos = Random.Range(1, Position.Length);
                    audioSrc.PlayOneShot(teleport);
                    Instantiate(TeleportParticle, transform.position, Quaternion.identity);
                    transform.position = Position[RandomPos].transform.position;
                    break;
                }
        }
    }
    public void PowerMove()
    {

        if (Phant)
        {
            int RandomPos = Random.Range(1, Position.Length);
            Instantiate(GhostDummy, Position[RandomPos].transform.position, Quaternion.identity);
            Instantiate(TeleportParticle, transform.position, Quaternion.identity);
        }
        else
        {
            Rigidbody Temporary_RigidBody;
            GameObject newBullet;
            audioSrc.PlayOneShot(powerShoot);
            if (Fire)
                Projectile = FireBall;
            else if (Dummy)
                Projectile = PhantBall;
            else if (Ice)
                Projectile = IceBall;
            newBullet = Instantiate(Projectile, EmitterCenter.transform.position, Emitter.transform.rotation);
            newBullet.transform.Rotate(0, 90, 0);
            newBullet.transform.LookAt(Camera.main.transform.position);
            Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce * 2);
            Destroy(newBullet, 30.0f);
        }
       
    }
    public void Shoot()
    {
        Rigidbody Temporary_RigidBody;
        GameObject newBullet;
        audioSrc.PlayOneShot(shoot);
        if (Fire)
            Projectile = FireCrystal;
        if (Phant || Dummy)
            Projectile = PhantCrystal;
        if (Ice)
            Projectile = IceCrystal;

        newBullet = Instantiate(Projectile, Emitter.transform.position, Emitter.transform.rotation);
        newBullet.transform.Rotate(0,90,0);
        newBullet.transform.LookAt(Camera.main.transform.position);
        Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
        Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce);
        Destroy(newBullet, 30.0f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (!Ice)
        {
            if (other.gameObject.CompareTag("MagicBlade"))
                HitDamage(1);
            if (other.gameObject.CompareTag("BroadBlade"))
                HitDamage(1);
            if (other.gameObject.CompareTag("ThunderBlade"))
                HitDamage(1);
            if (other.gameObject.CompareTag("IceBlade"))
                HitDamage(1);
        }
        else if (Dummy && !damageDummy)
        {
            if (other.gameObject.CompareTag("MagicBlade"))
                HitDamage(1);
            if (other.gameObject.CompareTag("BroadBlade"))
                HitDamage(1);
            if (other.gameObject.CompareTag("ThunderBlade"))
                HitDamage(1);
            if (other.gameObject.CompareTag("IceBlade"))
                HitDamage(1);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("StormShield") && !Ice)
        {
            audioSrc.PlayOneShot(frozen);
            unFreezeTime += 5.0f;
            anim.SetBool("Death", true);
        }
    }
}

