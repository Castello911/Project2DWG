using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditEnemy : MonoBehaviour
{
    Enemy enemy;
    Animator animator;
    Rigidbody2D rb;
    Transform rangedStartPosition;

    bool onLeft = true;

    bool onGround;
    public LayerMask groundLayer;

    public float seekDistance = 6f;
    public float nearDistance = 4f;
    public float movementSpeed;

    public float shootDelay;

    public GameObject projectile;

    public bool shooting;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        rangedStartPosition = transform.GetChild(1);
    }

    void Start()
    {

        StartCoroutine(Attack());
    }

    void Update()
    {
        if (enemy.target == null) //IF TARGET IS NULL
            return; //DESTROY GAMEOBJECT

        onGround = Physics2D.Raycast(transform.position, Vector2.down, (GetComponent<CapsuleCollider2D>().size.y / 2) + 0.2f, groundLayer);
        if (transform.position.x > enemy.target.position.x)
            onLeft = true;
        else if (transform.position.x < enemy.target.position.x)
            onLeft = false;


    }

    void FixedUpdate()
    {
        if (enemy.target == null) //IF TARGET IS NULL
            return; //DESTROY GAMEOBJECT

        float distance = Vector3.Distance(enemy.target.position, transform.position);
        if (distance < seekDistance)
        {
            if (distance > nearDistance)
            {
                shooting = false;
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
                shooting = true;
            }
            
        }
        else
        {
            shooting = false;
            animator.Play("idle");
        }
    }

    IEnumerator Attack()
    {
        Debug.Log("Shoot: " + shooting.ToString());
        if (shooting)
        {
            animator.Play("shoot");
            GameObject p = Instantiate(projectile, rangedStartPosition.position, rangedStartPosition.rotation); //CREATE RANDOMIZED PROJECTILE OBJECT
            BossProjectile bp = p.GetComponent<BossProjectile>();
            bp.onLeft = onLeft;
            bp.target = enemy.target;
        }
        yield return new WaitForSeconds(shootDelay);
        StartCoroutine(Attack());
    }
}
