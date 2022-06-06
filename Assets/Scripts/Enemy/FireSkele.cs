using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class FireSkele : Enemy
{
    private float wanderRadius = 50;
    private float wanderTimer = 8;
    private Transform target;
    private NavMeshAgent agent;
    private float timer;
    public ParticleSystem explosion;
    private NavMeshAgent navAgent;
    private GameObject player;
    public bool seen = false;
    private ParticleSystem firePartical;
    bool patrolState = true;
    bool coroutineRunning = false;
    bool recentlyExploded = false;
    public GameObject ammoPower;
    private float distance;

    public bool dead = false;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        firePartical = GetComponent<ParticleSystem>();
        timer = wanderTimer;
       
    }
    void Update()
    {
        //Compare player's position
    player = GameObject.Find("Player(Clone)");
    
    if(!dead) 
       {
        if(player)
        {
            distance = Vector3.Distance(transform.position, player.transform.position);
            if(distance <= 15) patrolState = false;
            if(distance > 15) patrolState = true;

            //Boo Behaviour
                if(!seen)
                {
                    // Seek Player
                    if(patrolState == false)
                    {
                        navAgent.SetDestination(player.transform.position);
                    }
                    else
                    {
                       timer += Time.deltaTime;
                        if (timer >= wanderTimer) 
                         {
                             Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                             navAgent.SetDestination(newPos);
                             timer = 0;
                         }
                    }
    
                }
                else
                {
                    navAgent.isStopped = true;
                }

                if(distance < 0)
                {
                    distance *= -1;
                }
                if( distance < 3f)
                {
                    explode();
                }
                else
                {
                    recentlyExploded = false;
                }
         }
       }
    }

    public void switchState(bool s)
    {
        if(!coroutineRunning && dead == false)
        {
            seen = s;
            navAgent.isStopped = s;
            anim.SetBool("Seen", s);
        }
    }

    private void OnCollisionEnter(Collision other) {

    if(other.transform.tag == "Bullet" || other.transform.tag == "Weapon")
        {
            if(!dead)
            {
                 navAgent.SetDestination(player.transform.position);
                 anim.Play("Hit");
                 TakeDamage(other.gameObject.GetComponent<Bullet>().damage);
            }
        }
    }
    void explode()
     {
        if (!coroutineRunning && !recentlyExploded)
            StartCoroutine("Explode");
    }

    IEnumerator Explode()
    {
        recentlyExploded = true;
        coroutineRunning = true;
        anim.SetBool("Screaming" , true);
        yield return new WaitForSeconds(1.5f);
        firePartical.Play();
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f);
        foreach (Collider hit in colliders)
        {
                if(hit.transform.tag == "Player")
                {
                    GameManager.instance.playerTakeDamage(50f);
                }
        }
        
        anim.SetBool("Screaming" , false);
        //
        yield return new WaitForSeconds(3.0f);
        //
        if(!dead)
        {
          navAgent.isStopped = false;
          anim.Play("Injured Run");
          coroutineRunning = false;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) 
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
        return navHit.position;
    }


    void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }
        GetComponent<Rigidbody>().isKinematic = !state;
    }

    void setColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach(Collider collider in colliders)
        {
            collider.enabled = state;
        }
        GetComponent<Collider>().enabled = !state;
    }

    public override void Death()
    {
            dead = true;
            Instantiate(ammoPower, transform.position , transform.rotation);
            //Ragdoll for death animation
            GetComponentInParent<Animator>().enabled = false; 
            GetComponentInParent<NavMeshAgent>().enabled = false;
            GetComponentInParent<ParticleSystem>().Stop();
            setRigidbodyState(false);
            setColliderState(true);
            Destroy(gameObject, 5f);
    }
}
