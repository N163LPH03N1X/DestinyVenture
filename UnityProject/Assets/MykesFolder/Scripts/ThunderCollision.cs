using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderCollision : MonoBehaviour {

    Rigidbody rb;
    IceGolem iceGolem;
    FireGolem fireGolem;
    EyeEnemy enemy;
    PhantomEnemy PhantomEnemy;
    FinalBoss finalBoss;
    GhostBoss GhostBoss;
    IceBoss boss;
    AudioSource audioSrc;
    GameObject closest;
    Vector3 Target;
    public float timer;
    public float distance; 
    public float maxDistance;

    public bool enemyDead;
    public float speed;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget("Eye");
        FindClosestTarget("BossEnemy");
        FindClosestTarget("EnemyPhantom");
        FindClosestTarget("GhostBoss");
        FindClosestTarget("FinalBoss");
        FindClosestTarget("FireGolem");
        FindClosestTarget("IceGolem");
        distance = Vector3.Distance(transform.position, Target);

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            timer = 0;
            Destroy(gameObject);
        }

        if (distance < maxDistance)
        {
            MoveToEnemy();
        }
   
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("FireGolem"))
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = true;

            fireGolem = other.gameObject.GetComponent<FireGolem>();
            if (fireGolem != null)
            {
                fireGolem.DoDamage(4);
                Destroy(gameObject, 2);
            }
        }
        else if(other.gameObject.CompareTag("IceGolem"))
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = true;

            iceGolem = other.gameObject.GetComponent<IceGolem>();
            if (iceGolem != null)
            {
                iceGolem.DoDamage(4);
                Destroy(gameObject, 2);
            }
        }
        else if(other.gameObject.CompareTag("Eye"))
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = true;
            enemy = other.gameObject.GetComponent<EyeEnemy>();
            if (enemy != null)
            {
                enemy.DoDamage(4);
                Destroy(gameObject, 2);
            }
        }
        else if(other.gameObject.CompareTag("GhostBoss"))
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = true;

            GhostBoss = other.gameObject.GetComponent<GhostBoss>();
            if (GhostBoss != null)
            {
                GhostBoss.DoDamage(4);
                Destroy(gameObject, 2);
            }
        }
        else if (other.gameObject.CompareTag("EnemyPhantom"))
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = true;

            PhantomEnemy = other.gameObject.GetComponent<PhantomEnemy>();
            if (PhantomEnemy != null)
            {
                PhantomEnemy.DoDamage(4);
                Destroy(gameObject, 2);
            }
        }
        else if(other.gameObject.CompareTag("FinalBoss"))
        {
            rb.useGravity = true;
        }
        else if(other.gameObject.CompareTag("BossIceField"))
        {
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
    public void MoveToEnemy()
    {
        if (FindClosestTarget("Eye") != null && isActiveAndEnabled)
        {
            var lookRot = Quaternion.LookRotation(FindClosestTarget("Eye").transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 5f);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else if (FindClosestTarget("EnemyPhantom") != null && isActiveAndEnabled)
        {
            var lookRot = Quaternion.LookRotation(FindClosestTarget("EnemyPhantom").transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 5f);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else if (FindClosestTarget("GhostBoss") != null && isActiveAndEnabled)
        {
            var lookRot = Quaternion.LookRotation(FindClosestTarget("GhostBoss").transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 5f);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else if (FindClosestTarget("FinalBoss") != null && isActiveAndEnabled)
        {
            var lookRot = Quaternion.LookRotation(FindClosestTarget("FinalBoss").transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 5f);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else if (FindClosestTarget("FireGolem") != null && isActiveAndEnabled)
        {
            var lookRot = Quaternion.LookRotation(FindClosestTarget("FireGolem").transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 5f);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else if (FindClosestTarget("IceGolem") != null && isActiveAndEnabled)
        {
            var lookRot = Quaternion.LookRotation(FindClosestTarget("IceGolem").transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 5f);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
