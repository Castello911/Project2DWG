using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;

    public int damageAverage;
    public int damageRadius;

    public bool onGround = false;

    public LayerMask groundLayer;
    public LayerMask enemyLayer;

    Rigidbody2D rb;
    Transform attackPosition;
    AudioPlayer audioPlayer;

    public enum AttackType
    {
        Melee, Projectile
    }

    public AttackType attackType;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        attackPosition = transform.GetChild(1);
        audioPlayer = GameObject.Find("FXSource").GetComponent<AudioPlayer>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        onGround = DetectGround();
        float horizontal = Input.GetAxis("Horizontal");
        
        if(onGround)
            rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            audioPlayer.PlayRandomByName("jump", 1, 4);
            rb.velocity += new Vector2(0, jumpForce);
        }
    }

    public void Attack()
    {
        if(attackType == AttackType.Melee)
        {
            audioPlayer.PlayRandomByName("laser", 1, 4);
            Collider2D[] cols = Physics2D.OverlapCircleAll(attackPosition.position, 1f, enemyLayer);
            foreach (Collider2D col in cols)
            {
                if (col.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(damageAverage + Random.Range(-damageRadius, damageRadius));
                }
                if (col.TryGetComponent(out Boss boss))
                {
                    boss.TakeDamage(damageAverage + Random.Range(-damageRadius, damageRadius));
                }
            }
        }
        else
        {
            GetComponent<ProjectileThrower>().Throw(attackPosition);
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * ((GetComponent<CapsuleCollider2D>().size.y / 2) + 0.2f));
        Gizmos.DrawRay(transform.position, Vector2.down * ((GetComponent<CapsuleCollider2D>().size.y / 2) + 0.2f) + new Vector2(0.45f, 0));
        Gizmos.DrawRay(transform.position, Vector2.down * ((GetComponent<CapsuleCollider2D>().size.y / 2) + 0.2f) + new Vector2(-0.45f, 0));
    }

    bool DetectGround()
    {
        bool detect1 = Physics2D.Raycast(transform.position, Vector2.down, (GetComponent<CapsuleCollider2D>().size.y / 2) + 0.2f, groundLayer);
        bool detect2 = Physics2D.Raycast(transform.position, Vector2.down + new Vector2(0.45f, 0), ((GetComponent<CapsuleCollider2D>().size.y / 2) + 0.2f), groundLayer);
        bool detect3 = Physics2D.Raycast(transform.position, Vector2.down + new Vector2(-0.45f, 0), ((GetComponent<CapsuleCollider2D>().size.y / 2) + 0.2f), groundLayer);

        if (!detect1 && !detect2 && !detect3)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

}
