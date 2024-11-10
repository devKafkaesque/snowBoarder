using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


public class FinishLine : MonoBehaviour
{
    [SerializeField]float load_time = 1f;
    [SerializeField]ParticleSystem load_particle;
    [SerializeField] AudioSource audiosource1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            load_particle.Play();
            audiosource1 = GetComponent<AudioSource>();
            audiosource1.Play();
            Invoke("Load_Scene", load_time);
        }
    }


    void Load_Scene()
    {
        SceneManager.LoadScene(0);
    }
}
