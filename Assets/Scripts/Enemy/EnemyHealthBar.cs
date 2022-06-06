using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : HealthBar
{
    private Transform cam;
       
       private void Start() 
       {
           if(!cam)
           {
               findCamera();
           }
           GameManager.instance.onLifeValueChange.AddListener(findCamera); 
       }
    void LateUpdate()
    {
        if(cam)
        {
           transform.LookAt(transform.position + cam.forward);
        }
        else
        {
            findCamera();
        }
    }

    private void findCamera(float f = 0f)
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
}
