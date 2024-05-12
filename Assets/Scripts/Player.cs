using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 100;
    [SerializeField] private float jumpSpeed = 100;
    [SerializeField] private float maxJumpTime;
    [SerializeField] private Collider2D groundCollider;
    [SerializeField] private Collider2D airCollider;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 2;
    [SerializeField] private LayerMask groundCheckLayers;
    [SerializeField] private float blinkDuration = 0.1f;
    [SerializeField] private HealthDisplay hd;
    [SerializeField] private ScreenManager gameOver;

    //private Animator         animator;
    private float            jumpTime;
    private int              currentHealth;
    private SpriteRenderer   sr;
    private Rigidbody2D      rb;
    private float            defaultGravity;
    private HealthSystem     hs;
    private float            blinkTimer;

    void Start()
    {
       Debug.Log($"Initializing {name}...");

       //animator = GetComponent<Animator>();
       rb = GetComponent<Rigidbody2D>();
       sr = GetComponent<SpriteRenderer>();
       defaultGravity = rb.gravityScale;

        hs = GetComponent<HealthSystem>();

        hs.onDeath += PlayerDied;
        hs.onInvulnerabilityToggle += ToggleInvulnerability;

    }

    private void OnDestroy()
    {
        if(hs)
        {
            hs.onInvulnerabilityToggle -= ToggleInvulnerability;
            hs.onDeath -= PlayerDied;
        }
    }

    void ToggleInvulnerability(bool active)
    {
        if(active)
        {
            blinkTimer = blinkDuration;
        }
        else
        {
            blinkTimer = 0;
            sr.enabled = true;
        }
    }
    
    void PlayerDied()
    {
        hd.DestroyHearts();
        Destroy(gameObject);
        gameOver.EndGame();
        Debug.Log("Dead");
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = IsGrounded();
        float deltaX = Input.GetAxis("Horizontal");

        if (blinkTimer > 0)
        {
            blinkTimer -= Time.deltaTime;
            if (blinkTimer <= 0)
            {
                sr.enabled = !sr.enabled;
                blinkTimer = blinkDuration;
            }
        }

        airCollider.enabled = !isGrounded;
        groundCollider.enabled = isGrounded;

        Vector3 currentVelocity = rb.velocity;
        currentVelocity.x = deltaX * moveSpeed;

        

        if ((Input.GetButtonDown("Jump")) && (isGrounded))
        {
            currentVelocity.y = jumpSpeed;
            rb.gravityScale = 1.0f;
            jumpTime = Time.time; 
        }
        else if ((Input.GetButton("Jump")) && ((Time.time - jumpTime) < maxJumpTime))
        {
            rb.gravityScale = 1.0f;
        }
        else
        {
            rb.gravityScale = defaultGravity;
        }

        rb.velocity = currentVelocity;

        //animator.SetFloat("AbsVelocity", MathF.Abs(currentVelocity.x));

        //Rotates sprite in 180º
        if ((currentVelocity.x < 0) && (transform.right.x > 0))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if ((currentVelocity.x > 0) && (transform.right.x < 0))
        {
            transform.rotation = Quaternion.identity;
        }
    }

    //Checks if the Entity is touching the ground
    private bool IsGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position,
        groundCheckRadius, groundCheckLayers);
        return (collider != null);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
