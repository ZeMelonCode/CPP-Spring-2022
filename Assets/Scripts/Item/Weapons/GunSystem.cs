using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GunSystem : Weapon
{
    //Gun stats
    public ParticleSystem noseFlash;
    public Animator anim;
    public int damage;
    public float timeBetweenShooting, spread, range , reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    int bulletsLeft, bulletsShot;
    bool shooting, readyToShoot, reloading;
    public Camera fpsCam;
    bool coroutineRunning = false;
    public Transform attackPoint;
    public RaycastHit raycastHit;
    public LayerMask whatIsEnemy;
    public GameObject bullet;
    private Collider gunCollider;
    bool inMeleeAttack = false;
    public GameObject ammoBoost;
    Vector3 bulletSpeed;
    private void Awake()
    {
           // ammoBoost = GameObject.Find("AmmoUp");
           //ammoBoost.SetActive(false);
            anim = GetComponent<Animator>();
            try
            {
                gunCollider = GetComponent<Collider>();
            }
            catch
            {
                Debug.Log("Collider");
            }
            bulletsLeft = magazineSize;
            readyToShoot = true;
    }
    private void Update()
    {
        MyInput();  
    }
    private void MyInput()
    {
        shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            noseFlash.Play();
            Shoot();
        }

        if(Input.GetKey(KeyCode.Mouse1) && inMeleeAttack == false)
        {
            StartCoroutine("Melee");
        }
        
    }
    
    
    private void Shoot()
    {
        //Spread 

        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));

        Vector3 targetPoint;
      
        targetPoint = ray.GetPoint(50);
    
        Vector3 direction = targetPoint - attackPoint.position + new Vector3(x,y,0);
        Debug.DrawRay(attackPoint.position, targetPoint, Color.green);

        //Instanciate the bullets

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, attackPoint.rotation);

        currentBullet.transform.forward = direction.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * range, ForceMode.Impulse);

        Destroy(currentBullet, 2f);

        readyToShoot = false;

        bulletsLeft --;

        bulletsShot --;
        
        //Shooting animation
        anim.speed = 1 + timeBetweenShooting;
        anim.Play("Shot");


        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }

    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinish", reloadTime);
    }

    private void ReloadFinish()
    {
        reloading = false;
        bulletsLeft = magazineSize;
    }
    IEnumerator Melee()
    {
        anim.Play("MeleeAttack");
        inMeleeAttack = true;
        gunCollider.enabled = true;
        yield return new WaitForSeconds(1f);
        inMeleeAttack = false;
        gunCollider.enabled = false;
    }

    public void ChangeFireRate()
    {
        if (!coroutineRunning)
            StartCoroutine("ChangeFireRatePower");
        else
        {
            StopCoroutine("ChangeFireRatePower");
            timeBetweenShooting *= 2;
            StartCoroutine("ChangeFireRatePower");
        }
    }

    IEnumerator ChangeFireRatePower()
    {
        coroutineRunning = true;
        timeBetweenShooting /= 2;
       // ammoBoost.SetActive(true);

        yield return new WaitForSeconds(5.0f);

       // ammoBoost.SetActive(false);

        timeBetweenShooting *= 2;
        coroutineRunning = false;
    }

}
