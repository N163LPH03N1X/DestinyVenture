using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class FinalBoss : MonoBehaviour, ISwordHittable
{
    GameObject BossBanner;
    private Image healthFillAmount;
    private Image shieldFillAmount;
    public Animator anim;
    AudioSource audioSrc;
    public AudioSource audioSrc2;
    NavMeshAgent nav;
    Rigidbody rb;
    GameObject player;
    Vector3 playerPosition;
    GameObject projectile;
    public GameObject BossBody;
    public Material[] elementMaterial;
    SkinnedMeshRenderer meshRend;
    public SkinnedMeshRenderer[] meshRends;
    BoxCollider bossCollider;

    SceneLoader music;
    PlayerStats shakeScreen;

    Image firePic;
    Image phantPic;
    Image icePic;

    public AudioClip hit;
    public AudioClip damage;
    public AudioClip swordhit;
    public AudioClip ting;
    public AudioClip death;
    public AudioClip down;
    public AudioClip encounter;
    public AudioClip swipe;
    public AudioClip teleport;

    public AudioClip norFireShot;
    public AudioClip norThunderShot;
    public AudioClip norIceShot;

    public AudioClip charFireShot;
    public AudioClip charThunderShot;
    public AudioClip charIceShot;

    public GameObject ChargeParticleFlame;
    public GameObject ChargeParticlePhantom;
    public GameObject ChargeParticleFrost;

    public GameObject FlashParticleFlame;
    public GameObject FlashParticlePhantom;
    public GameObject FlashParticleFrost;

    public float Speed;
    public int hitPoints;
    int healthMax;
    int hitMax;
    int resetPoints;
    public int health;
    int orgHealth;
    public float awarenessRange;
    public float meleeRange;
    
    float attackRate;
    float nextAttack;
    public float projectileForce;

    public bool isWalking;

    bool flameState;
    bool phantState;
    bool frostState;
    public Light glow;

    public GameObject fireBallPrefab;
    public GameObject thunderBallPrefab;
    public GameObject iceBallPrefab;

    public GameObject chargeFireBallPrefab;
    public GameObject chargeThunderBallPrefab;
    public GameObject chargeIceBallPrefab;

    public GameObject IceShield;
    public GameObject FireSlam;

    public GameObject FlameShield;
    public GameObject ShadowShield;
    public GameObject FrostShield;

    public GameObject normalEmitter;
    public GameObject chargedEmitter;
    public GameObject slamEmitter;
    public GameObject shieldEmitter;

    public Transform[] teleportPos;

    public float flashTime = 0.1f;
    float timeFlash;
    public bool flashBoss;
    Color bossOrgColor;
    bool returnColor;
    public float damping;
    float DownTime;
    public float TimeDown;
    bool DamageBoss;
    bool Dying;
    bool EndGame;
    float EndTime = 10;
    bool FlameSwordEnabled;
    bool ElectroSwordEnabled;
    bool GlacialSwordEnabled;

    public bool Attacking;
    Vector3 CurrentPos;
    float navOrgStopDist;

    // Use this for initialization
    void Start()
    {
        orgHealth = health;
        healthMax = health;
        hitMax = hitPoints;
        SwitchElementalState(ElementalState.Fire);
        audioSrc = GetComponent<AudioSource>();
        nav = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        nav.speed = Speed;
        navOrgStopDist = nav.stoppingDistance;
        ChargeParticleFlame.SetActive(false);
        ChargeParticlePhantom.SetActive(false);
        ChargeParticleFrost.SetActive(false);
        bossOrgColor = BossBody.GetComponent<SkinnedMeshRenderer>().material.color;
        timeFlash = flashTime;
        DownTime = TimeDown;
        resetPoints = hitPoints;
        bossCollider = GetComponent<BoxCollider>();
        attackRate = Random.Range(1, 10);
        FlameShield.SetActive(false);
        ShadowShield.SetActive(false);
        FrostShield.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isWalking)
        {
            nav.isStopped = false;
            nav.speed = Speed;
            nav.stoppingDistance = navOrgStopDist;
            nav.destination = playerPosition;
        }
        else
        {
            nav.isStopped = true;
            nav.speed = 0;
            nav.stoppingDistance = 1000;
            nav.velocity = Vector3.zero;
        }
    }
    void Update() {

        handler();
        if (DamageBoss && !EndGame)
        {
            if (DownTime > 0)
            {
                DownTime -= Time.deltaTime;
            }
            else if (DownTime < 0)
            {
                DamageBoss = false;
                DownTime = TimeDown;
                hitPoints = resetPoints;
                anim.Rebind();
                ChangeForm();
                TeleportBoss();
                AttackFlash(1);
            }
        }
        else if (EndGame)
        {

            ActivateBossBanner(false);
            player = GameObject.FindGameObjectWithTag("Player");
            shakeScreen = player.GetComponent<PlayerStats>();
            shakeScreen.QuakeState();
            if (EndTime > 0)
            {
                EndTime -= Time.deltaTime;
            }
            else if (EndTime < 0)
            {
                music = player.GetComponent<SceneLoader>();
                music.SwitchMusic(SceneLoader.Music.EndingCredits);
                EndTime = 0;
                EndGame = false;
                Destroy(gameObject);
            }
        }
        else
        {
           
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            CurrentPos = transform.position;
            var lookPos = playerPosition - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            if (Vector3.Magnitude(gameObject.transform.position - playerPosition) < awarenessRange && !DamageBoss && Vector3.Magnitude(gameObject.transform.position - playerPosition) > meleeRange && !EndGame)
            {
                ActivateBossBanner(true);
               
                if (Time.time > nextAttack)
                {
                    int Randomize = Random.Range(1, 9);
                    if (Randomize == 3)
                    {
                        SwitchState(EnemyState.notWalking);
                        SwitchState(EnemyState.chargedball);
                    }
                    else if (Randomize == 5)
                    {
                        SwitchState(EnemyState.notWalking);
                        SwitchState(EnemyState.changeform);
                    }
                    else
                    {
                        SwitchState(EnemyState.notWalking);
                        SwitchState(EnemyState.fireball);
                    }
                    attackRate = Random.Range(1, 10);
                    nextAttack = Time.time + attackRate;
                }
                else
                {
                    SwitchState(EnemyState.walk);
                }
            }
            else if (Vector3.Magnitude(gameObject.transform.position - playerPosition) <= meleeRange && !DamageBoss)
            {

                
                if (Time.time > nextAttack)
                {
                    nav.isStopped = true;
                    int Randomize = Random.Range(1, 6);
                    if (Randomize == 3)
                    {
                        SwitchState(EnemyState.notWalking);
                        SwitchState(EnemyState.slam);
                    }
                    else if (Randomize == 5)
                    {
                        SwitchState(EnemyState.notWalking);
                        SwitchState(EnemyState.changeform);
                    }
                    else
                    {
                        SwitchState(EnemyState.notWalking);
                        SwitchState(EnemyState.melee);
                    }
                    attackRate = Random.Range(0.01f, 0.05f);
                    nextAttack = Time.time + attackRate;
                }
                else
                {
                    SwitchState(EnemyState.notWalking);
                    SwitchState(EnemyState.idle);
                }
            }
        }
       
        if (flashBoss)
        {
            if (flashTime > 0)
            {
                flashTime -= Time.deltaTime;
            }
            else if (flashTime < 0)
            {
                if (DamageBoss)
                {
                    if (flameState)
                    {
                        elementMaterial[0].SetColor("_Emission", Color.red);
                        meshRend.material = elementMaterial[0];
                    }
                    else if (phantState)
                    {
                        elementMaterial[1].SetColor("_Emission", Color.red);
                        meshRend.material = elementMaterial[1];
                    }
                    else if (frostState)
                    {
                        elementMaterial[2].SetColor("_Emission", Color.red);
                        meshRend.material = elementMaterial[2];
                    }
                }
                else
                {
                    if (flameState)
                    {
                        elementMaterial[0].SetColor("_Emission", Color.white);
                        meshRend.material = elementMaterial[0];
                    }
                    else if (phantState)
                    {
                        elementMaterial[1].SetColor("_Emission", Color.white);
                        meshRend.material = elementMaterial[1];
                    }
                    else if (frostState)
                    {
                        elementMaterial[2].SetColor("_Emission", Color.white);
                        meshRend.material = elementMaterial[2];
                    }
                }
                flashTime = timeFlash;
                flashBoss = false;
                returnColor = true;
            }
        }
        if (returnColor)
        {
            meshRend.material.color = bossOrgColor;
            returnColor = false;
        }
    }
    public void DoHitPoints(int amount)
    {
        hitPoints -= amount;
        flashBoss = true;
        audioSrc.PlayOneShot(hit);
        if (hitPoints < 0)
        {
            hitPoints = 0;
            SwitchState(EnemyState.down);
            DamageBoss = true;
        }
    }
    public void DoDamage(int amount)
    {
        health -= amount;
        flashBoss = true;
        audioSrc.PlayOneShot(damage);
        if (health < 0)
        {
            FlameShield.SetActive(false);
            ShadowShield.SetActive(false);
            FrostShield.SetActive(false);
            health = 0;
            SwitchState(EnemyState.down);
            StartCoroutine(Fading(true));
            bossCollider.enabled = false;
            
        }
    }
    public void OnGetHitBySword(int hitValue)
    {
        audioSrc2.PlayOneShot(swordhit);
        FlameSwordEnabled = AnimatedSword.MagicSwordEnabled;
        ElectroSwordEnabled = AnimatedSword.ThunderSwordEnabled;
        GlacialSwordEnabled = AnimatedSword.IceSwordEnabled;
        if (flameState)
        {
            if (GlacialSwordEnabled && !DamageBoss)
                DoHitPoints(1);
            else if (GlacialSwordEnabled && DamageBoss)
                DoDamage(hitValue);
            else
                audioSrc.PlayOneShot(ting);
        }
        else if (phantState)
        {
            if (ElectroSwordEnabled && !DamageBoss)
                DoHitPoints(1);
            else if (ElectroSwordEnabled && DamageBoss)
                DoDamage(hitValue);
            else
                audioSrc.PlayOneShot(ting);
        }
        else if (frostState)
        {
            if (FlameSwordEnabled && !DamageBoss)
                DoHitPoints(1);
            else if (FlameSwordEnabled && DamageBoss)
                DoDamage(hitValue);
            else
                audioSrc.PlayOneShot(ting);
        }
    }
    public enum EnemyState { down, walk, idle, melee, slam, fireball, chargedball, changeform, isWalking, notWalking }
    public enum ElementalState { Fire, Thunder, Ice}
    public void SwitchState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.down:
                {
                    anim.SetTrigger("Down");
                    break;
                }
            case EnemyState.walk:
                {
                    anim.SetBool("Walk", true);
                    anim.SetBool("Idle", false);
                    break;
                }
            case EnemyState.idle:
                {

                    anim.SetBool("Idle", true);
                    anim.SetBool("Walk", false);
                    break;
                }
            case EnemyState.melee:
                {
         
                    anim.SetBool("Melee", true);
                    break;
                }
            case EnemyState.slam:
                {
 
                    anim.SetBool("Slam", true);
                    break;
                }
            case EnemyState.fireball:
                {
                    transform.LookAt(Camera.main.transform.position);
                    isWalking = false;
                    anim.SetTrigger("FireBall");
                    break;
                }
            case EnemyState.chargedball:
                {
                    transform.LookAt(Camera.main.transform.position);
                    isWalking = false;
                    anim.SetTrigger("Charge");
                    break;
                }
            case EnemyState.changeform:
                {
             
                    isWalking = false;
                    anim.SetBool("Change", true);
                    break;
                }
            case EnemyState.isWalking:
                {
                    isWalking = true;
                    break;
                }
            case EnemyState.notWalking:
                {
                    isWalking = false;
                    break;
                }
        }
    }
    public void SwitchElementalState(ElementalState state)
    {
        meshRend = BossBody.GetComponent<SkinnedMeshRenderer>();
        switch (state)
        {
            case ElementalState.Fire:
                {
                    firePic = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/FaceMask/FinalBossFlame").GetComponent<Image>();
                    phantPic = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/FaceMask/FinalBossPhant").GetComponent<Image>();
                    icePic = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/FaceMask/FinalBossFrost").GetComponent<Image>();
                    firePic.enabled = true;
                    phantPic.enabled = false;
                    icePic.enabled = false;
                    flameState = true;
                    phantState = false;
                    if (health < orgHealth / 2)
                    {
                        FlameShield.SetActive(true);
                        ShadowShield.SetActive(false);
                        FrostShield.SetActive(false);
                    }
                    meshRend.material = elementMaterial[0];
                    bossOrgColor = meshRend.material.color;
                    glow.color = new Color(255, 69, 0, 255);
                    break;
                }
            case ElementalState.Thunder:
                {
                    firePic = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/FaceMask/FinalBossFlame").GetComponent<Image>();
                    phantPic = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/FaceMask/FinalBossPhant").GetComponent<Image>();
                    icePic = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/FaceMask/FinalBossFrost").GetComponent<Image>();
                    firePic.enabled = false;
                    phantPic.enabled = true;
                    icePic.enabled = false;
                    flameState = false;
                    phantState = true;
                    frostState = false;
                    if (health < orgHealth / 2)
                    {
                        FlameShield.SetActive(false);
                        ShadowShield.SetActive(true);
                        FrostShield.SetActive(false);
                    }
                    meshRend.material = elementMaterial[1];
                    bossOrgColor = meshRend.material.color;
                    glow.color = new Color(72, 255, 0, 255);
                    break;
                }
            case ElementalState.Ice:
                {
                    firePic = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/FaceMask/FinalBossFlame").GetComponent<Image>();
                    phantPic = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/FaceMask/FinalBossPhant").GetComponent<Image>();
                    icePic = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/FaceMask/FinalBossFrost").GetComponent<Image>();
                    firePic.enabled = false;
                    phantPic.enabled = false;
                    icePic.enabled = true;
                    flameState = false;
                    phantState = false;
                    frostState = true;
                    if (health < orgHealth / 2)
                    {
                        FlameShield.SetActive(false);
                        ShadowShield.SetActive(false);
                        FrostShield.SetActive(true);
                    }
                    meshRend.material = elementMaterial[2];
                    bossOrgColor = meshRend.material.color;
                    glow.color = new Color(0, 132, 255, 255);
                    break;
                }
        }
    }
    public void ShootNormal()
    {
        if (flameState)
        {
            projectile = fireBallPrefab;
            audioSrc.PlayOneShot(norFireShot);
        }
        else if (phantState)
        {
            projectile = thunderBallPrefab;
            audioSrc.PlayOneShot(norThunderShot);
        }
        else if (frostState)
        {
            projectile = iceBallPrefab;
            audioSrc.PlayOneShot(norIceShot);
        }
        Rigidbody Temporary_RigidBody;
        GameObject newBullet;
 
        newBullet = Instantiate(projectile, normalEmitter.transform.position, normalEmitter.transform.rotation);
        newBullet.transform.LookAt(Camera.main.transform.position);
        Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
        Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce * 2);
        Destroy(newBullet, 30.0f);
    }
    public void ShootCharged()
    {
        if (flameState)
        {
            projectile = chargeFireBallPrefab;
            audioSrc.PlayOneShot(norFireShot);
        }
        else if (phantState)
        {
            projectile = chargeThunderBallPrefab;
            audioSrc.PlayOneShot(norThunderShot);
        }
        else if (frostState)
        {
            projectile = chargeIceBallPrefab;
            audioSrc.PlayOneShot(norIceShot);
        }
        Rigidbody Temporary_RigidBody;
        GameObject newBullet;

        newBullet = Instantiate(projectile, chargedEmitter.transform.position, chargedEmitter.transform.rotation);
        newBullet.transform.Rotate(0, 90, 0);
        newBullet.transform.LookAt(Camera.main.transform.position);
        Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
        Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce * 2);
        Destroy(newBullet, 30.0f);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, awarenessRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }
    public void Attacked(bool True)
    {
        if (True)
        {
            Attacking = true;
        }
        else
        {
            Attacking = false;
            ChargeParticleFlame.SetActive(false);
            ChargeParticlePhantom.SetActive(false);
            ChargeParticleFrost.SetActive(false);
        }
    }
    public void Charging(bool True)
    {
        if (True)
        {
            if (flameState)
            {
                ChargeParticleFlame.SetActive(true);
            }
            else if (phantState)
            {
                ChargeParticlePhantom.SetActive(true);
            }
            else if (frostState)
            {
                ChargeParticleFrost.SetActive(true);
            }
        }
        else
        {
            ChargeParticleFlame.SetActive(false);
            ChargeParticlePhantom.SetActive(false);
            ChargeParticleFrost.SetActive(false);
        }
       
    }
    public void AttackFlash(int num)
    {
        Vector3 InstantPos;
        if (num == 1)
        {
            InstantPos = transform.position;
            if (flameState)
            {
                Instantiate(FlashParticleFlame, InstantPos, Quaternion.identity, transform);
            }
            else if (phantState)
            {
                Instantiate(FlashParticlePhantom, InstantPos, Quaternion.identity, transform);
            }
            else if (frostState)
            {
                Instantiate(FlashParticleFrost, InstantPos, Quaternion.identity, transform);
            }
        }
        else
        {
            InstantPos = normalEmitter.transform.position;
            if (flameState)
            {
                Instantiate(FlashParticleFlame, InstantPos, Quaternion.identity, transform);
            }
            else if (phantState)
            {
                Instantiate(FlashParticlePhantom, InstantPos, Quaternion.identity, transform);
            }
            else if (frostState)
            {
                Instantiate(FlashParticleFrost, InstantPos, Quaternion.identity, transform);
            }
        }
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Blade"))
        {
            audioSrc.PlayOneShot(ting);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Fire"))
        {
            if (frostState)
            {
                if (DamageBoss)
                {
                    DoDamage(3);
                }
                else
                {
                    DoHitPoints(3);
                }
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Thunder"))
        {
            if (phantState)
            {
                if (DamageBoss)
                {
                    DoDamage(4);
                }
                else
                {
                    DoHitPoints(4);
                }
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("StormShield"))
        {
            if (flameState)
            {
                if (DamageBoss)
                {
                    DoDamage(5);
                }
                else
                {
                    DoHitPoints(5);
                }
            }
            Destroy(collision.gameObject);
        }
    }
    public void ActivateBossBanner(bool True)
    {
        if (True)
        {
            BossBanner = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner");
            BossBanner.SetActive(true);
            Text text = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/BossName").GetComponent<Text>();
            text.text = "Oblivious, The Elemental God Titan";
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
        shieldFillAmount.fillAmount = Map(hitPoints, 0, hitMax, 0, 1);

    }
    public void ChangeForm()
    {
        int Randomly = Random.Range(1, 3);
        if (flameState)
        {
            if (Randomly == 1)
                SwitchElementalState(ElementalState.Thunder);
            else
                SwitchElementalState(ElementalState.Ice);
        }
        else if (phantState)
        {
            if (Randomly == 1)
                SwitchElementalState(ElementalState.Fire);
            else
                SwitchElementalState(ElementalState.Ice);
        }
        else if (frostState)
        {
            if (Randomly == 1)
                SwitchElementalState(ElementalState.Fire);
            else 
                SwitchElementalState(ElementalState.Thunder);
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
        BossBody.SetActive(false);
        EndGame = true;
    }
    void MeshRendTurnOn(bool On)
    {
        if (On)
        {
            for (int i = 0; i < meshRends.Length; i++)
            {
                glow.enabled = true;
                meshRends[i].enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < meshRends.Length; i++)
            {
                glow.enabled = false;
                meshRends[i].enabled = false;
            }
        }
    }
    public void PowerSlam()
    {
        if (flameState)
        {
            Instantiate(FireSlam, slamEmitter.transform.position, Quaternion.identity);
        }
        else if (phantState)
        {
            TeleportBoss();
        }
        else if(frostState)
        {
            Instantiate(IceShield, shieldEmitter.transform.position, Quaternion.identity, transform);
        }
      
    }
    public void TeleportBoss()
    {
        int RandomPos = Random.Range(1, teleportPos.Length);
        audioSrc.PlayOneShot(teleport);
        Instantiate(FlashParticlePhantom, transform.position, Quaternion.identity);
        transform.position = teleportPos[RandomPos].transform.position;
    }

}
