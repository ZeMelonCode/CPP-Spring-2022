using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlants : MonoBehaviour
{
    private void OnTriggerStay(Collider other) 
    {
        if(other.transform.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().speed = 2f;
            other.GetComponent<PlayerMovement>().canSprint = false;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.transform.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().speed = 12f;
            other.GetComponent<PlayerMovement>().canSprint = true;
        }
    }
}
