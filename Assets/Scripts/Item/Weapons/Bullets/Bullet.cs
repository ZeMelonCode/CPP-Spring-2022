using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float _damage;
    public float damage
    {
        get { return _damage; }
        set
        {
            _damage = value;

            if (_damage > damage)
                _damage = damage;
        }
    }
    // Start is called before the first frame update
    virtual public void Awake()
    {
        _damage = 10f; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void applyDebuff()
    {
        //Base bullet doesn't have debuffs
    }
}
