using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpKingScript : MonoBehaviour
{
    public Rigidbody2D rb;    
    public Animator anim;
    public SpriteRenderer sprite;
    public LayerMask groundMask;
    public LayerMask wallMask;
    public PhysicsMaterial2D BounceMat, NormalMat;
    private Vector2 startingPosition;

    public float moveSpeed = 4f;
    public float moveInput;


    public bool isGrounded;
    public bool isCollidingWithWall;
    public bool canJump = true;
    public float jumpValue = 0.0f;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        if(jumpValue == 0.0f && isGrounded)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }

        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f),
        new Vector2(0.8f, 0.4f), 0f, groundMask);
        isCollidingWithWall = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x + (moveInput >= 0 ? 0.4f : -0.4f), gameObject.transform.position.y),
        new Vector2(0.225f, 0.9f), 0f, wallMask);

        if(!isGrounded)
        {
            rb.sharedMaterial = BounceMat;
        }
        else
        {
            anim.SetBool("Collide", false);
            rb.sharedMaterial = NormalMat;
        }

        if(Input.GetKey(KeyCode.Space) && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
            jumpValue += 0.3f;
        }
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            jumpValue = 3f;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }

        if(jumpValue >= 10f && isGrounded)
        {
            // float tempx = moveInput * moveSpeed;
            // float tempy = jumpValue;
            // rb.velocity = new Vector2(tempx, tempy);
            jumpValue = 10f;
            // Invoke("ResetJump", 0.2f);
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            if(isGrounded)
            {
                rb.velocity = new Vector2(moveInput * moveSpeed, jumpValue);
                jumpValue = 0.0f;
            }
            canJump = true;
        }

        UpdateAnimation();
    }        

    // void ResetJump()
    // {
    //     canJump = false;
    //     jumpValue = 0;
    // }

    void UpdateAnimation()
    {
        // Reset all animation states
        anim.SetBool("Running", false);
        anim.SetBool("Charging", false);
        anim.SetBool("Jumping", false);
        anim.SetBool("Falling", false);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded || Input.GetKey(KeyCode.Space) && isGrounded 
            || Input.GetKeyDown(KeyCode.Space) && isGrounded && isCollidingWithWall || Input.GetKey(KeyCode.Space) && isGrounded && isCollidingWithWall)
        {
            anim.SetBool("Charging", true);
        }             
        else if (isCollidingWithWall)
        {
            anim.SetBool("Collide", true);
        }
        else if (rb.velocity.y > 0)
        {
            anim.SetBool("Jumping", true);
        }
        else if (rb.velocity.y < 0)
        {            
            anim.SetBool("Falling", true);
        }
        else if (moveInput != 0)
        {
            anim.SetBool("Running", true);
            sprite.flipX = moveInput < 0; // Flip sprite when moving left
        }
    }
}

