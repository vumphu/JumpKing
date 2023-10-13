using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpKingScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 4f;
    public float moveInput;
    public LayerMask groundMask;
    public LayerMask wallMask;
    public float jumpStartY;
    public Animator anim;
    public SpriteRenderer sprite;
    public float fallStartY;
    public float collapseThreshold = 25.0f;
    public float fallingHeight;

    public PhysicsMaterial2D BounceMat, NormalMat;
    public bool isGrounded;
    public bool isCollidingWithWall;
    public bool canJump;
    public bool spacePressedLastFrame = false;
    private bool canChangeAnimation = true;
    public float jumpValue = 0.0f;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        jumpStartY = transform.position.y;
        fallStartY = transform.position.y;
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        bool spacePressedThisFrame = Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space);

        fallingHeight = fallStartY - transform.position.y;

        // Check if the Space key is pressed and can change animation
        if (spacePressedThisFrame && canChangeAnimation)
        {
            HandleSpacePressed();
        }

        // Track if the Space key was released
        if (!spacePressedThisFrame && spacePressedLastFrame)
        {
            spacePressedLastFrame = false;
            canChangeAnimation = true; // Reset the ability to change animation
        }

        if(jumpValue == 0.0f && isGrounded)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }

        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f),
        new Vector2(0.9f, 0.4f), 0f, groundMask);
        isCollidingWithWall = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x + (moveInput > 0 ? 0.5f : -0.5f), gameObject.transform.position.y),
        new Vector2(0.4f, 0.9f), 0f, wallMask);

        if(!isGrounded)
        {
            rb.sharedMaterial = BounceMat;
        }
        else
        {
            rb.sharedMaterial = NormalMat;
        }

        if(Input.GetKey(KeyCode.Space) && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
            jumpValue += 0.2f;
        }
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }

        if(jumpValue >= 20f && isGrounded)
        {
            // float tempx = moveInput * moveSpeed;
            // float tempy = jumpValue;
            // rb.velocity = new Vector2(tempx, tempy);
            jumpValue = 20f;
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

    void ResetJump()
    {
        canJump = false;
        jumpValue = 0;
    }

    void HandleSpacePressed()
    {
        // Add your logic for what should happen when Space key is pressed here
        if (isGrounded)
        {
            anim.SetBool("Charging", true);
            canChangeAnimation = false;
            spacePressedLastFrame = true; // Set to true to track that Space key was pressed
        }
    }
    
    void UpdateAnimation()
    {
        // Reset all animation states
        anim.SetBool("Running", false);
        anim.SetBool("Charging", false);
        anim.SetBool("Jumping", false);
        anim.SetBool("Falling", false);
        anim.SetBool("Collide", false);
        anim.SetBool("Collapse", false);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded|| Input.GetKey(KeyCode.Space) && isGrounded)
        {
            anim.SetBool("Charging", true);
        }
        else if (rb.velocity.y > 0)
        {
            if (isCollidingWithWall)
            {
                anim.SetBool("Collide", true);
            }
            else
            {
                anim.SetBool("Jumping", true);
            }
        }
        else if (rb.velocity.y < 0)
        {
            if (isCollidingWithWall)
            {
                anim.SetBool("Collide", true);
            }
            else
            {
                anim.SetBool("Falling", true);
            }
        }
        else if (moveInput != 0)
        {
            anim.SetBool("Running", true);
            sprite.flipX = moveInput < 0; // Flip sprite when moving left
        }
    }

}
