using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public CameraMovement cameraMovement;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PausedGame();
        }
    }

    void PausedGame()
    {
        bool isPaused = !Time.timeScale.Equals(0f); // Verifica si el juego está pausado o no

        if (isPaused)
        {
            Time.timeScale = 0f; // Pausa el tiempo del juego
        }
        else
        {
            Time.timeScale = 1f; // Reanuda el tiempo del juego
        }
    }
}