using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    Animator animator;
    SpriteRenderer spriteRenderer;
    PlayerMovement movement;
    Transform attackPosition;

    bool attack;
    float attackTime;
    public float attackTimeout;

    void Awake()
    {
        attack = false;
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        movement = GetComponent<PlayerMovement>();
        attackPosition = transform.GetChild(1);
    }

    void Start()
    {
        
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if(horizontal < 0)
        {
            spriteRenderer.flipX = true;
            attackPosition.localPosition = new Vector3(-0.5f, 0,0);
        }else if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
            attackPosition.localPosition = new Vector3(0.5f, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.X) && Time.time > attackTime && Mathf.Abs(horizontal) < 0.5f)
        {
            attack = true;
            attackTime = Time.time + attackTimeout;
            movement.Attack();
            Debug.Log("Attack" + Time.time);
        }

        animator.SetBool("attack", attack);
        animator.SetFloat("horizontal", Mathf.Abs(horizontal));
        animator.SetBool("onground", movement.onGround);
        attack = false;


    }
}
