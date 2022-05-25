using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float mouseSensivity  = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    GameObject tempSkeleton ;
    Camera cam;
    void Awake()
    {
        cam = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

   
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;
        xRotation -= mouseY ;
        xRotation = Mathf.Clamp(xRotation, -90f , 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f , 0f);
        playerBody.Rotate(Vector3.up * mouseX);


        //Ray cast 
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
         {
             if(hit.transform.tag =="FireSkeleton")
             {
               tempSkeleton = hit.transform.gameObject;
               tempSkeleton.GetComponent<FireSkele>().switchState(true);
             }
             if(hit.transform.tag != "FireSkeleton" || hit.transform.tag == null)
             {
                 try
                 {
                    tempSkeleton.GetComponent<FireSkele>().switchState(false);
                 }
                 catch
                 {
                     Debug.Log("You did not look at a skeleton yet");
                 }
             }
         }
    }
}
