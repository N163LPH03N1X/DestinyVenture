using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossAnimTriggers : MonoBehaviour {

    FinalBoss finBoss;
    public GameObject ScriptObject;
    public SphereCollider punchCollision;
    PlayerStats PlayST;
    bool shake;


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

    public void StartCharging(int num)
    {
        if (num == 1)
        {
            finBoss = ScriptObject.GetComponent<FinalBoss>();
            finBoss.Charging(true);
        }
        else
        {
            finBoss.Charging(false);
        }
        
    }
    public void FireNormal()
    {
        finBoss = ScriptObject.GetComponent<FinalBoss>();
        finBoss.ShootNormal();
    }
    public void FireCharged()
    {
        finBoss = ScriptObject.GetComponent<FinalBoss>();
        finBoss.ShootCharged();
    }
    public void Attacking(int num)
    {
        finBoss = ScriptObject.GetComponent<FinalBoss>();
        if (num == 1)
        {
            finBoss.Attacked(true);
        }
        else
        {
            finBoss.Attacked(false);
        }
    }
    public void Flash(int num)
    {
        if (num == 1)
        {
            finBoss = ScriptObject.GetComponent<FinalBoss>();
            finBoss.AttackFlash(1);
        }
        else
        {
            finBoss = ScriptObject.GetComponent<FinalBoss>();
            finBoss.AttackFlash(0);
        }
      
    }
    public void ChangeCurrentForm()
    {
        finBoss = ScriptObject.GetComponent<FinalBoss>();
        finBoss.ChangeForm();
    }
    public void StartWalking(int num)
    {
        if (num > 0)
        {
            shake = false;
            finBoss = ScriptObject.GetComponent<FinalBoss>();
            finBoss.SwitchState(FinalBoss.EnemyState.isWalking);
        }
        else
        {
            shake = false;
            finBoss = ScriptObject.GetComponent<FinalBoss>();
            finBoss.SwitchState(FinalBoss.EnemyState.notWalking);
        }
    }
    public void SlamPower()
    {
        shake = true;
        finBoss = ScriptObject.GetComponent<FinalBoss>();
        finBoss.PowerSlam();

    }
    public void EnablePunchCollider(int num)
    {
        if (num == 1)
            punchCollision.enabled = true;
        else
            punchCollision.enabled = false;
    }
}
