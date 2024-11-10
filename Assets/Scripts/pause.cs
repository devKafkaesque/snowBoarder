using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] AudioSource pausePlayer;
    [SerializeField] AudioClip pauseMusic;
    AudioListener audioListener;

    void Start()
    {
        audioListener = FindObjectOfType<AudioListener>();
        pausePlayer.clip = pauseMusic;
        pausePlayer.Play(); // Play pause music when starting the pause menu
    }

    void Update()
    {
        if (Input.anyKey)
        {
            UnpauseGame();
        }
    }

    void UnpauseGame()
    {
        Time.timeScale = 1; // Resume the game
        pausePlayer.Stop(); // Stop pause music
        audioListener.enabled = true; // Enable AudioListener again
        SceneManager.LoadScene(0); // Load the main game scene
    }
}
