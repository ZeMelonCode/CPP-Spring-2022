using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDamage : MonoBehaviour
{
    
    private void OnTriggerStay(Collider other) 
    {
        if(other.tag == "Player")
        {
            if(GameManager.instance.GhostShipDead == false)
            {
                if(GameManager.instance.playerInstance)
                GameManager.instance.playerTakeDamage(0.1f);
            }
        }
    }
}
