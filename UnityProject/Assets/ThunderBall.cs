using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBall : MonoBehaviour {

    PlayerStats player;
  
    AudioSource audioSrc;
    GameObject closest;
    Vector3 Target;
    float distance;
    public float maxDistance;

    public float speed;
    // Use this for initialization
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget("Player");
        
        distance = Vector3.Distance(transform.position, Target);

        if (distance < maxDistance)
        {
            MoveToEnemy();
        }

    }
    private void OnCollisionEnter()
    {
        Destroy(gameObject, 2);
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
        if (FindClosestTarget("Player") != null)
        {
            var lookRot = Quaternion.LookRotation(FindClosestTarget("Player").transform.position - transform.position);
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
