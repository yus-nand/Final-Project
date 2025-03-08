using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Profiling;

public class Player : MonoBehaviour
{   
    public float horizontalLookSens = 100f;
    public int score = 0;
    // public float jumpSpeed = 1f;
    public LayerMask layerMask;
    public float moveSpeed;
    public int playerHealth {get; set;}
    public bool isGrounded;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI healthText;
    private new Rigidbody rigidbody;
    private void Start()
    {
        playerHealth = 100;
        Cursor.lockState = CursorLockMode.Locked;
        healthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        rigidbody = GetComponent<Rigidbody>();
        UpdateHealthText(0);
    }
    private void Update()
    {
        Movement();
        Look();
        scoreText.text = "Score: "+score;
        if(playerHealth <= 0)
        {
            scoreText.enabled = false;
            healthText.enabled = false;
        }
    }
    private void FixedUpdate()
    {
        if(Input.GetKeyDown("space") && isGrounded)
        {
            Jump();
        }
    }
    private void Movement()
    {
        // Profiler.BeginSample("Movement");
        if(Input.GetKey("left shift"))
        {
            moveSpeed = 20f;
        }
        else
        {
            moveSpeed = 10f;
        }

        float horizontal = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * moveSpeed;
        float vertical = Input.GetAxis("Vertical") * Time.fixedDeltaTime * moveSpeed;   

        Vector3 move = new Vector3(horizontal, 0, vertical);
        transform.Translate(move * Time.fixedDeltaTime * 5f);
        // Profiler.EndSample();
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
        healthText.text = "Health Left: " + playerHealth;
    }
    private void Jump()
    {
        rigidbody.AddForce(Vector3.up * 50f, ForceMode.Impulse);
        isGrounded = false;
    }
}
