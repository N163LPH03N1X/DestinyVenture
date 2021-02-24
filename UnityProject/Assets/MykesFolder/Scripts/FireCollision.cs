using UnityEngine;

public class FireCollision : MonoBehaviour {

    Rigidbody rb;
    IceGolem iceGolem;
    FireGolem fireGolem;
    EyeEnemy EyeEnemy;
    PhantomEnemy PhantomEnemy;
    GhostBoss GhostBoss;
    FinalBoss finalBoss;

    AudioSource audioSrc;
    float Timer;
    public float SetTimer;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
        Timer = SetTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        if (Timer < 0)
        {
            Timer = 0;
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
   
        if (other.gameObject.CompareTag("FireGolem"))
        {
            fireGolem = other.gameObject.GetComponent<FireGolem>();
            if (fireGolem != null)
            {
                fireGolem.DoDamage(1);
            }
        }
        else if(other.gameObject.CompareTag("IceGolem"))
        {        
            iceGolem = other.gameObject.GetComponent<IceGolem>();
            if (iceGolem != null)
            {
                iceGolem.DoDamage(6);
            }
        }
        else if(other.gameObject.CompareTag("Eye"))
        {
            EyeEnemy = other.gameObject.GetComponent<EyeEnemy>();
            if (EyeEnemy != null)
            {
                EyeEnemy.DoDamage(3);
            }
        }
        else if(other.gameObject.CompareTag("GhostBoss"))
        {
            GhostBoss = other.gameObject.GetComponent<GhostBoss>();
            if (GhostBoss != null)
            {
                GhostBoss.DoDamage(3);
            }
        }
        else if(other.gameObject.CompareTag("EnemyPhantom"))
        {
            PhantomEnemy = other.gameObject.GetComponent<PhantomEnemy>();
            if(PhantomEnemy != null)
            {
                PhantomEnemy.DoDamage(3);
            }
        }
        else if(other.gameObject.CompareTag("FinalBoss"))
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = true;
        }
        else if (other.gameObject.CompareTag("BossIceField"))
        {
            Destroy(gameObject);
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Destroy(gameObject, 3);
        }
        
    }
}
