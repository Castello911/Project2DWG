using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 speed;
    public int damageAmount;
    void Update()
    {
        transform.Translate(speed*Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damageAmount);
        }
        else if(other.gameObject.tag == "Boss")
        {
            other.gameObject.GetComponent<Boss>().TakeDamage(damageAmount);
        }
        Destroy(this.gameObject);
    }
}
