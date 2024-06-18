using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource musicSource; // AudioSource para la música ambiental
    public AudioSource randomAudioSource; // AudioSource para los clips de audio aleatorios

    public AudioClip[] musicClips; // Clips de música ambiental
    public AudioClip[] randomClips; // Clips de audio aleatorios aparte de la música

    void Start()
    {
        // Comienza la reproducción de música ambiental en bucle
        if (musicClips.Length > 0)
        {
            musicSource.loop = true;
            PlayRandomMusic();
        }
        else
        {
            Debug.LogWarning("No se han asignado clips de música ambiental.");
        }

        // Reproducir aleatoriamente un clip aparte de la música ambiental
        if (randomClips.Length > 0)
        {
            InvokeRepeating("PlayRandomAudioClip", 5.0f, Random.Range(10.0f, 20.0f)); // Reproducir cada cierto tiempo aleatorio
        }
        else
        {
            Debug.LogWarning("No se han asignado clips de audio aleatorios.");
        }
    }

    void PlayRandomMusic()
    {
        // Seleccionar un clip de música ambiental aleatorio y reproducirlo
        int randomIndex = Random.Range(0, musicClips.Length);
        AudioClip randomClip = musicClips[randomIndex];

        musicSource.clip = randomClip;
        musicSource.Play();
    }

    void PlayRandomAudioClip()
    {
        // Seleccionar y reproducir un clip de audio aleatorio
        int randomIndex = Random.Range(0, randomClips.Length);
        AudioClip randomClip = randomClips[randomIndex];

        randomAudioSource.PlayOneShot(randomClip);
    }

    void Update()
    {
        // Verificar si todos los objetos con el tag "Player" han muerto
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        bool allPlayersDead = true;

        foreach (GameObject player in players)
        {
            if (player.activeSelf) // Verificar si el jugador está activo
            {
                allPlayersDead = false;
                break; // Romper el bucle al encontrar un jugador vivo
            }
        }

        if (allPlayersDead)
        {
            RestartLevel();
        }
    }

    void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); // Reiniciar la escena actual
    }

    //Hola :D
}
