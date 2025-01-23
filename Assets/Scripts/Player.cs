using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{   
    public float horizontalLookSens = 100f;
    public int score = 0;
    // public float jumpSpeed = 1f;
    public LayerMask layerMask;
    public float moveSpeed;
    private int playerHealth = 100;
    public bool isGrounded;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI healthText;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        healthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        UpdateHealthText(0);
    }
    private void Update()
    {
        Movement();
        Look();
        scoreText.text = "Score: "+score;
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
    public void TakeDamage(int damage)
    {
        Debug.Log("Player took damage");
        playerHealth = playerHealth - damage;
        Debug.Log(playerHealth + ", " +damage);
        UpdateHealthText(damage);
    }
    private void UpdateHealthText(int damage)
    {
        Debug.Log("Updating health text");
        healthText.text = "Health Left: " + (playerHealth - damage);
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
