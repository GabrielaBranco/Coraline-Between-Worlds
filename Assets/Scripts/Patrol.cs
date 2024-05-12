using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 2;
    [SerializeField] private LayerMask groundCheckLayers;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckRadius = 2;
    [SerializeField] private LayerMask wallCheckLayers;
    [SerializeField] private float speed = 150;
    private HealthSystem playerHs;
    private HealthSystem hs;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player player = FindObjectOfType<Player>();

        if(player != null)
        {
            playerHs = player.GetComponent<HealthSystem>();
            if (playerHs != null)
            {
                playerHs.onDeath += PlayerDied;
            }
        }
        playerHs.onDeath += PlayerDied;

        hs = GetComponent<HealthSystem>();
        hs.onDeath += Died;
    }

    private void OnDestroy()
    {
        if(hs)
        {
            hs.onDeath -= Died;
        }
    }

    void Died()
    {
        Destroy(gameObject);
    }

    void PlayerDied()
    {
        speed = 0.0f;
    }

    void Update()
    {
        if((!HasGroundAhead()) || (HasWallAhead()))
        {
            transform.rotation = transform.rotation * Quaternion.Euler(0,180,0);
        }

        Vector2 currentVelocity = rb.velocity;

        currentVelocity.x = transform.right.x * speed;

        rb.velocity = currentVelocity;
    }

    bool HasGroundAhead()
    {
        Collider2D collider = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundCheckLayers);
        return collider != null;
    }
    bool HasWallAhead()
    {
        Collider2D collider = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, wallCheckLayers);
        return collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        if(groundCheck)
        {
            Gizmos.color = new Color(1.0f,0.5f,0.0f,0.5f);
            Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
        }
        if(wallCheck)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(wallCheck.position, wallCheckRadius);
        }
    }
}
