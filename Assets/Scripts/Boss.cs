using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health; //CURRENT HEALTH, CLAMPED BETWEEN 0 AND MAXIMUM HEALTH
    public int maxHealth; //MAXIMUM HEALTH
    public int regeneration; //REGENERATION SCALE
    public float regenerationDelay; //REGENERATE AFTER MILLISECONDS
    public float nearLimit = 3f; //MAXIMUM DISTANCE BETWEEN PLAYER AND BOSS
    public float movementSpeed; //MOVEMENT SPEED

    //REQUIRED COMPONENTS
    Animator animator; 
    Rigidbody2D rb;
    Transform target;
    Transform rangedStartPosition;
    SpriteRenderer spriteRenderer;

    public float meleeLimit = 0.5f; //MELEE RANGE
    public bool meleeAttack = false; //IF IN MELEE RANGE

    public float attackDelay = 1f; //ATTACK DELAY

    public int meleeDamage; //MELEE DAMAGE

    bool onLeft; //IF PLAYER ON LEFT
    bool shown = false; //IF BOSS SEES PLAYER

    public float showDistance; //TRIGGER DISTANCE

    public float damageTimeout; //DAMAGE FEEDBACK TIMEOUT

    public GameObject[] projectiles; //BOSS PROJECTILES

    public GameObject portal; //ENABLE PORTAL AFTER DEATH

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //GET RIGIDBODY COMPONENT ON BOSS
        animator = GetComponentInChildren<Animator>(); //GET RIGIDBODY COMPONENT ON BOSS GRAPHICS(FIRST CHILD)
        shown = false;
        rangedStartPosition = transform.GetChild(1); //GET RANGED START POSITION TRANSFORM (SECOND CHILD)
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); //GET SPRITE RENDERER COMPONENT ON BOSS GRAPHICS(FIRST CHILD)
    }

    void Start()
    {
        health = maxHealth; //SET HEALTH TO MAXIMUM
        target = GameObject.FindGameObjectWithTag("Player").transform; //GET PLAYER TRANSFORM
        StartCoroutine(Regenerate()); //START REGENERATE SUBTHREAD
        StartCoroutine(Attack()); //START ATTACK SUBTHREAD
    }

    void Update()
    {
        if (target == null) //IF TARGET NOT FOUND
        {
            return; //RETURN VOID
        }

        if (health <= 0) //IF HEALTH IS MINUMUM
        {
            portal.GetComponent<Animator>().Play("open"); //OPEN PORTAL ANIMATION
            portal.GetComponent<Portal>().opened = true; //OPEN PORTAL USING PORTAL SCRIPT
            GetComponent<Destroyer>().DestroyMe(); //DESTROY BOSS USING DESTROYER SCRIPT
        }

        float distance = Vector3.Distance(transform.position, target.position); //GET DISTANCE BETWEEN PLAYER AND BOSS

        if (distance < showDistance) //IF BOSS SEES PLAYER
        {
            shown = true;
        }

        if (!shown) //IF BOSS DOESNT SEE THE PLAYER
        {
            return; //RETURN VOID
        }

        if(distance > nearLimit)
        {
            if (onLeft) //IF PLAYER ON LEFT
            {
                rb.velocity = new Vector2(-movementSpeed, 0); //SET VELOCITY TO LEFT

            }
            else
            {
                rb.velocity = new Vector2(movementSpeed, 0); //SET VELOCITY TO RIGHT
            }
        }

        meleeAttack = distance < meleeLimit;

        onLeft = transform.position.x > target.position.x;
    }

    void LateUpdate()
    {
        //THIS FUNCTION FOR GRAPHICS
        if (onLeft) //IF PLAYER ON LEFT
        {
            transform.localScale = new Vector3(1, 1, 1); //SCALE BOSS TO NORMAL
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); //FLIP ON X AXIS
        }
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(DamageFeedback());//START DAMAGE FEEDBACK SUBTHREAD
        health -= damage;//REDUCE HEALTH
    }
    IEnumerator DamageFeedback() //DAMAGE FEEDBACK SUBTHREAD
    {
        spriteRenderer.color = Color.red; //SET SPRITE RENDERER COLOR TO RED
        yield return new WaitForSeconds(damageTimeout); //WAIT FOR SECONDS (DAMAGE TIMEOUT)
        spriteRenderer.color = Color.white; //SET SPRITE RENDERER COLOR TO WHITE(DEFAULT)
    }

    IEnumerator Attack() //ATTACK SUBTHREAD
    {
        if (shown && target != null) //IF NOT SHOWN AND TARGET IS AVAILABLE
        {
            if (meleeAttack) //IF IN MELEE RANGE
            {
                animator.Play("melee"); //PLAY MELEE ANIMATION
                target.gameObject.GetComponent<PlayerManager>().TakeDamage(meleeDamage); //HIT PLAYER
            }
            else
            {
                animator.Play("ranged"); //PLAY RANGED ANIMATION
                GameObject p = Instantiate(projectiles[Random.Range(0, projectiles.Length)], rangedStartPosition.position, rangedStartPosition.rotation); //CREATE RANDOMIZED PROJECTILE OBJECT
                BossProjectile bp = p.GetComponent<BossProjectile>(); //GET BOSS PROJECTILE COMPONENT FROM PROJECTILE OBJECT
                bp.onLeft = onLeft; //SET SIDE INFORMATION
                bp.target = target; //SET TARGET INFORMATION
            }
        }
        
        yield return new WaitForSeconds(attackDelay); //WAIT FOR SECONDS (ATTACK DELAY)

        StartCoroutine(Attack()); //RESTART ATTACK SUBTHREAD
    }

    IEnumerator Regenerate() //REGENERATION SUBTHREAD
    {
        if(health > 0) //IF HEALTH GREATER THAN ZERO
        {
            if (health < maxHealth) //IF HEALTH LESS THAN MAX HEALTH
            {
                health += regeneration; //REGENERATE BY SCALE
            }
            yield return new WaitForSeconds(regenerationDelay);//WAIT FOR SECONDS (REGENERATION DELAY)
            StartCoroutine(Regenerate());//START REGENERATE SUBTHREAD
        }
    }
}
