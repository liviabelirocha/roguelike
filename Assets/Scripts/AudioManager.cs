using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private AudioSource levelMusic;

    [SerializeField]
    private AudioSource gameOverMusic;

    [SerializeField]
    private AudioSource winMusic;

    [SerializeField]
    private AudioSource[] sfx;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
    }

    void Update()
    {
    }

    public void PlayGameOver()
    {
        levelMusic.Stop();
        gameOverMusic.Play();
    }

    public void PlayLevelWin()
    {
        levelMusic.Stop();
        winMusic.Play();
    }

    public void PlaySFX(int sfxIndex)
    {
        sfx[sfxIndex].Stop();
        sfx[sfxIndex].Play();
    }
}
