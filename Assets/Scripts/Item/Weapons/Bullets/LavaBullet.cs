using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBullet : Bullet
{
    // Start is called before the first frame update
    public override void Awake()
    {
        _damage = 50f;
    }

    public override void applyDebuff()
    {
        
    }
}
