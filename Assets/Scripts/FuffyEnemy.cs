using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuffyEnemy : MonoBehaviour
{
    public Vector2 jumpForce;
    public float jumpTimeout;

    bool onGround;
    float jumpTime = 0f;

    public LayerMask groundLayer;

    Animator animator;
    Rigidbody2D rb;

    Transform target;

    Enemy enemy;

    bool onLeft = true;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        if (enemy.target == null)
            return;
        onGround = Physics2D.Raycast(transform.position, Vector2.down, (GetComponent<CapsuleCollider2D>().size.y / 2) + 0.2f, groundLayer);
        animator.SetBool("onground", onGround);

        if (transform.position.x > enemy.target.position.x)
            onLeft = true;
        else if (transform.position.x < enemy.target.position.x)
            onLeft = false;
    }

    void LateUpdate()
    {
        if (enemy.target == null)
            return;
        if (Vector3.Distance(enemy.target.position,transform.position) < 6f)
        {
            if (onGround && Time.time > jumpTime)
            {
                if (onLeft)
                    rb.velocity += new Vector2(jumpForce.x, jumpForce.y);
                else
                    rb.velocity += new Vector2(-jumpForce.x, jumpForce.y);

                jumpTime = Time.time + jumpTimeout;
                Debug.Log("Jump");
            }
        }
    }

}
