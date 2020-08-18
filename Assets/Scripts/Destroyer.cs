using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public GameObject deathEffects;
    public int point; //ENEMY VALUE
    bool destroyed = false;

    InGameManager igm; //IN GAME MANAGER TO ADD SCORE

    void Awake()
    {
        igm = GameObject.Find("Manager").GetComponent<InGameManager>(); //GET IN GAME MANAGER SCRIPT ON MANAGER GAMEOBJECT
    }

    public void DestroyMe()
    {
        if (!destroyed) //IF NOT DESTROYED
        {
            igm.currentScore += point; //ADD SCORE
            destroyed = true; //SET DESTROYED BOOLEAN TRUE FOR HANDLE REPEATING
            GameObject de = Instantiate(deathEffects, transform.position, Quaternion.identity); //INSTANTIATE DEATH EFFECTS PREFAB
            Destroy(de, 1); //DESTROTY DEATH EFFECTS AFTER 1 SECONDS
            Destroy(gameObject); //DESTROY THIS GAME OBJECT
        }
    }
}
