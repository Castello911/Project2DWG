using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileThrower : MonoBehaviour
{
    public Vector3 throwForce;
    public float destroyDelay;
    public GameObject projectilePrefab;

    public void Throw(Transform attackPosition)
    {
        Debug.Log("Throw"); 
        GameObject projectile = Instantiate(projectilePrefab, attackPosition.position, attackPosition.rotation);
        if (attackPosition.parent.position.x > attackPosition.position.x)
        {
            projectile.GetComponent<Projectile>().speed = -throwForce;
        }
        else
        {
            projectile.GetComponent<Projectile>().speed = throwForce;
        }
        
        Destroy(projectile, destroyDelay);
    }
}
