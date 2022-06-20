using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostShip : Enemy
{
    [Header("FOV")]
    public bool canSeePlayer;
    public LayerMask targetMask;
    public LayerMask obstuctionMask;
    public float radius;
    [Range(0,360)]
    public float angle;

    [Header("Shooting")]
    bool canFire = true;
    private Transform target;
    public GameObject player;
    private float timer;
    public bool seen = false;
    bool patrolState = true;
    bool coroutineRunning = false;
    bool recentlyFired = false;
    private float distance;
    public GameObject FireBallPrefab;
    [Header("FireBallSpawnPoints")]
    public Transform[] fireBallSpawns;

    public ParticleSystem BlowUpPartical;

    public bool dead = false;

    public GameObject ChaliceOfDeath;
   
    public override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ScanRoutine());     
    }
    void Update()
    {
        if(canSeePlayer && canFire)
        {
            StartCoroutine(FireDelay());
        }
    }


    private void OnCollisionEnter(Collision other) {

    if(other.transform.tag == "Bullet" || other.transform.tag == "Weapon")
        {
            if(!dead)
            {
                 TakeDamage(other.gameObject.GetComponent<Bullet>().damage);
            }
        }
    }


    private IEnumerator ScanRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return wait;
            FieldOFViewCheck();
        }
    }


     private IEnumerator FireDelay()
    {
        for(int i = 0; i <= 4 ; i ++)
        {
            Fire(fireBallSpawns[i]);
        }

        canFire = false;
        yield return new WaitForSeconds(10f);
        canFire = true;
        
    }

    void Fire(Transform spawn)
    {
        GameObject Fireball = Instantiate(FireBallPrefab, spawn.position, transform.rotation);
        Destroy(Fireball, 10f);
    }

    private void FieldOFViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget) < angle/2)
            {
                float distanToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, directionToTarget, distanToTarget, obstuctionMask))
                {
                    canSeePlayer = true;
                    Vector3 playerRange = (target.position - transform.position).normalized;
                    transform.forward = playerRange;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if(canSeePlayer)
        {
                canSeePlayer = false;
        }
    }

    public override void Death()
    {
            dead = true;
            //Ragdoll for death animation
            BlowUpPartical.Play();
            Destroy(gameObject, 1f);
            GameManager.instance.GhostShipDead = true;
            Instantiate(ChaliceOfDeath, transform.position, transform.rotation);
    }
}
