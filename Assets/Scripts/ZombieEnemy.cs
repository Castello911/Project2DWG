using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemy : MonoBehaviour
{
    Enemy enemy;
    Animator animator;
    Transform target;
    Rigidbody2D rb;

    bool onLeft = true;

    bool onGround;
    public LayerMask groundLayer;

    public float seekDistance = 6f;
    public float nearDistance = 1f;
    public float movementSpeed;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        if (enemy.target == null)
            return;

        onGround = Physics2D.Raycast(transform.position, Vector2.down, (GetComponent<CapsuleCollider2D>().size.y / 2) + 0.2f, groundLayer);
        if (transform.position.x > enemy.target.position.x)
            onLeft = true;
        else if (transform.position.x < enemy.target.position.x)
            onLeft = false;

        
    }

    void FixedUpdate()
    {
        if (enemy.target == null)
            return;

        float distance = Vector3.Distance(enemy.target.position, transform.position);
        if (distance < seekDistance && distance > nearDistance)
        {
            if (onLeft)
            {
                rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
            }
            animator.Play("walk");
        }
        else
        {
            animator.Play("idle");
        }
    }
}
