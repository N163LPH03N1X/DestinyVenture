using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType {Gizmo, Spire, Plasm, Devil, BossGizmo, BossSpire, BossPlasm, BossDevil }

public class EyeEnemy : MonoBehaviour, ISwordHittable
{
    [Header("Choose Eye")]
    public EnemyType enemyType;
    [Space]
    [Header("Audio Settings")]
    public AudioClip damage;
    public AudioClip death;
    public AudioClip shoot;
    public AudioClip frozen;
    public AudioClip teleport;
    [Space]
    [Header ("Item Drops")]
    public GameObject itemDropPos;
    [Space]
    public GameObject VitaSMPickupPrefab;
    public GameObject VitaMDPickupPrefab;
    public GameObject VitaLGPickupPrefab;
    public GameObject HealSMPickupPrefab;
    public GameObject HealMDPickupPrefab;
    public GameObject HealLGPickupPrefab;
    public GameObject PowGemSLPickupPrefab;
    public GameObject PowGemGDPickupPrefab;
    public GameObject GoldPrefab;
    [Space]
    [Header("On/Off Objects")]
    public GameObject ForceField;
    public GameObject FirePlatform;
    [Space]
    [Header("Enemy Settings")]
    public int health;
    public float fadePerSecond;
    public float Speed;
    public float attackRate;
    public float unFreezeTime;
    public float dampMovement = 2f;
    public float awarenessRange;
    public float projectileRange;
    public float projectileForce;
    public float setTeleportTimer;
    public GameObject EyeObject;
    public Light glow;
    public GameObject[] projectiles;
    public GameObject emitter;
    [SerializeField]
    private Material[] enemyMaterial;
    public GameObject EnemyObject;
    public NavMeshAgent nav;
    PlayerStats playST;
    ImpactReceiver Impact;
    MeshRenderer meshRend;
    SphereCollider sphereColid;
    GameObject Player;
    Rigidbody rb;
    AudioSource audioSrc;
    Vector3 playerPosition;
    Vector3 CurrentPos;
    Color OrgColor;
    GameObject projectile;
   
    public int idlestate;
    float freezeTimer;
    float nextAttack;
    float idleDurration;
    float flashTime = 0.2f;
    float timeFlash;
    float navOrgStopDist;
    bool PhantomBreastEnabled;
    bool enemyFroze;
    public bool isIdle;
    bool Dying;
    bool flashEnemy;
    public bool isMoving;
    int experience;
    bool Fade;
    public bool Teleport;
    public bool isRamming;
    Vector3 setTeleportPos;
    float telePosTimer;

    public bool devil;
    public bool spire;
    public bool gizmo;
    public bool plasm;
    public bool bossGizmo;
    public bool bossSpire;
    public bool bossPlasm;
    public bool bossDevil;

    bool muzzleFlash;
    float muzzleFlashTime = 0.2f;
    float muzzleTimedFlash;
    public GameObject FireFlash;
    public GameObject IceFlash;

    public bool devilSpawn;
    public bool spireSpawn;
    public bool gizmoSpawn;
    public bool plasmSpawn;

    bool inTemple;
    bool DontChangeType;
    bool Day;
    bool Night;
    bool activateNight;
    bool ActivateDay;
    float Accel;
   
    float deathTimer;
    SceneLoader music;
    void Start()
    {
        SwitchEnemyType(enemyType);
        isMoving = true;
        audioSrc = GetComponent<AudioSource>();
        idleDurration = 5;
        rb = GetComponent<Rigidbody>();
        nav.speed = Speed;
        navOrgStopDist = nav.stoppingDistance;
        OrgColor = EyeObject.GetComponent<MeshRenderer>().material.color;
        timeFlash = flashTime;
        audioSrc = GetComponent<AudioSource>();
        meshRend = EyeObject.GetComponent<MeshRenderer>();
        sphereColid = GetComponent<SphereCollider>();
        OrgColor = meshRend.material.GetColor("_EmissionColor");
        setTeleportPos = EnemyObject.transform.position;
        telePosTimer = setTeleportTimer;
        muzzleTimedFlash = muzzleFlashTime;
        Accel = nav.acceleration;
        IceFlash.SetActive(false);
        FireFlash.SetActive(false);
        
        if (bossGizmo || bossSpire || bossPlasm || bossDevil)
        {
            if (bossGizmo)
            {
                if (ForceField != null)
                {
                    ForceField.SetActive(true);
                }

                if (FirePlatform != null)
                {
                    FirePlatform.SetActive(false);
                }
            }
            transform.localScale += new Vector3(4f, 4f, 4f);
        }
        else
        {
            ForceField = null;
            FirePlatform = null;
        }
       
        
        if (gizmo)
        {
            gizmoSpawn = gizmo;
        }
        else if (spire)
        {
            spireSpawn = spire;
        }
        else if (plasm)
        {
            plasmSpawn = plasm;
        }
        else if (devil)
        {
            devilSpawn = devil;
        }
    }

    // Update is called once per frame
    void Update()
    {
        inTemple = SceneLoader.Entered;
        Day = DayNightScript.Day;
        Night = DayNightScript.Night;
        PhantomBreastEnabled = Breast.PhantBreastActive;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (!inTemple)
        {
            DontChangeType = false;
            if (Day)
            {
                if (!ActivateDay)
                {
                    if (gizmoSpawn)
                    {
                        SwitchEnemyType(EnemyType.Gizmo);
                    }
                    else if (spireSpawn)
                    {
                        SwitchEnemyType(EnemyType.Spire);
                    }
                    else if (plasmSpawn)
                    {
                        SwitchEnemyType(EnemyType.Plasm);
                    }
                    else if (devilSpawn)
                    {
                        SwitchEnemyType(EnemyType.Devil);
                    }
                    ActivateDay = true;
                    activateNight = false;

                }
            }
            else if (Night)
            {
                if (!activateNight)
                {
                    SwitchEnemyType(EnemyType.Devil);
                    ActivateDay = false;
                    activateNight = true;
                }
            }
        }
        else
        {
            if (!DontChangeType)
            {
                if (gizmoSpawn)
                {
                    SwitchEnemyType(EnemyType.Gizmo);
                }
                else if (spireSpawn)
                {
                    SwitchEnemyType(EnemyType.Spire);
                }
                else if (plasmSpawn)
                {
                    SwitchEnemyType(EnemyType.Plasm);
                }
                else if (devilSpawn)
                {
                    SwitchEnemyType(EnemyType.Devil);
                }
                DontChangeType = true;
            }
        }
       

        if (isMoving && !enemyFroze)
        {
            nav.isStopped = false;
            if (isIdle)
            {
                nav.speed = Speed;
            }
            else if(!PhantomBreastEnabled)
            {
                if (isRamming)
                {
                    nav.speed = Speed * 10;
                    nav.acceleration = Accel * 2;
                }
                else
                {
                    nav.speed = Speed * 2;
                    nav.acceleration = Accel;
                }
                
                var lookPos = playerPosition - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampMovement);
                nav.stoppingDistance = navOrgStopDist;
                nav.destination = playerPosition;
            }
        }
        else
        {
            nav.isStopped = true;
            nav.speed = 0;
            nav.stoppingDistance = 1000;
            nav.velocity = Vector3.zero;
        }
        if (Vector3.Magnitude(gameObject.transform.position - playerPosition) > awarenessRange && !Teleport)
        {
            isIdle = true;
            IdleMovement();
        }
        else if (Vector3.Magnitude(gameObject.transform.position - playerPosition) <= awarenessRange && !Teleport && !PhantomBreastEnabled)
        {
            isIdle = false;
            if (devil || plasm || spire || bossSpire || bossPlasm || bossDevil )
            {
                if (Time.time > nextAttack && !enemyFroze)
                {
                    if (spire || bossSpire)
                    {
                        SwitchState(EnemyState.teleport);
                    }
                    else if (plasm || bossPlasm)
                    {
                        int randomNum = Random.Range(1, 3);
                        if (randomNum == 1)
                        {
                            SwitchState(EnemyState.teleport);
                        }
                        else
                        {
                            SwitchState(EnemyState.shoot);
                        }
                    }
                    else if (devil || bossDevil)
                    {
                        int randomNum = Random.Range(1, 5);
                        if (randomNum == 1)
                        {
                            SwitchState(EnemyState.teleport);
                        }
                        else if (randomNum == 2)
                        {
                            SwitchState(EnemyState.shoot);
                        }
                        else
                        {
                            SwitchState(EnemyState.ram);
                        }
                    }

                    nextAttack = Time.time + attackRate;
                }
            }
        }
        if (Teleport)
        {
            if (Fade)
            {
                nav.speed = 0;
                nav.isStopped = true;
                var color = meshRend.material.color;
                SetupMaterialWithBlendMode(meshRend.material, BlendMode.Fade);
                meshRend.material.color = new Color(color.r, color.g, color.b, color.a - (4 * Time.deltaTime));
                if (color.a <= 0.1f)
                {
                    audioSrc.PlayOneShot(teleport);
                    EnemyObject.transform.position = setTeleportPos;
                    Fade = false;
                }
            }
            else if (!Fade)
            {
                nav.speed = 0;
                nav.isStopped = true;
                var color = meshRend.material.color;
                SetupMaterialWithBlendMode(meshRend.material, BlendMode.Opaque);
                meshRend.material.color = new Color(color.r, color.g, color.b, color.a + (4 * Time.deltaTime));
                if (color.a >= 0.9f)
                {
                    nav.speed = Speed;
                    nav.isStopped = false;
                    Teleport = false;
                }
            }
        }
        if (spire || plasm || devil || bossSpire || bossPlasm || bossDevil)
        {
            telePosTimer -= Time.deltaTime;
        }
        else if(telePosTimer < 0)
        {
            setTeleportPos = EnemyObject.transform.position;
            telePosTimer = setTeleportTimer;
        }
        if (muzzleFlash)
        {
            if (muzzleTimedFlash > 0)
            {
                muzzleTimedFlash -= Time.deltaTime;
                if (plasm || bossPlasm)
                {
                    IceFlash.SetActive(true);
                }
                else if (devil || bossDevil)
                {
                    FireFlash.SetActive(true);
                }
            }
            else if (muzzleTimedFlash < 0)
            {
                muzzleFlash = false;
                muzzleTimedFlash = muzzleFlashTime;
                IceFlash.SetActive(false);
                FireFlash.SetActive(false);
                
            }
        }
       
        if (flashEnemy)
        {
            if (flashTime > 0)
            {
                flashTime -= Time.deltaTime;
                meshRend.material.SetColor("_EmissionColor", Color.red);
            }
            else if (flashTime < 0)
            {
                meshRend.material.SetColor("_EmissionColor", OrgColor);
                flashTime = timeFlash;
                flashEnemy = false;
            }
        }
        else if (enemyFroze && !flashEnemy)
        {
            nav.speed = 0.0f;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            meshRend.material.SetColor("_EmissionColor", Color.blue);
        }
        else if (enemyFroze && flashEnemy)
        {
            nav.speed = 0.0f;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            meshRend.material.SetColor("_EmissionColor", Color.red);
        }
        if (freezeTimer > 0)
        {
            freezeTimer -= Time.deltaTime;
            enemyFroze = true;
        }
        else if (freezeTimer < 0)
        {
            freezeTimer = 0;
            meshRend.material.SetColor("_EmissionColor", OrgColor);
            enemyFroze = false;
        }
        if (Dying)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(transform.forward * -50);
            rb.AddForce(transform.up * -20);
            nav.speed = 0;
            nav.isStopped = true;
            var color = meshRend.material.color;
            SetupMaterialWithBlendMode(meshRend.material, BlendMode.Fade);
            meshRend.material.color = new Color(color.r, color.g, color.b, color.a - (0.7f * Time.deltaTime));
            if (color.a <= 0.1f)
            {
                Destroy(EnemyObject);
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("StormShield"))
        {
            audioSrc.PlayOneShot(frozen);
            FreezeEnemy(true);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            if (isRamming)
            {
                SwitchState(EnemyState.teleport);
                isRamming = false;
            }
            Impact = other.gameObject.GetComponent<ImpactReceiver>();
            Vector3 direction = (other.transform.position - this.transform.position).normalized;
            Impact.AddImpact(direction, 10);
            playST = other.gameObject.GetComponent<PlayerStats>();
            playST.doDamage(Random.Range(1, 5));
        }
    }

    public void OnGetHitBySword(int hitValue)
    {
        if (!Dying)
        {
            DoDamage(hitValue);
        }
      
    }
    public void DoDamage(int amount)
    {
        audioSrc.volume = Random.Range(0.8f, 1.0f);
        audioSrc.pitch = Random.Range(0.8f, 1.0f);
        audioSrc.PlayOneShot(damage);
        health -= amount;
        flashEnemy = true;
        if (health < 1)
        {
            if (bossGizmo)
            {
                music = GameObject.FindGameObjectWithTag("Player").GetComponent<SceneLoader>();
                music.SwitchMusic(SceneLoader.Music.FireTemple);
            }
            else if (bossSpire)
            {
                music = GameObject.FindGameObjectWithTag("Player").GetComponent<SceneLoader>();
                music.SwitchMusic(SceneLoader.Music.PhantTemple);
            }
            else if (bossPlasm)
            {
                music = GameObject.FindGameObjectWithTag("Player").GetComponent<SceneLoader>();
                music.SwitchMusic(SceneLoader.Music.IceTemple);
            }
            else if (bossDevil)
            {
                music = GameObject.FindGameObjectWithTag("Player").GetComponent<SceneLoader>(); 
                music.SwitchMusic(SceneLoader.Music.OverWorld);
            }
            Dying = true;
            deathTimer = 1;
            DropPickup();
            playST = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
            if (PhantomBreastEnabled)
            {
                playST.gainExp(experience *2);
            }
            else
            {
                playST.gainExp(experience);
            }
            if (bossGizmo)
            {
                if (ForceField != null)
                {
                    ForceField.SetActive(false);
                }

                if (FirePlatform != null)
                {
                    FirePlatform.SetActive(true);
                }
            }
            else
            {
                ForceField = null;
                FirePlatform= null;
            }
            audioSrc.volume = Random.Range(0.8f, 1.0f);
            audioSrc.pitch = Random.Range(0.8f, 1.0f);
            audioSrc.PlayOneShot(death);
            sphereColid.enabled = false;
         
            health = 0;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, awarenessRange);
    }
    public void FreezeEnemy(bool froze)
    {
        freezeTimer += unFreezeTime;
    }
    public void IdleMovement()
    {
        if (idleDurration > 0)
        {
            Vector3 dir;
            idleDurration -= Time.deltaTime;
            if (idlestate == 1)
            {
                
            }
            else if (idlestate == 2)
            {
                nav.updateRotation = true;

                dir = Vector3.forward;
                var lookPos = dir;
                var rotation = Quaternion.LookRotation(lookPos);
                lookPos.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampMovement);
                nav.Move(dir * (Speed * Time.deltaTime));
            }
            else if (idlestate == 3)
            {
                nav.updateRotation = true;
                dir = Vector3.back;
                var lookPos = dir;
                var rotation = Quaternion.LookRotation(lookPos);
                lookPos.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampMovement);
                nav.Move(dir * (Speed * Time.deltaTime));
            }
            else if (idlestate == 4)
            {
                nav.updateRotation = true;
                dir = Vector3.left;
                var lookPos = dir;
                var rotation = Quaternion.LookRotation(lookPos);
                lookPos.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampMovement);
                nav.Move(dir * (Speed * Time.deltaTime));
            }
            else if (idlestate == 5)
            {
                nav.updateRotation = true;
                dir = Vector3.right;
                var lookPos = dir;
                var rotation = Quaternion.LookRotation(lookPos);
                lookPos.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampMovement);
                nav.Move(dir * (Speed * Time.deltaTime));
            }
        }
        else if (idleDurration < 0)
        {
            idlestate = Random.Range(1, 6);
            idleDurration = Random.Range(1, 10);
            Invoke("IdleMovement", 0.1f);
        }
    }
    public void DropPickup()
    {
        int[] VitaSMnum = { 2 };
        int[] VitaMDnum = { 17 };
        int[] HealSMnum = { 3 };
        int[] HealMDnum = { 20 };
        int[] PowGemSLnum = { 29 };
        int[] Goldnum = { 1, 4, 5, 6, 8, 10, 12, 14, 16, 18, 19, 21, 23, 28, 7, 9, 11, 13, 15, 22, 24, 25, 27, 30, 31, 33, 35, 36, 34, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 };

        int RandomNumber = Random.Range(1, 50);

        for (int i = 0; i < 1; i++)
        {
            for (int vsm = 0; vsm < VitaSMnum.Length; vsm++)
            {
                if (VitaSMnum[vsm] == RandomNumber)
                {
                    Instantiate(VitaSMPickupPrefab, itemDropPos.transform.position, Quaternion.identity);
                }
            }
            for (int vmd = 0; vmd < VitaMDnum.Length; vmd++)
            {
                if (VitaMDnum[vmd] == RandomNumber)
                {
                    Instantiate(VitaMDPickupPrefab, itemDropPos.transform.position, Quaternion.identity);
                }
            }
            for (int hsm = 0; hsm < HealSMnum.Length; hsm++)
            {
                if (HealSMnum[hsm] == RandomNumber)
                {
                    Instantiate(HealSMPickupPrefab, itemDropPos.transform.position, Quaternion.identity);
                }
            }
            for (int hmd = 0; hmd < HealMDnum.Length; hmd++)
            {
                if (HealMDnum[hmd] == RandomNumber)
                {
                    Instantiate(HealMDPickupPrefab, itemDropPos.transform.position, Quaternion.identity);
                }
            }
         
            for (int pgsl = 0; pgsl < PowGemSLnum.Length; pgsl++)
            {
                if (PowGemSLnum[pgsl] == RandomNumber)
                {
                    Instantiate(PowGemSLPickupPrefab, itemDropPos.transform.position, Quaternion.identity);
                }
            }
          
            for (int gold = 0; gold < Goldnum.Length; gold++)
            {
                if (Goldnum[gold] == RandomNumber)
                {
                    Instantiate(GoldPrefab, itemDropPos.transform.position, Quaternion.identity);
                }
            }
        }    
    }
    public void SwitchEnemyType(EnemyType type)
    {
        meshRend = EyeObject.GetComponent<MeshRenderer>();
        switch (type)
        {
            case EnemyType.Gizmo:
                {
                    experience = 100;
                    glow.color = Color.yellow;
                    health = 5;
                    devil = false;
                    spire = false;
                    gizmo = true;
                    plasm = false;
                    bossGizmo = false;
                    bossSpire = false;
                    bossPlasm = false;
                    bossDevil = false;
                    meshRend.material = enemyMaterial[1];
                    OrgColor = meshRend.material.color;
                    break;
                }
            case EnemyType.Spire:
                {
                    experience = 200;
                    glow.color = Color.cyan;
                    health = 11;
                    devil = false;
                    spire = true;
                    gizmo = false;
                    plasm = false;
                    bossGizmo = false;
                    bossSpire = false;
                    bossPlasm = false;
                    bossDevil = false;
                    meshRend.material = enemyMaterial[2];
                    OrgColor = meshRend.material.color;
                    break;
                }
            case EnemyType.Plasm:
                {
                    experience = 300;
                    glow.color = Color.blue;
                    health = 17;
                    devil = false;
                    spire = false;
                    gizmo = false;
                    plasm = true;
                    bossGizmo = false;
                    bossSpire = false;
                    bossPlasm = false;
                    bossDevil = false;
                    meshRend.material = enemyMaterial[3];
                    OrgColor = meshRend.material.color;
                    break;
                }
            case EnemyType.Devil:
                {
                    experience = 400;
                    glow.color = Color.red;
                    health = 23;
                    devil = true;
                    spire = false;
                    gizmo = false;
                    plasm = false;
                    bossGizmo = false;
                    bossSpire = false;
                    bossPlasm = false;
                    bossDevil = false;
                    meshRend.material = enemyMaterial[4];
                    OrgColor = meshRend.material.color;
                    break;
                }
            case EnemyType.BossGizmo:
                {
                    experience = 500;
                    health = 15;
                    devil = false;
                    spire = false;
                    gizmo = false;
                    plasm = false;
                    bossGizmo = true;
                    bossSpire = false;
                    bossPlasm = false;
                    bossDevil = false;
                    meshRend.material = enemyMaterial[5];
                    OrgColor = meshRend.material.color;
                    break;
                }
            case EnemyType.BossSpire:
                {
                    experience = 600;
                    health = 30;
                    devil = false;
                    spire = false;
                    gizmo = false;
                    plasm = false;
                    bossGizmo = false;
                    bossSpire = true;
                    bossPlasm = false;
                    bossDevil = false;
                    meshRend.material = enemyMaterial[2];
                    OrgColor = meshRend.material.color;
                    break;
                }
            case EnemyType.BossPlasm:
                {
                    experience = 700;
                    health = 60;
                    devil = false;
                    spire = false;
                    gizmo = false;
                    plasm = false;
                    bossGizmo = false;
                    bossSpire = false;
                    bossPlasm = true;
                    bossDevil = false;
                    meshRend.material = enemyMaterial[3];
                    OrgColor = meshRend.material.color;
                    break;
                }
            case EnemyType.BossDevil:
                {
                    experience = 800;
                    health = 120;
                    devil = false;
                    spire = false;
                    gizmo = false;
                    plasm = false;
                    bossGizmo = false;
                    bossSpire = false;
                    bossPlasm = false;
                    bossDevil = true;
                    meshRend.material = enemyMaterial[4];
                    OrgColor = meshRend.material.color;
                    break;
                }
        }
    }
    public enum EnemyState { shoot, teleport, ram}
    public void SwitchState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.shoot:
                {
                    Shoot();
                    break;
                }
            case EnemyState.teleport:
                {
                    Fade = true;
                    Teleport = true;
                    break;
                }
            case EnemyState.ram:
                {
                    isRamming = true;
                    isMoving = true;
                    nav.speed = 20;
                    break;
                }
        }


    }
    public void Shoot()
    {
        if (devil || bossDevil)
        {
            projectile = projectiles[1];
            audioSrc.PlayOneShot(shoot);
        }
        else if (plasm || bossPlasm)
        {
            projectile = projectiles[0];
            audioSrc.PlayOneShot(shoot);
        }
        Rigidbody Temporary_RigidBody;
        GameObject newBullet;
        muzzleFlash = true;
        audioSrc.PlayOneShot(shoot);
        newBullet = Instantiate(projectile, emitter.transform.position, emitter.transform.rotation);
        newBullet.transform.LookAt(Camera.main.transform.position);
        Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
        Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce);
        Destroy(newBullet, 30.0f);
    }
    public enum BlendMode { Opaque, Cutout, Fade, Transparent }
    public static void SetupMaterialWithBlendMode(Material material, BlendMode blendMode)
    {
        switch (blendMode)
        {
            case BlendMode.Opaque:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                break;
            case BlendMode.Cutout:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 2450;
                break;
            case BlendMode.Fade:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
            case BlendMode.Transparent:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
        }
    }
}
