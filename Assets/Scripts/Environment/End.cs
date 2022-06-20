using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
   private void OnTriggerEnter(Collider other) 
   {
       if(other.gameObject.tag == "Player")
       {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Win");
       }
   }
}
