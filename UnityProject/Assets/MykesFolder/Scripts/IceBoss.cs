using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class IceBoss : MonoBehaviour , ISwordHittable
{
    SceneLoader music;
    bool StopMusic = false;
    Animator anim;
    NavMeshAgent Nav;
    int RandomNumber;
    [SerializeField]
    private int movementSpeed;
    private bool isMoving;
    bool On = false;
    public float DeathTimer;
    public MeshRenderer[] meshRends;
    PlayerStats playST;
    MeshRenderer meshRend;
    Color meshRendOrg;
    GameObject Player;
    public GameObject IceSpikeProjectile;
    public GameObject IceStormProjectile;
    public GameObject IceSpikeEmitterLeft;
    public GameObject IceSpikeEmitterRight;
    public GameObject IceStormEmitter;
    GameObject Projectile;
    float nextFire;
    public float fireRate;
    public float projectileForce;
    public Transform[] telePoints;
    public bool shootLeft;
    public bool shootRight;
    public bool shootCenter;
    Rigidbody rb;
    Vector3 resetVector;
    public BoxCollider Collider;
    public float fallingSpeed;
    public float FallDistance;
    public float fadePerSecond;
    public Transform[] Emitters;
    public float StoppingDistance;
    AudioSource audioSrc;
    AudioSource audioSrc2;
    public AudioClip monsterHit;
    public AudioClip monsterDeath;
    public AudioClip bossThrow;
    public AudioClip enemyFrozefx;
    public AudioClip monsterGrowl;
    public AudioClip Teleport;

    GameObject BossBanner;

    public int health;
    float Timer;
    float hurtTimer;
    public float unFreezeTime;
    bool Dying;

    [HideInInspector]
    Vector3 PlayerPosition;

    public float awarenessRange;

    float flashobjectTimer;
    public float flashObjectTime = 0.1f;
    bool flashObject;
    bool enemyFroze;
    bool returnColor;
    public int enemyState;
    public float Duration;
    public bool idling;
    public bool startIdle;
    bool Dead;
    bool PhantomBreastEnabled;
    public bool PlayerFound = false;
    public GameObject BossObject;
    bool hurtPlayer;
    public GameObject IceShield;
    public bool ShieldUp;
    public int shieldCounter = 4;
    float shieldTimer;
    bool MagicSwordEnabled;
    private Image healthFillAmount;
    private Image shieldFillAmount;
    bool weakAttack;
    bool mediumAttack;




    private void Awake()
    {
        if (gameObject.activeInHierarchy && enabled)
        {
            StartCoroutine(FindPlayer());
        }
    }
    private void OnEnable()
    {
        StartCoroutine(FindPlayer());
    }
    // Use this for initialization
    void Start () {

        StartCoroutine(FindPlayer());
        anim = GetComponent<Animator>();
        resetVector = Vector3.zero;
        StartCoroutine(FindPlayer());
        Dying = false;
        enemyFroze = false;
        Nav = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        meshRend = BossObject.GetComponent<MeshRenderer>();
        flashobjectTimer = flashObjectTime;
        health = 200;
        Timer = 0;
        hurtTimer = 0;
        audioSrc = GetComponent<AudioSource>();
        audioSrc2 = BossObject.GetComponent<AudioSource>();
        StartCoroutine(FindPlayer());
        shootLeft = true;
        shootRight = false;
        Collider = GetComponent<BoxCollider>();
        meshRendOrg = meshRend.material.GetColor("_EmissionColor");
        ShieldUp = true;
        Dead = false;
    }

    void Update () {
        if (gameObject.activeInHierarchy && enabled && !PlayerFound)
        {
            StartCoroutine(FindPlayer());
          
        }

        PhantomBreastEnabled = Breast.PhantomBreast;
       
        handler();
        if (DeathTimer > 0)
        {
            DeathTimer -= Time.deltaTime;
        }
        if (DeathTimer < 0)
        {
            StartCoroutine(Fading());
            DeathTimer = 0;
        }

        if (health <= 200 && health > 175)
        {
            fireRate = 2f;
        }
        else if (health <= 175 && health > 150)
        {
            fireRate = 1.5f;
        }
        else if (health <= 150 && health > 125)
        {
            fireRate = 1f;
        }
        else if (health <= 125 && health > 100)
        {
            fireRate = 0.8f;
        }
        else if (health <= 100 && health > 75)
        {
            fireRate = 0.6f;
        }
        else if (health <= 100 && health > 75)
        {
            fireRate = 0.6f;
        }
        else if (health <= 75 && health > 50)
        {
            fireRate = 0.5f;
        }
        else if (health <= 50 && health > 25)
        {
            fireRate = 0.4f;
        }
        else if (health <= 25 && health > 0)
        {
            fireRate = 0.3f;
        }
        if (PlayerFound)
        {
            PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

            if (shieldTimer > 0 && !Dying)
            {
                ShieldUp = false;
                shieldTimer -= Time.deltaTime;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            if (shieldTimer < 0)
            {
                shieldCounter = 4;
                ShieldUp = true;
                TeleportBoss();
                anim.Rebind();
                SpikeRing();
                shieldTimer = 0;
            }
            if (ShieldUp)
            {
                Nav.speed = 50;
                rb.constraints = RigidbodyConstraints.None;
                transform.LookAt(Camera.main.transform.position);
                IceShield.SetActive(true);

                if (Vector3.Magnitude(gameObject.transform.position - PlayerPosition) < awarenessRange && !Dying && !PhantomBreastEnabled)
                {
                    if (Time.time > nextFire + fireRate)
                    {
                        shoot();
                        nextFire = Time.time + fireRate;
                    }
                }

                if (Vector3.Magnitude(gameObject.transform.position - PlayerPosition) < StoppingDistance)
                {
                    Nav.speed = 0;
                }
                else if (Vector3.Magnitude(gameObject.transform.position - PlayerPosition) >= StoppingDistance)
                {
                    Nav.speed = 40;
                    Nav.destination = PlayerPosition;
                }

            }
            else if (!ShieldUp) 
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                rb.Sleep();
                rb.velocity = Vector3.zero;
                Nav.speed = 0;
                Nav.velocity = Vector3.zero;
                anim.SetTrigger("Down");
                IceShield.SetActive(false);
            }
            if (flashObject)
            {
                flashobjectTimer -= Time.deltaTime;
                meshRend.material.SetColor("_EmissionColor", Color.red);
            }
            if (flashobjectTimer < 0)
            {
                returnColor = false;
                flashObject = false;
                flashobjectTimer = flashObjectTime;
            }

            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
            }
            if (Timer < 0)
            {
                enemyFroze = false;
                returnColor = false;
                Timer = 0;
            }
            if (!returnColor)
            {
                meshRend.material.SetColor("_EmissionColor", meshRendOrg);
                returnColor = true;
            }
            if (enemyFroze && !flashObject)
            {
                Nav.speed = 0.0f;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                meshRend.material.SetColor("_EmissionColor", Color.blue);
            }
            else if (enemyFroze && flashObject)
            {
                meshRend.material.SetColor("_EmissionColor", Color.red);
            }
            if (Dying)
            {
                
                Nav.enabled = false;
                var color = meshRend.material.color;
                meshRend.material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
                transform.Translate(Vector3.up * fallingSpeed * Time.deltaTime);
                ActivateBossBanner(false);
                if (!StopMusic)
                {
                    music = GameObject.FindGameObjectWithTag("Player").GetComponent<SceneLoader>();
                    music.SwitchMusic(SceneLoader.Music.IceTemple);
                }
            }
            else
            {
                ActivateBossBanner(true);
            }
        }
    }
    IEnumerator Fading()
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
        Destroy(gameObject);
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
    public void OnGetHitBySword(int hitValue)
    {
        MagicSwordEnabled = PowerCounter.MagicSword;
        if (!ShieldUp)
        {
            weakAttack = AnimatedSword.WeakAttack;
            mediumAttack = AnimatedSword.StrongAttack;
            if (MagicSwordEnabled)
            {
                if (weakAttack)
                {
                    DoDamage(20);
                }
                else if (mediumAttack)
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
    public void DoDamage(int amount)
    {
        audioSrc.PlayOneShot(monsterHit);
        audioSrc2.PlayOneShot(monsterGrowl);
        health -= amount;
        flashObject = true;
        if (health <= 0)
        {
            if (!Dead)
            {  
                Dead = true;
            }
            playST = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>(); 
            Dying = true;
      
            if (PhantomBreastEnabled)
            {
                playST.gainExp(5000 * 2);
            }
            else
            {
                playST.gainExp(5000);
            }

            audioSrc.PlayOneShot(monsterDeath);
            Debug.Log("Boss Dead...");
            health = 0;
            Collider.enabled = false;
            DeathTimer = 0.5f;

        }
    }
    void TeleportBoss()
    {
        RandomNumber = Random.Range(1, 5);
        for (int i = 0; i < telePoints.Length; i++)
        {
            if (RandomNumber == 1)
            {
                audioSrc.PlayOneShot(Teleport, 0.7f);
                transform.position = telePoints[0].transform.position;
            }
            if (RandomNumber == 2)
            {
                audioSrc.PlayOneShot(Teleport, 0.7f);
                transform.position = telePoints[1].transform.position;
            }
            if (RandomNumber == 3)
            {
                audioSrc.PlayOneShot(Teleport, 0.7f);
                transform.position = telePoints[2].transform.position;
            }
            if (RandomNumber == 4)
            {
                audioSrc.PlayOneShot(Teleport, 0.7f);
                transform.position = telePoints[3].transform.position;
            }
            else
            {
                audioSrc.PlayOneShot(Teleport, 0.7f);
                transform.position = telePoints[0].transform.position;
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, awarenessRange);
    }
    IEnumerator FindPlayer()
    {
        Player = GameObject.Find("/GameControl/Player/PlayerController/");
        if (Player == null)
        {
            yield return null;
        }
        else
        {
            Player = GameObject.Find("/GameControl/Player/PlayerController/");
            PlayerFound = true;
        }
    }
    public void FreezeEnemy(bool froze)
    {
        enemyFroze = true;
        Timer += unFreezeTime;
    }
    public void HitCounter(int amount)
    {
        shieldCounter -= amount;
        audioSrc.PlayOneShot(bossThrow);
        if (shieldCounter <= 0)
        {
            shieldCounter = 0;
            ShieldUp = false;
            shieldTimer = 5;
        }
    }
    public void shoot()
    {
        Rigidbody Temporary_RigidBody;
        GameObject newBullet;
        if (shootLeft)
        {
            anim.SetTrigger("ThrowLeft");
            audioSrc.PlayOneShot(bossThrow);
            shootLeft = false;
            shootRight = true;
            Projectile = IceSpikeProjectile;
            newBullet = Instantiate(Projectile, IceSpikeEmitterLeft.transform.position, IceSpikeEmitterLeft.transform.rotation);
            newBullet.transform.LookAt(Camera.main.transform.position);
            Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce);
            Destroy(newBullet, 30.0f);


        }
        else if (shootRight)
        {
            anim.SetTrigger("ThrowRight");
            audioSrc.PlayOneShot(bossThrow);
            shootRight = false;
            shootLeft = true;
            Projectile = IceSpikeProjectile;
            newBullet = Instantiate(Projectile, IceSpikeEmitterRight.transform.position, IceSpikeEmitterRight.transform.rotation);
            newBullet.transform.LookAt(Camera.main.transform.position);
            Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(newBullet.transform.forward * projectileForce);
            Destroy(newBullet, 10.0f);
        }
     
    }
    public void BossAwareness()
    {
        awarenessRange = 100;
    }
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    void handler()
    {
        healthFillAmount = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/HealthMask/Health").GetComponent<Image>();
        shieldFillAmount = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner/ShieldMask/Shield").GetComponent<Image>();
        healthFillAmount.fillAmount = Map(health, 0, 200, 0, 1);
        shieldFillAmount.fillAmount = Map(shieldCounter, 0, 4, 0, 1);
      
    }
    public void SpikeRing()
    {
        for (int i = 0; i < Emitters.Length; i++)
        {
            Rigidbody Temporary_RigidBody;
            GameObject newBullet;
            int force = 500;
            Projectile = IceSpikeProjectile;
           
            if (i == 0)
            {
                newBullet = Instantiate(Projectile, Emitters[0].transform.position, Emitters[0].transform.rotation);
                Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(newBullet.transform.forward * force);
                Destroy(newBullet, 10.0f);
            }
            if (i == 1)
            {
                newBullet = Instantiate(Projectile, Emitters[1].transform.position, Emitters[1].transform.rotation);
                Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(newBullet.transform.forward * force);
                Destroy(newBullet, 10.0f);
            }
            if (i == 2)
            {
                newBullet = Instantiate(Projectile, Emitters[2].transform.position, Emitters[2].transform.rotation);
                Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(newBullet.transform.forward * force);
                Destroy(newBullet, 10.0f);
            }
            if (i == 3)
            {
                newBullet = Instantiate(Projectile, Emitters[3].transform.position, Emitters[3].transform.rotation);
                Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(newBullet.transform.forward * force);
                Destroy(newBullet, 10.0f);
            }
            if (i == 4)
            {
                newBullet = Instantiate(Projectile, Emitters[4].transform.position, Emitters[4].transform.rotation);
                Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(newBullet.transform.forward * force);
                Destroy(newBullet, 10.0f);
            }
            if (i == 5)
            {
                newBullet = Instantiate(Projectile, Emitters[5].transform.position, Emitters[5].transform.rotation);
                Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(newBullet.transform.forward * force);
                Destroy(newBullet, 10.0f);
            }
            if (i == 6)
            {
                newBullet = Instantiate(Projectile, Emitters[6].transform.position, Emitters[6].transform.rotation);
                Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(newBullet.transform.forward * force);
                Destroy(newBullet, 10.0f);
            }
            if (i == 7)
            {
                newBullet = Instantiate(Projectile, Emitters[7].transform.position, Emitters[7].transform.rotation);
                Temporary_RigidBody = newBullet.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(newBullet.transform.forward * force);
                Destroy(newBullet, 10.0f);
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
            text.text = "Jotun, The Ice Genie";
        }
        else
        {
            BossBanner = GameObject.Find("GameControl/Player/InGameCanvas/BossBanner");
            BossBanner.SetActive(false);
        }
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
}
