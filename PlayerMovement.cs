using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float xSpeed = 2f, ySpeed = 5f, jumpSpeed=10f,gravity=10f;
    float yVelocity = 0f;
    Vector3 moveVelocity = Vector3.zero;
    CharacterController controller;
    UIManager uIManager;
    public int killedEnemies=0;
    AudioSource aud;
    [SerializeField]
    AudioClip footsteps;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        aud = GetComponent<AudioSource>();
        if (aud != null)
        {
            aud.clip = footsteps;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (uIManager.isAlive)
        {
            Movement();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MouseCursorVisibility(!Cursor.visible);
        }
        
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        moveVelocity = new Vector3(horizontalInput * xSpeed, 0 , verticalInput * ySpeed);
        
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpSpeed;
                
            }
        }
        else
        {
            yVelocity -= gravity;
            
        }
        moveVelocity.y = yVelocity;
        moveVelocity = transform.transform.TransformDirection(moveVelocity);
        controller.Move(moveVelocity * Time.deltaTime);
        if (horizontalInput !=0 || verticalInput !=0 )
        {
            //if(AudioManager.PlaySound("Footsteps"))
            if (!aud.isPlaying)
            {
                aud.PlayOneShot(footsteps);
                
            }
        }
        else
        {
            aud.Stop();
        }
    }
    public void MouseCursorVisibility(bool state)
    {
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = state;
    }
}
