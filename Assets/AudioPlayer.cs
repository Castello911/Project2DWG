using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayByName(string name)
    {
        try
        {
            foreach (AudioClip clip in clips)
            {
                if (clip.name.Trim() == name.Trim())
                {
                    source.PlayOneShot(clip);
                }
            }
        }
        catch
        {
            Debug.Log("can't play right now.");
        }
        
    }

    public void PlayRandomByName(string name, int min, int max)
    {
        if (min < max || min == max)
        {
            PlayByName(name+Random.Range(min,max));
        }
    }
}
