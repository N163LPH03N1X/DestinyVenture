using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PhantomEnemy : MonoBehaviour, ISwordHittable
{

    [Header("Item Drops")]
    public GameObject VitaSMPickupPrefab;
    public GameObject VitaMDPickupPrefab;
    public GameObject VitaLGPickupPrefab;
    public GameObject HealSMPickupPrefab;
    public GameObject HealMDPickupPrefab;
    public GameObject HealLGPickupPrefab;
    public GameObject PowGemSLPickupPrefab;
    public GameObject PowGemGDPickupPrefab;
    public GameObject GoldPrefab;
    int RandomNumber;
    [SerializeField]
    [Space]
    [Header("Enemy Control")]
    int movementSpeed = 3;
    private bool isMoving;
    

    public NavMeshAgent Nav;
    public GameObject Ground;
    PlayerStats playST;
    SphereCollider sphereColid;
    GameObject Player;
    public GameObject Trigger;
    Rigidbody rb;
    Vector3 resetVector;

    public float fallingSpeed;
    public float FallDistance;
    public float fadePerSecond;


    AudioSource audioSrc;
    public AudioClip monsterHit;
    public AudioClip encounter;
    public AudioClip monsterDeath;
    
    int health;
    bool Dying;

    [HideInInspector]
    Vector3 CameraPosition;
    
    public float awarenessRange;
    public float stoppingRange;
    float flashobjectTimer;
    public float flashObjectTime = 0.1f;
    bool flashObject;
    public bool enemyFroze;
    bool returnColor;
    public int enemyState;
    public float Duration;
    bool idling;
    bool startIdle;
   
    bool PhantomBreastEnabled;
    bool PlayerFound = false;
    public GameObject PhantomObject;
    public GameObject EnemyObject;
    public GameObject EnemyMask;
    public GameObject EnemyFace;
    
    Color meshRendOrg;
    Color maskOrgColor;
    Color faceOrgColor;
    MeshRenderer meshRend;
    MeshRenderer maskRend;
    MeshRenderer faceRend;
    
    public int hitCount;
    PlayerController PlayCtrl;
    bool DrainPlayer;
    public Vector3 PlayerPosition;
    public Vector3 EnemyPosition;

    void Start()
    {
        resetVector = Vector3.zero;
        idling = true;
        startIdle = true;
       
        Dying = false;
        enemyFroze = false;
        rb = GetComponent<Rigidbody>();
        flashobjectTimer = flashObjectTime;
        health = 14;

        hitCount = 2;
        audioSrc = GetComponent<AudioSource>();
        meshRend = EnemyObject.GetComponent<MeshRenderer>();
        maskRend = EnemyMask.GetComponent<MeshRenderer>();
        faceRend = EnemyFace.GetComponent<MeshRenderer>();
     
        sphereColid = GetComponent<SphereCollider>();
        maskOrgColor = maskRend.material.GetColor("_EmissionColor");
        meshRendOrg = meshRend.material.GetColor("_EmissionColor");
        faceOrgColor = faceRend.material.GetColor("_EmissionColor");
     
    }
    void Update()
    {
        EnemyPosition = transform.position;
        Player = GameObject.FindGameObjectWithTag("Player");
        PhantomBreastEnabled = Breast.PhantBreastActive;
        CameraPosition = Camera.main.transform.position;
        PlayerPosition = Player.transform.position;

        if (hitCount > 0 && !flashObject)
        {
            maskRend.material.SetColor("_EmissionColor", maskOrgColor);
            meshRend.material.SetColor("_EmissionColor", meshRendOrg);
            faceRend.material.SetColor("_EmissionColor", faceOrgColor);
        }
        else if (hitCount <= 0 && !flashObject && meshRend != null)
        {
            meshRend.material.SetColor("_EmissionColor", Color.gray);
            faceRend.material.SetColor("_EmissionColor", Color.grey);
        }

        if (Vector3.Magnitude(EnemyPosition - PlayerPosition) < awarenessRange && !Dying && !PhantomBreastEnabled)
        {
            Nav.speed = 10;
            PlayerFound = true;
            rb.velocity = Vector3.zero;
            var lookPos = PlayerPosition - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
            Nav.destination = PlayerPosition;
        }
        if (Vector3.Magnitude(EnemyPosition - PlayerPosition) >= awarenessRange && !Dying)
        {
            PlayerFound = false;
            Nav.speed = 0;
            Nav.destination = EnemyPosition;
        }

        if (Vector3.Magnitude(EnemyPosition - PlayerPosition) < stoppingRange && !Dying && !PhantomBreastEnabled)
        {
            PlayCtrl = Player.GetComponent<PlayerController>();
            Nav.speed = 0;
            if (!DrainPlayer)
            {
                audioSrc.PlayOneShot(encounter);
                PlayCtrl.reducePlayer(true);
                DrainPlayer = true;
            }
        }
        else if (Vector3.Magnitude(EnemyPosition - PlayerPosition) >= stoppingRange && !Dying)
        {
            PlayCtrl = Player.GetComponent<PlayerController>();
            if (DrainPlayer)
            {
                PlayCtrl.reducePlayer(false);
                DrainPlayer = false;
            }
           
        }
       
        if (flashObject)
        {
            flashobjectTimer -= Time.deltaTime;
            if (hitCount > 0)
            {
                maskRend.material.SetColor("_EmissionColor", Color.red);
            }
            else
            {
                faceRend.material.SetColor("_EmissionColor", Color.red);
                meshRend.material.SetColor("_EmissionColor", Color.red);
            }
            
        }
        if (flashobjectTimer < 0)
        {
            if (hitCount == 0)
            {
                meshRend.material.SetColor("_EmissionColor", Color.white);
                faceRend.material.SetColor("_EmissionColor", Color.white);
            }

            else if (hitCount > 0)
            {
                maskRend.material.SetColor("_EmissionColor", maskOrgColor);
            }
            else
            {
                meshRend.material.SetColor("_EmissionColor", meshRendOrg);
                faceRend.material.SetColor("_EmissionColor", faceOrgColor);
            }
            flashObject = false;
            flashobjectTimer = flashObjectTime;
        }
        if (Dying)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(transform.forward * -50);
            rb.AddForce(transform.up * -20);
            Nav.speed = 0;
            Nav.isStopped = true;
            var color = meshRend.material.color;
            var color2 = faceRend.material.color;
            SetupMaterialWithBlendMode(meshRend.material, BlendMode.Fade);
            SetupMaterialWithBlendMode(faceRend.material, BlendMode.Fade);
            meshRend.material.color = new Color(color.r, color.g, color.b, color.a - (0.5f * Time.deltaTime));
            faceRend.material.color = new Color(color2.r, color2.g, color2.b, color2.a - (0.5f * Time.deltaTime));
            if (color.a <= 0.1f)
            {
                meshRend.material.color = new Color(color.r, color.g, color.b, color.a - (0));
                faceRend.material.color = new Color(color2.r, color2.g, color2.b, color2.a - (0));
                Destroy(PhantomObject, 3);
            }
        }
    }
    public void OnGetHitBySword(int hitValue)
    {
        if (hitCount > 0)
        {
            HitDamage(1);
        }
        else
        {
            DoDamage(hitValue);
        }
    }
    public void DoDamage(int amount)
    {
        audioSrc.volume = Random.Range(0.8f, 1.0f);
        audioSrc.pitch = Random.Range(0.8f, 1.0f);
        audioSrc.PlayOneShot(monsterHit);
        health -= amount;
        flashObject = true;
        if (health <= 0)
        {
            playST = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
            playST.ShutOffDrainScreen();
            PlayCtrl = Player.GetComponent<PlayerController>();
            Nav.speed = 0;
            PlayCtrl.reducePlayer(false);
            DrainPlayer = false;
            Dying = true;
            DropPickup();
            if (PhantomBreastEnabled)
            {
                playST.gainExp(500 * 2);
            }
            else
            {
                playST.gainExp(500);
            }
            audioSrc.volume = Random.Range(0.8f, 1.0f);
            audioSrc.pitch = Random.Range(0.8f, 1.0f);
            audioSrc.PlayOneShot(monsterDeath);
            sphereColid.enabled = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            if (hitCount > 0)
            {
                HitDamage(3);
            }
            else
            {
                DoDamage(3);
            }
            flashObject = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Thunder"))
        {
            if (hitCount > 0)
            {
                HitDamage(4);
            }
            else
            {
                DoDamage(4);
            }
            flashObject = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("StormShield"))
        {
            if (hitCount > 0)
            {
                HitDamage(5);
            }
            else
            {
                DoDamage(5);
            }
            flashObject = true;
            Destroy(collision.gameObject);
        }
    }
    public void HitDamage(int amount)
    {
        hitCount -= amount;
        flashObject = true;
        audioSrc.volume = Random.Range(0.8f, 1.0f);
        audioSrc.pitch = Random.Range(0.8f, 1.0f);
        audioSrc.PlayOneShot(monsterHit);
        if (hitCount <= 0)
        {
            hitCount = 0;
            EnemyMask.SetActive(false);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, awarenessRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stoppingRange);
    }
    public void DropPickup()
    {
        int[] VitaSMnum = { 2, 4, 6 };
        int[] VitaMDnum = { 17, 19 };
        int[] VitaLGnum = { 25, };
        int[] HealSMnum = { 1, 3, 5 };
        int[] HealMDnum = { 18, 20 };
        int[] HealLGnum = { 26 };
        int[] PowGemSLnum = { 27, 29 };
        int[] PowGemGDnum = { 32 };
        int[] Goldnum = { 8, 10, 12, 14, 16, 21, 23, 28, 7, 9, 11, 13, 15, 22, 24, 30, 31, 33, 35, 36, 34, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 };

        RandomNumber = Random.Range(1, 50);

        for (int i = 0; i < 1; i++)
        {
            for (int vsm = 0; vsm < VitaSMnum.Length; vsm++)
            {
                if (VitaSMnum[vsm] == RandomNumber)
                {
                    Instantiate(VitaSMPickupPrefab, Ground.transform.position, Quaternion.identity);
                }
            }
            for (int vmd = 0; vmd < VitaMDnum.Length; vmd++)
            {
                if (VitaMDnum[vmd] == RandomNumber)
                {
                    Instantiate(VitaMDPickupPrefab, Ground.transform.position, Quaternion.identity);
                }
            }
            for (int vlg = 0; vlg < VitaLGnum.Length; vlg++)
            {
                if (VitaLGnum[vlg] == RandomNumber)
                {
                    Instantiate(VitaLGPickupPrefab, Ground.transform.position, Quaternion.identity);
                }
            }
            for (int hsm = 0; hsm < HealSMnum.Length; hsm++)
            {
                if (HealSMnum[hsm] == RandomNumber)
                {
                    Instantiate(HealSMPickupPrefab, Ground.transform.position, Quaternion.identity);
                }
            }
            for (int hmd = 0; hmd < HealMDnum.Length; hmd++)
            {
                if (HealMDnum[hmd] == RandomNumber)
                {
                    Instantiate(HealMDPickupPrefab, Ground.transform.position, Quaternion.identity);
                }
            }
            for (int hlg = 0; hlg < HealLGnum.Length; hlg++)
            {
                if (HealLGnum[hlg] == RandomNumber)
                {
                    Instantiate(HealLGPickupPrefab, Ground.transform.position, Quaternion.identity);
                }
            }
            for (int pgsl = 0; pgsl < PowGemSLnum.Length; pgsl++)
            {
                if (PowGemSLnum[pgsl] == RandomNumber)
                {
                    Instantiate(PowGemSLPickupPrefab, Ground.transform.position, Quaternion.identity);
                }
            }
            for (int pggd = 0; pggd < PowGemGDnum.Length; pggd++)
            {
                if (PowGemGDnum[pggd] == RandomNumber)
                {
                    Instantiate(PowGemGDPickupPrefab, Ground.transform.position, Quaternion.identity);
                }
            }
            for (int gold = 0; gold < Goldnum.Length; gold++)
            {
                if (Goldnum[gold] == RandomNumber)
                {
                    Instantiate(GoldPrefab, Ground.transform.position, Quaternion.identity);
                }
            }
        }
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
