using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : HealthBar
{
    private Transform cam;
       
       private void Start() 
       {
           try
           {
              cam = GameObject.Find("Main Camera").GetComponent<Transform>();
           }
           catch
           {
               Debug.Log("No camera found");
           }
       }
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
