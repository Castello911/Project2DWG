using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public Transform target;
    public bool flatThrow;
    public bool onLeft;

    AudioPlayer audioPlayer;
    public string audioName = "shoot";

    void Awake()
    {
        audioPlayer = GameObject.Find("FXSource").GetComponent<AudioPlayer>();
    }

    void Start()
    {

        audioPlayer.PlayRandomByName(audioName, 1, 4);
        Vector3 diff;

        if (flatThrow)
        {
            if(onLeft)
                diff = (transform.position + Vector3.left) - transform.position;
            else
                diff = (transform.position + Vector3.right) - transform.position;
        }
        else
        {
            diff = target.position - transform.position;
        }

        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        Destroy(gameObject,5);
    }

    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerManager>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            if(other.gameObject.tag != "Boss")
            {
                Destroy(gameObject);
            }
        }

    }

}
