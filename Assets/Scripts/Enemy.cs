using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health; //STARTING HEALTH
    public float damageTimeout; //DEAMAGE FEEDBACK TIMEOUT

    public Transform target; //PLAYER TRANSFORM

    //COMPONENTS
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    AudioPlayer audioPlayer;

    //ON LEFT FOR INDIVIDUAL ENEMY SCRIPTS
    public bool reverseSprite;

    void Awake()
    {
        audioPlayer = GameObject.Find("FXSource").GetComponent<AudioPlayer>();
    }

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); //GET SPRITE RENDERER ON GRAPHICS (FIRST CHILD)
        rb = GetComponent<Rigidbody2D>(); //GET RIGIDBODY COMPONENT
        target = GameObject.FindGameObjectWithTag("Player").transform; //SET TARGET
    }

    public void TakeDamage(int damage)
    {
        audioPlayer.PlayRandomByName("hit", 1, 4);
        StartCoroutine(DamageFeedback()); //START DAMAGE FEEDBACK SUBTHREAD
        health -= damage; //REDUCE HEALTH
    }

    public void Update()
    {
        if (target == null) //IF TARGET IS NULL
            return; //DESTROY GAMEOBJECT

        if (health <= 0) //IF HEALTH IS LESS THAN 1
        {
            GetComponent<Destroyer>().DestroyMe(); //DESTROY BOSS USING DESTROYER SCRIPT
        }

        if (!reverseSprite)
        {
            if (target.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (target.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            if (target.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (target.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

    }

    IEnumerator DamageFeedback()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(damageTimeout);
        spriteRenderer.color = Color.white;
    }
}
