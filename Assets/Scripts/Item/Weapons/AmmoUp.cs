using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUp : MonoBehaviour
{ 
  private void OnTriggerStay(Collider other) 
  {
      if(other.transform.tag == "Player")
      {
          other.GetComponentInChildren<GunSystem>().ChangeFireRate();
          Destroy(gameObject);
      }
  }
}
