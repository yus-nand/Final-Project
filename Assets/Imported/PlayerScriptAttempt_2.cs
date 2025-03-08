using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerScriptAttempt_2 : MonoBehaviour
{
    public Rigidbody hitRB;
    public LayerMask shootableLayer;
    [SerializeField] public LayerMask layerMask;
    [SerializeField] private float speed = 100f;
    [SerializeField] private float mouseSens = 10;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float jumpSpeed = 3f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float sprintSpeed = 1000f;
    [SerializeField] private float fireRange = 20f;
    [SerializeField] private float bulletForce = 350f;
    private CharacterController controller;
    private Vector3 moveDir;
    private Vector3 velocity;
    private float horizontal, vertical;
    private float mouseX;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        Movemnent();
        Look();
    }
    private void FixedUpdate()
    {
        Shoot();
    }
    private void Movemnent()
    {
        horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        
        if(IsGrounded())
        {   
            velocity.y = -0.1f;
            Jump(); 
            Sprint();
        }
        else
        {
            velocity.y -= gravity;
        }
        
        moveDir = transform.forward * vertical + transform.right * horizontal;
        Vector3 move = moveDir + velocity * Time.deltaTime;
        controller.Move(move * Time.deltaTime);
    }
    public void Look()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        transform.Rotate(Vector3.up, mouseX);
    }
    private bool IsGrounded()
    {   
        Vector3 playerAss = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        isGrounded = Physics.Raycast(playerAss,Vector3.down, 0.1f, layerMask);
        return isGrounded;
    }
    private void Jump()
    {
        if(Input.GetKeyDown("space"))
        {
            velocity.y = jumpSpeed;
        }
    }
    private void Sprint()
    {
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            vertical += sprintSpeed * Time.deltaTime;
        }
    }
    private void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward, out hit, fireRange, shootableLayer))
        {
            hitRB = hit.transform.GetComponent<Rigidbody>();
            if(Input.GetMouseButtonDown(0))
            {
                hitRB.AddForce(bulletForce * Time.deltaTime * transform.forward, ForceMode.Impulse);
            }
        }
        else
        {
            hitRB = null;
        }
    }

}

// using System.Collections;
// using System.Collections.Generic;
// using Unity.Mathematics;
// using UnityEngine;

// public class PlayerScriptAttempt_2 : MonoBehaviour
// {
//     public CharacterController controller;
//     [SerializeField] private float speed = 100f;
//     [SerializeField] private float mouseSens = 10;
//     [SerializeField] private LayerMask layerMask;
//     [SerializeField] private float jumpSpeed = 3f;
//     [SerializeField] private float gravity = 9.81f;
//     private Vector3 velocity;
//     private float horizontal, vertical;
//     private float mouseX;

//     private void Start()
//     {
//         controller = GetComponent<CharacterController>();
//         Cursor.lockState = CursorLockMode.Locked;
//     }

//     private void Update()
//     {
//         Movement();
//         Look();
//     }

//     private void Movement()
//     {
//         horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
//         vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
//         Vector3 moveDir = transform.forward * vertical + transform.right * horizontal;

//         if (IsGrounded())
//         {
//             // Reset the vertical velocity when grounded
//             velocity.y = -0.1f; // Small negative value to ensure the character stays grounded
//             if (Input.GetKeyDown(KeyCode.Space)) // Use GetKeyDown to trigger the jump once
//             {
//                 velocity.y = jumpSpeed;
//             }
//         }
//         else
//         {
//             // Apply gravity when in the air
//             velocity.y -= gravity * Time.deltaTime;
//         }

//         // Move the character based on velocity
//         Vector3 move = moveDir + new Vector3(0, velocity.y, 0);
//         controller.Move(move);
//     }

//     private void Look()
//     {
//         mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
//         transform.Rotate(Vector3.up, mouseX);
//     }

//     private bool IsGrounded()
//     {
//         Vector3 playerAss = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
//         return Physics.Raycast(playerAss, Vector3.down, 0.1f, layerMask);
//     }
// }
