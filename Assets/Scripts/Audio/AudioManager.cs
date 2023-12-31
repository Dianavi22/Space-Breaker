using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    public AudioClip[] songs;
    public float volume;
    [SerializeField] GameManager _gameManager;

    private void Start()
    {
        _audioSource.clip = songs[0];
    }
    private void Update()
    {
        _audioSource.volume = 0.3f;
        if (!_gameManager.isGamePlaying && !PauseMenu.gameIsPaused)
        {
            _audioSource.volume = 0.27f;
        }
        if (!_audioSource.isPlaying && !PauseMenu.gameIsPaused)
        {
            _audioSource.volume = 0.3f;

            _audioSource.clip = songs[1];
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}
