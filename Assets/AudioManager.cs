using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource fxSource,musicSource;

    void Start()
    {
        SetAll();   
    }

    public void SetAll()
    {
        fxSource.volume = (float)PlayerPrefs.GetInt("FXLevel") / 10f;
        musicSource.volume = (float)PlayerPrefs.GetInt("MusicLevel") / 10f;
    }
}
