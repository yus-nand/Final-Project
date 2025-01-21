using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public float horizontalLookSens = 100f;
    // public float jumpSpeed = 1f;
    public TextMeshProUGUI healthText;
    public LayerMask layerMask;
    public float moveSpeed;
    public int playerHealth;
    public bool isGrounded;
    private void Start()
    {
        healthText.text = "Health Left: " + playerHealth;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        Movement();
        Look();
        healthText.text = "Health Left: " + playerHealth;
        // Jump();
    }
    private void Movement()
    {
        if(Input.GetKey("left shift"))
        {
            moveSpeed = 30;
        }
        else
        {
            moveSpeed = 20;
        }

        float horizontal = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * moveSpeed;
        float vertical = Input.GetAxis("Vertical") * Time.fixedDeltaTime * moveSpeed;   

        Vector3 move = new Vector3(horizontal, 0, vertical);
        transform.Translate(move * Time.fixedDeltaTime * 5f);
    }
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.fixedDeltaTime * horizontalLookSens;
        transform.Rotate(Vector3.up * mouseX);
    }
    // private void Jump()
    // {
    //     isGrounded = Physics.Raycast(transform.position - Vector3.down, Vector3.down, 5f, layerMask);
    //     if(Input.GetKeyDown("space") && isGrounded)
    //     {
    //         transform.Translate(Vector3.up * 5);
    //     }
    //     else if(!isGrounded)
    //     {
    //         transform.Translate(Vector3.down * 3);
    //     }
    // }
}
