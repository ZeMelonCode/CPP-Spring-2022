using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
   private void OnTriggerEnter(Collider other) 
   {
       if(other.gameObject.tag == "Player")
       {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
       }
   }
}
