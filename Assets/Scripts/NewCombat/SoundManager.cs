using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A singleton to handle enemy audio clips
public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance { get { return _instance; } }

    private AudioSource audioSource;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlaySound(AudioClip sound)
    {
        if (sound != null)
        {
            //Debug.Log("audio" + Time.time);
            audioSource.clip = sound;
            audioSource.Play();
        }
    }


}
