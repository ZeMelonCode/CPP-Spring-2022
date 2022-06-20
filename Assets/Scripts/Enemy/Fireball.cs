using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject player ;
    public float speed = 10f;
    public float turnSpeed = 5000f;
    private bool ready = false;
    Rigidbody rb;

    public virtual void Start()
    {
        SoundManager.instance.PlayFireSpawn();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
        GameObject[] fireSkeles = GameObject.FindGameObjectsWithTag("FloatingSkull");
        foreach(GameObject fireSkele in fireSkeles)
        {
            Physics.IgnoreCollision(fireSkele.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), true);
        }
        try
        {
            GameObject[] ghostShips = GameObject.FindGameObjectsWithTag("GhostShip");
            foreach(GameObject ghostShip in ghostShips)
            {
                Physics.IgnoreCollision(ghostShip.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), true);
            }
        }
        catch
        {
            Debug.Log("GhostShipIsDead");
        }

        StartCoroutine(Suspence());
    }
    private void FixedUpdate()
    {
        if(ready)
        {
            rb.velocity = transform.forward * speed;
            Quaternion ballRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, ballRotation , turnSpeed));
        }
    }

    public virtual void OnCollisionEnter(Collision other) 
    {
        if(other.transform.tag == "Player")
        {
            GameManager.instance.playerTakeDamage(10f);
            Destroy(gameObject);
        }
    }

    IEnumerator Suspence()
    {
        yield return new WaitForSeconds(1.5f);
        SoundManager.instance.PlayFireThrow();
        ready = true;
    }
}
