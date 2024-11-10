using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float loadTime = 1f;
    [SerializeField] ParticleSystem loadParticles;
    [SerializeField] AudioClip crashSound;
    [SerializeField] float baseSpeed;
    [SerializeField] float boostSpeed;
    [SerializeField] ParticleSystem boostParticles;
    [SerializeField] AudioClip boostSound;
    AudioSource audioSource;
    [SerializeField] AudioClip bgSong;

    Rigidbody2D rb2d;
    SurfaceEffector2D surfaceEffector;
    [SerializeField] float torqueAmount = 3f;
    AudioListener audioListener;

    bool isBoosting = false; // Tracks if boost sound has played

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector = FindObjectOfType<SurfaceEffector2D>();
        audioSource = GetComponent<AudioSource>();
        audioListener = FindObjectOfType<AudioListener>();
        audioSource.PlayOneShot(bgSong,0.7f);
    }

    void Update()
    {
        Rotate();
        BoostUp();
    }

    void Rotate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddTorque(-torqueAmount);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload current scene
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScene();
        }
    }

    void BoostUp()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            surfaceEffector.speed = boostSpeed;

            if (!isBoosting)
            {
                isBoosting = true;
                audioSource.PlayOneShot(boostSound, 0.7f);
            }

            if (!boostParticles.isPlaying)
            {
                boostParticles.Play();
            }
        }
        else
        {
            surfaceEffector.speed = baseSpeed;
            isBoosting = false;
            boostParticles.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            loadParticles.Play();
            audioSource.PlayOneShot(crashSound, 0.7f);
            Invoke("LoadScene", loadTime);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(0); // Load the main scene
    }

    void PauseScene()
    {
        Time.timeScale = 0; // Pause the game
        audioListener.enabled = false; // Disable AudioListener
        SceneManager.LoadScene(1); // Load the pause menu scene
    }
}
