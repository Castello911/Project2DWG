using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    public GameObject[] characters;
    public bool newGame = false;
    public Vector3 worldStartPosition;
    bool paused = false;

    public CameraFollower cameraFollower;

    public GameObject pausePanel;

    public int currentScore;
    public Text scoreText;

    public float levelStartX,levelEndX;
    public Slider levelCompletionSlider;
    GameObject player;

    public bool dead;

    void Awake()
    {
        newGame = PlayerPrefs.GetInt("NewGame") == 1;
        paused = false;

        if (newGame)
        {
            int characterId = PlayerPrefs.GetInt("SelectedCharacter");
            GameObject instance = GameObject.Instantiate(characters[characterId], worldStartPosition, Quaternion.identity);
            player = instance;
            cameraFollower.target = instance.transform;
            currentScore = 0;
        }
        else
        {
            currentScore = PlayerPrefs.GetInt("Score");
        }
    }

    void Start()
    {
        IngoreCollisions();
    }

    void Update()
    {
        PlayerPrefs.SetInt("Score", currentScore);

        if (dead && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (player == null)
            return;

        if (paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        pausePanel.SetActive(paused);

        if (Input.GetKeyDown(KeyCode.P))
            paused = !paused;

        float levelWidth = Mathf.Abs(levelStartX) + Mathf.Abs(levelEndX);
        float characterX = player.transform.position.x;

        float calculatedCompletion = characterX / levelWidth;

        levelCompletionSlider.value = calculatedCompletion;
    }

    void LateUpdate()
    {
        scoreText.text = currentScore.ToString();
    }

    void IngoreCollisions()
    {
        if(player != null)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), true);
            }
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Boss"))
            {
                Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), true);
            }
        }
    }
}
