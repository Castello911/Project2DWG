using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int maxHealth;

    public int health;

    Image healthImage;
    AudioPlayer audioPlayer;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private GameObject characterPanel,gameoverPanel;

    private InGameManager inGameManager;

    public GameObject deathEffects;
    bool dead = false;

    void Awake()
    {
        inGameManager = GameObject.Find("Manager").GetComponent<InGameManager>();
        characterPanel = GameObject.Find("CharacterPanel");
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        gameoverPanel = characterPanel.transform.parent.GetChild(2).gameObject;
        healthImage = characterPanel.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        audioPlayer = GameObject.Find("FXSource").GetComponent<AudioPlayer>();
    }

    void Start()
    {
        Time.timeScale = 1;
        health = maxHealth;
        dead = false;
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if(transform.position.y < -17f)
        {
            TakeDamage(maxHealth);
        }

        if(health <= 0 && !dead)
        {
            dead = true;
            spriteRenderer.enabled = false;
            StartCoroutine(Death());
        }

    }

    IEnumerator Death()
    {
        spriteRenderer.enabled = false;
        GameObject p = Instantiate(deathEffects, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        characterPanel.SetActive(false);
        gameoverPanel.SetActive(true);
        inGameManager.dead = true;
        Destroy(p);
        Destroy(gameObject);
    }

    void LateUpdate()
    {
        healthImage.fillAmount = float.Parse(health.ToString()) / float.Parse(maxHealth.ToString());
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(DamageFeedback());
        audioPlayer.PlayRandomByName("hit", 1, 4);
    }

    IEnumerator DamageFeedback()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.color = Color.white;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Portal")
        {
            Portal p = other.GetComponent<Portal>();
            if (p.opened)
            {
                p.LoadLevel();
            }
        }
    }
}
