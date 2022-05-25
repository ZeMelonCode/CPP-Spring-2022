using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool verbose;

    [SerializeField] protected int maxHealth;

    private EnemyHealthBar healthBar;
    protected float _health;
    private bool isDamaged;
    protected SpriteRenderer sr;
    protected Animator anim;

    public float health
    {
        get { return _health; }
        set
        {
            _health = value;

            if (_health > maxHealth)
                _health = maxHealth;

            if (_health <= 0)
                Death();
        }
    }

    public virtual void Death()
    {
        if (verbose)
            Debug.Log("Can be overriden in child classes to implement their own game over/death");
    }

    public virtual void TakeDamage(float damage)
    {
        if(!isDamaged)
        {
            isDamaged = true;
            healthBar.gameObject.SetActive(true);
        }
        health -= damage;
        healthBar.setHealth(health);
    }

    public virtual void DestroyObj()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        anim = GetComponent<Animator>();

        if (maxHealth <= 0)
            maxHealth = 10;

        health = maxHealth;
        healthBar.SetMaxHealth(health);
        healthBar.gameObject.SetActive(false);
    }
    
}
