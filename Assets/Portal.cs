using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string nextLevel;
    public bool opened;

    public void LoadLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
