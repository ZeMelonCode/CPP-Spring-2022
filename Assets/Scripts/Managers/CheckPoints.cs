using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player" && transform.tag == "Farm" && GameManager.instance.checkPoint >= 0)
        {
            Debug.Log("Checkpoint reached!");
            GameManager.instance.checkPoint = 1;
        }

         if(other.tag == "Player" && transform.tag == "Lagoon" && GameManager.instance.checkPoint >= 1)
        {
            Debug.Log("Checkpoint reached!");
            GameManager.instance.checkPoint = 2;
        }

         if(other.tag == "Player" && transform.tag == "Boat" && GameManager.instance.checkPoint >= 2)
        {
            Debug.Log("Checkpoint reached!");
            GameManager.instance.checkPoint = 3;
        }
    }
}
