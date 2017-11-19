using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A singleton to handle enemy audio clips
public class EnemySoundManager : MonoBehaviour
{
    private static EnemySoundManager _instance;
    public static EnemySoundManager Instance { get { return _instance; } }

    public AudioClip enemyExplosion;
    


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

    public void playEnemyExplosionSound()
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = enemyExplosion;
        newAudio.volume = .7f;
        newAudio.Play();
    }
   

}
