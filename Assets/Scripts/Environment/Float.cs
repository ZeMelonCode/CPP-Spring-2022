using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    public Rigidbody rb;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;

    public int floaterCount = 4;

    public Transform water;
  
    // Update is called once per frame
    private void FixedUpdate() 
    {
        rb.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);
       if(transform.position.y < 142)
        {
            float displacementMultiplier = Mathf.Clamp01((transform.position.y / depthBeforeSubmerged)) * displacementAmount;
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
        }
    }
}
