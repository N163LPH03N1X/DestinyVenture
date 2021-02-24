using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikeCollision : MonoBehaviour {
    Rigidbody rb;
    PlayerStats playST;
    GhostBoss boss;
    FinalBoss finalBoss;
    EyeEnemy Eye;
    AudioSource audioSrc;
    GameObject closest;
    Vector3 Target;
    public AudioClip iceCollide;
    public AudioClip iceBurn;
    public AudioClip iceHit;
    public float distance;
    public float maxDistance;
    public float speed;
    public bool HitBoss;
    public bool Hit;
    public bool CrystalProjectile;
    public bool finalB;
    public bool ghostB;
    public bool Enemy;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
        HitBoss = false;

    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget("Player");
        if (ghostB)
        {
            FindClosestTarget("GhostBoss");
        }
        else if (finalB)
        {
            FindClosestTarget("FinalBoss");
        }
        else if (Enemy)
        {
            FindClosestTarget("Eye");
        }

       
       
        if (!HitBoss)
        {
            distance = Vector3.Distance(transform.position, Target);

            if (distance < maxDistance)
            {
                MoveToPlayer();
            }
        }
        if (HitBoss)
        {
            distance = Vector3.Distance(transform.position, Target);

            if (distance < maxDistance)
            {
                MoveToBoss();
            }
        }
        if (Hit)
        {
            ShootForward(800);
        }
    }
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            audioSrc.PlayOneShot(iceCollide);
            playST = other.gameObject.GetComponent<PlayerStats>();
            playST.doDamage(5);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("GhostBoss"))
        {
            if (Hit)
            {
                audioSrc.PlayOneShot(iceCollide);
                boss = other.gameObject.GetComponent<GhostBoss>();
                if (CrystalProjectile)
                {
                    boss.HitShieldCounter(1);
                }
                else
                {
                    boss.HitShieldCounter(2);
                }
                Destroy(gameObject);
            }
           
        }
        else if (other.gameObject.CompareTag("FinalBoss"))
        {
            if (Hit)
            {
                audioSrc.PlayOneShot(iceCollide);
                finalBoss = other.gameObject.GetComponent<FinalBoss>();
                finalBoss.DoHitPoints(5);
                Destroy(gameObject);
            }

        }
        else if (other.gameObject.CompareTag("Eye"))
        {
            if (Hit)
            {
                audioSrc.PlayOneShot(iceCollide);
                Eye = other.gameObject.GetComponent<EyeEnemy>();
                if (Eye != null)
                {
                    Eye.FreezeEnemy(true);
                }
                Destroy(gameObject);
            }

        }
        else
        {
            Destroy(gameObject, 5);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("IceBlade"))
        {
            audioSrc.PlayOneShot(iceHit);
            rb.velocity = Vector3.zero;
            HitBoss = true;
            speed = 10;
            Hit = true;
        }
        if (other.gameObject.CompareTag("MagicBlade"))
        {
            audioSrc.PlayOneShot(iceBurn);
            Destroy(gameObject);
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
    public void MoveToPlayer()
    {

            var lookRot = Quaternion.LookRotation(FindClosestTarget("Player").transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 5f);
            transform.position += transform.forward * speed * Time.deltaTime;
    }
    public void MoveToBoss()
    {
        if (ghostB)
        {
            var lookRot = Quaternion.LookRotation(FindClosestTarget("GhostBoss").transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 5f);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else if (finalB)
        {
            var lookRot = Quaternion.LookRotation(FindClosestTarget("FinalBoss").transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 5f);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else if (Enemy)
        {
            var lookRot = Quaternion.LookRotation(FindClosestTarget("Eye").transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 5f);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
      
    }
    void ShootForward(int magnitude)
    {
        if (ghostB)
        {
            transform.LookAt(FindClosestTarget("GhostBoss").transform.position);
            rb.AddForce(transform.forward * magnitude, ForceMode.Acceleration);
            Destroy(gameObject, 10.0f);
        }
        else if (finalB)
        {
            transform.LookAt(FindClosestTarget("FinalBoss").transform.position);
            rb.AddForce(transform.forward * magnitude, ForceMode.Acceleration);
            Destroy(gameObject, 10.0f);
        }
        else if (Enemy)
        {
            transform.LookAt(FindClosestTarget("Eye").transform.position);
            rb.AddForce(transform.forward * magnitude, ForceMode.Acceleration);
            Destroy(gameObject, 10.0f);
        }
    }
}
