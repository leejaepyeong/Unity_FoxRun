using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    //public AudioClip deathClip;
    private AudioSource playerAudio;
    private Animator animator;

    bool isMove;
    bool isJump;
    bool isDie = false;
    bool isKey = false;

    Rigidbody playerRb;
    

    float moveForce = 35f;
    float jumpForce = 17500f;
    float horizontal;


    int jumpCount = 0;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        PlayerMove();
        PlayerJump();
    }


    void GetInput()
    {
        isMove = Input.GetButton("Horizontal");
        isJump = Input.GetButtonDown("Jump");
    }

    public void PlayerMoveLeftKey()
    {
        isKey = true;
        isMove = true;
        horizontal = -0.9f;
    }

    public void PlayerMoveRightKey()
    {
        isKey = true;
        isMove = true;
        horizontal = 0.9f;
    }

    public void PlayerMoveKeyUp()
    {
        isKey = false;
        isMove = false;
        horizontal = 0;
    }

    void PlayerMove()
    {
        

        if(isMove)
        {
            if(isKey == false)
            {
                horizontal = Input.GetAxis("Horizontal");
            }
            


            transform.Translate(Vector3.right * horizontal * moveForce*Time.deltaTime);
            
        }
    }

    public void PlayerJumpKeyPad()
    {
        isJump = true;
        PlayerJump();
    }

    public void PlayerJump()
    {
        if (isJump && jumpCount < 2)
        {
            jumpCount++;

            playerRb.velocity = Vector3.zero;
            playerRb.AddForce(new Vector3(0,jumpForce,0));

            animator.SetBool("doJump", true);

        }
        else if (!isJump && playerRb.velocity.y>0)
        {
            playerRb.velocity = playerRb.velocity * 0.5f;
        }

        
    }

    public void JumpUp()
    {
        jumpCount = 0;
    }

    public void Die()
    {
        if(!isDie)
            animator.SetTrigger("isDead");

        isDie = true;

        

        //gameObject.SetActive(false);

    }

    void Finish()
    {
        GameManager.instance.Win();
    }

    private void OnCollisionEnter(Collision collision)
    {
        jumpCount = 0;
        animator.SetBool("doJump", false);

        if (collision.gameObject.tag == "Finish")
        {
            Finish();
            animator.SetTrigger("isGoal");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DeadZone" && !isDie)
        {
            GameManager.instance.Damage(100);
        }
    }

    
}


// Player script edit