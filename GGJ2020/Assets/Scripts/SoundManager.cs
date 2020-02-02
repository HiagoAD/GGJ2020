using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip playerMiss;
    [SerializeField] AudioClip playerHit;
    [SerializeField] AudioClip playerDied;
    [SerializeField] AudioClip enemyHit;

    AudioSource source;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void CancelSounds()
    {
        source.Stop();
        source.clip = null;
    }

    public void PlayPlayerMiss()
    {
        CancelSounds();
        source.clip = playerMiss;
        source.loop = false;
        source.Play();
    }

    public void PlayPlayerHit()
    {
        CancelSounds();
        source.clip = playerHit;
        source.loop = false;
        source.Play();
    }

    public void PlayPlayerDied()
    {
        CancelSounds();
        source.clip = playerDied;
        source.loop = false;
        source.Play();
    }

    public void PlayEnemyHit()
    {
        CancelSounds();
        source.clip = enemyHit;
        source.loop = false;
        source.Play();
    }


}
