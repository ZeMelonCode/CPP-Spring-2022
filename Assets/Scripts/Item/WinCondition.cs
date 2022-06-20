using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : Fireball
{
    public override void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Player")
        {
            GameManager.instance.WinCondition = true;
            Destroy(gameObject);
        }
    }
}
