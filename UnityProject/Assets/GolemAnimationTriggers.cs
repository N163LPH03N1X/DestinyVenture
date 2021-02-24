using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAnimationTriggers : MonoBehaviour {

    FireGolem GolemEnemy;
    IceGolem GolemEnemy2;
    public GameObject ScriptObject;
    public SphereCollider punchCollision;
    PlayerStats PlayST;
    bool shake;

    public bool Fire;
    public bool Ice;


    public void Start()
    {
        punchCollision.enabled = false;
    }
    public void Update()
    {
        if (shake)
        {
            PlayST = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
            PlayST.QuakeState();
        }

    }

   
    public void Attacking(int num)
    {
        if (Fire)
        {
            GolemEnemy = ScriptObject.GetComponent<FireGolem>();
            if (num == 1)
            {
                GolemEnemy.Attacked(true);
            }
            else
            {
                GolemEnemy.Attacked(false);
            }
        }
        else if (Ice)
        {
            GolemEnemy2 = ScriptObject.GetComponent<IceGolem>();
            if (num == 1)
            {
                GolemEnemy2.Attacked(true);
            }
            else
            {
                GolemEnemy2.Attacked(false);
            }
        }
    }
   
    public void StartWalking(int num)
    {
        if (Fire)
        {
            if (num > 0)
            {
                shake = false;
                GolemEnemy = ScriptObject.GetComponent<FireGolem>();
                GolemEnemy.SwitchState(FireGolem.EnemyState.isWalking);
            }
            else
            {
                shake = false;
                GolemEnemy = ScriptObject.GetComponent<FireGolem>();
                GolemEnemy.SwitchState(FireGolem.EnemyState.notWalking);
            }
        }
        else if (Ice)
        {
            if (num > 0)
            {
                shake = false;
                GolemEnemy2 = ScriptObject.GetComponent<IceGolem>();
                GolemEnemy2.SwitchState(IceGolem.EnemyState.isWalking);
            }
            else
            {
                shake = false;
                GolemEnemy2 = ScriptObject.GetComponent<IceGolem>();
                GolemEnemy2.SwitchState(IceGolem.EnemyState.notWalking);
            }
        }
    
    }
    public void ResetAttackRateFire()
    {
        GolemEnemy.resetAttackRate();
        
    }
    public void ResetAttackRateIce()
    {
        GolemEnemy2.resetAttackRate();
    }
   
    public void EnablePunchCollider(int num)
    {
        if (num == 1)
            punchCollision.enabled = true;
        else
            punchCollision.enabled = false;
    }
}
