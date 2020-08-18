using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    public int damageAverage;
    public int damageRadius;
    public float delay;
    public float damageRange = 1f;
    public LayerMask layer;

    void Start()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, damageRange, layer);

        Debug.Log("Attack: " + cols.Length);
        foreach (Collider2D col in cols)
        {
            col.GetComponent<PlayerManager>().TakeDamage(Random.Range(-damageRadius, damageRadius) + damageAverage);
        }

        yield return new WaitForSeconds(delay);
        StartCoroutine(Attack());
    }
}
