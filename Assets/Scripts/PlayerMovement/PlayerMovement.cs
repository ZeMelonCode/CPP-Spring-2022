using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller ;
    public float speed = 12f;
    public float gravity = -9.81f;
    public bool canSprint = true;
    public float jumpHeight = 10f;
    Vector3 velocity;
    void Update()
    {
        
         float x = Input.GetAxisRaw("Horizontal");
         float z = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.LeftShift) && canSprint == true)
        {
            speed = 20f;
        }
         if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 12f;
        }
         Vector3 move = transform.right * x + transform.forward * z;
         controller.Move(move * speed * Time.deltaTime);
         velocity.y += gravity * Time.deltaTime;
         controller.Move(velocity * Time.deltaTime);
    }
}
