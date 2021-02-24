using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class IceGolem : MonoBehaviour, ISwordHittable
{
    PlayerStats playST;
    public Animator anim;
    AudioSource audioSrc;
    NavMeshAgent nav;
    Rigidbody rb;
    public Vector3 playerPosition;
    public GameObject itemDropPos;
    public GameObject BossBody;
    SkinnedMeshRenderer meshRend;
    public SkinnedMeshRenderer[] meshRends;
    SphereCollider enemyCollider;
    public SphereCollider armCollider;
    public AudioClip swordhit;

    public AudioClip death;
    public AudioClip swipe;

    public float Speed;

    public int health;
    public float awarenessRange;
    public float meleeRange;

    public float attackRate;
    float nextAttack;

    public bool isWalking;
    public bool isIdle;
    public Light glow;

    public int idlestate;
    public float idleDurration;
    public float flashTime = 0.2f;
    float timeFlash;
    public bool flashEnemy;
    Color OrgColor;
    bool returnColor;
    public float damping;

    public bool Attacking;
    Vector3 CurrentPos;
    float navOrgStopDist;
    bool repeat;
    public bool Dying;
    public float fadePerSecond;
    bool PhantomBreastEnabled;

    public GameObject VitaSMPickupPrefab;
    public GameObject VitaMDPickupPrefab;
    public GameObject VitaLGPickupPrefab;
    public GameObject HealSMPickupPrefab;
    public GameObject HealMDPickupPrefab;
    public GameObject HealLGPickupPrefab;
    public GameObject PowGemSLPickupPrefab;
    public GameObject PowGemGDPickupPrefab;
    public GameObject GoldPrefab;

    // Use this for initialization
    void Start()
    {

        audioSrc = GetComponent<AudioSource>();
        nav = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        nav.speed = Speed;
        navOrgStopDist = nav.stoppingDistance;
        meshRend = BossBody.GetComponent<SkinnedMeshRenderer>();
        OrgColor = BossBody.GetComponent<SkinnedMeshRenderer>().material.color;
        timeFlash = flashTime;

        enemyCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame

    void Update()
    {
        PhantomBreastEnabled = Breast.PhantBreastActive;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        CurrentPos = transform.position;
        if (isWalking)
        {
            nav.isStopped = false;

            if (isIdle)
            {
                anim.SetFloat("WalkSpeed", 1.0f);
                nav.speed = Speed;
            }
            else if(!PhantomBreastEnabled)
            {
                anim.SetFloat("WalkSpeed", 3.0f);
                nav.speed = Speed * 5;
                var lookPos = playerPosition - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
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
        
        if (Vector3.Magnitude(gameObject.transform.position - playerPosition) > awarenessRange && !Dying)
        {
            isIdle = true;      
            IdleMovement();
        }
        else if (Vector3.Magnitude(gameObject.transform.position - playerPosition) <= awarenessRange && Vector3.Magnitude(gameObject.transform.position - playerPosition) > meleeRange && !Dying && !PhantomBreastEnabled)
        {
           
            isIdle = false;
            SwitchState(EnemyState.walk);
        }
        
        else if (Vector3.Magnitude(gameObject.transform.position - playerPosition) <= meleeRange && !Dying && !PhantomBreastEnabled)
        {
            isIdle = false;
            if (Time.time > nextAttack)
            {
                nav.isStopped = true;
                SwitchState(EnemyState.melee);
            }
            else
            {
                SwitchState(EnemyState.idle);
            }
        }

        if (flashEnemy)
        {
            if (flashTime > 0)
            {
                flashTime -= Time.deltaTime;
                meshRend.material.color = Color.red;
            }
            else if (flashTime < 0)
            {
                meshRend.material.color = OrgColor;
                flashTime = timeFlash;
                flashEnemy = false;
            }
        }
        if (Dying)
        {
            nav.speed = 0;
            nav.isStopped = true;
            var color = meshRend.material.color;
            SetupMaterialWithBlendMode(meshRend.material, BlendMode.Fade);
            meshRend.material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
            if (color.a <= 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void DoDamage(int amount)
    {
        health -= amount;
        flashEnemy = true;
        audioSrc.PlayOneShot(swordhit);
        if (health < 0)
        {

            isWalking = false;
            Dying = true;
            playST = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
           
            if (PhantomBreastEnabled)
            {
                playST.gainExp(1050 * 2);
            }
            else
            {
                playST.gainExp(1050);
            }
            DropPickup();
            audioSrc.volume = Random.Range(0.8f, 1.0f);
            audioSrc.pitch = Random.Range(0.8f, 1.0f);
            audioSrc.PlayOneShot(death);
            SwitchState(EnemyState.down);
            //StartCoroutine(Fading(true));
            enemyCollider.enabled = false;
            armCollider.enabled = false;
            health = 0;
        }
    }
    public void OnGetHitBySword(int hitValue)
    {
        DoDamage(hitValue);
    }
    public enum EnemyState { down, idle, walk, melee, isWalking, notWalking }

    public void SwitchState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.down:
                {
                    isWalking = false;
                    anim.SetTrigger("Down");
                    anim.SetBool("Walk", false);
                    anim.SetBool("Idle", false);
                    anim.SetBool("Melee", false);
                    break;
                }
            case EnemyState.walk:
                {
                    isWalking = true;
                    anim.SetBool("Walk", true);
                    anim.SetBool("Idle", false);
                    anim.SetBool("Melee", false);
                    break;
                }
            case EnemyState.idle:
                {
                    isWalking = false;
                    anim.SetBool("Walk", false);
                    anim.SetBool("Idle", true);
                    anim.SetBool("Melee", false);
                    break;
                }
            case EnemyState.melee:
                {
                    isWalking = false;
                    anim.SetBool("Walk", false);
                    anim.SetBool("Idle", false);
                    anim.SetBool("Melee", true);
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
        }
    }
    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Fire"))
        {
            DoDamage(3);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Thunder"))
        {
            DoDamage(4);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("StormShield"))
        {
            DoDamage(5);
            Destroy(collision.gameObject);
        }
    }

    IEnumerator Fading(bool On)
    {
        Dying = true;
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
    public void resetAttackRate()
    {
        nextAttack = Time.time + attackRate;
    }

    public void IdleMovement()
    {
        if (idleDurration > 0)
        {
            Vector3 dir;
            idleDurration -= Time.deltaTime;
            if (idlestate == 1)
            {
                SwitchState(EnemyState.idle);
            }
            else if (idlestate == 2)
            {
                nav.updateRotation = true;
                dir = Vector3.forward;
                var lookPos = dir;
                var rotation = Quaternion.LookRotation(lookPos);
                SwitchState(EnemyState.walk);
                lookPos.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
                nav.Move(dir * (Speed * Time.deltaTime));
            }
            else if (idlestate == 3)
            {
                nav.updateRotation = true;
                dir = Vector3.back;
                var lookPos = dir;
                var rotation = Quaternion.LookRotation(lookPos);
                SwitchState(EnemyState.walk);
                lookPos.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
                nav.Move(dir * (Speed * Time.deltaTime));
            }
            else if (idlestate == 4)
            {
                nav.updateRotation = true;
                dir = Vector3.left;
                var lookPos = dir;
                var rotation = Quaternion.LookRotation(lookPos);
                SwitchState(EnemyState.walk);
                lookPos.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
                nav.Move(dir * (Speed * Time.deltaTime));
            }
            else if (idlestate == 5)
            {
                nav.updateRotation = true;
                dir = Vector3.right;
                var lookPos = dir;
                var rotation = Quaternion.LookRotation(lookPos);
                SwitchState(EnemyState.walk);
                lookPos.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
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
